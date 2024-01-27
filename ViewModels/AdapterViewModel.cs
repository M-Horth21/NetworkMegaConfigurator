using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NetworkMegaConfigurator.Commands;
using System.Net.NetworkInformation;
using NetworkMegaConfigurator.Stores;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class AdapterViewModel : ViewModelBase, IComparable<AdapterViewModel>
  {
    static readonly Dictionary<NetworkInterfaceType, int> k_NetworkPriority = new()
    {
      [NetworkInterfaceType.Ethernet] = 1,
      [NetworkInterfaceType.Wireless80211] = 2,
    };

    readonly NetworkInterface _adapter;
    readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;

    public string Name => _adapter.Name;
    public string Description => _adapter.Description;
    public string Configuration => GetConfiguration();
    public bool DhcpEnabled => _adapter.GetIPProperties().GetIPv4Properties().IsDhcpEnabled;
    public bool Status => _adapter.OperationalStatus == OperationalStatus.Up;
    public NetworkInterfaceType Type => _adapter.NetworkInterfaceType;
    public int Priority => k_NetworkPriority.ContainsKey(Type) ? k_NetworkPriority[Type] : int.MaxValue;
    public long Speed => _adapter.Speed / 1000000;
    public string Address => _adapter
      .GetIPProperties().UnicastAddresses
      .Where(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
      .First().Address
      .ToString();

    public ICommand AdapterSelected { get; }

    public AdapterViewModel(NetworkInterface networkInterface, NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
    {
      _adapter = networkInterface;
      _navigationStore = navigationStore;
      _modalNavigationStore = modalNavigationStore;
      AdapterSelected = new NavigateCommand(_navigationStore, CreateConfigureAdapterViewModel);
    }

    ConfigureAdapterViewModel CreateConfigureAdapterViewModel()
    {
      return new ConfigureAdapterViewModel(_navigationStore, _modalNavigationStore)
      {
        Adapter = this._adapter,
        AdapterName = Name,
        DhcpEnabled = DhcpEnabled,
      };
    }

    string GetConfiguration()
    {
      if (!Status) return "Disconnected";
      if (DhcpEnabled) return $"DHCP - {Address}";
      return $"Static - {Address}";
    }

    public int CompareTo(AdapterViewModel? other)
    {
      if (other == null) { return -1; }

      // First sort by enabled or not.
      if (this.Status != other.Status)
      {
        return this.Status ? -1 : 1;
      }

      // Then by type.
      if (this.Type != other.Type)
      {
        return this.Priority - other.Priority;
      }

      // Then by name.
      return Name.CompareTo(other.Name);
    }
  }
}
