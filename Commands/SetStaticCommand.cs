using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkMegaConfigurator.Commands
{
  class SetStaticCommand : BaseCommand
  {
    readonly ConfigureAdapterViewModel _configViewModel;

    public SetStaticCommand(ConfigureAdapterViewModel configViewModel)
    {
      _configViewModel = configViewModel;
    }

    public override void Execute(object? parameter)
    {
      _configViewModel.DhcpEnabled = false;
    }
  }
}
