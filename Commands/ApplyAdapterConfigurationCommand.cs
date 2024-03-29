﻿using NetworkMegaConfigurator.Models;
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
    readonly ModalNavigationStore _modalNavigationStore;
    readonly ConfigureAdapterViewModel _configurationViewModel;
    AppliedConfigViewModel _appliedConfigViewModel;

    string Name => _configurationViewModel.AdapterName;
    string IpAddress => _configurationViewModel.IpAddress;
    string Mask => _configurationViewModel.Mask;
    string Gateway => _configurationViewModel.Gateway;
    bool Dhcp => _configurationViewModel.DhcpEnabled;

    ConfigurationModel ConfigModel => new()
    {
      AdapterName = Name,
      Dhcp = Dhcp,
      IpAddress = IpAddress,
      SubnetMask = Mask,
      Gateway = Gateway
    };

    public ApplyAdapterConfigurationCommand(ConfigureAdapterViewModel configurationViewModel, ModalNavigationStore modalNavigationStore)
    {
      _configurationViewModel = configurationViewModel;
      _modalNavigationStore = modalNavigationStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
      _appliedConfigViewModel = new AppliedConfigViewModel(ConfigModel, _modalNavigationStore);
      _modalNavigationStore.CurrentViewModel = _appliedConfigViewModel;

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

        await Task.Delay(1000);
        process.WaitForExit();
      }
      catch (Exception)
      {
        MessageBox.Show("Failed to set adapter configuration.", "Error");
      }

      RecentsStore.Add(ConfigModel);
      _appliedConfigViewModel.ConfigComplete = true;
    }
  }
}
