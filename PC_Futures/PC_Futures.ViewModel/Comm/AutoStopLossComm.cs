using Futures.Enum;
using PC_Futures.Models;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Utilities;
using Utility;

namespace PC_Futures.ViewModel.Comm
{
  public  class AutoStopLossComm
    {
        public static List<string> PostinIds = new List<string>();
        /// <summary>
        /// 触发自动止盈止损
        /// </summary>
        public static void StartAutoStopLoss(QuotationEntity futures)
        {
            if (ContractVariety.ContracPostionID.Count == 0) { return; }
            if (ContractVariety.ContracPostionID.ContainsKey(futures.cd))
            {
                //如果自动止盈止损包含此合约号
                //循环查询持仓明细，进行比较
                foreach (string value in ContractVariety.ContracPostionID[futures.cd])
                {
                    PotionDetailModelViewModel item = PositionAllViewModel.Instance().DetPMList.FirstOrDefault(x => x.PsitionId == value);
                    if (item != null)
                    {
                        AutoStopLossModel aslm = CommParameterSetting.AutoStopLossModel.FirstOrDefault(x => x.Direction == item.Direction && x.Agreement == item.ContractId);
                        if (aslm == null) break;
                        string[] VarietiesKey = futures.cd.Split(' ');
                        if (VarietiesKey.Length != 3)
                        {
                            return;
                        }
                        string varietie = VarietiesKey[1];
                        VarietyModel vm = null;
                        if (ContractVariety.Varieties.ContainsKey(varietie))
                        {
                            vm = ContractVariety.Varieties[varietie];
                        }
                        if (vm == null) break;

                        if (aslm.StopLossPotion > 0)
                        {
                            //止损价》最新价触发止损
                            if (item.Direction == "B")
                            {
                                if (ContractVariety.PostionPrice[item.PsitionId].LossPrice >= futures.lp)
                                {
                                    //平仓
                                    if (!PostinIds.Contains(item.PsitionId))
                                    {
                                        OpenCloseing(item, 0);
                                        PostinIds.Add(item.PsitionId);
                                        int count = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DBPath, CommandType.Text, "delete from AutoStopLoss where ContractID='" + item.ContractId + "' and PostionID='" + item.PsitionId + "';");

                                        LogHelper.Debug("买:持仓的止损价" + ContractVariety.PostionPrice[item.PsitionId].LossPrice + ": 行情最新价" + futures.lp);
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                if (ContractVariety.PostionPrice[item.PsitionId].LossPrice <= futures.lp)
                                {
                                    //平仓
                                    if (!PostinIds.Contains(item.PsitionId))
                                    {
                                        OpenCloseing(item, 0);
                                        PostinIds.Add(item.PsitionId);
                                        int count = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DBPath, CommandType.Text, "delete from AutoStopLoss where ContractID='" + item.ContractId + "' and PostionID='" + item.PsitionId + "';");

                                        LogHelper.Debug("卖:持仓的止损价" + ContractVariety.PostionPrice[item.PsitionId].LossPrice + ": 行情最新价" + futures.lp);
                                        continue;
                                    }
                                }

                            }
                        }
                        if (aslm.StopProfitPotion > 0)
                        {
                            //止盈价《最新价触发止盈
                            if (item.Direction == "B")
                            {
                                if ((item.OpenPrice + aslm.StopProfitPotion * vm.tick_size) <= futures.lp)
                                {
                                    //平仓
                                    if (!PostinIds.Contains(item.PsitionId))
                                    {
                                        OpenCloseing(item, 0);
                                        PostinIds.Add(item.PsitionId);
                                        int count = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DBPath, CommandType.Text, "delete from AutoStopLoss where ContractID='" + item.ContractId + "' and PostionID='" + item.PsitionId + "';");

                                        LogHelper.Debug("买:持仓的止盈价" + ContractVariety.PostionPrice[item.PsitionId].LossPrice + ": 行情最新价" + futures.lp);
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                if ((item.OpenPrice - aslm.StopProfitPotion * vm.tick_size) >= futures.lp)
                                {
                                    //平仓
                                    if (!PostinIds.Contains(item.PsitionId))
                                    {
                                        OpenCloseing(item, 0);
                                        PostinIds.Add(item.PsitionId);
                                        int count = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DBPath, CommandType.Text, "delete from AutoStopLoss where ContractID='" + item.ContractId + "' and PostionID='" + item.PsitionId + "';");

                                        LogHelper.Debug("卖:持仓的止盈价" + ContractVariety.PostionPrice[item.PsitionId].LossPrice + ": 行情最新价" + futures.lp);
                                        continue;
                                    }
                                }

                            }
                        }
                        if (aslm.FloatingProfitAndLoss > 0 && aslm.StopLossPotion > 0)
                        {
                            if (item.Direction == "B")
                            {
                                double cha = futures.lp - ContractVariety.PostionPrice[item.PsitionId].NewPrice;
                                if (cha > aslm.FloatingProfitAndLoss * vm.tick_size)
                                {
                                    int bs = (int)(cha / aslm.FloatingProfitAndLoss * vm.tick_size);
                                    ContractVariety.PostionPrice[item.PsitionId].NewPrice = ContractVariety.PostionPrice[item.PsitionId].NewPrice + (bs * (aslm.FloatingProfitAndLoss * vm.tick_size));
                                    ContractVariety.PostionPrice[item.PsitionId].LossPrice = ContractVariety.PostionPrice[item.PsitionId].LossPrice + (bs * (aslm.FloatingProfitAndLoss * vm.tick_size));
                                    // 修改数据库中数据
                                    int count = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DBPath, CommandType.Text, "UPDATE AutoStopLoss set LossPrice=" + ContractVariety.PostionPrice[item.PsitionId].LossPrice + ",newprice=" + ContractVariety.PostionPrice[item.PsitionId].NewPrice + " WHERE UserID='" + UserInfoHelper.UserId + "' and PostionID='" + item.PsitionId + "' and ContractID='" + item.ContractId + "';");
                                    LogHelper.Debug("买：更新止损价：" + ContractVariety.PostionPrice[item.PsitionId].LossPrice + ":止损价基数" + ContractVariety.PostionPrice[item.PsitionId].NewPrice);
                                }
                            }
                            else
                            {
                                double cha = ContractVariety.PostionPrice[item.PsitionId].NewPrice - futures.lp;//买就是反过来减
                                if (cha > aslm.FloatingProfitAndLoss * vm.tick_size)
                                {
                                    int bs = (int)(cha / aslm.FloatingProfitAndLoss * vm.tick_size);
                                    ContractVariety.PostionPrice[item.PsitionId].NewPrice = ContractVariety.PostionPrice[item.PsitionId].NewPrice - (bs * (aslm.FloatingProfitAndLoss * vm.tick_size));
                                    ContractVariety.PostionPrice[item.PsitionId].LossPrice = ContractVariety.PostionPrice[item.PsitionId].LossPrice - (bs * (aslm.FloatingProfitAndLoss * vm.tick_size));
                                    // 修改数据库中数据
                                    int count = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DBPath, CommandType.Text, "UPDATE AutoStopLoss set LossPrice=" + ContractVariety.PostionPrice[item.PsitionId].LossPrice + ",newprice=" + ContractVariety.PostionPrice[item.PsitionId].NewPrice + " WHERE UserID='" + UserInfoHelper.UserId + "' and PostionID='" + item.PsitionId + "' and ContractID='" + item.ContractId + "';");
                                    LogHelper.Debug("卖：更新止损价：" + ContractVariety.PostionPrice[item.PsitionId].LossPrice + ":止损价基数" + ContractVariety.PostionPrice[item.PsitionId].NewPrice);

                                }

                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 平仓
        /// </summary>
        public static void OpenCloseing(PotionDetailModelViewModel item, int num, bool isClosing = true)
        {
            TransactionModel tm = new TransactionModel();

            tm.direction = item.Direction == "B" ? "S" : "B";

            tm.user_id = UserInfoHelper.UserId;
            tm.contract_id = item.ContractId;
            if (isClosing)
            {
                tm.open_offset = (int)OffsetType.OFFSET_COVER;
            }
            else
            {
                tm.open_offset = (int)OffsetType.OFFSET_OPEN;
            }
            tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
            tm.order_orderref = Guid.NewGuid().ToString();
            tm.order_price = 0;
            tm.operator_id = UserInfoHelper.LoginName;
            tm.price_type = "M";//根据选中的来判断；
            tm.order_volume = item.PositionVolume - num;
            ReqTransactionModel rtm = new ReqTransactionModel();
            rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
            rtm.content = tm;
            ScoketManager.GetInstance().SendTradeWSInfo(Newtonsoft.Json.JsonConvert.SerializeObject(rtm));
        }

    }
}
