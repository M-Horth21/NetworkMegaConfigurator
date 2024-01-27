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
  internal class SpeedToColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (long)value switch
      {
        > 10 and <= 100 => "#e2e520",
        > 100 and <= 1000 => "#CCFFFFFF",
        > 1000 => "#20e530",
        _ => "#CCE52020"
      };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
