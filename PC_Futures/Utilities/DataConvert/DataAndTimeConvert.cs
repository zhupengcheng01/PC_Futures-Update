using Abt.Controls.SciChart.Visuals.Axes;
using Abt.Controls.SciChart.Visuals.RenderableSeries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Utilities
{
    class DataAndTimeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;

            AxisInfo axis = (AxisInfo)value;
            AxisAlignment aligment = axis.AxisAlignment;
            if (aligment == AxisAlignment.Right)
            {
                return axis.AxisFormattedDataValue;
            }
            else if (aligment == AxisAlignment.Left)
            {
                return axis.AxisFormattedDataValue;
            }

            Type type = axis.DataValue.GetType();
            if (type.Name.Equals("DateTime"))
            {
                DateTime t = (DateTime)axis.DataValue;

                String formatedT = string.Format("{0:yyyy-MM-dd HH:mm}", t).ToString();
                return formatedT;
            }
            else if (type.Name.Equals("Double"))
            {
                Double t = (Double)axis.DataValue;
                Double formatedT = ((int)(t * 10000)) / 10000.0;
                return formatedT;
            }

            return axis.DataValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}



