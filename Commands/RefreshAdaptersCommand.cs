using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Commands
{
  internal class RefreshAdaptersCommand : CommandBase
  {
    readonly HomeViewModel _homeViewModel;

    public RefreshAdaptersCommand(HomeViewModel homeViewModel)
    {
      _homeViewModel = homeViewModel;
    }
    public override void Execute(object? parameter)
    {
      _homeViewModel.GetAllAdapters();
    }
  }
}
