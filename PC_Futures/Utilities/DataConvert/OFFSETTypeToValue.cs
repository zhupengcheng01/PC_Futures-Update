using Futures.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Utilities
{
    public class OFFSETTypeToValue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int type = (int)value;
            string resut = "";
            if (type == (int)OffsetType.OFFSET_OPEN)
            {
                resut = "开仓";
            }
            else if (type == (int)OffsetType.OFFSET_COVER)
            {
                resut = "平仓";
            }
            else if (type == (int)OffsetType.OFFSET_COVER_TODAY)
            {
                resut = "平今";
            }
            else
            {
                resut = "未知";
            }
            return resut;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
