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
    readonly NavigationStore _navigationStore;
    readonly ModalNavigationStore _modalNavigationStore;

    bool _refreshing;
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

    readonly ObservableCollection<QuickActionViewModel> _favorites;
    public IEnumerable<QuickActionViewModel> Favorites => _favorites;

    readonly ObservableCollection<QuickActionViewModel> _recents;
    public IEnumerable<QuickActionViewModel> Recents => _recents;


    readonly ObservableCollection<AdapterViewModel> _adapters;
    public IEnumerable<AdapterViewModel> Adapters => _adapters;

    public ICommand Refresh { get; }

    public HomeViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
    {
      Refresh = new RefreshAdaptersCommand(this);
      _navigationStore = navigationStore;
      _modalNavigationStore = modalNavigationStore;
      _adapters = new();
      _favorites = new();
      _recents = new();

      GetAllAdapters();
      GetAllFavorites();
      GetAllRecents();

      RecentsStore.OnStoreUpdated += OnRecentsStoreUpdated;
      FavoritesStore.OnStoreUpdated += OnFavoritesStoreUpdate;
    }

    ~HomeViewModel()
    {
      RecentsStore.OnStoreUpdated -= OnRecentsStoreUpdated;
      FavoritesStore.OnStoreUpdated -= OnFavoritesStoreUpdate;
    }

    void OnRecentsStoreUpdated()
    {
      GetAllRecents();
    }

    void OnFavoritesStoreUpdate()
    {
      GetAllFavorites();
    }

    void GetAllFavorites()
    {
      _favorites.Clear();
      foreach (var item in FavoritesStore.GetFavorites)
      {
        _favorites.Add(new(item, _navigationStore, _modalNavigationStore));
      }
    }

    void GetAllRecents()
    {
      _recents.Clear();
      foreach (var item in RecentsStore.Configs)
      {
        _recents.Add(new(item, _navigationStore, _modalNavigationStore));
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
