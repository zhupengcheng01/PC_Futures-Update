using MicroMvvm;
using PC_Futures.Models;
using System;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class FundsViewModel : ObservableObject
    {
        private static FundsViewModel _instance;
        public static FundsViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FundsViewModel();
            }
            return _instance;
        }
        private string _LoginName;
        public string LoginName
        {
            get
            {
                return _LoginName;
            }
            set
            {
                if (_LoginName != value)
                {
                    _LoginName = value;
                    RaisePropertyChanged(nameof(LoginName));
                }
            }
        }
        private string _AccountName;
        public string AccountName
        {
            get
            {
                return _AccountName;
            }
            set
            {
                if (_AccountName != value)
                {
                    _AccountName = value;
                    RaisePropertyChanged(nameof(AccountName));
                }
            }
        }
        private decimal _AbleFund;
        /// <summary>
        /// 可用资金
        /// </summary>
        public decimal AbleFund
        {
            get
            {
                long alll = Convert.ToInt64(_AbleFund * (int)ContractVariety.effectivityLenth);
                return Convert.ToDecimal(alll / ContractVariety.effectivityLenth);
            }
            set
            {
                if(_AbleFund!=value)
                {
                    _AbleFund = value;
                    RaisePropertyChanged(nameof(AbleFund));
                }
            }
        }
        private double _CurrentEquity;
        /// <summary>
        /// 当前权益
        /// </summary>
        public double CurrentEquity
        {
            get
            {
                return _CurrentEquity;
            }
            set
            {
                if (_CurrentEquity != value)
                {
                    _CurrentEquity = value;
                    RaisePropertyChanged(nameof(CurrentEquity));
                }
            }
        }
        private double _Closeprofitloss;
        /// <summary>
        /// 平仓盈亏
        /// </summary>
        public double Closeprofitloss
        {
            get
            {
                return _Closeprofitloss;
            }
            set
            {
                if (_Closeprofitloss != value)
                {
                    _Closeprofitloss = value;
                    RaisePropertyChanged(nameof(Closeprofitloss));
                }
            }
        }
        private double _Floatprofitloss;
        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public double Floatprofitloss
        {
            get
            {
                return _Floatprofitloss;
            }
            set
            {
                if (_Floatprofitloss != value)
                {
                    _Floatprofitloss = value;
                    RaisePropertyChanged(nameof(Floatprofitloss));
                }
            }
        }

        public void HandleViewModelData(TodayFundsModel dataModel)
        {
            AbleFund = dataModel.able_fund;
            CurrentEquity = dataModel.current_equity;
            Floatprofitloss = dataModel.float_profit_loss;
        }
    }
}
