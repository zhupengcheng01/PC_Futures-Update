using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class ResultTransactionModelComm
    {
        public int cmdcode { get; set; }

        public ResultTransactionModel content { get; set; }
        public int errcode { get; set; }
    }

    public class ResultTransactionModel
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        public string contract_code { get; set; }
        /// <summary>
        /// 价格类型
        /// </summary>
        public int price_type { get; set; }
        /// <summary>
        /// 下单手数
        /// </summary>
        public int order_volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double order_price { get; set; }
        /// <summary>
        /// 下单方向
        /// </summary>
        public double direction { get; set; }
        /// <summary>
        /// 下单编号
        /// </summary>
        public string order_orderref { get; set; }
        /// <summary>
        /// 开平方向
        /// </summary>

        public int open_offset { get; set; }
        /// <summary>
        /// 下单状态
        /// </summary>
        public int order_status { get; set; }
        /// <summary>
        /// 交易手数
        /// </summary>
        public int trade_volume { get; set; }
        /// <summary>
        /// 剩余手数
        /// </summary>
        public int left_volume { get; set; }
        /// <summary>
        /// 详细状态
        /// </summary>
        public string detail_status { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string order_time { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string operator_name { get; set; }
    }
}
