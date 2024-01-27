using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Material.Icons;
using Material.Icons.WPF;

namespace NetworkMegaConfigurator.Converters
{
  internal class SpeedToIconConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (long)value switch
      {
        > 10 and <= 100 => MaterialIconKind.GaugeLow,
        >100 and <= 1000 => MaterialIconKind.Gauge,
        > 1000 => MaterialIconKind.GaugeFull,
        _ => MaterialIconKind.GaugeEmpty
      };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
