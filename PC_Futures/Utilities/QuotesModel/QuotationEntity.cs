using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Futures
{
    public class ResultQuotationEntity
    {
        public string type { get; set; }
        public string dataType { get; set; }
        public QuotationEntity data { get; set; }
    }
    public class QuotationEntity
    {
        /// <summary>
        /// 卖一价
        /// </summary>
        public double ap1 { get; set; }
        /// <summary>
        /// 卖二价
        /// </summary>
        public double ap2 { get; set; }
        /// <summary>
        /// 卖三价
        /// </summary>
        public double ap3 { get; set; }
        /// <summary>
        /// 卖四价
        /// </summary>
        public double ap4 { get; set; }
        /// <summary>
        /// 卖五价
        /// </summary>
        public double ap5 { get; set; }
        /// <summary>
        /// 卖一量
        /// </summary>
        public int as1 { get; set; }
        /// <summary>
        /// 卖二量
        /// </summary>
        public int as2 { get; set; }
        /// <summary>
        /// 卖三量
        /// </summary>
        public int as3 { get; set; }
        /// <summary>
        /// 卖四量
        /// </summary>
        public int as4 { get; set; }
        /// <summary>
        /// 卖五量
        /// </summary>
        public int as5 { get; set; }
        /// <summary>
        /// 买一价
        /// </summary>
        public double bp1 { get; set; }
        /// <summary>
        /// 买二价
        /// </summary>
        public double bp2 { get; set; }
        /// <summary>
        /// 买三价
        /// </summary>
        public double bp3 { get; set; }
        /// <summary>
        /// 买四价
        /// </summary>
        public double bp4 { get; set; }
        /// <summary>
        /// 买五价
        /// </summary>
        public double bp5 { get; set; }
        /// <summary>
        /// 买一量
        /// </summary>
        public int bs1 { get; set; }
        /// <summary>
        /// 买二量
        /// </summary>
        public int bs2 { get; set; }
        /// <summary>
        /// 买三量
        /// </summary>
        public int bs3 { get; set; }
        /// <summary>
        /// 买四量
        /// </summary>
        public int bs4 { get; set; }
        /// <summary>
        /// 买五量
        /// </summary>
        public int bs5 { get; set; }
        /// <summary>
        /// 今日收盘价
        /// </summary>
        public double clp { get; set; }
        /// <summary>
        /// 昨日收盘价
        /// </summary>
        public double pclp { get; set; }
        /// <summary>
        /// 昨日结算价
        /// </summary>
        public double pslp { get; set; }

        /// <summary>
        /// 最高价
        /// </summary>
        public double hp { get; set; }
        /// <summary>
        /// 最新价
        /// </summary>
        public double lp { get; set; }
        /// <summary>
        /// 涨停价
        /// </summary>
        public double lup { get; set; }
        /// <summary>
        /// 跌停价
        /// </summary>
        public double ldp { get; set; }
        /// <summary>
        /// 最新价成交量
        /// </summary>
        public int ls { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public double lop { get; set; }
        /// <summary>
        /// 开盘价
        /// </summary>
        public double op { get; set; }
        /// <summary>
        /// 代号ID
        /// </summary>
        public int ti { get; set; }
        /// <summary>
        /// 推送时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 总成交量
        /// </summary>
        public int ts { get; set; }
        /// <summary>
        /// 持仓量
        /// </summary>
        public int tr { get; set; }
        /// <summary>
        /// 合约标识
        /// </summary>
        public string cd { get; set; }
        /// <summary>
        /// 页签标识
        /// </summary>
        public string ed { get; set; }
        /// <summary>
        /// 品种代码
        /// </summary>
        public string pd { get; set; }
        /// <summary>
        /// 品种名称
        /// </summary>
        public string pn { get; set; }
        /// <summary>
        /// 有效位数
        /// </summary>
        public int? py { get; set; }
        public EachDealDataModel mk { get; set; }
    }
    public class EachDealDataModel
    {
        /// <summary>
        /// 增仓
        /// </summary>
        public int rp { get; set; }
        /// <summary>
        /// 变化仓类型
        /// </summary>
        public int rt { get; set; }
        /// <summary>
        /// 变化仓标识
        /// </summary>
        public int rf { get; set; }
    }
}
