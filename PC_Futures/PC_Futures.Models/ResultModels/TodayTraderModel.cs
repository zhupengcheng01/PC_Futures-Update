using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{

    public class RestTodayTraderModel
    {
        public string errMsg;

        public int cmdcode { get; set; }
        public int errcode { get; set; }
        public TodayTraderModel content { get; set; }
    }
    public class TodayTraderModel
    {
        public int precision { get; set; }

        public bool bLast { get; set; }
        /// <summary>
        /// 合约代码
        /// </summary>
        public string contract_code { get; set; }
        /// <summary>
        /// OrderStyle
        /// </summary>
        public string price_type { get; set; }
        /// <summary>
        /// 成交价格
        /// </summary>
        public double trade_price { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 交易编号
        /// </summary>
        public string trade_id { get; set; }
        /// <summary>
        /// OffsetType 买卖方向
        /// </summary>
        public int open_offset { get; set; }
        /// <summary>
        /// 交易手数
        /// </summary>
        public double trade_volume { get; set; }
        /// <summary>
        /// 成交时间
        /// </summary>
        public string trade_time { get; set; }

        /// <summary>
        /// 下单影子id
        /// </summary>
        public string shadow_orderID { get; set; }

        public string contract_id { get; set; }
        /// <summary>
        /// 影子成交编号
        /// </summary>
        public string shadow_tradeID { get; set; }
        public string trade_date { get; set; }
        public string order_id { get; set; }

    }
}
