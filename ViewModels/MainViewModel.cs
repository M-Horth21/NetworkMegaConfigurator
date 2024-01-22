using NetworkMegaConfigurator.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class MainViewModel : ViewModelBase
  {
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    readonly NavigationStore _navigationStore;

    public MainViewModel(NavigationStore navigationStore)
    {
      _navigationStore = navigationStore;
      _navigationStore.CurrentViewModelChanged += HandleCurrentViewModelChanged;
    }

    private void HandleCurrentViewModelChanged()
    {
      OnPropertyChanged(nameof(CurrentViewModel));
    }
  }
}
