using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModels
{
    public class TodayTraderModelViewModel : ObservableObject
    {
        TodayTraderModel _TodayTraderModel = null;
        public TodayTraderModelViewModel(TodayTraderModel ttm)
        {
            _TodayTraderModel = ttm;
        }

        /// <summary>
        /// 合约代码
        /// </summary>
        public string ContractCode
        {
            get
            {
                if (string.IsNullOrEmpty(_TodayTraderModel.contract_code))
                {
                    _TodayTraderModel.contract_code = _TodayTraderModel.contract_id;
                }
                return _TodayTraderModel.contract_code;
            }
            set
            {
                if (value != _TodayTraderModel.contract_code)
                {
                    _TodayTraderModel.contract_code = value;
                    RaisePropertyChanged("ContractCode");
                }
            }
        }
        /// <summary>
        /// 合约代码
        /// </summary>
        public string ContractId
        {
            get
            {
                return _TodayTraderModel.contract_id;
            }
            set
            {
                if (value != _TodayTraderModel.contract_id)
                {
                    _TodayTraderModel.contract_id = value;
                    RaisePropertyChanged("ContractId");
                }
            }
        }
        /// <summary>
        /// OrderStyle
        /// </summary>
        public string PriceType
        {
            get { return _TodayTraderModel.price_type; }
            set
            {
                if (value != _TodayTraderModel.price_type)
                {
                    _TodayTraderModel.price_type = value;
                    RaisePropertyChanged("PriceType");
                }
            }
        }
        /// <summary>
        /// 成交价格
        /// </summary>
        public double TradePrice
        {
            get { return Math.Round(_TodayTraderModel.trade_price, Precision);  }
            set
            {
                if (value != _TodayTraderModel.trade_price)
                {
                    _TodayTraderModel.trade_price = value;
                    RaisePropertyChanged("TradePrice");
                }
            }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction
        {
            get { return _TodayTraderModel.direction; }
            set
            {
                if (value != _TodayTraderModel.direction)
                {
                    _TodayTraderModel.direction = value;
                    RaisePropertyChanged("Direction");
                }
            }
        }
        /// <summary>
        /// 交易编号
        /// </summary>
        public string TradeNumber
        {
            get { return _TodayTraderModel.trade_id; }
            set
            {
                if (value != _TodayTraderModel.trade_id)
                {
                    _TodayTraderModel.trade_id = value;
                    RaisePropertyChanged("TradeNumber");
                }
            }
        }
        /// <summary>
        /// OffsetType 买卖方向
        /// </summary>
        public int OpenOffset
        {
            get { return _TodayTraderModel.open_offset; }
            set
            {
                if (value != _TodayTraderModel.open_offset)
                {
                    _TodayTraderModel.open_offset = value;
                    RaisePropertyChanged("OpenOffset");
                }
            }
        }
        /// <summary>
        /// 交易手数
        /// </summary>
        public double TradeVolume
        {
            get { return _TodayTraderModel.trade_volume; }
            set
            {
                if (value != _TodayTraderModel.trade_volume)
                {
                    _TodayTraderModel.trade_volume = value;
                    RaisePropertyChanged("TradeVolume");
                }
            }
        }
        /// <summary>
        /// 成交时间
        /// </summary>
        public string TradeTime
        {
            get { return _TodayTraderModel.trade_date + " " + _TodayTraderModel.trade_time; }
            set
            {
                if (value != _TodayTraderModel.trade_time)
                {
                    _TodayTraderModel.trade_time = value;
                    RaisePropertyChanged("TradeTime");
                }
            }
        }
        /// <summary>
        /// 委托编号
        /// </summary>
        public string OrderOrderref
        {
            get { return _TodayTraderModel.order_id; }
            set
            {
                if (value != _TodayTraderModel.order_id)
                {
                    _TodayTraderModel.order_id = value;
                    RaisePropertyChanged("OrderOrderref");
                }
            }
        }
        /// <summary>
        /// 下单影子id
        /// </summary>
        public string ShadowOrderID
        {
            get { return _TodayTraderModel.shadow_orderID; }
            set
            {
                if (value != _TodayTraderModel.shadow_orderID)
                {
                    _TodayTraderModel.shadow_orderID = value;
                    RaisePropertyChanged("ShadowOrderID");
                }
            }
        }

        /// <summary>
        /// 下单影子id
        /// </summary>
        public string ShadowTradeID
        {
            get { return _TodayTraderModel.shadow_tradeID; }
            set
            {
                if (value != _TodayTraderModel.shadow_tradeID)
                {
                    _TodayTraderModel.shadow_tradeID = value;
                    RaisePropertyChanged("ShadowTradeID");
                }
            }
        }

        private double _AllPrice;
        public double AllPrice
        {
            get { return _AllPrice; }
            set
            {
                if (value != _AllPrice)
                {
                    _AllPrice = value;
                    RaisePropertyChanged("AllPrice");
                }
            }
        }
        public int Precision
        {
            get { return _TodayTraderModel.precision; }

            set
            {
                if (_TodayTraderModel.precision != value)
                {
                    _TodayTraderModel.precision = value;
                    RaisePropertyChanged("Precision");
                }
            }
        }

        public TodayTraderModelViewModel Clone(TodayTraderModelViewModel tpm)
        {

            TodayTraderModelViewModel temp = new TodayTraderModelViewModel(new TodayTraderModel());
            temp.AllPrice = tpm.AllPrice;            
            temp.ContractCode = tpm.ContractCode;
            temp.ContractId = tpm.ContractId;            
            temp.Direction = tpm.Direction;
            temp.OpenOffset = tpm.OpenOffset;
            temp.OrderOrderref = tpm.OrderOrderref;
            temp.PriceType = tpm.PriceType;
            temp.ShadowTradeID = tpm.ShadowTradeID;
            temp.ShadowOrderID = tpm.ShadowOrderID;
            temp.TradeNumber = tpm.TradeNumber;
            temp.TradePrice = tpm.TradePrice;
            temp.TradeTime = tpm.TradeTime;
            temp.TradeVolume = tpm.TradeVolume;
            temp.Precision = tpm.Precision;
            return temp;
        }

    }
}
