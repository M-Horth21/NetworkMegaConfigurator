using NetworkMegaConfigurator.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Commands
{
  class CloseModalCommand : BaseCommand
  {
    readonly ModalNavigationStore _modalNavigationStore;

    public CloseModalCommand(ModalNavigationStore modalNavigationStore)
    {
      _modalNavigationStore = modalNavigationStore;
    }

    public override void Execute(object? parameter)
    {
      _modalNavigationStore.CurrentViewModel = null;
    }
  }
}
