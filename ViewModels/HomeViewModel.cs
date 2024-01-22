using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NetworkMegaConfigurator.Stores;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class HomeViewModel : ViewModelBase
  {
    readonly ObservableCollection<AdapterViewModel> _adapters;
    readonly NavigationStore _navigationStore;

    public IEnumerable<AdapterViewModel> Adapters => _adapters;

    public HomeViewModel(NavigationStore navigationStore)
    {
      _navigationStore = navigationStore;
      _adapters = new();

      foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
      {
        AdapterViewModel newAdapter = new(item, navigationStore);
        _adapters.Add(newAdapter);
      }
    }
  }
}
