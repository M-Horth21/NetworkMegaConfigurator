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
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    readonly NavigationStore _navigationStore;

    public ICommand GoToGitHub { get; }

    public MainViewModel(NavigationStore navigationStore)
    {
      GoToGitHub = new OpenUrlCommand("https://github.com/M-Horth21/NetworkMegaConfigurator");

      _navigationStore = navigationStore;
      _navigationStore.CurrentViewModelChanged += HandleCurrentViewModelChanged;
    }

    private void HandleCurrentViewModelChanged()
    {
      OnPropertyChanged(nameof(CurrentViewModel));
    }
  }
}
