using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class RestPotionDetailModel
    {
        public string errMsg;

        public int cmdcode { get; set; }

        public PotionDetailModel content { get; set; }
        public int errcode { get; set; }
    }
    public class PotionDetailModel
    {
        public int precision;

        public bool bLast { get; set; }

        public string user_id { get; set; }
        /// <summary>
        /// 合约ID
        /// </summary>
        public string contract_id { get; set; }
        /// <summary>
        /// 合约代码
        /// </summary>
        public string contract_code { get; set; }
        /// <summary>
        /// 持仓编号
        /// </summary>
        public string position_id { get; set; }
        /// <summary>
        /// 持仓影子编号
        /// </summary>
        public string shadow_positionid { get; set; }
        /// <summary>
        /// 持仓手数
        /// </summary>
        public int position_volume { get; set; }
        /// <summary>
        /// B
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 可用手数
        /// </summary>
        public int able_volume { get; set; }
        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public double position_profit_loss { get; set; }
        /// <summary>
        /// 开仓价
        /// </summary>
        public double open_price { get; set; }
        /// <summary>
        /// 开仓时间
        /// </summary>
        public string open_time { get; set; }
        /// <summary>
        /// 开仓日期
        /// </summary>
        public string open_date { get; set; }
        /// <summary>
        /// 成交编号
        /// </summary>
        public string trade_id { get; set; }
        /// <summary>
        /// 成交影子订单
        /// </summary>
        public string shadow_tradeid { get; set; }
        /// <summary>
        /// ordered
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 下单影子id
        /// </summary>
        public string shadow_orderID { get; set; }
        /// <summary>
        /// 占用保证金
        /// </summary>
        public double use_margin { get; set; }

        /// <summary>
        /// 占用保证金
        /// </summary>
        public double sys_margin { get; set; }
        
    }
}
