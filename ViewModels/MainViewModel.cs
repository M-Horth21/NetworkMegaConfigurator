using NetworkMegaConfigurator.Commands;
using NetworkMegaConfigurator.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class MainViewModel : ViewModelBase
  {
    readonly NavigationStore _navigationStore;
    readonly ModalNavigationStore _modalNavigationStore;

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

    public bool ModalOpen => _modalNavigationStore.IsOpen;

    public ICommand GoToGitHub { get; }

    public MainViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
    {
      GoToGitHub = new OpenUrlCommand("https://github.com/M-Horth21/NetworkMegaConfigurator");

      _navigationStore = navigationStore;
      _modalNavigationStore = modalNavigationStore;

      _navigationStore.CurrentViewModelChanged += HandleCurrentViewModelChanged;
      _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
    }

    void OnCurrentModalViewModelChanged()
    {
      OnPropertyChanged(nameof(CurrentModalViewModel));
      OnPropertyChanged(nameof(ModalOpen));
    }

    void HandleCurrentViewModelChanged()
    {
      OnPropertyChanged(nameof(CurrentViewModel));
    }
  }
}
