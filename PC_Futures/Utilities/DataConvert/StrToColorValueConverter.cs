using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Utilities
{
    public class StrToColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Media.SolidColorBrush scBrush = new System.Windows.Media.SolidColorBrush();
            System.Windows.Media.Color clr = new System.Windows.Media.Color();
            Color result = Color.Red;
            string volom = value.ToString().ToUpper();
            if (volom =="S")
            {
                result = Color.Green;
            }
            else 
            {
                result = Color.Red;
            }
            clr.A = result.A;
            clr.B = result.B;
            clr.G = result.G;
            clr.R = result.R;
            scBrush.Color = clr;
            return scBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
