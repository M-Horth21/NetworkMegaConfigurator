using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Commands
{
  class SetDhcpCommand : CommandBase
  {
    readonly ConfigureAdapterViewModel _configViewModel;

    public SetDhcpCommand(ConfigureAdapterViewModel configViewModel)
    {
      _configViewModel = configViewModel;
    }

    public override void Execute(object? parameter)
    {
      _configViewModel.DhcpEnabled= true;
    }
  }
}
