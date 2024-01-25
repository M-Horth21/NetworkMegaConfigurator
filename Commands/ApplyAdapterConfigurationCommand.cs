using NetworkMegaConfigurator.Stores;
using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkMegaConfigurator.Commands
{
  internal class ApplyAdapterConfigurationCommand : AsyncCommandBase
  {
    readonly ConfigureAdapterViewModel _configViewModel;
    private readonly ModalNavigationStore _modalNavigationStore;

    string Name => _configViewModel.AdapterName;
    string IpAddress => _configViewModel.IpAddress;
    string Mask => _configViewModel.Mask;
    string Gateway => _configViewModel.Gateway;
    bool Dhcp => _configViewModel.DhcpEnabled;

    public ApplyAdapterConfigurationCommand(ConfigureAdapterViewModel configureAdapterViewModel, ModalNavigationStore modalNavigationStore)
    {
      _configViewModel = configureAdapterViewModel;
      _modalNavigationStore = modalNavigationStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
      _configViewModel.IsLoading = true;
      _modalNavigationStore.CurrentViewModel = new ViewModelBase();

      string args;
      if (Dhcp)
      {
        args = $"interface ipv4 set address name=\"{Name}\" dhcp";
      }
      else
      {
        args = $"interface ipv4 set address name=\"{Name}\" static {IpAddress} {Mask} {Gateway}";
      }

      try
      {
        Process process = new();
        process.StartInfo = new ProcessStartInfo("netsh", args);
        process.StartInfo.CreateNoWindow = true;
        process.Start();

        process.WaitForExit();

        await Task.Delay(1000);
      }
      catch (Exception)
      {
        MessageBox.Show("Failed to set adapter configuration.", "Error");
      }

      _configViewModel.IsLoading = false;
      _modalNavigationStore.CurrentViewModel = null;
    }
  }
}
