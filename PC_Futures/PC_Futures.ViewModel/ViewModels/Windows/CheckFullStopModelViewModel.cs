using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModel
{
    public class CheckFullStopModelViewModel : ObservableObject
    {
        private CheckFullStopModel _CheckFullStopModel;
        public CheckFullStopModelViewModel(CheckFullStopModel cfsm)
        {
            _CheckFullStopModel = cfsm;
        }

        private int _MaxNum = 0;
        public int MaxNum
        {
            get { return _MaxNum; }
            set
            {
                if (_MaxNum != value)
                {
                    _MaxNum = value;
                    RaisePropertyChanged("MaxNum");
                }
            }
        }
        private double _Increment = 1;
        public double Increment
        {
            get { return _Increment; }
            set
            {
                if (_Increment != value)
                {
                    _Increment = value;
                    RaisePropertyChanged("Increment");
                }
            }
        }

        private int _precision = 1;
        public int Precision {
            get { return _precision; }
            set
            {
                if (_precision != value)
                {
                    _precision = value;
                    RaisePropertyChanged("Precision");
                }
            }
        }
        /// <summary>
        /// UserId
        /// </summary>
        public string UserId
        {
            get { return _CheckFullStopModel.user_id; }
            set
            {
                if (_CheckFullStopModel.user_id != value)
                {
                    _CheckFullStopModel.user_id = value;
                    RaisePropertyChanged("UserId");
                }
            }
        }
        /// <summary>
        /// 条件单ID
        /// 
        /// </summary>
        public string StoplossId
        {
            get { return _CheckFullStopModel.stoploss_id; }
            set
            {
                if (_CheckFullStopModel.stoploss_id != value)
                {
                    _CheckFullStopModel.stoploss_id = value;
                    RaisePropertyChanged("StoplossId");
                }
            }
        }
        /// <summary>
        /// 合约代码
        /// </summary>
        public string ContractCode
        {
            get { return _CheckFullStopModel.contract_code; }
            set
            {
                if (_CheckFullStopModel.contract_code != value)
                {
                    _CheckFullStopModel.contract_code = value;
                    RaisePropertyChanged("ContractCode");
                }
            }
        }
        /// <summary>
        /// 合约ID
        /// </summary>
        public string ContractId
        {
            get { return _CheckFullStopModel.contract_id; }
            set
            {
                if (_CheckFullStopModel.contract_id != value)
                {
                    _CheckFullStopModel.contract_id = value;
                    RaisePropertyChanged("ContractId");
                }
            }
        }

        /// <summary>
        /// YunTrrigerStyle
        /// </summary>
        public int TrrigerPriceType
        {
            get { return _CheckFullStopModel.trriger_price_type; }
            set
            {
                if (_CheckFullStopModel.trriger_price_type != value)
                {
                    _CheckFullStopModel.trriger_price_type = value;
                    RaisePropertyChanged("TrrigerPriceType");
                }
            }
        }

        /// <summary>
        /// 触发的价格
        /// </summary>
        public double TrrigerPrice
        {
            get { return _CheckFullStopModel.trriger_price; }
            set
            {
                if (_CheckFullStopModel.trriger_price != value)
                {
                    _CheckFullStopModel.trriger_price = value;
                    RaisePropertyChanged("TrrigerPrice");
                }
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return _CheckFullStopModel.status; }
            set
            {
                if (_CheckFullStopModel.status != value)
                {
                    _CheckFullStopModel.status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }
        /// <summary>
        /// 触发条件
        /// </summary>
        public string TrrigerCondition
        {
            get { return _CheckFullStopModel.trriger_condition; }
            set
            {
                if (_CheckFullStopModel.trriger_condition != value)
                {
                    _CheckFullStopModel.trriger_condition = value;
                    RaisePropertyChanged("TrrigerCondition");
                }
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            get { return _CheckFullStopModel.create_date + " " + _CheckFullStopModel.create_time; }
            set
            {
                if (_CheckFullStopModel.create_time != value)
                {
                    _CheckFullStopModel.create_time = value;
                    RaisePropertyChanged("CreateTime");
                }
            }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate
        {
            get { return _CheckFullStopModel.create_date; }
            set
            {
                if (_CheckFullStopModel.create_date != value)
                {
                    _CheckFullStopModel.create_date = value;
                    RaisePropertyChanged("CreateDate");
                }
            }
        }
        /// <summary>
        /// 触发时间
        /// </summary>
        public string TrrigerTime
        {
            get { return _CheckFullStopModel.trriger_time; }
            set
            {
                if (_CheckFullStopModel.trriger_time != value)
                {
                    _CheckFullStopModel.trriger_time = value;
                    RaisePropertyChanged("TrrigerTime");
                }
            }
        }
        /// <summary>
        /// 触发日期
        /// </summary>
        public string TrrigerDate
        {
            get { return _CheckFullStopModel.trriger_date; }
            set
            {
                if (_CheckFullStopModel.trriger_date != value)
                {
                    _CheckFullStopModel.trriger_date = value;
                    RaisePropertyChanged("TrrigerDate");
                }
            }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction
        {
            get { return _CheckFullStopModel.direction; }
            set
            {
                if (_CheckFullStopModel.direction != value)
                {
                    _CheckFullStopModel.direction = value;
                    RaisePropertyChanged("Direction");
                }
            }
        }
        /// <summary>
        /// 开平
        /// </summary>
        public int OpenOffset
        {
            get { return _CheckFullStopModel.open_offset; }
            set
            {
                if (_CheckFullStopModel.open_offset != value)
                {
                    _CheckFullStopModel.open_offset = value;
                    RaisePropertyChanged("OpenOffset");
                }
            }
        }
        /// <summary>
        /// 下单手数
        /// </summary>
        public int OrderVolume
        {
            get { return _CheckFullStopModel.order_volume; }
            set
            {
                if (_CheckFullStopModel.order_volume != value)
                {
                    _CheckFullStopModel.order_volume = value;
                    RaisePropertyChanged("OrderVolume");
                }
            }
        }
        /// <summary>
        /// 止损价
        /// </summary>
        public double StoplossPrice
        {
            get { return _CheckFullStopModel.stoploss_price; }
            set
            {
                if (_CheckFullStopModel.stoploss_price != value)
                {
                    _CheckFullStopModel.stoploss_price = Math.Round(value,Precision);
                    RaisePropertyChanged("StoplossPrice");
                }
            }
        }
        /// <summary>
        /// 止盈价
        /// </summary>
        public double StopprofitPrice
        {
            get { return _CheckFullStopModel.stopprofit_price; }
            set
            {
                if (_CheckFullStopModel.stopprofit_price != value)
                {
                    _CheckFullStopModel.stopprofit_price = Math.Round(value, Precision);
                    RaisePropertyChanged("StopprofitPrice");
                }
            }
        }
        /// <summary>
        /// 浮动止损
        /// </summary>
        public double FloatLoss
        {
            get { return _CheckFullStopModel.float_loss; }
            set
            {
                if (_CheckFullStopModel.float_loss != value)
                {
                    _CheckFullStopModel.float_loss = Math.Round(value, Precision);
                    RaisePropertyChanged("FloatLoss");
                }
            }
        }
        /// <summary>
        /// 浮动止损标志
        /// </summary>
        public int FloatLossFlag
        {
            get { return _CheckFullStopModel.float_loss_flag; }
            set
            {
                if (_CheckFullStopModel.float_loss_flag != value)
                {
                    _CheckFullStopModel.float_loss_flag = value;
                    RaisePropertyChanged("FloatLossFlag");
                }
            }
        }
        /// <summary>
        /// 浮动止损标志
        /// </summary>
        public string Validity
        {
            get { return "永久有效"; }
            set
            {

                RaisePropertyChanged("Validity");

            }
        }
    }
}
