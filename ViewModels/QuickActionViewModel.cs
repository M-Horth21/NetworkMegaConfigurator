using NetworkMegaConfigurator.Commands;
using NetworkMegaConfigurator.Models;
using NetworkMegaConfigurator.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkMegaConfigurator.ViewModels
{
  class QuickActionViewModel : ViewModelBase
  {
    ConfigurationModel _config;
    public ConfigurationModel Config
    {
      get
      {
        return _config;
      }
      set
      {
        _config = value;
        OnPropertyChanged(nameof(Config));
      }
    }

    public string AdapterName => Config.AdapterName;
    public string Caption
    {
      get
      {
        if (Config.Dhcp) return "DHCP";
        else return Config.IpAddress;
      }
    }

    public ICommand Apply { get; }

    public QuickActionViewModel(ConfigurationModel config, NavigationStore navStore, ModalNavigationStore modalStore)
    {
      Config = config;

      ConfigureAdapterViewModel tempConfigurationViewModel = new(navStore, modalStore)
      {
        AdapterName = Config.AdapterName,
        DhcpEnabled = Config.Dhcp,
        IpAddress = Config.IpAddress,
        Mask = Config.SubnetMask,
        Gateway = Config.Gateway,
      };

      Apply = new ApplyAdapterConfigurationCommand(tempConfigurationViewModel, modalStore);
    }
  }
}
