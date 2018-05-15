using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    /// <summary>
    /// 止盈止损设置
    /// </summary>
    public class StopLossModel
    {
        /// <summary>
        /// 止损连续笔数
        /// </summary>
     //   public int StopLoss { get; set; }

        /// <summary>
        /// 指定价时,触发止损
        /// </summary>
        public string StopLossPrice { get; set; }

        /// <summary>
        /// 止盈连续笔数
        /// </summary>
        //public int StopProfit { get; set; }

        /// <summary>
        /// 指定价时,触发止损
        /// </summary>
      //  public string StopProfitPrice { get; set; }

        /// <summary>
        /// 止损时按照发出委托
        /// </summary>
        public string StopLossDelegation { get; set; }

        /// <summary>
        /// 止盈时按照发出委托
        /// </summary>
        public string StopProfitDelegation { get; set; }
        /// <summary>
        /// 买入调整
        /// </summary>
        public bool CkBuy { get; set; }
        /// <summary>
        /// 买入调整个价位发出委托
        /// </summary>
        public int BuyNum { get; set; }

        /// <summary>
        /// 卖出调整
        /// </summary>
        public bool CkSell { get; set; }
        /// <summary>
        /// 卖出调整个价位发出委托
        /// </summary>
        public int SellNum { get; set; }
        /// <summary>
        /// 如发出委托
        /// </summary>
      //  public bool CkADelegate { get; set; }
        /// <summary>
        /// 如发出委托多少秒后秒后仍未全部成交,则撤单
        /// </summary>
      //  public int ADelegateSecond { get; set; }
        /// <summary>
        /// 如发出委托全部
        /// </summary>
       // public bool CkReADelegate { get; set; }
        /// <summary>
        /// 如发出委托多少秒后秒后仍未全部成交,则撤单，重新委托
        /// </summary>
      //  public int ReADelegateAllSecond { get; set; }
        ///// <summary>
        ///// 止损止盈默认有效期
        ///// </summary>
        //public string DateValidity { get; set; }

    }

    /// <summary>
    /// 自动止盈止损设置
    /// </summary>
    public class AutoStopLossModel
    {
        /// <summary>
        /// 品种
        /// </summary>
        public string Variety { get; set; }
        /// <summary>
        /// 合约号
        /// </summary>
        public string Agreement { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// 止损点位
        /// </summary>
        public int StopLossPotion { get; set; }
        /// <summary>
        /// 止盈点位
        /// </summary>
        public int StopProfitPotion { get; set; }

        /// <summary>
        /// 浮动盈亏
        /// </summary>
        public int FloatingProfitAndLoss { get; set; }

    }

    /// <summary>
    /// 下单版设置
    /// </summary>
    public class OrderVersionModel
    {
        /// <summary>
        /// 下单前确认
        /// </summary>
        public bool BeforeOrderEnter { get; set; }
        /// <summary>
        /// 默认手数
        /// </summary>
        public int DefaultLot { get; set; }

        /// <summary>
        /// 最大手数
        /// </summary>
        public int MaxLot { get; set; }

    }

    /// <summary>
    /// 快捷键设置
    /// </summary>
    public class ShortcutKeyModel
    {
        /// <summary>
        /// 锁定菜单版
        /// </summary>
        public bool LockMenu { get; set; }
        /// <summary>
        /// 买开
        /// </summary>
        public string BuyOpen { get; set; }
        /// <summary>
        /// 卖开
        /// </summary>
        public string SellOpen { get; set; }
        /// <summary>
        /// 买平
        /// </summary>
        public string ClosingBuy { get; set; }

        /// <summary>
        /// 卖平
        /// </summary>
        public string ClosingSell { get; set; }
        /// <summary>
        /// 撤单
        /// </summary>
        public string Revoke { get; set; }
        /// <summary>
        /// 清仓
        /// </summary>
        public string Clearance { get; set; }


        public int IntBuyOpen { get; set; }
        /// <summary>
        /// 卖开
        /// </summary>
        public int IntSellOpen { get; set; }
        /// <summary>
        /// 买平
        /// </summary>
        public int IntClosingBuy { get; set; }

        /// <summary>
        /// 卖平
        /// </summary>
        public int IntClosingSell { get; set; }
        /// <summary>
        /// 撤单
        /// </summary>
        public int IntRevoke { get; set; }
        /// <summary>
        /// 清仓
        /// </summary>
        public int IntClearance { get; set; }
    }

    /// <summary>
    /// 消息提示
    /// </summary>
    public class MessageAlert
    {
        /// <summary>
        /// 成交提示
        /// </summary>
        public string TradeAlert { get; set; }
        /// <summary>
        /// 下单提示
        /// </summary>

        public string OrderAlert { get; set; }
        /// <summary>
        /// 撤单提示
        /// </summary>
        public string RevokeAlert { get; set; }
    }
}
