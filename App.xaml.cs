using NetworkMegaConfigurator.Stores;
using NetworkMegaConfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkMegaConfigurator
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    readonly NavigationStore _navigationStore;

    public App()
    {
      _navigationStore = new();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
      _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore);

      MainWindow = new MainWindow()
      {
        DataContext = new MainViewModel(_navigationStore)
      };
      MainWindow.Show();

      base.OnStartup(e);
    }
  }
}
