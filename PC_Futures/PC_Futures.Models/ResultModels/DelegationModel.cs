using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class RestDelegationModel
    {
        public int cmdcode { get; set; }

        public DelegationModel content { get; set; }

        public int errcode { get; set; }
        public string errMsg { get; set; }
    }
    public class DelegationModel
    {
        public int precision { get; set; }

        public bool bLast { get; set; }

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string contract_id { get; set; }

        public string user_id { get; set; }
        /// <summary>
        /// 合约代码
        /// </summary>
        public string contract_code { get; set; }
        /// <summary>
        /// OrderStyle
        /// </summary>
        public string price_type { get; set; }
        /// <summary>
        /// 下单手数
        /// </summary>
        public int order_volume { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public double order_price { get; set; }
        /// <summary>
        /// 方向
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 下单编号
        /// </summary>
        public string order_orderref { get; set; }
        /// <summary>
        /// OffsetType
        /// </summary>
        public int open_offset { get; set; }
        /// <summary>
        /// OrderStateType
        /// </summary>
        public int order_status { get; set; }
        /// <summary>
        /// 详细状态
        /// </summary>
        public string fail_msg { get; set; }
        /// <summary>
        /// 交易手数
        /// </summary>
        public int trade_volume { get; set; }
        /// <summary>
        /// 剩余手数
        /// </summary>
        public int left_volume { get; set; }
        /// <summary>
        /// 成交均价
        /// </summary>
        public double trade_price { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string order_time { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string order_date { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string update_time { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public string update_date { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string operator_id { get; set; }
        /// <summary>
        /// 下单影子id
        /// </summary>
        public string shadow_orderID { get; set; }

        public string order_id { get; set; }

    }
}
