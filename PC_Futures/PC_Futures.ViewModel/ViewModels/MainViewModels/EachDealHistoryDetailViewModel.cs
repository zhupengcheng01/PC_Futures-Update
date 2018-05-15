using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModel
{
    public class EachDealHistoryDetailViewModel : ObservableObject
    {
        private EachDealHistoryModel _dataModel;
        private FuturesTickViewModel _tick;
        public EachDealHistoryDetailViewModel(EachDealHistoryModel dataModel, FuturesTickViewModel tick)
        {
            _dataModel = dataModel;
            _tick = tick;
            EachDealKeepDigits = tick.KeepDigits;
            EachDealColor = dataModel.lastPrice >= tick.PreClosePrice ? "Red" : "#00ff00";
            EachDealSizeColor = dataModel.lastPrice <= tick.BidP1 ? "#00ff00" : "Red";

        }
        public string Time
        {
            get
            {
                return _dataModel.time;
            }
        }
        public DateTime OrderByTime
        {
            get
            {
                if(string.IsNullOrEmpty(_dataModel.time))
                {
                    return new DateTime();
                }
                else
                {
                    return Convert.ToDateTime(_dataModel.time);
                }
               
            }
        }
        public string ShowTime
        {
            get
            {
                return string.IsNullOrEmpty(_dataModel.time) ? "" : Convert.ToDateTime(_dataModel.time).ToString("HH:mm:ss");
            }
        }
        public double LastPrice
        {
            get
            {
                return _dataModel.lastPrice;
            }
        }
        public int EachDealSize
        {
            get
            {
                return _dataModel.lastSize;
            }
        }
        public string RepoTypeName
        {
            get
            {
                return _dataModel.repoType == 1 ? "空换" : "多换";
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
