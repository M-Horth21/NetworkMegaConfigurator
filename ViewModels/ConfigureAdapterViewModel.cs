using NetworkMegaConfigurator.Commands;
using NetworkMegaConfigurator.Exceptions;
using NetworkMegaConfigurator.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Input;
using System.Globalization;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class ConfigureAdapterViewModel : ViewModelBase
  {
    string _adapterName;
    public string AdapterName
    {
      get
      {
        return _adapterName;
      }
      set
      {
        _adapterName = value;
        OnPropertyChanged(nameof(AdapterName));
      }
    }

    bool _dhcpEnabled;
    public bool DhcpEnabled
    {
      get
      {
        return _dhcpEnabled;
      }
      set
      {
        _dhcpEnabled = value;
        OnPropertyChanged(nameof(DhcpEnabled));
        OnPropertyChanged(nameof(ShowInputs));
      }
    }
    public bool ShowInputs => !DhcpEnabled;

    string _ipAddress;
    public string IpAddress
    {
      get
      {
        return _ipAddress;
      }
      set
      {
        _ipAddress = value;
        OnPropertyChanged(nameof(IpAddress));
      }
    }

    string _mask;
    public string Mask
    {
      get
      {
        return _mask;
      }
      set
      {
        _mask = value;
        OnPropertyChanged(nameof(Mask));
      }
    }

    string _gateway;

    public string Gateway
    {
      get
      {
        return _gateway;
      }
      set
      {
        _gateway = value;
        OnPropertyChanged(nameof(Gateway));
      }
    }

    readonly NavigationStore _navigationStore;

    public ICommand SetDhcp { get; }
    public ICommand SetStatic { get; }
    public ICommand Apply { get; }
    public ICommand Back { get; }

    public ConfigureAdapterViewModel(NavigationStore navigationStore)
    {
      this._navigationStore = navigationStore;
      Apply = new ApplyAdapterConfigurationCommand(this);
      SetDhcp = new SetDhcpCommand(this);
      SetStatic = new SetStaticCommand(this);
      Back = new NavigateCommand(_navigationStore, CreateHomeViewModel);
    }

    public bool ValidateString(string input)
    {
      if (string.IsNullOrEmpty(IpAddress))
      {
        throw new InvalidIpException(IpAddress);
      }

      if (IpAddress.Split('.').Length != 4)
      {
        throw new InvalidIpException(IpAddress);
      }

      return true;
    }

    HomeViewModel CreateHomeViewModel()
    {
      return new HomeViewModel(_navigationStore);
    }
  }
}
