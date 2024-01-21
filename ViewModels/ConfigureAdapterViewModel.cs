using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
      }
    }

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

    public ICommand SetDhcp { get; }
    public ICommand SetStatic { get; }

    public ConfigureAdapterViewModel()
    {

    }
  }
}
