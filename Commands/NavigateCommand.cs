using NetworkMegaConfigurator.Stores;
using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Commands
{
  internal class NavigateCommand : CommandBase
  {
    readonly NavigationStore _navigationStore;
    private readonly Func<ViewModelBase> _createViewModel;

    public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
    {
      _navigationStore = navigationStore;
      this._createViewModel = createViewModel;
    }

    public override void Execute(object? parameter)
    {
      _navigationStore.CurrentViewModel = _createViewModel();
    }
  }
}
