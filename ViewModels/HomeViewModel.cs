﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NetworkMegaConfigurator.Commands;
using NetworkMegaConfigurator.Stores;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class HomeViewModel : ViewModelBase
  {
    readonly ObservableCollection<AdapterViewModel> _adapters;
    readonly NavigationStore _navigationStore;
    private readonly ModalNavigationStore _modalNavigationStore;

    public ICommand Refresh { get; }

    public IEnumerable<AdapterViewModel> Adapters => _adapters;

    public HomeViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
    {
      Refresh = new RefreshAdaptersCommand(this);
      _navigationStore = navigationStore;
      _modalNavigationStore = modalNavigationStore;
      _adapters = new();

      GetAllAdapters();
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
