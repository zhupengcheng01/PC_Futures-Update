using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.Models
{
    #region 合约
    public class ResultContractModel
    {
        public int retCode { get; set; }
        public string message { get; set; }
        public List<ContractModel> data { get; set; }

    }
    public class ContractModel
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        public string contractCode { get; set; }
        /// <summary>
        /// 合约名称
        /// </summary>
        public string contractName { get; set; }
        /// <summary>
        /// 品种代码
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 品种名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 所属页签
        /// </summary>
        public string exchangeDisplay { get; set; }
        /// <summary>
        /// 交割日
        /// </summary>
        public string deliveryDate { get; set; }
        /// <summary>
        /// 有效标识
        /// </summary>
        public int? status { get; set; }
        /// <summary>
        /// 限价标识
        /// </summary>
        public bool? limitPrice { get; set; }
    }
    #endregion

    #region 品种
    public class NewRestVarietyModel
    {
        public int retCode { get; set; }
        public List<NewVarietyModel> data { get; set; }
        public string message { get; set; }
    }

    public class NewVarietyModel
    {
        /// <summary>
        /// 品种代码
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 品种中文名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 所属交易所代码
        /// </summary>
        public string exchangeCode { get; set; }
        /// <summary>
        /// 所属交易所名称
        /// </summary>
        public string exchangeName { get; set; }
        /// <summary>
        /// 币种代码
        /// </summary>
        public string currencyCode { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string currencyName { get; set; }
        /// <summary>
        /// 合约乘数(倍数)
        /// </summary>
        public int? multiple { get; set; }
        /// <summary>
        /// 最小变得价位(步长)
        /// </summary>
        public double? tickSize { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public string settleTime { get; set; }
        /// <summary>
        /// 有效位数
        /// </summary>
        public int? precy { get; set; }
        /// <summary>
        /// 所属页签
        /// </summary>
        public string exchangeDisplay { get; set; }
        /// <summary>
        /// 有效标识
        /// </summary>
        public int? status { get; set; }
        /// <summary>
        /// 交易时间段
        /// </summary>
        public string timeList { get; set; }
        /// <summary>
        /// 主力合约换月
        /// </summary>
        public string changeMonth { get; set; }
        /// <summary>
        /// 时间段描述
        /// </summary>
        public string timeListRemark { get; set; }
    }
    #endregion

    #region 自选合约
    public class ResultOptionalContractModel
    {
        public int cmdcode { get; set; }
        public int errcode { get; set; }
        public OptionalContractModel content { get; set; }
    }
    public class OptionalContractModel
    {
        public bool bLast { get; set; }
        public string contract_id { get; set; }
        public string serial_number { get; set; }
    }
    #endregion

    #region 逐笔成交历史
    public class ResultEachDealHistoryModel
    {
        public int retCode { get; set; }
        public List<EachDealHistoryModel> data { get; set; }
        public string message { get; set; }
    }

    public class EachDealHistoryModel
    {
        /// <summary>
        /// 时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 最新成交价
        /// </summary>
        public double lastPrice { get; set; }
        /// <summary>
        /// 现手
        /// </summary>
        public int lastSize { get; set; }
        /// <summary>
        /// 增仓
        /// </summary>
        public int repo { get; set; }
        /// <summary>
        /// 变化仓类型（1-空换 2-多换）
        /// </summary>
        public int repoType { get; set; }
        /// <summary>
        /// 变化仓标识
        /// </summary>
        public int repoFlg { get; set; }
       
    }
    #endregion
}
