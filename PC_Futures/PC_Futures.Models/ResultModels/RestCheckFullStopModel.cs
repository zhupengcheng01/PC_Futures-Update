using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    public class RestCheckFullStopModel
    {
        public string errMsg;

        public int cmdcode { get; set; }

        public CheckFullStopModel content { get; set; }
        public int errcode { get; set; }
    }

    public class ListCheckFullStopModel
    {
        public string errMsg;

        public int cmdcode { get; set; }

        public List<CheckFullStopModel> content { get; set; }
        public int errcode { get; set; }
    }
    public class CheckFullStopModel
    {
        public bool blast { get; set; }
        public string user_id { get; set; }

        /// <summary>
        /// 条件单ID
        /// </summary>
        public string stoploss_id { get; set; }
        /// <summary>
        /// 合约代码
        /// </summary>
        public string contract_code { get; set; }
        /// <summary>
        /// contractid
        /// </summary>
        public string contract_id { get; set; }
        /// <summary>
        /// YunTrrigerStyle
        /// </summary>
        public int trriger_price_type { get; set; }

        /// <summary>
        /// 触发价格
        /// </summary>
        public double trriger_price { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 触发条件
        /// </summary>
        public string trriger_condition { get; set; }

        /// <summary>
        /// 下单价格
        /// </summary>
        public double order_price { get; set; }
        /// <summary>
        /// OrderStyle
        /// </summary>
        public string price_type { get; set; }
        public double order_add_price { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string create_date { get; set; }
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
        /// 开平
        /// </summary>
        public int open_offset { get; set; }
        /// <summary>
        /// 下单手数
        /// </summary>
        public int order_volume { get; set; }
        /// <summary>
        /// 止损价
        /// </summary>
        public double stoploss_price { get; set; }
        /// <summary>
        /// 止盈价
        /// </summary>
        public double stopprofit_price { get; set; }
        /// <summary>
        /// 浮动止损
        /// </summary>
        public double float_loss { get; set; }
        /// <summary>
        /// 浮动止损标志
        /// </summary>
        public int float_loss_flag { get; set; }

        public int resource { get; set; }

 
    }
}
