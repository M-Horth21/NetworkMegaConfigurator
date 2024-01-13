using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace NetworkMegaConfigurator
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public string SelectedAdapter => _adapters[AdapterList.SelectedIndex].Name;

    List<NetworkInterface> _adapters = new();

    public MainWindow()
    {
      InitializeComponent();

      GetNetworkAdapters();

      stackRecentActions.Children.Clear();

      foreach (var item in _adapters)
      {
        var quickAction = new QuickAction
        {
          Adapter = item
        };

        stackRecentActions.Children.Add(quickAction);
      }
    }

    void GetNetworkAdapters()
    {
      _adapters = NetworkInterface.GetAllNetworkInterfaces().ToList();
      foreach (var adapter in _adapters)
      {
        AdapterList.Items.Add($"{adapter.Name} - {adapter.Description}");
      }
    }

    void btnSetStatic_Click(object sender, RoutedEventArgs e)
    {
      string args = $"interface ipv4 set address name=\"{SelectedAdapter}\" static 192.168.1.10";
      SetIp(args);
    }

    void btnSetDhcp_Click(object sender, RoutedEventArgs e)
    {
      string args = $"interface ipv4 set address name=\"{SelectedAdapter}\" dhcp";
      SetIp(args);
    }

    static void SetIp(string args)
    {
      Debug.WriteLine(args);
      Process process = new();
      process.StartInfo = new ProcessStartInfo("netsh", args);
      process.StartInfo.CreateNoWindow = true;
      process.Start();

      process.WaitForExit();
    }

    private void TopBarMouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton == MouseButtonState.Pressed)
      {
        DragMove();
      }
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void OnRecentActionMouseWheel(object sender, MouseWheelEventArgs e)
    {
      var scrollViewer = (ScrollViewer)sender;
      if (scrollViewer == null) return;

      scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
      e.Handled = true;
    }
  }
}
