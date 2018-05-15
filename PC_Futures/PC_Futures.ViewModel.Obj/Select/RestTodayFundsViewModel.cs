using MicroMvvm;
using PC_Futures.CommonClass;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModels
{
    public class RestTodayFundsViewModel : ObservableObject
    {

        private TodayFundsModel _TodayFundsModel;
        public RestTodayFundsViewModel(TodayFundsModel rtfm)
        {
            _TodayFundsModel = rtfm;
        }
        public string UserID
        {
            get
            {
                return _TodayFundsModel.user_id;
            }
            set
            {
                if (_TodayFundsModel.user_id != value)
                {
                    _TodayFundsModel.user_id = value;
                    RaisePropertyChanged("UserID");
                }
            }
        }
        public string LoginName
        {
            get
            {
                return _TodayFundsModel.login_name;
            }
            set
            {
                if (_TodayFundsModel.login_name != value)
                {
                    _TodayFundsModel.login_name = value;
                    RaisePropertyChanged("LoginName");
                }
            }
        }
        public string AccountName
        {
            get
            {
                return _TodayFundsModel.account_name;
            }
            set
            {
                if (_TodayFundsModel.account_name != value)
                {
                    _TodayFundsModel.account_name = value;
                    RaisePropertyChanged("AccountName");
                }
            }
        }
        public double UseMargin
        {
            get
            {
                return _TodayFundsModel.use_margin;
            }
            set
            {
                if (_TodayFundsModel.use_margin != value)
                {
                    _TodayFundsModel.use_margin = value;
                    RaisePropertyChanged("UseMargin");
                }
            }
        }
        public double YesterEquity
        {
            get
            {
                return _TodayFundsModel.yester_equity;
            }
            set
            {
                if (_TodayFundsModel.yester_equity != value)
                {
                    _TodayFundsModel.yester_equity = value;
                    RaisePropertyChanged("YesterEquity");
                }
            }
        }
        public double InferiorFund
        {
            get
            {
                return _TodayFundsModel.inferior_fund;
            }
            set
            {
                if (_TodayFundsModel.inferior_fund != value)
                {
                    _TodayFundsModel.inferior_fund = value;
                    RaisePropertyChanged("InferiorFund");
                }
            }
        }
        public double PriorityFund
        {
            get
            {
                return _TodayFundsModel.priority_fund;
            }
            set
            {
                if (_TodayFundsModel.priority_fund != value)
                {
                    _TodayFundsModel.priority_fund = value;
                    RaisePropertyChanged("InferiorFund");
                }
            }
        }
        public decimal AbleFund
        {
            get
            {
                long alll = Convert.ToInt64(_TodayFundsModel.able_fund * (int)ContractVariety.effectivityLenth);
                return Convert.ToDecimal(alll / ContractVariety.effectivityLenth);
            }
            set
            {
                if (_TodayFundsModel.able_fund != value)
                {
                    _TodayFundsModel.able_fund = value;
                    RaisePropertyChanged("AbleFund");
                }
            }
        }
        public double Fee
        {
            get
            {
                return _TodayFundsModel.fee;
            }
            set
            {
                if (_TodayFundsModel.fee != value)
                {
                    _TodayFundsModel.fee = value;
                    RaisePropertyChanged("Fee");
                }
            }
        }
        public double CloseProfitLoss
        {
            get
            {
                return _TodayFundsModel.close_profit_loss;
            }
            set
            {
                if (_TodayFundsModel.close_profit_loss != value)
                {
                    _TodayFundsModel.close_profit_loss = value;
                    RaisePropertyChanged("CloseProfitLoss");
                }
            }
        }
        public double FloatProfitLoss
        {
            get
            {
                return _TodayFundsModel.float_profit_loss;
            }
            set
            {
                if (_TodayFundsModel.float_profit_loss != value)
                {
                    _TodayFundsModel.float_profit_loss = value;
                    RaisePropertyChanged("FloatProfitLoss");
                }
            }
        }
        public double CurrentEquity
        {
            get
            {
                return _TodayFundsModel.current_equity;
            }
            set
            {
                if (_TodayFundsModel.current_equity != value)
                {
                    _TodayFundsModel.current_equity = value;
                    RaisePropertyChanged("CurrentEquity");
                }
            }
        }
        public double OutMoney
        {
            get
            {
                return _TodayFundsModel.out_money;
            }
            set
            {
                if (_TodayFundsModel.out_money != value)
                {
                    _TodayFundsModel.out_money = value;
                    RaisePropertyChanged("OutMoney");
                }
            }
        }
        public double InMoney
        {
            get
            {
                return _TodayFundsModel.in_money;
            }
            set
            {
                if (_TodayFundsModel.in_money != value)
                {
                    _TodayFundsModel.in_money = value;
                    RaisePropertyChanged("InMoney");
                }
            }
        }
        public double RiskLevels
        {
            get
            {
                return _TodayFundsModel.risk_levels;
            }
            set
            {
                if (_TodayFundsModel.risk_levels != value)
                {
                    _TodayFundsModel.risk_levels = value;
                    RaisePropertyChanged("RiskLevels");
                    RaisePropertyChanged("RiskLevelsStr");
                }
            }
        }
        public string RiskLevelsStr
        {
            get
            {
                return Math.Round((RiskLevels * 100),2) + "%";
            }
            set
            {
                RaisePropertyChanged("RiskLevelsStr");

            }
        }
    }


}
