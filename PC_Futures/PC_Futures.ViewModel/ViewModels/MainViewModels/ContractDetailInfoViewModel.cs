using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace PC_Futures
{
    public class ContractDetailInfoViewModel : ObservableObject
    {
        public ContractDetailInfoViewModel(FuturesViewModel futuresViewModel)
        {
            var variModel = TradeInfoHelper.VarietyModelList.FirstOrDefault(o => string.Equals(o.productCode, futuresViewModel.ProductCode));
            if(variModel!=null)
            {
                ProductCode = variModel.productCode;
                ProductName = variModel.productName;
                ExchangeName = variModel.exchangeName;
                CurrencyName = variModel.currencyName;
                Multiple = variModel.multiple ?? 0;
                TickSize = new decimal(variModel.tickSize ?? 0);
                TimeListRemark = variModel.timeListRemark;//variModel.timeList.Replace(",", "-");
                SettleTime = variModel.settleTime;
                ChangeMonth = variModel.changeMonth;
            }
        }
        private string _ProductCode;
        /// <summary>
        /// 品种代码
        /// </summary>
        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode == value) return;
                _ProductCode = value;
                RaisePropertyChanged(nameof(ProductCode));
            }
        }
        private string _ProductName;
        /// <summary>
        /// 品种名称
        /// </summary>
        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                if (_ProductName == value) return;
                _ProductName = value;
                RaisePropertyChanged(nameof(ProductName));
            }
        }
        private string _ExchangeName;
        /// <summary>
        /// 交易所
        /// </summary>
        public string ExchangeName
        {
            get { return _ExchangeName; }
            set
            {
                if (_ExchangeName == value) return;
                _ExchangeName = value;
                RaisePropertyChanged(nameof(ExchangeName));
            }
        }
        private string _CurrencyName;
        /// <summary>
        /// 交易币种名称
        /// </summary>
        public string CurrencyName
        {
            get { return _CurrencyName; }
            set
            {
                if (_CurrencyName == value) return;
                _CurrencyName = value;
                RaisePropertyChanged(nameof(CurrencyName));
            }
        }
        private int _Multiple;
        /// <summary>
        /// 合约大小
        /// </summary>
        public int Multiple
        {
            get { return _Multiple; }
            set
            {
                if (_Multiple == value) return;
                _Multiple = value;
                RaisePropertyChanged(nameof(Multiple));
            }
        }
        private decimal _TickSize;
        /// <summary>
        /// 最小变动价位
        /// </summary>
        public decimal TickSize
        {
            get { return _TickSize; }
            set
            {
                if (_TickSize == value) return;
                _TickSize = value;
                RaisePropertyChanged(nameof(TickSize));
            }
        }
        private string _TimeListRemark;
        /// <summary>
        /// 交易时段
        /// </summary>
        public string TimeListRemark
        {
            get { return _TimeListRemark; }
            set
            {
                if (_TimeListRemark == value) return;
                _TimeListRemark = value;
                RaisePropertyChanged(nameof(TimeListRemark));
            }
        }
        private string _SettleTime;
        /// <summary>
        /// 最后交易日
        /// </summary>
        public string SettleTime
        {
            get { return _SettleTime; }
            set
            {
                if (_SettleTime == value) return;
                _SettleTime = value;
                RaisePropertyChanged(nameof(SettleTime));
            }
        }
        private string _ChangeMonth;
        /// <summary>
        /// 主力合约换月
        /// </summary>
        public string ChangeMonth
        {
            get { return _ChangeMonth; }
            set
            {
                if (_ChangeMonth == value) return;
                _ChangeMonth = value;
                RaisePropertyChanged(nameof(ChangeMonth));
            }
        }
       
    }
}
