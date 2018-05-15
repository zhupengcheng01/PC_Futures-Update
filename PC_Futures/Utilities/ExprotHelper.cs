
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace Utilities
{
   public class ExprotHelper
    {
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="list">传过来的要导出集合</param>
        /// <param name="ExportName">需要导出的名称</param>
        /// <param name="Type">导出的类型（1持仓、2委托、3成交、4资金、5资金流水）</param>
        /// <returns></returns>
        //public static bool ExportToExcelTemp(object list,string ExportName,int Type)
        //{
        
        //    System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
        //    //frm.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx";
        //    frm.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx";
        //    frm.FileName = ExportName + DateTime.Now.ToString("yyyy_MM_dd") + "_" + DateTime.Now.Hour + ".xls";
        //    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        DataTable dt = null;
        //        if (Type == 1)
        //        {
        //            var PositionList = list as ObservableCollection<PotionDetailModelViewModel>;
        //            dt = ListToDataTable(PositionList);
        //        }
        //        if (Type == 2)
        //        {
        //            var OrderList = list as ObservableCollection<DelegationModelViewModel>;
        //            dt = ListToDataTable(OrderList);
        //        }
        //        if (Type == 3)
        //        {
        //            var TraderList = list as ObservableCollection<TodayTraderModelViewModel>;
        //            dt = ListToDataTable(TraderList);
        //        }
        //        if (Type ==4)
        //        {
        //            var PositionList = list as ObservableCollection<PotionDetailModelViewModel>;
        //            dt = ListToDataTable(PositionList);
        //        }
        //        if (Type == 5)
        //        {
        //            var PositionList = list as ObservableCollection<ConditionBillModelViewModel>;
        //            dt = ListToDataTable(PositionList);
        //        }


        //        if (!string.IsNullOrEmpty(frm.FileName) && null != dt && dt.Rows.Count > 0)
        //        {
        //            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
        //            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("Sheet1");

        //            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
        //            #region 持仓
        //            if (Type == 1)
        //            {
        //                string[] PositionColumnName = new string[] { "合约", "方向", "持仓", "可用", "持仓均价", "占用保证金", "浮动盈亏", "持仓盈亏基币(USD)", "止损/数量", "止盈/数量"};

        //                for (int i = 0; i < PositionColumnName.Count(); i++)
        //                {
        //                    row.CreateCell(i).SetCellValue(PositionColumnName[i]);
        //                }
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);
        //                    for (int j = 0; j < dt.Columns.Count; j++)
        //                    {
        //                        if (j == 0)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ContractCode"]));
        //                        }
        //                        if (j == 1)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["Direction"])=="B"?"买":"卖");
        //                        }

        //                        if (j == 2)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["PositionVolume"]));
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["StockCode"]));
        //                        }
        //                        if (j == 3)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["AbleVolume"]));

        //                        }
        //                        if (j == 4)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OpenPrice"]));

        //                        }
        //                        if (j == 5)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["UseMargin"]));
        //                        }
        //                        if (j ==6)
        //                        {
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["PositionPrice"]));
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[i]["PositionProfitLoss"]), 2)));
        //                        }
        //                        if (j == 7)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["PositionProfitLossJB"]));
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["CanSellVolume"]));
        //                        }
        //                        if (j == 8)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["LossVolume"]));
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["CanSellVolume"]));
        //                        }
        //                        if (j == 9)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ProfitVolume"]));
        //                            // row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["FrozenVolume"]));
        //                        }
                               
        //                    }
        //                }
        //            }
        //            #endregion

        //            #region 委托
        //            if (Type == 2)
        //            {
        //                string[] OrderColumnName = new string[] { "合约", "买卖", "开平", "委托状态","委托价格", "委托手数", "成交手数", "剩余手数", "详细状态", "委托时间", "委托编号", "操作员"};

        //                for (int i = 0; i < OrderColumnName.Count(); i++)
        //                {
        //                    row.CreateCell(i).SetCellValue(OrderColumnName[i]);
        //                }
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);
        //                    for (int j = 0; j < dt.Columns.Count; j++)
        //                    {
        //                        if (j == 0)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ContractCode"]));
        //                        }
        //                        if (j == 1)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["Direction"]) == "B" ? "买" : "卖");
        //                        }

        //                        if (j == 2)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OpenOffset"])=="1"?"开仓":"平仓");
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["StockCode"]));
        //                        }
        //                        if (j == 3)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(new StateALLToValueConverter().ConvertString(Convert.ToInt32(dt.Rows[i]["OrderStatus"])));

        //                        }
        //                        if (j == 4)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OrderPrice"]));
        //                        }
        //                        if (j == 5)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OrderVolume"]));
        //                        }
        //                        if (j == 6)
        //                        {
                                   
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["TradeVolume"]));
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["CanSellVolume"]));
        //                        }
        //                        if (j == 7)
        //                        {
        //                           // row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["orderpice"]));
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["LeftVolume"]));
        //                        }
        //                        if (j == 8)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(new StateToValueConverter().ConvertString(Convert.ToInt32(dt.Rows[i]["OrderStatus"])));
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["PositionCost"]));
        //                        }
        //                        if (j == 9)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OrderTime"]));
        //                        }
        //                        if (j == 10)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ShadowOrderID"]));
        //                        }
        //                        if (j == 11)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OperatorID"]));
        //                        }
                             
        //                    }
        //                }
                        
        //            }
        //            #endregion

        //            #region 成交
        //            if (Type ==3)
        //            {
        //                string[] OrderColumnName = new string[] { "合约", "买卖", "开平", "成交价格", "委托编号", "成交手数", "成交时间", "成交编号" };

        //                for (int i = 0; i < OrderColumnName.Count(); i++)
        //                {
        //                    row.CreateCell(i).SetCellValue(OrderColumnName[i]);
        //                }
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);
        //                    for (int j = 0; j < dt.Columns.Count; j++)
        //                    {
        //                        if (j == 0)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ContractCode"]));
        //                        }
        //                        if (j == 1)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["Direction"]) == "B" ? "买" : "卖");
        //                        }

        //                        if (j == 2)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OpenOffset"]) == "1" ? "开仓" : "平仓");
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["StockCode"]));
        //                        }
        //                        if (j == 3)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["TradePrice"]));

        //                        }
        //                        if (j == 4)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ShadowOrderID"]));
        //                        }
        //                        if (j == 5)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["TradeVolume"]));
        //                        }
        //                        if (j == 6)
        //                        {
                                    
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["TradeTime"]));
        //                        }
        //                        if (j == 7)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ShadowTradeID"]));
        //                            // row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["FrozenVolume"]));
        //                        }
                              
        //                    }
        //                }

        //            }
        //            #endregion

        //            #region 持仓
        //            if (Type == 4)
        //            {
        //                string[] PositionColumnName = new string[] { "合约", "买卖", "开仓价", "手数", "开仓时间", "持仓盈亏", "成交编号"};

        //                for (int i = 0; i < PositionColumnName.Count(); i++)
        //                {
        //                    row.CreateCell(i).SetCellValue(PositionColumnName[i]);
        //                }
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);
        //                    for (int j = 0; j < dt.Columns.Count; j++)
        //                    {
        //                        if (j == 0)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ContractCode"]));
        //                        }
        //                        if (j == 1)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["Direction"]) == "B" ? "买" : "卖");
        //                        }

        //                        if (j == 2)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OpenPrice"]));
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["StockCode"]));
        //                        }
        //                        if (j == 3)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["AbleVolume"]));

        //                        }
        //                        if (j == 4)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OpenDate"]));

        //                        }
        //                        if (j == 5)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[i]["PositionProfitLoss"]), 2)));
        //                        }
        //                        if (j == 6)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ShadowTradeId"]));
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion
        //            #region 条件单
        //            if (Type == 5)
        //            {
        //                string[] PositionColumnName = new string[] { "合约", "买卖", "开平", "状态", "触发条件", "下单价格", "下单手数","下单时间","触发时间" };

        //                for (int i = 0; i < PositionColumnName.Count(); i++)
        //                {
        //                    row.CreateCell(i).SetCellValue(PositionColumnName[i]);
        //                }
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);
        //                    for (int j = 0; j < dt.Columns.Count; j++)
        //                    {
        //                        if (j == 0)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["ContractCode"]));
        //                        }
        //                        if (j == 1)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["Direction"]) == "B" ? "买" : "卖");
        //                        }

        //                        if (j == 2)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OpenOffset"]) == "1" ? "开仓" : "平仓");
        //                            //row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["StockCode"]));
        //                        }
        //                        if (j == 3)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(new BillStateToValueConverter().ConvertString(Convert.ToInt32(dt.Rows[i]["Status"])));

        //                        }
        //                        if (j == 4)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["TrrigerCond"]));

        //                        }
        //                        if (j == 5)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[i]["OrderPrice"]), 2)));
        //                        }
        //                        if (j == 6)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["OrderVolume"]));
        //                        }
        //                        if (j == 7)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["CreateTime"]));
        //                        }
        //                        if (j == 8)
        //                        {
        //                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i]["TrrigerTime"]));
        //                        }
        //                    }
        //                }
        //            }
        //            #endregion
        //            // 写入到客户端  
        //            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //            {
        //                book.Write(ms);
        //                try
        //                {
        //                    using (FileStream fs = new FileStream(frm.FileName, FileMode.Create, FileAccess.Write))
        //                    {
        //                        byte[] data = ms.ToArray();
        //                        fs.Write(data, 0, data.Length);
        //                        fs.Flush();
        //                        MessageBox.Show("导出成功，共" + dt.Rows.Count + "行记录！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                    }
        //                    book = null;
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                }
        //                finally
        //                {
        //                    ms.Close();
        //                    ms.Dispose();
        //                }
        //            }
        //        }
        //    }
        //    return true;

        //}
        #region list转datatable
        public static DataTable ListToDataTable<T>(ObservableCollection<T> c)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (c.Count() > 0)
            {
                for (int i = 0; i < c.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo item in props)
                    {
                        object obj = item.GetValue(c.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    dt.LoadDataRow(tempList.ToArray(), true);
                }
            }
            return dt;
        }
        #endregion
        #region 打开保存excel对话框返回文件名
        public static string SaveExcelFileDialog()
        {
            var sfd = new Microsoft.Win32.SaveFileDialog()
            {
                DefaultExt = "xls",
                Filter = "excel files(*.xls)|*.xls|All files(*.*)|*.*",
                FilterIndex = 1
            };

            if (sfd.ShowDialog() != true)
                return null;
            return sfd.FileName;
        }
        #endregion

        /// <summary>
        /// 获取委托状态
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetOrderStateType(int type)
        {
            string strError = string.Empty;
            switch (type)
            {
                case 0: strError = "指令失败"; break;
                case 1:
                    strError = "提交中";
                    break;
                case 2:
                    strError = "已挂起";
                    break;
                case 3:
                    strError = "已排队";
                    break;
                case 4:
                    strError = "已受理";
                    break;
                case 5:
                    strError = "待撤消";
                    break;
                case 6:
                    strError = "部分撤单";
                    break;
                case 7:
                    strError = "完全撤单";
                    break;
                case 8:
                    strError = "部分成交";
                    break;
                case 9:
                    strError = "完全成交";
                    break;

            }
            return strError;
        }
        /// <summary>
        /// 资金流水变动类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetChangeTypeName(int type)
        {
            string name = string.Empty;
            switch (type)
            {
                case -1:
                    name = "未知";
                    break;
                case 0:
                    name = "加劣后资金";
                    break;
                case 1:
                    name = "加优先资金";
                    break;
                case 2:
                    name = "资金蓝补";
                    break;
                case 3:
                    name = "扣劣后资金";
                    break;
                case 4:
                    name = "扣优先资金";
                    break;
                case 5:
                    name = "资金红冲";
                    break;
                case 6:
                    name = "开仓委托冻结资金,可用资金减少";
                    break;
                case 7:
                    name = "撤单,冻结资金返还";
                    break;
                case 8:
                    name = "开仓成交";
                    break;
                case 9:
                    name = "开仓成交交易佣金手续费";
                    break;
                case 10:
                    name = "开仓成交过户费";
                    break;
                case 11:
                    name = "开仓成交信息撮合费";
                    break;
                case 12:
                    name = "委托冻结资金返还";
                    break;
                case 13:
                    name = "平仓成交资金返还";
                    break;
                case 14:
                    name = "平仓成交交易佣金手续费";
                    break;
                case 15:
                    name = "平仓成交过户费";
                    break;
                case 16:
                    name = "平仓成交信息撮合费";
                    break;
                case 17:
                    name = "平仓成交印花税";
                    break;
                case 18:
                    name = "系统返佣";
                    break;
                case 19:
                    name = "系统资金结算,改为可提取资金";
                    break;
                case 20:
                    name = "系统分成抽取";
                    break;
                case 21:
                    name = "配资入金";
                    break;
                case 22:
                    name = "出金提现";
                    break;
                case 23:
                    name = "融券开仓成交利息支出";
                    break;
                case 24:
                    name = "融券开仓成交利息收入";
                    break;
                case 25:
                    name = "递延费收取利息";
                    break;
                case 26:
                    name = " 开仓成交印花税";
                    break;
            }
            return name;
        }


    }
}
