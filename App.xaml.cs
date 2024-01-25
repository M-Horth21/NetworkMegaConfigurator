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
using Forms = System.Windows.Forms;

namespace NetworkMegaConfigurator
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    readonly NavigationStore _navigationStore;
    readonly ModalNavigationStore _modalNavigationStore;
    readonly Forms.NotifyIcon _notifyIcon;

    public App()
    {
      _notifyIcon = new();
      _navigationStore = new();
      _modalNavigationStore = new();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
      _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore, _modalNavigationStore);

      MainWindow = new MainWindow()
      {
        DataContext = new MainViewModel(_navigationStore,_modalNavigationStore)
      };
      MainWindow.Show();

      _notifyIcon.Icon = new System.Drawing.Icon("Resources/icon.ico");
      _notifyIcon.Visible = true;
      _notifyIcon.Text = "Network Mega-Configurator";
      _notifyIcon.Click += OnNotifyIconClicked;

      base.OnStartup(e);
    }

    void OnNotifyIconClicked(object? sender, EventArgs e)
    {
      MainWindow.WindowState = WindowState.Normal;
      MainWindow.Activate();
    }

    protected override void OnExit(ExitEventArgs e)
    {
      _notifyIcon.Dispose();

      base.OnExit(e);
    }
  }
}
