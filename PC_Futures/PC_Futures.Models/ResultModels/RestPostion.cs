using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class RestPostion
    {
        public string errMsg;

        public int cmdcode { get; set; }
        public int errcode { get; set; }
        public PotionModel content { get; set; }
    }
    public class PotionModel
    {
       
        /// <summary>
        /// 合约代码
        /// </summary>
        public string contract_code { get; set; }
        /// <summary>
        /// 持仓编号
        /// </summary>
        public string position_id { get; set; }
        /// <summary>
        /// 持仓手数
        /// </summary>
        public int position_volume { get; set; }
        /// <summary>
        /// 占用保证金
        /// </summary>
        public double use_margin { get; set; }
        /// <summary>
        /// 占用保证金
        /// </summary>
        public double sys_margin { get; set; }

        /// <summary>
        /// B
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 持仓均价
        /// </summary>
        public double position_price { get; set; }
    
        /// <summary>
        /// 浮动盈亏
        /// </summary>
        public double float_profit_loss { get; set; }
        /// <summary>
        /// 止盈数目
        /// </summary>
        public int profit_volume { get; set; }
        /// <summary>
        /// 止损数目
        /// </summary>
        public int loss_volume { get; set; }
        /// <summary>
        /// 下单影子id
        /// </summary>
        public string shadow_orderID { get; set; }
        public string shadow_positionid { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        public  string user_id { get; set; }

        public string contract_id { get; set; }
        public int isyestoday { get; set; }
        public string position_time { get; set; }
        public string position_date { get; set; }
        /// <summary>
        /// 成交影子订单
        /// </summary>
        public string shadow_tradeid { get; set; }
    }
}
