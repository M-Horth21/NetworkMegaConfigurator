using Material.Icons;
using Material.Icons.WPF;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace NetworkMegaConfigurator.Views
{
  public partial class AdapterButton : UserControl
  {
    public AdapterButton()
    {
      InitializeComponent();
    }
  }

  public class StatusToIconConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (bool)value ? MaterialIconKind.Connection : MaterialIconKind.PowerPlugOff;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

  public class TypeToIconConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (NetworkInterfaceType)value switch
      {
        NetworkInterfaceType.Ethernet => MaterialIconKind.Ethernet,
        NetworkInterfaceType.Wireless80211 => MaterialIconKind.Wifi,
        _ => MaterialIconKind.Connection
      };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

  public class StatusToColor : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (bool)value ? "#7FFFFFFF" : "#FF880808";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
