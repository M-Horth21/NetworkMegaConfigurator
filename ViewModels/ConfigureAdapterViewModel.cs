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
using System.Net.NetworkInformation;
using NetworkMegaConfigurator.Models;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class ConfigureAdapterViewModel : ViewModelBase
  {
    NetworkInterface _adapter;
    public NetworkInterface Adapter
    {
      get
      {
        return _adapter;
      }
      set
      {
        _adapter = value;
        OnPropertyChanged(nameof(Adapter));
      }
    }

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

        if (_dhcpEnabled)
        {
          IpAddress = string.Empty;
          Mask = string.Empty;
          Gateway = string.Empty;
          ShowAdvanced = false;
        }
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
        AutoSetGateway();
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

    bool _showAdvanced = false;
    public bool ShowAdvanced
    {
      get => _showAdvanced;
      set
      {
        _showAdvanced = value;
        OnPropertyChanged(nameof(ShowAdvanced));

        Mask = "255.255.255.0";
      }
    }

    bool _isLoading;

    public bool IsLoading
    {
      get
      {
        return _isLoading;
      }
      set
      {
        _isLoading = value;
        OnPropertyChanged(nameof(IsLoading));
      }
    }

    readonly NavigationStore _navigationStore;
    readonly ModalNavigationStore _modalNavigationStore;

    public ICommand SetDhcp { get; }
    public ICommand SetStatic { get; }
    public ICommand Apply { get; }
    public ICommand Back { get; }

    public ConfigureAdapterViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
    {
      _navigationStore = navigationStore;
      _modalNavigationStore = modalNavigationStore;
      Apply = new ApplyAdapterConfigurationCommand(this, _modalNavigationStore);
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
      return new HomeViewModel(_navigationStore, _modalNavigationStore);
    }

    void AutoSetGateway()
    {
      if (string.IsNullOrEmpty(IpAddress))
      {
        Gateway = string.Empty;
        return;
      }
      if (string.IsNullOrEmpty(Gateway))
      {
        Gateway = IpAddress;
        return;
      }

      if (Gateway.Count(x => x == '.') < 3)
      {
        Gateway = IpAddress;
        return;
      }

      if (Gateway[^1] == '.') Gateway += '1';
    }
  }
}