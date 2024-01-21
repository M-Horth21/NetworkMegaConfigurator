using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class HomeViewModel : ViewModelBase
  {
    readonly ObservableCollection<AdapterViewModel> _adapters;
    public IEnumerable<AdapterViewModel> Adapters => _adapters;

    public ICommand ConfigureAdapter { get; }

    public HomeViewModel()
    {
      _adapters = new();

      foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
      {
        _adapters.Add(new(new(item)));
      }
    }
  }
}
