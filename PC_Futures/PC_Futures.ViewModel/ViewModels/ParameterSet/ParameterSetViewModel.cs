using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Utilities;
using Utility;

namespace PC_Futures.ViewModel
{
    public class ParameterSetViewModel : ObservableObject
    {
        public StopLossModelViewModel _StopLossModelViewModel = new StopLossModelViewModel();
        public StopLossModelViewModel StopLossModelViewModel
        {
            get { return _StopLossModelViewModel; }
            set
            {
                if (_StopLossModelViewModel != value)
                {
                    _StopLossModelViewModel = value;
                    RaisePropertyChanged("StopLossModelViewModel");
                }
            }

        }
       
        public OrderVersionModelViewModel _OrderVersionModelViewModel = new OrderVersionModelViewModel();
        public OrderVersionModelViewModel OrderVersion
        {
            get { return _OrderVersionModelViewModel; }
            set
            {
                if (_OrderVersionModelViewModel != value)
                {
                    _OrderVersionModelViewModel = value;
                    RaisePropertyChanged("OrderVersion");
                }
            }

        }

        public ShortcutKeyModelViewModel _ShortcutKeyModelViewModel = new ShortcutKeyModelViewModel();
        public ShortcutKeyModelViewModel ShortcutKey
        {
            get { return _ShortcutKeyModelViewModel; }
            set
            {
                if (_ShortcutKeyModelViewModel != value)
                {
                    _ShortcutKeyModelViewModel = value;
                    RaisePropertyChanged("ShortcutKey");
                }
            }

        }

        public MessageAlertViewModel _MessageAlertViewModel = new MessageAlertViewModel();
        public MessageAlertViewModel MessageAlert
        {
            get { return _MessageAlertViewModel; }
            set
            {
                if (_MessageAlertViewModel != value)
                {
                    _MessageAlertViewModel = value;
                    RaisePropertyChanged("MessageAlert");
                }
            }

        }
        private ObservableCollection<AutoStopLossModelViewModel> _Aslmvm = new ObservableCollection<AutoStopLossModelViewModel>();
        public ObservableCollection<AutoStopLossModelViewModel> Aslmvm
        {
            get { return _Aslmvm; }
            set
            {
                if (_Aslmvm != value)
                {
                    _Aslmvm = value;
                    RaisePropertyChanged("Aslmvm");
                }
            }

        }
        private AutoStopLossModelViewModel _SelectedItem;
        public AutoStopLossModelViewModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }

        }
      
        public ParameterSetViewModel()
        {
            LoadPara();
        }
        private void LoadPara()
        {            
            StopLossModelViewModel.BuyNum = CommParameterSetting.StopLossModel.BuyNum;
            StopLossModelViewModel.CkBuy = CommParameterSetting.StopLossModel.CkBuy;
            StopLossModelViewModel.CkSell = CommParameterSetting.StopLossModel.CkSell;
            StopLossModelViewModel.SellNum = CommParameterSetting.StopLossModel.SellNum;
            StopLossModelViewModel.StopLossDelegation = CommParameterSetting.StopLossModel.StopLossDelegation;
            StopLossModelViewModel.StopLossPrice = CommParameterSetting.StopLossModel.StopLossPrice;
            // comStopProfit.Text = CommParameterSetting.StopLossModel.StopProfitPrice;
            //加载自动止盈止损 自动盈亏是用；分割行，分割列；
            if (CommParameterSetting.AutoStopLossModel != null && CommParameterSetting.AutoStopLossModel.Count > 0)
            {
                foreach (AutoStopLossModel item in CommParameterSetting.AutoStopLossModel)
                {
                    AutoStopLossModelViewModel asmvm = new AutoStopLossModelViewModel();
                    asmvm.VarietySelectedItem = item.Variety;
                    asmvm.Agreement = item.Agreement;
                    asmvm.Direction = item.Direction=="B"? "买":"卖";
                    asmvm.FloatingProfitAndLoss = item.FloatingProfitAndLoss;
                    asmvm.StopLossPotion = item.StopLossPotion;
                    asmvm.StopProfitPotion = item.StopProfitPotion;
                    asmvm.Variety = TransactionViewModel.Instance().Variety;
                    asmvm.ContractCode = MainViewModel.GetInstance().VarietyList[asmvm.VarietySelectedItem].ToList();
                    Aslmvm.Add(asmvm);
                }
            }
            //下单版设置
            OrderVersion.BeforeOrderEnter = CommParameterSetting.OrderVersion.BeforeOrderEnter;
            OrderVersion.DefaultLot = CommParameterSetting.OrderVersion.DefaultLot;
            OrderVersion.MaxLot = CommParameterSetting.OrderVersion.MaxLot;
         

            //快捷键设置
            ShortcutKey.LockMenu = CommParameterSetting.ShortcutKey.LockMenu;
            ShortcutKey.BuyOpen = CommParameterSetting.ShortcutKey.BuyOpen;
            ShortcutKey.Clearance = CommParameterSetting.ShortcutKey.Clearance;
            ShortcutKey.ClosingBuy = CommParameterSetting.ShortcutKey.ClosingBuy;
            ShortcutKey.ClosingSell = CommParameterSetting.ShortcutKey.ClosingSell;
            ShortcutKey.Revoke = CommParameterSetting.ShortcutKey.Revoke;
            ShortcutKey.SellOpen = CommParameterSetting.ShortcutKey.SellOpen;
            //消息提示
            MessageAlert.OrderAlert = CommParameterSetting.MessageAlert.OrderAlert;
            MessageAlert.RevokeAlert = CommParameterSetting.MessageAlert.RevokeAlert;
            MessageAlert.TradeAlert = CommParameterSetting.MessageAlert.TradeAlert;
        }


        /// <summary>
        /// 增加
        /// </summary>
        public ICommand AddCommand { get { return new RelayCommand(AddExecuteChanged, AddCanExecuteChanged); } }
        public void AddExecuteChanged()
        {
            AutoStopLossModelViewModel aslmv = new AutoStopLossModelViewModel();
            aslmv.Variety = TransactionViewModel.Instance().Variety;
            Aslmvm.Add(aslmv);

        }
        public bool AddCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 删除
        /// </summary>
        public ICommand DeleteCommand { get { return new RelayCommand(DeleteExecuteChanged, DeleteCanExecuteChanged); } }
        public void DeleteExecuteChanged()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("请选择删除的行!", "提示");
                return;

            }
            Aslmvm.Remove(SelectedItem);
        }
        public bool DeleteCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 增加
        /// </summary>
        public ICommand EnterCommand { get { return new RelayCommand(EnterExecuteChanged, EnterCanExecuteChanged); } }
        public void EnterExecuteChanged()
        {
            try
            {
                if (OrderVersion.DefaultLot > OrderVersion.MaxLot && OrderVersion.MaxLot > 0)
                {
                    MessageBox.Show("下单默认数量不能大于最大数量!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommParameterSetting.StopLossModel.BuyNum = StopLossModelViewModel.BuyNum;
                CommParameterSetting.StopLossModel.CkBuy = StopLossModelViewModel.CkBuy;
                CommParameterSetting.StopLossModel.CkSell = StopLossModelViewModel.CkSell;
                CommParameterSetting.StopLossModel.SellNum = StopLossModelViewModel.SellNum;
                CommParameterSetting.StopLossModel.StopLossDelegation = StopLossModelViewModel.StopLossDelegation;
                CommParameterSetting.StopLossModel.StopLossPrice = StopLossModelViewModel.StopLossPrice;
                IniHelper.ProfileWriteValue("CheckFullStop", "BuyNum", CommParameterSetting.StopLossModel.BuyNum.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("CheckFullStop", "CkBuy", CommParameterSetting.StopLossModel.CkBuy.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("CheckFullStop", "CkSell", CommParameterSetting.StopLossModel.CkSell.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("CheckFullStop", "SellNum", CommParameterSetting.StopLossModel.SellNum.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("CheckFullStop", "StopLossDelegation", CommParameterSetting.StopLossModel.StopLossDelegation.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("CheckFullStop", "StopLossPrice", CommParameterSetting.StopLossModel.StopLossPrice.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("CheckFullStop", "StopProfitDelegation", CommParameterSetting.StopLossModel.StopProfitDelegation.ToString(), IniHelper.parameterSetting);


                //加载自动止盈止损 自动盈亏是用；分割行，分割列；

                if (Aslmvm != null)
                {
                    string para = "";
                    lock (CommParameterSetting.AutoStopLossModel)
                    {
                        CommParameterSetting.AutoStopLossModel.Clear();
                    }
                    List<string> AgreementList = new List<string>();
                    foreach (AutoStopLossModelViewModel item in Aslmvm)
                    {

                        if (string.IsNullOrEmpty(item.Agreement)|| string.IsNullOrEmpty(item.VarietySelectedItem) ||string.IsNullOrEmpty(item.Direction)) continue;
                            AutoStopLossModel asmvm = new AutoStopLossModel();                       
                        asmvm.Variety = item.VarietySelectedItem;
                        asmvm.Agreement = item.Agreement;
                        asmvm.Direction = item.Direction == "买" ? "B" : "S";
                        asmvm.FloatingProfitAndLoss = item.FloatingProfitAndLoss;
                        asmvm.StopLossPotion = item.StopLossPotion;
                        asmvm.StopProfitPotion = item.StopProfitPotion;
                        CommParameterSetting.AutoStopLossModel.Add(asmvm);
                        if (!AgreementList.Contains(asmvm.Agreement))
                        {
                            AgreementList.Add(asmvm.Agreement);
                        }
                        para += item.VarietySelectedItem + "," + item.Agreement + "," + asmvm.Direction + "," + item.FloatingProfitAndLoss + "," + item.StopLossPotion + "," + item.StopProfitPotion + ";";

                        //将新增的添加到现有的集合中
                        if (!ContractVariety.ContracPostionID.ContainsKey(asmvm.Agreement))
                        {
                            ContractVariety.ContracPostionID.Add(asmvm.Agreement, new List<string>());
                        }
                    }

                    List<string> tempKeys = ContractVariety.ContracPostionID.Keys.ToList();
                    string Agreements = "";
                    foreach (string item in tempKeys)
                    {
                        if (!AgreementList.Contains(item))
                        {
                            lock (ContractVariety.ContracPostionID)
                            {
                                ContractVariety.ContracPostionID.Remove(item);
                            }
                            Agreements += item + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(Agreements))
                    {
                        int count = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DBPath, CommandType.Text, "delete from AutoStopLoss where ContractID in ('" + Agreements.TrimEnd(',') + "')");
                    }
                    IniHelper.ProfileWriteValue("AutoCheckFullStop", "AutoStopLoss", para.TrimEnd(';'), IniHelper.parameterSetting);

                }
                //}
                //下单版设置
                CommParameterSetting.OrderVersion.BeforeOrderEnter = OrderVersion.BeforeOrderEnter;
                CommParameterSetting.OrderVersion.DefaultLot = OrderVersion.DefaultLot;
                CommParameterSetting.OrderVersion.MaxLot = OrderVersion.MaxLot;
             
                IniHelper.ProfileWriteValue("OrderVersion", "BeforeOrderEnter", CommParameterSetting.OrderVersion.BeforeOrderEnter.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("OrderVersion", "DefaultLot", CommParameterSetting.OrderVersion.DefaultLot.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("OrderVersion", "MaxLot", CommParameterSetting.OrderVersion.MaxLot.ToString(), IniHelper.parameterSetting);
                if (OrderVersion.DefaultLot > 0)
                    TransactionViewModel.Instance().FTNum = OrderVersion.DefaultLot;
                if (OrderVersion.MaxLot > 0)
                    TransactionViewModel.Instance().MaxLot = OrderVersion.MaxLot;
             
                //快捷键设置
                CommParameterSetting.ShortcutKey.LockMenu = ShortcutKey.LockMenu;
                CommParameterSetting.ShortcutKey.BuyOpen = ShortcutKey.BuyOpen;
                CommParameterSetting.ShortcutKey.Clearance = ShortcutKey.Clearance;
                CommParameterSetting.ShortcutKey.ClosingBuy = ShortcutKey.ClosingBuy;
                CommParameterSetting.ShortcutKey.ClosingSell = ShortcutKey.ClosingSell;
                CommParameterSetting.ShortcutKey.Revoke = ShortcutKey.Revoke;
                CommParameterSetting.ShortcutKey.SellOpen = ShortcutKey.SellOpen;
                IniHelper.ProfileWriteValue("ShortcutKey", "LockMenu", CommParameterSetting.ShortcutKey.LockMenu.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "BuyOpen", CommParameterSetting.ShortcutKey.BuyOpen.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "Clearance", CommParameterSetting.ShortcutKey.Clearance.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "ClosingBuy", CommParameterSetting.ShortcutKey.ClosingBuy.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "ClosingSell", CommParameterSetting.ShortcutKey.ClosingSell.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "Revoke", CommParameterSetting.ShortcutKey.Revoke.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "SellOpen", CommParameterSetting.ShortcutKey.SellOpen.ToString(), IniHelper.parameterSetting);
               
                IniHelper.ProfileWriteValue("ShortcutKey", "IntBuyOpen", CommParameterSetting.ShortcutKey.IntBuyOpen.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "IntClearance", CommParameterSetting.ShortcutKey.IntClearance.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "IntClosingBuy", CommParameterSetting.ShortcutKey.IntClosingBuy.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "IntClosingSell", CommParameterSetting.ShortcutKey.IntClosingSell.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "IntRevoke", CommParameterSetting.ShortcutKey.IntRevoke.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("ShortcutKey", "IntSellOpen", CommParameterSetting.ShortcutKey.IntSellOpen.ToString(), IniHelper.parameterSetting);

                //消息提示
                CommParameterSetting.MessageAlert.OrderAlert = MessageAlert.OrderAlert;
                CommParameterSetting.MessageAlert.RevokeAlert = MessageAlert.RevokeAlert;
                CommParameterSetting.MessageAlert.TradeAlert = MessageAlert.TradeAlert;
                IniHelper.ProfileWriteValue("MessageAlert", "OrderAlert", CommParameterSetting.MessageAlert.OrderAlert.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("MessageAlert", "RevokeAlert", CommParameterSetting.MessageAlert.RevokeAlert.ToString(), IniHelper.parameterSetting);
                IniHelper.ProfileWriteValue("MessageAlert", "TradeAlert", CommParameterSetting.MessageAlert.TradeAlert.ToString(), IniHelper.parameterSetting);
                this.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.Message);
            }
           

        }
        public bool EnterCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 删除
        /// </summary>
        public ICommand CancelCommand { get { return new RelayCommand(CancelExecuteChanged, CancelCanExecuteChanged); } }
        public void CancelExecuteChanged()
        {
            Close();
        }
        public bool CancelCanExecuteChanged()
        {
            return true;
        }
        public Action CloseAction
        {
            get; set;
        }
        public void Close()
        {

            CloseAction?.Invoke();
        }

     

    }
}
