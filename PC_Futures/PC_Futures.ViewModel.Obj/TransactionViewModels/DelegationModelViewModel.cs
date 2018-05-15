using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModels
{
    public class DelegationModelViewModel : ObservableObject
    {
        private DelegationModel _DelegationModel;
        public DelegationModelViewModel(DelegationModel dm)
        {
            _DelegationModel = dm;
        }
        /// <summary>
        /// 合约代码
        /// </summary>
        public string UserID
        {
            get { return _DelegationModel.user_id; }

            set
            {
                if (_DelegationModel.user_id != value)
                {
                    _DelegationModel.user_id = value;
                    RaisePropertyChanged("UserID");
                }
            }
        }

        /// <summary>
        /// 合约代码
        /// </summary>
        public string ContractID
        {
            get { return _DelegationModel.contract_id; }

            set
            {
                if (_DelegationModel.contract_id != value)
                {
                    _DelegationModel.contract_id = value;
                    RaisePropertyChanged("ContractID");
                }
            }
        }

        /// <summary>
        /// 合约代码
        /// </summary>
        public string ContractCode
        {
            get
            {
                if (string.IsNullOrEmpty(_DelegationModel.contract_code))
                {
                    _DelegationModel.contract_code = _DelegationModel.contract_id;
                }
                return _DelegationModel.contract_code;
            }

            set
            {
                if (_DelegationModel.contract_code != value)
                {
                    _DelegationModel.contract_code = value;
                    RaisePropertyChanged("ContractCode");
                }
            }
        }
        /// <summary>
        /// OrderStyle
        /// </summary>
        public string PriceType
        {
            get { return _DelegationModel.price_type; }

            set
            {
                if (_DelegationModel.price_type != value)
                {
                    _DelegationModel.price_type = value;
                    RaisePropertyChanged("PriceType");
                    RaisePropertyChanged("OrderPriceStr");
                }
            }
        }
        /// <summary>
        /// 下单手数
        /// </summary>
        public int OrderVolume
        {
            get { return _DelegationModel.order_volume; }

            set
            {
                if (_DelegationModel.order_volume != value)
                {
                    _DelegationModel.order_volume = value;
                    RaisePropertyChanged("OrderVolume");
                }
            }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public double OrderPrice
        {
            get { return _DelegationModel.order_price; }

            set
            {
                if (_DelegationModel.order_price != value)
                {
                    _DelegationModel.order_price = value;
                    RaisePropertyChanged("OrderPrice");
                    RaisePropertyChanged("OrderPriceStr");
                }
            }
        }

        private string _OrderPriceStr;
        /// <summary>
        /// 价格
        /// </summary>
        public string OrderPriceStr
        {
            get {
                if (_DelegationModel.price_type == "M")
                    return "市价";
                else
                return _DelegationModel.order_price.ToString(); }
            set
            {
                if (_OrderPriceStr != value)
                {
                    _OrderPriceStr = value;
                    RaisePropertyChanged("OrderPriceStr");
                }
            }

        }
    
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction
        {
            get { return _DelegationModel.direction; }

            set
            {
                if (_DelegationModel.direction != value)
                {
                    _DelegationModel.direction = value;
                    RaisePropertyChanged("Direction");
                }
            }
        }
        /// <summary>
        /// 下单编号
        /// </summary>
        public string OrderOrderref
        {
            get { return _DelegationModel.order_orderref; }

            set
            {
                if (_DelegationModel.order_orderref != value)
                {
                    _DelegationModel.order_orderref = value;
                    RaisePropertyChanged("OrderOrderref");
                }
            }
        }
        /// <summary>
        /// OffsetType
        /// </summary>
        public int OpenOffset
        {
            get { return _DelegationModel.open_offset; }

            set
            {
                if (_DelegationModel.open_offset != value)
                {
                    _DelegationModel.open_offset = value;
                    RaisePropertyChanged("OpenOffset");
                }
            }
        }
        /// <summary>
        /// OrderStateType
        /// </summary>
        public int OrderStatus
        {
            get { return _DelegationModel.order_status; }

            set
            {
                if (_DelegationModel.order_status != value)
                {
                    _DelegationModel.order_status = value;
                    RaisePropertyChanged("OrderStatus");
                }
            }
        }

        public string FailMsg
        {
            get { return _DelegationModel.fail_msg; }

            set
            {
                if (_DelegationModel.fail_msg != value)
                {
                    _DelegationModel.fail_msg = value;
                    RaisePropertyChanged("FailMsg");
                }
            }
        }
        /// <summary>
        /// 交易手数
        /// </summary>
        public int TradeVolume
        {
            get { return _DelegationModel.trade_volume; }

            set
            {
                if (_DelegationModel.trade_volume != value)
                {
                    _DelegationModel.trade_volume = value;
                    RaisePropertyChanged("TradeVolume");
                }
            }
        }
        /// <summary>
        /// 剩余手数
        /// </summary>
        public int LeftVolume
        {
            get { return _DelegationModel.left_volume; }

            set
            {
                if (_DelegationModel.left_volume != value)
                {
                    _DelegationModel.left_volume = value;
                    RaisePropertyChanged("LeftVolume");
                }
            }
        }
        /// <summary>
        /// 成交均价
        /// </summary>
        public double TradePrice
        {
            get { return Math.Round(_DelegationModel.trade_price, Precision); }

            set
            {
                if (_DelegationModel.trade_price != value)
                {
                    _DelegationModel.trade_price = value;
                    RaisePropertyChanged("TradePrice");
                }
            }
        }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string OrderTime
        {
            get { return _DelegationModel.order_time; }

            set
            {
                if (_DelegationModel.order_time != value)
                {
                    _DelegationModel.order_time = value;
                    RaisePropertyChanged("OrderTime");
                }
            }
        }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string OrderDate
        {
            get { return _DelegationModel.order_date; }

            set
            {
                if (_DelegationModel.order_date != value)
                {
                    _DelegationModel.order_date = value;
                    RaisePropertyChanged("OrderDate");
                }
            }
        }
        /// <summary>
        /// Upadae时间
        /// </summary>
        public string UpdateTime
        {
            get { return _DelegationModel.update_time; }

            set
            {
                if (_DelegationModel.update_time != value)
                {
                    _DelegationModel.update_time = value;
                    RaisePropertyChanged("UpdateTime");
                }
            }
        }
        /// <summary>
        /// Upadae时间
        /// </summary>
        public string UpdateDate
        {
            get { return _DelegationModel.update_date; }

            set
            {
                if (_DelegationModel.update_date != value)
                {
                    _DelegationModel.update_date = value;
                    RaisePropertyChanged("UpdateDate");
                }
            }
        }
        /// <summary>
        /// 操作员
        /// </summary>
        public string OperatorID
        {
            get { return _DelegationModel.operator_id; }

            set
            {
                if (_DelegationModel.operator_id != value)
                {
                    _DelegationModel.operator_id = value;
                    RaisePropertyChanged("OperatorID");
                }
            }
        }
        /// <summary>
        /// 下单影子id
        /// </summary>
        public string ShadowOrderID
        {
            get { return _DelegationModel.shadow_orderID; }

            set
            {
                if (_DelegationModel.shadow_orderID != value)
                {
                    _DelegationModel.shadow_orderID = value;
                    RaisePropertyChanged("ShadowOrderID");
                }
            }
        }

        /// <summary>
        /// 下单影子id
        /// </summary>
        public string OrderId
        {
            get { return _DelegationModel.order_id; }

            set
            {
                if (_DelegationModel.order_id != value)
                {
                    _DelegationModel.order_id = value;
                    RaisePropertyChanged("OrderId");
                }
            }
        }

        public int Precision
        {
            get { return _DelegationModel.precision; }

            set
            {
                if (_DelegationModel.precision != value)
                {
                    _DelegationModel.precision = value;
                    RaisePropertyChanged("Precision");
                }
            }
        }
    }
}
