using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace NetworkMegaConfigurator.Components
{
  public class QuickAction : Control
  {
    NetworkInterface _adapter;
    public NetworkInterface Adapter
    {
      get { return _adapter; }
      set
      {
        Description = value.Description;
        Setting = value.GetIPProperties().GetIPv4Properties().IsDhcpEnabled ? "DHCP" : "Static";
        _adapter = value;
      }
    }

    public string Setting
    {
      get { return (string)GetValue(SettingProperty); }
      set { SetValue(SettingProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Configuration.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SettingProperty =
        DependencyProperty.Register(nameof(Setting), typeof(string), typeof(QuickAction), new PropertyMetadata("Setting"));



    public string Description
    {
      get { return (string)GetValue(DescriptionProperty); }
      set { SetValue(DescriptionProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register(nameof(Description), typeof(string), typeof(QuickAction), new PropertyMetadata("Description"));




    static QuickAction()
    {

      DefaultStyleKeyProperty.OverrideMetadata(typeof(QuickAction), new FrameworkPropertyMetadata(typeof(QuickAction)));
    }
  }
}
