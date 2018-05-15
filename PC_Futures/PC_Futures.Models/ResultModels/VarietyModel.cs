using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class RestVarietyModel
    {
        public string errMsg;

        public int cmdcode { get; set; }
        public VarietyModel content { get; set; }
        public int errcode { get; set; }
    }

    public class VarietyModel
    {
        public bool bLast { get; set; }
        /// <summary>
        /// 品种ID
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 品种代码
        /// </summary>
        public string product_code { get; set; }
        /// <summary>
        /// 品种中文名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 所属交易所id
        /// </summary>
        public string exchange_id { get; set; }
        /// <summary>
        /// 币种代码，USD
        /// </summary>
        public string currency_code { get; set; }
        /// <summary>
        /// 合约乘数(倍数)
        /// </summary>
        public int multiple { get; set; }
        /// <summary>
        /// 最小变得价位(步长)
        /// </summary>
        public double tick_size { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public int settle_time { get; set; }
        /// <summary>
        /// 有效位数
        /// </summary>
        public int precision { get; set; }

    }
}
