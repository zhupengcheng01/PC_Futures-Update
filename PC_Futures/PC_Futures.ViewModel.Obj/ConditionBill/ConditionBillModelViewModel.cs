using Futures.Enum;
using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModels
{
    public class ConditionBillModelViewModel : ObservableObject
    {
        private ConditionBillModel _ConditionBillModel;
        public ConditionBillModelViewModel(ConditionBillModel cbm)
        {
            _ConditionBillModel = cbm;
        }

        /// <summary>
        /// 条件单ID
        /// </summary>
        public string ConditionOrderID
        {
            get { return _ConditionBillModel.condition_orderID; }
            set
            {
                if (_ConditionBillModel.condition_orderID != value)
                {
                    _ConditionBillModel.condition_orderID = value;
                    RaisePropertyChanged("ConditionOrderID");
                }
            }
        }
        /// <summary>
        /// 条件单ID
        /// </summary>
        public string TrrigerCond
        {
            get
            {
                string ss = "";
                if (_ConditionBillModel.condition_type == (int)YunConditionType.Y_PRICE)
                {
                    if (_ConditionBillModel.trriger_price_type == 0)
                    {
                        ss += "最新价";
                        if (_ConditionBillModel.trriger_condition == 0)
                        {
                            ss += ">=";
                        }
                        else if (_ConditionBillModel.trriger_condition == 1)
                        {
                            ss += "<=";
                        }
                    }
                    else if (_ConditionBillModel.trriger_price_type == 1)
                    {
                        ss += "买一价";
                        if (_ConditionBillModel.trriger_condition == 0)
                        {
                            ss += ">=";
                        }
                        else if (_ConditionBillModel.trriger_condition == 1)
                        {
                            ss += "<=";
                        }
                    }
                    else if (_ConditionBillModel.trriger_price_type == 2)
                    {
                        ss += "卖一价";
                        if (_ConditionBillModel.trriger_condition == 0)
                        {
                            ss += ">=";
                        }
                        else if (_ConditionBillModel.trriger_condition == 1)
                        {
                            ss += "<=";
                        }
                    }
                    ss += _ConditionBillModel.trriger_price + "触发";
                }
                else if (_ConditionBillModel.condition_type == (int)YunConditionType.Y_TIME)
                {
                    ss += "时间到达" + _ConditionBillModel.trriger_contime + "触发";
                }
                else if (_ConditionBillModel.condition_type == (int)YunConditionType.Y_TIMEPRICE)
                {
                    if (_ConditionBillModel.trriger_price_type == 0)
                    {
                        ss += "最新价";
                        if (_ConditionBillModel.trriger_condition == 0)
                        {
                            ss += ">=";
                        }
                        else if (_ConditionBillModel.trriger_condition == 1)
                        {
                            ss += "<=";
                        }
                    }
                    else if (_ConditionBillModel.trriger_price_type == 1)
                    {
                        ss += "买一价";
                        if (_ConditionBillModel.trriger_condition == 0)
                        {
                            ss += ">=";
                        }
                        else if (_ConditionBillModel.trriger_condition == 1)
                        {
                            ss += "<=";
                        }
                    }
                    else if (_ConditionBillModel.trriger_price_type == 2)
                    {
                        ss += "卖一价";
                        if (_ConditionBillModel.trriger_condition == 0)
                        {
                            ss += ">=";
                        }
                        else if (_ConditionBillModel.trriger_condition == 1)
                        {
                            ss += "<=";
                        }
                    }
                    ss += _ConditionBillModel.trriger_price + "且时间到达" + _ConditionBillModel.trriger_contime + "触发"; ;
                }
                return ss;
            }

        }
        public int Status
        {
            get { return _ConditionBillModel.status; }
            set
            {
                if (_ConditionBillModel.status != value)
                {
                    _ConditionBillModel.status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }
        /// <summary>
        /// 条件单ID
        /// </summary>
        public string ContractId
        {
            get { return _ConditionBillModel.contract_id; }
            set
            {
                if (_ConditionBillModel.contract_id != value)
                {
                    _ConditionBillModel.contract_id = value;
                    RaisePropertyChanged("ContractId");
                }
            }
        }
        /// <summary>
        /// 合约代码
        /// </summary>
        public string ContractCode
        {
            get { return _ConditionBillModel.contract_code; }
            set
            {
                if (_ConditionBillModel.contract_code != value)
                {
                    _ConditionBillModel.contract_code = value;
                    RaisePropertyChanged("ContractCode");
                }
            }
        }
        /// <summary>
        /// YunTriggerType
        /// </summary>
        public int TrrigerPriceType
        {
            get { return _ConditionBillModel.trriger_price_type; }
            set
            {
                if (_ConditionBillModel.trriger_price_type != value)
                {
                    _ConditionBillModel.trriger_price_type = value;
                    RaisePropertyChanged("TrrigerPriceType");
                    RaisePropertyChanged("TrrigerCond");
                }
            }
        }
        /// <summary>
        /// 触发价格
        /// </summary>
        public double TrrigerPrice
        {
            get { return _ConditionBillModel.trriger_price; }
            set
            {
                if (_ConditionBillModel.trriger_price != value)
                {
                    _ConditionBillModel.trriger_price = value;
                    RaisePropertyChanged("TrrigerPrice");
                    RaisePropertyChanged("TrrigerCond");
                }
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate
        {
            get { return _ConditionBillModel.create_date; }
            set
            {
                if (_ConditionBillModel.create_date != value)
                {
                    _ConditionBillModel.create_date = value;
                    RaisePropertyChanged("CreateDate");
                    RaisePropertyChanged("TrrigerCond");
                }
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            get { return _ConditionBillModel.create_date + " " + _ConditionBillModel.create_time; }
            set
            {
                if (_ConditionBillModel.create_time != value)
                {
                    _ConditionBillModel.create_time = value;
                    RaisePropertyChanged("CreateTime");
                    RaisePropertyChanged("TrrigerCond");
                }
            }
        }
        /// <summary>
        /// 触发时间
        /// </summary>
        public string TrrigerTime
        {
            get
            {
                if (Status == (int)ConditionStateType.CONDITION_STATE_HADTRRIGER)
                {
                    return _ConditionBillModel.trriger_date + " " + _ConditionBillModel.trriger_time;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (_ConditionBillModel.trriger_time != value)
                {
                    _ConditionBillModel.trriger_time = value;
                    RaisePropertyChanged("TrrigerTime");
                    RaisePropertyChanged("TrrigerCond");
                }
            }
        }
        /// <summary>
        /// 触发时间
        /// </summary>
        public string TrrigerDate
        {
            get { return _ConditionBillModel.trriger_date; }
            set
            {
                if (_ConditionBillModel.trriger_date != value)
                {
                    _ConditionBillModel.trriger_date = value;
                    RaisePropertyChanged("TrrigerDate");
                    RaisePropertyChanged("TrrigerCond");
                }
            }
        }
        public string TrrigerContime
        {
            get
            {

                return _ConditionBillModel.trriger_contime;


            }
            set
            {
                if (_ConditionBillModel.trriger_contime != value)
                {
                    _ConditionBillModel.trriger_contime = value;
                    RaisePropertyChanged("TrrigerContime");
                    RaisePropertyChanged("TrrigerCond");
                }
            }
        }
        public string TrrigerCondate
        {
            get
            {

                return _ConditionBillModel.trriger_condate;


            }
            set
            {
                if (_ConditionBillModel.trriger_condate != value)
                {
                    _ConditionBillModel.trriger_condate = value;
                    RaisePropertyChanged("TrrigerCondate");
                }
            }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction
        {
            get { return _ConditionBillModel.direction; }
            set
            {
                if (_ConditionBillModel.direction != value)
                {
                    _ConditionBillModel.direction = value;
                    RaisePropertyChanged("Direction");
                }
            }
        }
        /// <summary>
        /// OffsetType
        /// </summary>
        public int OpenOffset
        {
            get { return _ConditionBillModel.open_offset; }
            set
            {
                if (_ConditionBillModel.open_offset != value)
                {
                    _ConditionBillModel.open_offset = value;
                    RaisePropertyChanged("OpenOffset");
                }
            }
        }
        /// <summary>
        /// 下单手数
        /// </summary>
        public int OrderVolume
        {
            get { return _ConditionBillModel.order_volume; }
            set
            {
                if (_ConditionBillModel.order_volume != value)
                {
                    _ConditionBillModel.order_volume = value;
                    RaisePropertyChanged("OrderVolume");
                }
            }
        }
        /// <summary>
        /// 下单价格
        /// </summary>
        public double OrderPrice
        {
            get { return _ConditionBillModel.order_price; }
            set
            {
                if (_ConditionBillModel.order_price != value)
                {
                    _ConditionBillModel.order_price = value;
                    RaisePropertyChanged("OrderPrice");
                    RaisePropertyChanged("ShowOrderPrice");
                    RaisePropertyChanged("TrrigerCond");
                }
            }
        }
        /// <summary>
        /// OrderStyle
        /// </summary>
        public string PriceType
        {
            get { return _ConditionBillModel.price_type; }
            set
            {
                if (_ConditionBillModel.price_type != value)
                {
                    _ConditionBillModel.price_type = value;
                    RaisePropertyChanged("PriceType");
                    RaisePropertyChanged("TrrigerCond");
                    RaisePropertyChanged("ShowOrderPrice");
                }
            }
        }
        /// <summary>
        /// YunConditionType
        /// </summary>
        public int ConditionType
        {
            get { return _ConditionBillModel.condition_type; }
            set
            {
                if (_ConditionBillModel.condition_type != value)
                {
                    _ConditionBillModel.condition_type = value;
                    RaisePropertyChanged("ConditionType");
                    RaisePropertyChanged("TrrigerCond");
                    RaisePropertyChanged("ShowOrderPrice");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserID
        {
            get { return _ConditionBillModel.user_id; }
            set
            {
                if (_ConditionBillModel.user_id != value)
                {
                    _ConditionBillModel.user_id = value;
                    RaisePropertyChanged("UserID");
                }
            }
        }
        /// <summary>
        /// OrderStateType 订单状态
        /// </summary>
        public int TrrigerCondition
        {
            get { return _ConditionBillModel.trriger_condition; }
            set
            {
                if (_ConditionBillModel.trriger_condition != value)
                {
                    _ConditionBillModel.trriger_condition = value;
                    RaisePropertyChanged("TrrigerCondition");
                    RaisePropertyChanged("TrrigerCond");
                    
                }
            }
        }

        private string _ShowOrderPrice;
        /// <summary>
        /// 下单价格
        /// </summary>
        public string ShowOrderPrice
        {
            get
            {

                if (_ConditionBillModel.price_type == "M")
                {
                    _ShowOrderPrice = "市价";
                }
                else if (_ConditionBillModel.price_type == "R")
                {
                    _ShowOrderPrice = "对手价";
                }
                else
                {
                    _ShowOrderPrice = _ConditionBillModel.order_price.ToString();
                }
                return _ShowOrderPrice;
            }
            set
            {
                if (_ShowOrderPrice != value)
                {
                    _ShowOrderPrice = value;
                    RaisePropertyChanged("ShowOrderPrice");
                }
            }
        }

    }
}
