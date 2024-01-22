using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkMegaConfigurator.Commands
{
  internal class ApplyAdapterConfigurationCommand : CommandBase
  {
    readonly ConfigureAdapterViewModel _configViewModel;

    string Name => _configViewModel.AdapterName;
    string IpAddress => _configViewModel.IpAddress;
    string Mask => _configViewModel.Mask;
    string Gateway => _configViewModel.Gateway;
    bool Dhcp => _configViewModel.DhcpEnabled;

    public ApplyAdapterConfigurationCommand(ConfigureAdapterViewModel configureAdapterViewModel)
    {
      _configViewModel = configureAdapterViewModel;
    }

    public override void Execute(object? parameter)
    {
      string args = string.Empty;

      if (Dhcp)
      {
        args = $"interface ipv4 set address name=\"{Name}\" dhcp";
      }
      else
      {
        args = $"interface ipv4 set address name=\"{Name}\" static {IpAddress} {Mask} {Gateway}";
      }

      Debug.WriteLine(args);
      Process process = new();
      process.StartInfo = new ProcessStartInfo("netsh", args);
      process.StartInfo.CreateNoWindow = true;
      process.Start();

      process.WaitForExit();
    }
  }
}
