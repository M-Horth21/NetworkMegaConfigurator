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

    public string Name => _adapter.Name;
    public string Description => _adapter.Description;
    public string Configuration => (DhcpEnabled ? "DHCP" : $"Static") + $" - {_adapter
        .GetIPProperties().UnicastAddresses
        .Where(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        .First().Address}";
    public bool DhcpEnabled => _adapter.GetIPProperties().GetIPv4Properties().IsDhcpEnabled;
    public bool Enabled => _adapter.OperationalStatus == OperationalStatus.Up;
    public NetworkInterfaceType Type => _adapter.NetworkInterfaceType;
    public int Priority => k_NetworkPriority.ContainsKey(Type) ? k_NetworkPriority[Type] : int.MaxValue;

    public ICommand AdapterSelected { get; }

    public AdapterViewModel(NetworkInterface networkInterface, NavigationStore navigationStore)
    {
      _adapter = networkInterface;
      _navigationStore = navigationStore;
      AdapterSelected = new NavigateCommand(_navigationStore, CreateConfigureAdapterViewModel);
    }

    ConfigureAdapterViewModel CreateConfigureAdapterViewModel()
    {
      return new ConfigureAdapterViewModel(_navigationStore)
      {
        AdapterName = Name,
        DhcpEnabled = DhcpEnabled,
      };
    }

    public int CompareTo(AdapterViewModel? other)
    {
      if (other == null) { return -1; }

      // First sort by enabled or not.
      if (this.Enabled != other.Enabled)
      {
        return this.Enabled ? -1 : 1;
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
