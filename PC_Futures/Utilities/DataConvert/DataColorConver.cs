using System;
using System.Globalization;
using System.Windows.Data;

namespace Utilities
{
  /// <summary>
  /// 颜色
  /// </summary>
    public class DataColorConver : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "#ffffff";
            if (!double.IsNaN((double)value))
            {
                double data = (double)value;

                if (data < 0)
                {
                    //result = Color.Green;
                    result = "#008000";
                }
                else if (data > 0)
                {
                    result = "#FF0000";
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    /// <summary>
    /// 涨幅需要%
    /// </summary>
    public class DataFmateConver : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d = (double)value;
            if (d.Equals(double.NaN) || double.IsInfinity(d))
            {
                return "-";
            }
            else
            {
                return string.Format("{0:F2}%", value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
