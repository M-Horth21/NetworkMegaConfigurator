using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NetworkMegaConfigurator.Commands;
using NetworkMegaConfigurator.Models;
using NetworkMegaConfigurator.Stores;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class HomeViewModel : ViewModelBase
  {
    readonly ObservableCollection<AdapterViewModel> _adapters;
    readonly ObservableCollection<QuickActionViewModel> _favorites;
    readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;

    private bool _refreshing;
    public bool Refreshing
    {
      get
      {
        return _refreshing;
      }
      set
      {
        _refreshing = value;
        OnPropertyChanged(nameof(Refreshing));
      }
    }


    public IEnumerable<QuickActionViewModel> Favorites => _favorites;

    public ICommand Refresh { get; }

    public IEnumerable<AdapterViewModel> Adapters => _adapters;

    public HomeViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
    {
      Refresh = new RefreshAdaptersCommand(this);
      _navigationStore = navigationStore;
      _modalNavigationStore = modalNavigationStore;
      _adapters = new();
      _favorites = new();

      GetAllAdapters();
      GetAllFavorites(navigationStore, modalNavigationStore);
    }

    void GetAllFavorites(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
    {
      foreach (var item in FavoritesStore.GetFavorites)
      {
        _favorites.Add(new(item, navigationStore, modalNavigationStore));
      }
    }

    public void GetAllAdapters()
    {
      var foundAdapters = NetworkInterface.GetAllNetworkInterfaces()
        .Select(x => new AdapterViewModel(x, _navigationStore, _modalNavigationStore))
        .ToArray();

      Array.Sort(foundAdapters);

      _adapters.Clear();
      foreach (var adapter in foundAdapters)
      {
        _adapters.Add(adapter);
      }
    }
  }
}
