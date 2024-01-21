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
using NetworkMegaConfigurator.Components;
using System.Collections.ObjectModel;

namespace NetworkMegaConfigurator
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public ObservableCollection<InterfaceProps> NetworkInterfaces
    {
      get { return (ObservableCollection<InterfaceProps>)GetValue(NetworkInterfacesProperty); }
      set { SetValue(NetworkInterfacesProperty, value); }
    }

    // Using a DependencyProperty as the backing store for NetworkInterfaces.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NetworkInterfacesProperty =
        DependencyProperty.Register(
          nameof(NetworkInterfaces),
          typeof(ObservableCollection<InterfaceProps>),
          typeof(MainWindow),
          new PropertyMetadata(new ObservableCollection<InterfaceProps>()));

    public MainWindow()
    {
      InitializeComponent();

      GetNetworkAdapters();
    }

    void GetNetworkAdapters()
    {
      NetworkInterfaces.Clear();

      var interfaces = NetworkInterface.GetAllNetworkInterfaces().ToList();
      foreach (var adapter in interfaces)
      {
        NetworkInterfaces.Add(new InterfaceProps(adapter));
      }
    }

    void btnSetStatic_Click(object sender, RoutedEventArgs e)
    {
      //string args = $"interface ipv4 set address name=\"{SelectedAdapter}\" static 192.168.1.10";
      //SetIp(args);
    }

    void btnSetDhcp_Click(object sender, RoutedEventArgs e)
    {
      //string args = $"interface ipv4 set address name=\"{SelectedAdapter}\" dhcp";
      //SetIp(args);
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

      scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta / 3);
      e.Handled = true;
    }

    private void OnAdapterListRenew(object sender, MouseButtonEventArgs e)
    {
      GetNetworkAdapters();
    }

  }

  public class InterfaceProps
  {
    public string Name { get; }
    public string Description { get; }
    public string Config { get; }
    public NetworkInterfaceType Type { get; }

    public InterfaceProps(NetworkInterface @interface)
    {
      Name = @interface.Name;
      Description = @interface.Description;
      Config = @interface.GetIPProperties().GetIPv4Properties().IsDhcpEnabled ? "DHCP" : "Static";
      Type = @interface.NetworkInterfaceType;
    }
  }
}
