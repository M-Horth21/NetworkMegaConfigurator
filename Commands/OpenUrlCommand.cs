using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMegaConfigurator.Commands
{
  class OpenUrlCommand : CommandBase
  {
    readonly string _url;

    public OpenUrlCommand(string Url)
    {
      _url = Url;
    }

    public override void Execute(object? parameter)
    {
      Process.Start(new ProcessStartInfo()
      {
        UseShellExecute = true,
        FileName = _url
      });
    }
  }
}
