
using Newtonsoft.Json;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Utilities
{
    public class HttpRequestContractHelper
    {
      //  public Dictionary<string, List<FuturesViewModel>> DocList = new Dictionary<string, List<FuturesViewModel>>();
        /// <summary>
        /// 获取合约
        /// </summary>
        /// <returns></returns>
        public void loadBarSeries()
        {
            //try
            //{
            //    string url = ConfigurationManager.AppSettings["FuturesAddress"];
            //    string strJson = getData(url, "utf-8");
            //    if (!string.IsNullOrEmpty(strJson))
            //    {
            //        ResultContractModel resultModel = JsonConvert.DeserializeObject<ResultContractModel>(strJson);
            //        if (resultModel != null && resultModel.data != null && resultModel.data.Count > 0)
            //        {
            //            TradeInfoHelper.ContractModelList = resultModel.data;
            //            TradeInfoHelper.ExchangeDisplayList = resultModel.data.Where(o => !string.IsNullOrEmpty(o.exchangeDisplay)).GroupBy(o => o.exchangeDisplay).Select(o => o.Key).ToList();
            //            TradeInfoHelper.ContractModelGroupList = resultModel.data.Where(o => !string.IsNullOrEmpty(o.exchangeDisplay)).GroupBy(o => o.exchangeDisplay).ToDictionary(o => o.Key, o => o.ToList().ConvertAll(b => new FuturesViewModel(b))); 
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.Info("获取合约发生错误:" + ex.Message);
            //}
        }
        /// <summary>
        /// 获取品种集合
        /// </summary>
        public void GetVarietyList()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["VarietyAddress"];
                string strJson = getData(url, "utf-8");
                if (!string.IsNullOrEmpty(strJson))
                {
                    NewRestVarietyModel resultModel = JsonConvert.DeserializeObject<NewRestVarietyModel>(strJson);
                    if (resultModel != null && resultModel.data != null && resultModel.data.Count > 0)
                    {
                        TradeInfoHelper.VarietyModelList = resultModel.data;
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.Info("获取品种发生错误:" + ex.Message);
            }
        }
        /// <summary>
        /// 获取逐笔成交历史数据
        /// </summary>
        public List<EachDealHistoryModel> GetEachDealHistory(string contractCode, string productCode, int pageSize, string time = "", int type = 0)
        {
            List<EachDealHistoryModel> list = new List<EachDealHistoryModel>();
            try
            {
                string eachDealAddress = ConfigurationManager.AppSettings["EachDealAddress"];
                string url = string.Format("{0}?contractCode={1}&productCode={2}&pageSize={3}&time={4}&type={5}", eachDealAddress, contractCode, productCode, pageSize, time, type);
                string strJson = getData(url, "utf-8");
                if (!string.IsNullOrEmpty(strJson))
                {
                    ResultEachDealHistoryModel resultModel = JsonConvert.DeserializeObject<ResultEachDealHistoryModel>(strJson);
                    if (resultModel != null && resultModel.data != null && resultModel.data.Count > 0)
                    {
                        list = resultModel.data;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Info("获取逐笔成交历史数据发生错误:" + ex.Message);
            }
            return list;
        }
        private string getData(string url, string charset)
        {
            string retString = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=" + charset;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(charset));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception)
            {

                //throw;
            }


            return retString;
        }
    }
}
