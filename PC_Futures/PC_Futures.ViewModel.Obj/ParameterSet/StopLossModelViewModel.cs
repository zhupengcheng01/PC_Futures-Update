using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModels
{
    public class StopLossModelViewModel : ObservableObject
    {
        private string _StopLossPrice;

        public bool _IsReadOnly = false;
        public bool IsReadOnly
        {
            get { return _IsReadOnly; }
            set
            {
                if (_IsReadOnly != value)
                {
                    _IsReadOnly = value;
                    RaisePropertyChanged("IsReadOnly");
                }
            }

        }

        //if (StopLossModelViewModel. )
        //   {
        //       IsReadOnly = true;
        //   }
        //   else
        //   {
        //       IsReadOnly = false;
        //   }

        /// <summary>
        /// 指定价时,触发止损
        /// </summary>
        public string StopLossPrice
        {
            get { return _StopLossPrice; }
            set
            {
                if (_StopLossPrice != value)
                {
                    _StopLossPrice = value;
                    RaisePropertyChanged("StopLossPrice");
                }
            }
        }

        private string _StopLossDelegation = "市价";
        /// <summary>
        /// 止损时按照发出委托
        /// </summary>
        public string StopLossDelegation
        {
            get { return _StopLossDelegation; }
            set
            {
                if (_StopLossDelegation != value)
                {
                    _StopLossDelegation = value;
                    if (_StopLossDelegation == "市价")
                    {
                        IsReadOnly = false;
                        CkBuy = false;
                        CkSell = false;
                    }
                    else
                    {
                        IsReadOnly = true;
                        CkBuy = true;
                        CkSell = true;
                    }
                    RaisePropertyChanged("StopLossDelegation");
                }
            }
        }
        private string _StopProfitDelegation;

        /// <summary>
        /// 止盈时按照发出委托
        /// </summary>
        public string StopProfitDelegation
        {
            get { return _StopLossDelegation; }
            set
            {
                if (_StopLossDelegation != value)
                {
                    _StopLossDelegation = value;
                    RaisePropertyChanged("StopLossDelegation");
                }
            }
        }
        private bool _CkBuy = false;
        /// <summary>
        /// 买入调整
        /// </summary>
        public bool CkBuy
        {
            get { return _CkBuy; }
            set
            {
                if (_CkBuy != value)
                {
                    _CkBuy = value;
                    RaisePropertyChanged("CkBuy");
                }
            }
        }
        private int _BuyNum;
        /// <summary>
        /// 买入调整个价位发出委托
        /// </summary>
        public int BuyNum
        {
            get { return _BuyNum; }
            set
            {
                if (_BuyNum != value)
                {
                    _BuyNum = value;
                    RaisePropertyChanged("BuyNum");
                }
            }
        }
        private bool _CkSell = false;
        /// <summary>
        /// 卖出调整
        /// </summary>
        public bool CkSell
        {
            get { return _CkSell; }
            set
            {
                if (_CkSell != value)
                {
                    _CkSell = value;
                    RaisePropertyChanged("CkSell");
                }
            }
        }
        private int _SellNum;
        /// <summary>
        /// 卖出调整个价位发出委托
        /// </summary>
        public int SellNum
        {
            get { return _SellNum; }
            set
            {
                if (_SellNum != value)
                {
                    _SellNum = value;
                    RaisePropertyChanged("SellNum");
                }
            }
        }
    }

    /// <summary>
    /// 下单版设置
    /// </summary>
    public class OrderVersionModelViewModel : ObservableObject
    {
        private bool _BeforeOrderEnter;
        /// <summary>
        /// 下单前确认
        /// </summary>
        public bool BeforeOrderEnter
        {
            get { return _BeforeOrderEnter; }
            set
            {
                if (_BeforeOrderEnter != value)
                {
                    _BeforeOrderEnter = value;
                    RaisePropertyChanged("BeforeOrderEnter");
                }
            }
        }
        private int _DefaultLot;
        /// <summary>
        /// 默认手数
        /// </summary>
        public int DefaultLot
        {
            get { return _DefaultLot; }
            set
            {
                if (_DefaultLot != value)
                {
                    _DefaultLot = value;
                    RaisePropertyChanged("DefaultLot");
                }
            }
        }
        private int _MaxLot;
        /// <summary>
        /// 最大手数
        /// </summary>
        public int MaxLot
        {
            get { return _MaxLot; }
            set
            {
                if (_MaxLot != value)
                {
                    _MaxLot = value;
                    RaisePropertyChanged("MaxLot");
                }
            }
        }

    }

    /// <summary>
    /// 快捷键设置
    /// </summary>
    public class ShortcutKeyModelViewModel : ObservableObject
    {
        private bool _LockMenu;
        /// <summary>
        /// 锁定菜单版
        /// </summary>
        public bool LockMenu
        {
            get { return _LockMenu; }
            set
            {
                if (_LockMenu != value)
                {
                    _LockMenu = value;
                    RaisePropertyChanged("LockMenu");
                }
            }
        }
        private string _BuyOpen;
        /// <summary>
        /// 买开
        /// </summary>
        public string BuyOpen
        {
            get { return _BuyOpen; }
            set
            {
                if (_BuyOpen != value)
                {
                    _BuyOpen = value;
                    RaisePropertyChanged("BuyOpen");
                }
            }
        }
        private string _SellOpen;
        /// <summary>
        /// 卖开
        /// </summary>
        public string SellOpen
        {
            get { return _SellOpen; }
            set
            {
                if (_SellOpen != value)
                {
                    _SellOpen = value;
                    RaisePropertyChanged("SellOpen");
                }
            }
        }
        private string _ClosingBuy;
        /// <summary>
        /// 买平
        /// </summary>
        public string ClosingBuy
        {
            get { return _ClosingBuy; }
            set
            {
                if (_ClosingBuy != value)
                {
                    _ClosingBuy = value;
                    RaisePropertyChanged("ClosingBuy");
                }
            }
        }

        private string _ClosingSell;
        /// <summary>
        /// 卖平
        /// </summary>
        public string ClosingSell
        {
            get { return _ClosingSell; }
            set
            {
                if (_ClosingSell != value)
                {
                    _ClosingSell = value;
                    RaisePropertyChanged("ClosingSell");
                }
            }
        }
        private string _Revoke;
        /// <summary>
        /// 撤单
        /// </summary>
        public string Revoke
        {
            get { return _Revoke; }
            set
            {
                if (_Revoke != value)
                {
                    _Revoke = value;
                    RaisePropertyChanged("Revoke");
                }
            }
        }

        private string _Clearance;
        /// <summary>
        /// 清仓
        /// </summary>
        public string Clearance
        {
            get { return _Clearance; }
            set
            {
                if (_Clearance != value)
                {
                    _Clearance = value;
                    RaisePropertyChanged("Clearance");
                }
            }
        }


        private int _IntBuyOpen;
        /// <summary>
        /// 买开
        /// </summary>
        public int IntBuyOpen
        {
            get { return _IntBuyOpen; }
            set
            {
                if (_IntBuyOpen != value)
                {
                    _IntBuyOpen = value;
                    RaisePropertyChanged("IntBuyOpen");
                }
            }
        }
        private int _IntSellOpen;
        /// <summary>
        /// 卖开
        /// </summary>
        public int IntSellOpen
        {
            get { return _IntSellOpen; }
            set
            {
                if (_IntSellOpen != value)
                {
                    _IntSellOpen = value;
                    RaisePropertyChanged("IntSellOpen");
                }
            }
        }
        private int _IntClosingBuy;
        /// <summary>
        /// 买平
        /// </summary>
        public int IntClosingBuy
        {
            get { return _IntClosingBuy; }
            set
            {
                if (_IntClosingBuy != value)
                {
                    _IntClosingBuy = value;
                    RaisePropertyChanged("IntClosingBuy");
                }
            }
        }

        private int _IntClosingSell;
        /// <summary>
        /// 卖平
        /// </summary>
        public int IntClosingSell
        {
            get { return _IntClosingSell; }
            set
            {
                if (_IntClosingSell != value)
                {
                    _IntClosingSell = value;
                    RaisePropertyChanged("IntClosingSell");
                }
            }
        }
        private int _IntRevoke;
        /// <summary>
        /// 撤单
        /// </summary>
        public int IntRevoke
        {
            get { return _IntRevoke; }
            set
            {
                if (_IntRevoke != value)
                {
                    _IntRevoke = value;
                    RaisePropertyChanged("IntRevoke");
                }
            }
        }

        private int _IntClearance;
        /// <summary>
        /// 清仓
        /// </summary>
        public int IntClearance
        {
            get { return _IntClearance; }
            set
            {
                if (_IntClearance != value)
                {
                    _IntClearance = value;
                    RaisePropertyChanged("IntClearance");
                }
            }
        }
    }

    /// <summary>
    /// 消息提示
    /// </summary>
    public class MessageAlertViewModel : ObservableObject
    {

        private string _TradeAlert;

        /// <summary>
        /// 成交提示
        /// </summary>
        public string TradeAlert
        {
            get { return _TradeAlert; }
            set
            {
                if (_TradeAlert != value)
                {
                    _TradeAlert = value;
                    RaisePropertyChanged("TradeAlert");
                }
            }
        }

        private string _OrderAlert;
        /// <summary>
        /// 下单提示
        /// </summary>

        public string OrderAlert
        {
            get { return _OrderAlert; }
            set
            {
                if (_OrderAlert != value)
                {
                    _OrderAlert = value;
                    RaisePropertyChanged("OrderAlert");
                }
            }
        }

        private string _RevokeAlert;
        /// <summary>
        /// 撤单提示
        /// </summary>
        public string RevokeAlert
        {
            get { return _RevokeAlert; }
            set
            {
                if (_RevokeAlert != value)
                {
                    _RevokeAlert = value;
                    RaisePropertyChanged("RevokeAlert");
                }
            }
        }
    }


}
