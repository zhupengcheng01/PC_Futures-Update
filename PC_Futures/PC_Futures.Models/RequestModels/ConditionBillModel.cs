using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class ReqConditionBillModel
    {
        public int cmdcode { get; set; }

        public RConditionBillModel content { get; set; }

    }
    /// <summary>
    /// 接受的Model
    /// </summary>
    public class ConditionBillModel
    {
        public bool blast { get; set; }

        /// <summary>
        /// 条件单的唯一ID
        /// </summary>
        public string contract_id { get; set; }
        /// <summary>
        /// 条件单ID
        /// </summary>
        public string condition_orderID { get; set; }
        /// <summary>
        /// 合约代码
        /// </summary>
        public string contract_code { get; set; }
        /// <summary>
        /// YunTriggerType
        /// </summary>
        public int trriger_price_type { get; set; }
        /// <summary>
        /// 触发价格
        /// </summary>
        public double trriger_price { get; set; }
        /// <summary>
        ///       "":YunTriggerType”,
        /// </summary>
        public int trriger_condition { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string create_date { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 触发时间
        /// </summary>
        public string trriger_time { get; set; }
        /// <summary>
        /// 触发时间
        /// </summary>
        public string trriger_date { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// OffsetType
        /// </summary>
        public int open_offset { get; set; }
        /// <summary>
        /// 下单手数
        /// </summary>
        public int order_volume { get; set; }
        /// <summary>
        /// 下单价格
        /// </summary>
        public double order_price { get; set; }
        /// <summary>
        /// OrderStyle
        /// </summary>
        public string price_type { get; set; }
        /// <summary>
        /// YunConditionType
        /// </summary>
        public int condition_type { get; set; }

        public string user_id { get; set; }
        /// <summary>
        ///TimeType
        /// </summary>
        public int time_condition_type { get; set; }

        public int status { get; set; }

        /// <summary>
        /// 触发条件时间
        /// </summary>
        ///
          public string trriger_contime { get; set; }
        /// <summary>
        /// 触发条件日期
        /// </summary>
        ///
        public string trriger_condate { get; set; }
    }

    /// <summary>
    ///增加修改的Model
    /// </summary>
    public class RConditionBillModel
    {
        /// <summary>
        /// 条件单的唯一ID
        /// </summary>
        public string contract_id { get; set; }
        /// <summary>
        /// 条件单ID
        /// </summary>
        public string condition_orderID { get; set; }

        public string user_id { get; set; }

        /// <summary>
        /// YunTriggerType
        /// </summary>
        public int trriger_price_type { get; set; }
        /// <summary>
        /// 触发价格
        /// </summary>
        public double trriger_price { get; set; }

        /// <summary>
        ///  YunTriggerType
        /// </summary>
        public int trriger_condition { get; set; }
        /// <summary>
        /// 触发条件时间
        /// </summary>
        public string trriger_contime { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// OffsetType
        /// </summary>
        public int open_offset { get; set; }
        /// <summary>
        /// 下单手数
        /// </summary>
        public int order_volume { get; set; }
        /// <summary>
        /// 下单价格
        /// </summary>
        public double order_price { get; set; }
        /// <summary>
        /// OrderStyle
        /// </summary>
        public string price_type { get; set; }
        /// <summary>
        /// YunConditionType
        /// </summary>
        public int condition_type { get; set; }

        /// <summary>
        ///TimeType
        /// </summary>
        public int time_condition_type { get; set; }

        public int resource { get; set; }
        /// <summary>
        /// 触发条件日期
        /// </summary>
        ///
        public string trriger_condate { get; set; }
    }
}
