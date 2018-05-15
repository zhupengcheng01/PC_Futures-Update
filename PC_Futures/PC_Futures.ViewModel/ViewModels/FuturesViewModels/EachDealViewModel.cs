using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures
{
    public class EachDealViewModel : ObservableObject
    {
        private FuturesTickViewModel _tick;
        public EachDealViewModel(FuturesTickViewModel tick=null)
        {
            if(tick!=null)
            {
                _tick = tick;
                EachDealTime = tick.QuotesTime;
                EachDealPrice = tick.LastPrice;
                EachDealColor = tick.Color;
                EachDealKeepDigits = tick.KeepDigits;
                EachDealSize = tick.LastSize;
                EachDealSizeColor = tick.LastPrice <= tick.BidP1 ? "#00ff00" : "Red";
                OrderByTime = string.IsNullOrEmpty(tick.Time) ? new DateTime() : Convert.ToDateTime(tick.Time);
            }
        }
        private DateTime _OrderByTime;
        public DateTime OrderByTime
        {
            get
            {
                return _OrderByTime;
            }
            set
            {
                if (_OrderByTime.Equals(value))
                    return;
                _OrderByTime = value;
                RaisePropertyChanged(nameof(OrderByTime));
            }
        }
        private string _EachDealTime = "-";
        /// <summary>
        /// 更新时间
        /// </summary>
        public string EachDealTime
        {
            get
            {
                return _EachDealTime;
            }
            set
            {
                if (_EachDealTime.Equals(value))
                    return;
                _EachDealTime = value;
                RaisePropertyChanged(nameof(EachDealTime));
            }
        }
        private double _EachDealPrice = double.NaN;
        /// <summary>
        /// 每笔最新价
        /// </summary>
        public double EachDealPrice
        {
            get
            {
                return _EachDealPrice;
            }
            set
            {
                if (_EachDealPrice.Equals(value))
                {
                    return;
                }
                _EachDealPrice = value;
                RaisePropertyChanged(nameof(EachDealPrice));
            }
        }
        private double _EachDealSize = double.NaN;
        /// <summary>
        /// 每笔成交量
        /// </summary>
        public double EachDealSize
        {
            get
            {
                return _EachDealSize;
            }
            set
            {
                if (_EachDealSize.Equals(value))
                {
                    return;
                }
                _EachDealSize = value;
                RaisePropertyChanged(nameof(EachDealSize));
            }
        }
        private string _EachDealSizeColor = "#00ff00";
        /// <summary>
        /// 成交量颜色
        /// </summary>
        public string EachDealSizeColor
        {
            get
            {
                return _EachDealSizeColor;
            }
            set
            {
                if (_EachDealSizeColor.Equals(value))
                    return;
                _EachDealSizeColor = value;
                RaisePropertyChanged(nameof(EachDealSizeColor));
            }
        }
        private string _EachDealColor = "#00ff00";
        /// <summary>
        /// 最新价颜色
        /// </summary>
        public string EachDealColor
        {
            get
            {
                return _EachDealColor;
            }
            set
            {
                if (_EachDealColor.Equals(value))
                    return;
                _EachDealColor = value;
                RaisePropertyChanged(nameof(EachDealColor));
            }
        }
        private string _EachDealKeepDigits = "F2";
        /// <summary>
        /// 小数位数格式化
        /// </summary>
        public string EachDealKeepDigits
        {
            get
            {
                return _EachDealKeepDigits;
            }
            set
            {
                if (_EachDealKeepDigits.Equals(value))
                    return;
                _EachDealKeepDigits = value;
                RaisePropertyChanged(nameof(EachDealKeepDigits));
            }
        }
    }
}
