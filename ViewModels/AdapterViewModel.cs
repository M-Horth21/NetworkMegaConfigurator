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
        return this.Type switch
        {
          NetworkInterfaceType.Ethernet => -2,
          NetworkInterfaceType.Wireless80211 => -1,
          _ => 1
        };
      }

      // Then by name.
      return Name.CompareTo(other.Name);
    }
  }
}
