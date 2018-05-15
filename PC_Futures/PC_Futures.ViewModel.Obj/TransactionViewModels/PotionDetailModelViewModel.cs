using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModels
{
    public class PotionDetailModelViewModel : ObservableObject
    {
        private PotionDetailModel pmDetial = null;
        public PotionDetailModelViewModel(PotionDetailModel pdm)
        {
            pmDetial = pdm;
        }
        /// <summary>
        /// 开仓价
        /// </summary>
        public int Precision
        {
            get { return pmDetial.precision; }

            set
            {
                if (pmDetial.precision != value)
                {
                    pmDetial.precision = value;
                    RaisePropertyChanged("Precision");
                }
            }
        }
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID
        {
            get { return pmDetial.user_id; }

            set
            {
                if (pmDetial.user_id != value)
                {
                    pmDetial.user_id = value;
                    RaisePropertyChanged("UserID");
                }
            }
        }
        /// <summary>
        /// 合约ID
        /// </summary>
        public string ContractId
        {
            get { return pmDetial.contract_id; }

            set
            {
                if (pmDetial.contract_id != value)
                {
                    pmDetial.contract_id = value;
                    RaisePropertyChanged("ContractId");
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
                if (string.IsNullOrEmpty(pmDetial.contract_code))
                {
                    pmDetial.contract_code = pmDetial.contract_id;
                }
                return pmDetial.contract_code;
            }

            set
            {
                if (pmDetial.contract_code != value)
                {
                    pmDetial.contract_code = value;
                    RaisePropertyChanged("ContractCode");
                }
            }
        }
        /// <summary>
        /// 持仓编号
        /// </summary>
        public string PsitionId
        {
            get { return pmDetial.position_id; }

            set
            {
                if (pmDetial.position_id != value)
                {
                    pmDetial.position_id = value;
                    RaisePropertyChanged("PsitionId");
                }
            }
        }
        /// <summary>
        /// 持仓影子编号
        /// </summary>
        public string ShadowPositionid
        {
            get { return pmDetial.shadow_positionid; }

            set
            {
                if (pmDetial.shadow_positionid != value)
                {
                    pmDetial.shadow_positionid = value;
                    RaisePropertyChanged("ShadowPositionid");
                }
            }
        }

        /// <summary>
        /// 持仓手数
        /// </summary>
        public int PositionVolume
        {
            get { return pmDetial.position_volume; }

            set
            {
                if (pmDetial.position_volume != value)
                {
                    pmDetial.position_volume = value;
                    RaisePropertyChanged("PositionVolume");
                }
            }
        }
        /// <summary>
        /// B
        /// </summary>
        public string Direction
        {
            get { return pmDetial.direction; }

            set
            {
                if (pmDetial.direction != value)
                {
                    pmDetial.direction = value;
                    RaisePropertyChanged("Direction");
                }
            }
        }
        /// <summary>
        /// 可用手数
        /// </summary>
        public int AbleVolume
        {
            get { return pmDetial.able_volume; }

            set
            {
                if (pmDetial.able_volume != value)
                {
                    pmDetial.able_volume = value;
                    RaisePropertyChanged("AbleVolume");
                }
            }
        }

        private int _BDAbleVolume = 0;
        /// <summary>
        /// 可用手数
        /// </summary>
        public int BDAbleVolume
        {
            get
            {
                if (_BDAbleVolume < 0)
                    return 0;
                return _BDAbleVolume;
            }

            set
            {
                if (_BDAbleVolume != value)
                {
                    _BDAbleVolume = value;
                    RaisePropertyChanged("BDAbleVolume");
                }
            }
        }
        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public double PositionProfitLoss
        {
            get { return Math.Round(pmDetial.position_profit_loss, Precision); }

            set
            {
                if (pmDetial.position_profit_loss != value)
                {
                    pmDetial.position_profit_loss = value;
                    RaisePropertyChanged("PositionProfitLoss");
                    RaisePropertyChanged("PositionProfitLossStr");
                }
            }
        }

        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public string PositionProfitLossStr
        {
            get { return PositionProfitLoss.ToString("f" + Precision); }

            set
            {
                if (PositionProfitLoss != Convert.ToDouble(value))
                {
                    PositionProfitLoss = Convert.ToDouble(value);
                    RaisePropertyChanged("PositionProfitLossStr");
                }
            }
        }
        /// <summary>
        /// 开仓价
        /// </summary>
        public double OpenPrice
        {
            get { return Math.Round(pmDetial.open_price, Precision); }

            set
            {
                if (pmDetial.open_price != value)
                {
                    pmDetial.open_price = value;
                    RaisePropertyChanged("OpenPrice");
                    RaisePropertyChanged("OpenPriceStr");
                }
            }
        }

        /// <summary>
        /// 开仓价
        /// </summary>
        public string OpenPriceStr
        {
            get { return OpenPrice.ToString("f" + Precision); }

            set
            {
                if (OpenPrice != Convert.ToDouble(value))
                {
                    OpenPrice = Convert.ToDouble(value);
                    RaisePropertyChanged("OpenPriceStr");
                }
            }
        }

        double allPrice = 0;
        public double AllPrice
        {
            get { return allPrice; }

            set
            {
                if (allPrice != value)
                {
                    allPrice = value;
                    RaisePropertyChanged("AllPrice");
                }
            }
        }
        /// <summary>
        /// 开仓时间
        /// </summary>
        public string OpenTime
        {
            get { return pmDetial.open_date + " " + pmDetial.open_time; }

            set
            {
                if (pmDetial.open_time != value)
                {
                    pmDetial.open_time = value;
                    RaisePropertyChanged("OpenTime");
                }
            }
        }

        /// <summary>
        /// 开仓时间
        /// </summary>
        public string OpenDate
        {
            get { return pmDetial.open_date; }

            set
            {
                if (pmDetial.open_date != value)
                {
                    pmDetial.open_date = value;
                    RaisePropertyChanged("OpenDate");
                }
            }
        }


        /// <summary>
        /// 成交编号
        /// </summary>
        public string TradeId
        {
            get { return pmDetial.trade_id; }

            set
            {
                if (pmDetial.trade_id != value)
                {
                    pmDetial.trade_id = value;
                    RaisePropertyChanged("TradeId");
                }
            }
        }

        /// <summary>
        /// 下单影子id
        /// </summary>
        public string ShadowTradeId
        {
            get { return pmDetial.shadow_tradeid; }

            set
            {
                if (pmDetial.shadow_tradeid != value)
                {
                    pmDetial.shadow_tradeid = value;
                    RaisePropertyChanged("ShadowTradeId");
                }
            }
        }
        /// <summary>
        /// 下单影子id
        /// </summary>
        public string OrderId
        {
            get { return pmDetial.order_id; }

            set
            {
                if (pmDetial.order_id != value)
                {
                    pmDetial.order_id = value;
                    RaisePropertyChanged("OrderId");
                }
            }
        }
        /// <summary>
        /// 下单影子id
        /// </summary>
        public string ShadowOrderID
        {
            get { return pmDetial.shadow_orderID; }

            set
            {
                if (pmDetial.shadow_orderID != value)
                {
                    pmDetial.shadow_orderID = value;
                    RaisePropertyChanged("ShadowOrderID");
                }
            }
        }
        /// <summary>
        /// 占用保证金
        /// </summary>
        public double UseMargin
        {
            get { return Math.Round(pmDetial.use_margin, 2); }

            set
            {
                if (pmDetial.use_margin != value)
                {
                    pmDetial.use_margin = value;
                    RaisePropertyChanged("UseMargin");
                    RaisePropertyChanged("UseMarginStr");
                }
            }
        }

        /// <summary>
        /// 占用保证金
        /// </summary>
        public string UseMarginStr
        {
            get { return UseMargin.ToString("f2"); }

            set
            {
                if (UseMargin != Convert.ToDouble(value))
                {
                    UseMargin = Convert.ToDouble(value);
                    RaisePropertyChanged("UseMarginStr");
                }
            }
        }
        /// <summary>
        /// 占用保证金
        /// </summary>
        public double SysUseMargin
        {
            get { return Math.Round(pmDetial.sys_margin, 2); }

            set
            {
                if (pmDetial.sys_margin != value)
                {
                    pmDetial.sys_margin = value;
                    RaisePropertyChanged("SysUseMargin");
                    RaisePropertyChanged("SysUseMarginStr");
                }
            }
        }
        /// <summary>
        /// 占用保证金
        /// </summary>
        public string SysUseMarginStr
        {
            get { return SysUseMargin.ToString("f2"); }

            set
            {
                if (SysUseMargin != Convert.ToDouble(value))
                {
                    SysUseMargin = Convert.ToDouble(value);
                    RaisePropertyChanged("SysUseMarginStr");
                }
            }
        }

        private string _LossVolume;
        public string LossVolume
        {
            get { return _LossVolume; }

            set
            {
                if (_LossVolume != value)
                {
                    _LossVolume = value;
                    RaisePropertyChanged("LossVolume");
                }
            }
        }

        private string _ProfitVolume;
        public string ProfitVolume
        {
            get { return _ProfitVolume; }

            set
            {
                if (_ProfitVolume != value)
                {
                    _ProfitVolume = value;
                    RaisePropertyChanged("ProfitVolume");
                }
            }
        }
        private double _PositionProfitLossJB;
        public double PositionProfitLossJB
        {
            get { return Math.Round(_PositionProfitLossJB, 2); }

            set
            {
                if (_PositionProfitLossJB != value)
                {
                    _PositionProfitLossJB = value;
                    RaisePropertyChanged("PositionProfitLossJB");
                    RaisePropertyChanged("PositionProfitLossJBStr");
                }
            }
        }
        public string PositionProfitLossJBStr
        {
            get { return PositionProfitLossJB.ToString("f2"); }

            set
            {
                if (_PositionProfitLossJB != Convert.ToDouble(value))
                {
                    _PositionProfitLossJB = Convert.ToDouble(value);
                    RaisePropertyChanged("PositionProfitLossJBStr");
                }
            }
        }




        //private double _LossPrice;
        /// <summary>
        /// 止损价 为了给自动止盈止损用
        /// </summary>
        //public double LossPrice
        //{
        //    get { return _LossPrice; }

        //    set
        //    {
        //        if (_LossPrice != value)
        //        {
        //            _LossPrice = value;
        //            RaisePropertyChanged("LossPrice");
        //        }
        //    }
        //}


        //private double _NewPrice;
        ///// <summary>
        ///// 最新价 为了给自动止盈止损用
        ///// </summary>
        //public double NewPrice
        //{
        //    get { return _NewPrice; }

        //    set
        //    {
        //        if (_NewPrice != value)
        //        {
        //            _NewPrice = value;
        //            RaisePropertyChanged("NewPrice");
        //        }
        //    }
        //}



        public PotionDetailModelViewModel Clone(PotionDetailModelViewModel tpm)
        {

            PotionDetailModelViewModel temp = new PotionDetailModelViewModel(new PotionDetailModel());
            temp.AbleVolume = tpm.AbleVolume;
            temp.ContractCode = tpm.ContractCode;
            temp.ContractId = tpm.ContractId;
            temp.Direction = tpm.Direction;
            temp.LossVolume = tpm.LossVolume;
            temp.OpenDate = tpm.OpenDate;
            temp.OpenPrice = tpm.OpenPrice;
            temp.OpenTime = tpm.OpenTime;
            temp.OrderId = tpm.OrderId;
            temp.PositionProfitLoss = tpm.PositionProfitLoss;
            temp.PositionProfitLossJB = tpm.PositionProfitLossJB;
            temp.PositionVolume = tpm.PositionVolume;
            temp.ProfitVolume = tpm.ProfitVolume;
            temp.PsitionId = tpm.PsitionId;
            temp.ShadowOrderID = tpm.ShadowOrderID;
            temp.ShadowPositionid = tpm.ShadowPositionid;
            temp.ShadowTradeId = tpm.ShadowTradeId;
            temp.TradeId = tpm.TradeId;
            temp.UseMargin = tpm.UseMargin;
            temp.SysUseMargin = tpm.SysUseMargin;
            temp.UserID = tpm.UserID;
            temp.Precision = tpm.Precision;
            return temp;
        }
    }
}
