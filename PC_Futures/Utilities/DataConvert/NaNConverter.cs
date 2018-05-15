using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Utilities
{
    public class NaNConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (double)value;
            if (d.Equals(double.NaN) || double.IsInfinity(d) || d == 0)
            {
                return "-";
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class NaNConverter1 : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Count() != 2)
                return "-";
            double d1 = value[0] == null ? 0 : (double)value[0];
            double lastPrice = value[1] == null ? 0 : (double)value[1];
            if (d1.Equals(double.NaN) || double.IsInfinity(d1))
            {
                return "-";
            }
            else
            {
                if (lastPrice == 0 && d1 == 0)
                {
                    return "-";
                }
                else
                {
                    return d1;
                }

            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] splitValues = ((string)value).Split(' ');
            return splitValues;
        }
    }
}
