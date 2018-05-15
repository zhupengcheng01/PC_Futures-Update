using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class RestTransactionInfoModel
    {
        public string errMsg;

        public int cmdcode { get; set; }

        public TransactionInfoModel content { get; set; }
        public int errcode { get; set; }

    }
    public class TransactionInfoModel
    {

        public string contract_id { get; set; }

        /// <summary>
        /// 合约代码
        /// </summary>
        public string contract_code { get; set; }
        public double trade_price { get; set; }
        /// <summary>
        /// OffsetType
        /// </summary>
        public int open_offset { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 交易编号
        /// </summary>
        public string trade_number { get; set; }
        /// <summary>
        /// OrderStateType
        /// </summary>
        public int order_status { get; set; }
        /// <summary>
        /// ORDER_STATE_PARTFINISHED
        /// </summary>
        public string price_type { get; set; }
        /// <summary>
        /// 交易手数
        /// </summary>
        public int trade_volume { get; set; }
        /// <summary>
        /// 成交时间
        /// </summary>
        public string trade_time { get; set; }
        public string trade_date { get; set; }
        public string shadow_orderID { get; set; }
        /// <summary>
        /// orderID
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// traded
        /// </summary>
        public string trade_id { get; set; }
        /// <summary>
        /// 影子成交编号
        /// </summary>
        public string shadow_tradeID { get; set; }
    }
}
