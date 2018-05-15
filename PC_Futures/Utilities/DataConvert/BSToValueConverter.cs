using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Utilities
{
    public class BSToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string rest = "";
            string str = value.ToString().ToUpper();
            if (str == "B")
            {
                rest = "买";
            }
            else
            {
                rest = "卖";
            }
            return rest;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
