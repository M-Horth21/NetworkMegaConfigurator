using NetworkMegaConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.ViewModels
{
  internal class AdapterViewModel : ViewModelBase
  {
    private readonly Adapter _adapter;

    public string Name => _adapter.NetInterface.Name;
    public string Description => _adapter.NetInterface.Description;
    public string Configuration => _adapter.NetInterface
      .GetIPProperties()
      .GetIPv4Properties().IsDhcpEnabled ? "DHCP" : "Static";

    public AdapterViewModel(Adapter adapter)
    {
      _adapter = adapter;
    }
  }
}
