using NetworkMegaConfigurator.Stores;
using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Commands
{
  internal class RefreshAdaptersCommand : AsyncCommandBase
  {
    readonly HomeViewModel _homeViewModel;

    public RefreshAdaptersCommand(HomeViewModel homeViewModel)
    {
      _homeViewModel = homeViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
      _homeViewModel.Refreshing = true;

      _homeViewModel.GetAllAdapters();

      await Task.Delay(500);
      _homeViewModel.Refreshing = false;
    }
  }
}
