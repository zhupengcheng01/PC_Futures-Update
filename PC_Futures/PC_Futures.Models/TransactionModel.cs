using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class TransactionModel
    {
        /// <summary>
        /// 子账户
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// 合约ID
        /// </summary>
        public string contract_id { get; set; }
        /// <summary>
        /// 市价
        /// </summary>
        public string price_type { get; set; }
        /// <summary>
        /// 下单手数
        /// </summary>
        public int order_volume { get; set; }
        public double order_price { get; set; }
        /// <summary>
        /// "B" (buy)表示买， "S" (sell)表示卖
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 下单编号
        /// </summary>
        public string order_orderref { get; set; }
        /// <summary>
        /// 开平方向
        /// </summary>
        public int open_offset { get; set; }
        /// <summary>
        /// TimeType
        /// </summary>
        public int time_condition_type { get; set; }
        public string operator_id { get; set; }

        public int resource { get; set; }

    }
}
