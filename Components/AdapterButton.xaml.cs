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
  public partial class AdapterButton : UserControl
  {
    public string AdapterName
    {
      get { return (string)GetValue(AdapterNameProperty); }
      set { SetValue(AdapterNameProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AdapterNameProperty =
        DependencyProperty.Register(nameof(AdapterName), typeof(string), typeof(AdapterButton), new PropertyMetadata("Adapter name"));

    public string AdapterDescription
    {
      get { return (string)GetValue(AdapterDescriptionProperty); }
      set { SetValue(AdapterDescriptionProperty, value); }
    }

    // Using a DependencyProperty as the backing store for AdapterDescription.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AdapterDescriptionProperty =
        DependencyProperty.Register(nameof(AdapterDescription), typeof(string), typeof(AdapterButton), new PropertyMetadata("Description"));

    public string AdapterConfig
    {
      get { return (string)GetValue(AdapterConfigProperty); }
      set { SetValue(AdapterConfigProperty, value); }
    }

    // Using a DependencyProperty as the backing store for AdapterConfig.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AdapterConfigProperty =
        DependencyProperty.Register(nameof(AdapterConfig), typeof(string), typeof(AdapterButton), new PropertyMetadata("Configuration"));

    public NetworkInterfaceType AdapterType
    {
      get { return (NetworkInterfaceType)GetValue(AdapterTypeProperty); }
      set { SetValue(AdapterTypeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for AdapterType.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty AdapterTypeProperty =
        DependencyProperty.Register(nameof(AdapterType), typeof(NetworkInterfaceType), typeof(AdapterButton), new PropertyMetadata(NetworkInterfaceType.Ethernet));


    public AdapterButton()
    {
      InitializeComponent();
    }
  }
}
