using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.CommonClass;
using PC_Futures.CommonClass.ParameterSetting;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PC_Futures.ViewModels
{
    public class PositionViewModel : ObservableObject
    {
        private ObservableCollection<PotionDetailModelViewModel> _PMList = new ObservableCollection<PotionDetailModelViewModel>();
        public ObservableCollection<PotionDetailModelViewModel> PMList
        {
            get { return _PMList; }
            set
            {
                if (_PMList != value)
                {
                    _PMList = value;
                    RaisePropertyChanged("PMList");
                }
            }

        }


        private PotionDetailModelViewModel _SelectedItem;
        public PotionDetailModelViewModel SelectedItem
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
        public bool _IsReadOnly = true;
        public bool IsReadOnly
        {
            get { return _IsReadOnly; }
            set
            {
                if (value != _IsReadOnly)
                {
                    _IsReadOnly = value;
                    RaisePropertyChanged("IsReadOnly");
                }
            }
        }


        #region 构造函数
        private static PositionViewModel _PositionViewModel;

        public static PositionViewModel Instance()
        {
            if (_PositionViewModel == null)
            {
                _PositionViewModel = new PositionViewModel();
            }
            return _PositionViewModel;
        }
        private PositionViewModel()
        {
            LoadData();
        }

        #endregion
        #region 自定义方法
        private void LoadData()
        {
        }

        public void JSAbleVolume()
        {
            try
            {
                foreach (PotionDetailModelViewModel item in PMList)
                {
                    List<DelegationModelViewModel> pdmvm1 = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == item.ContractCode && x.Direction != item.Direction && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
                    int cknum0 = 0;
                    if (pdmvm1 != null && pdmvm1.Count > 0)
                    {
                        cknum0 = pdmvm1.Sum(x => x.OrderVolume);
                    }
                    item.BDAbleVolume = item.PositionVolume - cknum0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 提供行情信息 显示要交易的期货行情的信息
        /// </summary>
        /// <param name="futures"></param>
        public void SendMarket(QuotationEntity futures)
        {
        }

        #region 命令

        /// <summary>
        /// 全部平仓
        /// </summary>
        public ICommand TradeAllCommand { get { return new RelayCommand(TradeAllExecuteChanged, TradeAllCanExecuteChanged); } }
        public void TradeAllExecuteChanged()
        {
            if (PMList.Count > 0)
            {
                if (MessageBox.Show("对所有持仓执行平仓操作,是否确认?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;

                bool isUns = false;

                foreach (PotionDetailModelViewModel item in PMList)
                {
                    List<DelegationModelViewModel> ckitem = null;
                    ckitem = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == item.ContractId && x.Direction != item.Direction && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
                    int ckNum = 0;
                    if (ckitem != null && ckitem.Count > 0)
                    {
                        ckNum = ckitem.Sum(x => x.OrderVolume);
                    }
                    if (item.PositionVolume - ckNum <= 0)
                    {
                        isUns = true;
                        continue;
                    }
                    CommParameterSetting.OpenCloseing(item, ckNum);
                    // LogHelper.Info(item.ContractCode + " " + (item.PositionVolume - ckNum) + ":总持仓:" + item.PositionVolume + " 挂单数量:" + ckNum);
                }
                if (isUns)
                {
                    MessageBox.Show("持仓中部分合约可平手数不足!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

        }
        public bool TradeAllCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 快捷平仓
        /// </summary>
        public ICommand QuicklyUnwindCommand { get { return new RelayCommand(QuicklyUnwindExecuteChanged, QuicklyUnwindCanExecuteChanged); } }
        public void QuicklyUnwindExecuteChanged()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("请选择平仓项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            List<DelegationModelViewModel> ckitem = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == SelectedItem.ContractId && x.Direction != SelectedItem.Direction && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
            int ckNum = 0;
            if (ckitem != null && ckitem.Count > 0)
            {
                ckNum = ckitem.Sum(x => x.OrderVolume);
            }
            if (SelectedItem.PositionVolume - ckNum <= 0)
            {
                MessageBox.Show("可平手数不足!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            TransactionViewModel.Instance().SelectFutures(SelectedItem.ContractId, SelectedItem.PositionVolume - ckNum);
            CommParameterSetting.OpenCloseing(SelectedItem, ckNum);
        }
        public bool QuicklyUnwindCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 快捷平仓
        /// </summary>
        public ICommand CloseingCommand { get { return new RelayCommand(CloseingExecuteChanged, CloseingCanExecuteChanged); } }
        public void CloseingExecuteChanged()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("请选择平仓项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
            {
                if (MessageBox.Show("确定平仓合约" + SelectedItem.ContractId + "吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
            }
            List<DelegationModelViewModel> ckitem = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractID == SelectedItem.ContractId && x.Direction != SelectedItem.Direction && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
            int ckNum = 0;
            if (ckitem != null && ckitem.Count > 0)
            {
                ckNum = ckitem.Sum(x => x.OrderVolume);
            }
            if (SelectedItem.PositionVolume - ckNum <= 0)
            {
                MessageBox.Show("可平手数不足!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            CommParameterSetting.OpenCloseing(SelectedItem, ckNum);

        }
        public bool CloseingCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 快捷锁仓
        /// </summary>
        public ICommand LockingCommand { get { return new RelayCommand(LockingExecuteChanged, CloseingCanExecuteChanged); } }
        public void LockingExecuteChanged()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("请选择快捷锁仓", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //if (CommParameterSetting.OrderVersion != null && CommParameterSetting.OrderVersion.BeforeOrderEnter)
            //{
            //    if (MessageBox.Show("确定快捷锁仓合约" + SelectedItem.ContractId + "吗?", "", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel) return;
            //}

            TransactionModel tm = new TransactionModel();
            if (SelectedItem.Direction == "S")
            {
                tm.direction = "B";
            }
            else
            {
                tm.direction = "S";
            }
            tm.contract_id = SelectedItem.ContractId;
            tm.user_id = UserInfoHelper.UserId;
            tm.open_offset = (int)OffsetType.OFFSET_OPEN;
            tm.resource = (int)OperatorTradeType.OPERATOR_TRADE_PC;
            tm.order_orderref = Guid.NewGuid().ToString();
            tm.order_price = 0;
            tm.operator_id = UserInfoHelper.LoginName;
            tm.price_type = "M";//根据选中的来判断；
            tm.order_volume = SelectedItem.PositionVolume;
            ReqTransactionModel rtm = new ReqTransactionModel();
            rtm.cmdcode = RequestCmdCode.PlaceOrderCode;
            rtm.content = tm;
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rtm));

        }
        public bool LockingCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 快捷反手
        /// </summary>
        public ICommand QuickBackhandCommand { get { return new RelayCommand(QuickBackhandExecuteChanged, QuickBackhandCanExecuteChanged); } }
        public void QuickBackhandExecuteChanged()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("请选择快捷反手项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            IsReadOnly = false;
            Action RefreshHandel = Refresh;
            RefreshHandel.BeginInvoke(null, RefreshHandel);
            List<DelegationModelViewModel> ckitem = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == SelectedItem.ContractId && x.Direction != SelectedItem.Direction && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
            int ckNum = 0;
            if (ckitem != null && ckitem.Count > 0)
            {
                ckNum = ckitem.Sum(x => x.OrderVolume);
            }
            if (SelectedItem.PositionVolume - ckNum <= 0)
            {
                MessageBox.Show("可平手数不足!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            CommParameterSetting.OpenCloseing(SelectedItem, ckNum);
            CommParameterSetting.OpenCloseing(SelectedItem, ckNum, false);
        }
        private void Refresh()
        {
            DateTime t1 = DateTime.Now;
            while (true)
            {
                if (t1.AddSeconds(5) < DateTime.Now)
                {
                    IsReadOnly = true;
                    break;
                }
            }
        }
        public bool QuickBackhandCanExecuteChanged()
        {
            return true;
        }

        public ICommand CheckFullStopCommand { get { return new RelayCommand(CheckFullStopExecuteChanged, CheckFullStopCanExecuteChanged); } }
        public void CheckFullStopExecuteChanged()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("请选择持仓项!");
                return;
            }
            List<DelegationModelViewModel> ckitem = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == SelectedItem.ContractId && x.Direction != SelectedItem.Direction && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
            int ckNum = 0;
            if (ckitem != null && ckitem.Count > 0)
            {
                ckNum = ckitem.Sum(x => x.OrderVolume);
            }
            TransactionViewModel.Instance().dic = SelectedItem.Direction;
            TransactionViewModel.Instance().SelectFutures(SelectedItem.ContractId, SelectedItem.PositionVolume - ckNum);
            TransactionViewModel.Instance().IsEnabled = true;
            CheckFullStop cfs = new CheckFullStop(SelectedItem);
            cfs.Owner = ContractVariety.main;
            cfs.ShowDialog();

        }
        public bool CheckFullStopCanExecuteChanged()
        {
            return true;
        }

        public ICommand SelectionChangedCommand { get { return new RelayCommand(SelectionChangedExecuteChanged, SelectionChangedCanExecuteChanged); } }
        public void SelectionChangedExecuteChanged()
        {
            if (SelectedItem == null) return;
            TransactionViewModel.Instance().SelectFutures(SelectedItem.ContractId, SelectedItem.PositionVolume);

        }
        public bool SelectionChangedCanExecuteChanged()
        {
            return true;
        }
        public ICommand GotFocusCommand { get { return new RelayCommand(GotFocusExecuteChanged, GotFocusCanExecuteChanged); } }
        public void GotFocusExecuteChanged()
        {
            if (SelectedItem == null) return;
            List<DelegationModelViewModel> ckitem = OrderCancelViewModel.Instance().KCDelegations.Where(x => x.ContractCode == SelectedItem.ContractId && x.Direction != SelectedItem.Direction && x.OpenOffset == (int)OffsetType.OFFSET_COVER).ToList();
            int ckNum = 0;
            if (ckitem != null && ckitem.Count > 0)
            {
                ckNum = ckitem.Sum(x => x.OrderVolume);
            }
            TransactionViewModel.Instance().dic = SelectedItem.Direction;
            TransactionViewModel.Instance().SelectFutures(SelectedItem.ContractId, SelectedItem.PositionVolume - ckNum);
            TransactionViewModel.Instance().IsEnabled = true;

        }
        public bool GotFocusCanExecuteChanged()
        {
            return true;
        }

        public ICommand ExportCommand { get { return new RelayCommand(ExportExecute, ExportCanExecuteChanged); } }
        public void ExportExecute()
        {
            ExprotHelper.ExportToExcelTemp(PMList, "持仓总汇", 1);
        }
        public bool ExportCanExecuteChanged()
        {
            return true;
        }
        #endregion


        internal void Sorting(string name, bool isDesc)
        {
            List<PotionDetailModelViewModel> temp = null;
            switch (name)
            {
                case "ContractCode":
                    if (isDesc)
                    {
                        temp = PMList.OrderBy(x => x.ContractCode).ToList();
                    }
                    else
                    {
                        temp = PMList.OrderByDescending(x => x.ContractCode).ToList();
                    }

                    break;
                case "Direction":
                    if (isDesc)
                    {
                        temp = PMList.OrderBy(x => x.Direction).ToList();
                    }
                    else
                    {
                        temp = PMList.OrderByDescending(x => x.Direction).ToList();
                    }

                    break;
                case "PositionVolume":
                    if (isDesc)
                    {
                        temp = PMList.OrderBy(x => x.PositionVolume).ToList();
                    }
                    else
                    {
                        temp = PMList.OrderByDescending(x => x.PositionVolume).ToList();
                    }

                    break;
                case "AbleVolume":
                    if (isDesc)
                    {
                        temp = PMList.OrderBy(x => x.AbleVolume).ToList();
                    }
                    else
                    {
                        temp = PMList.OrderByDescending(x => x.AbleVolume).ToList();
                    }

                    break;
                case "OpenPrice":
                    if (isDesc)
                    {
                        temp = PMList.OrderBy(x => x.OpenPrice).ToList();
                    }
                    else
                    {
                        temp = PMList.OrderByDescending(x => x.OpenPrice).ToList();
                    }

                    break;
                case "UseMargin":
                    if (isDesc)
                    {
                        temp = PMList.OrderBy(x => x.UseMargin).ToList();
                    }
                    else
                    {
                        temp = PMList.OrderByDescending(x => x.UseMargin).ToList();
                    }

                    break;
                case "PositionProfitLoss":
                    if (isDesc)
                    {
                        temp = PMList.OrderBy(x => x.PositionProfitLoss).ToList();
                    }
                    else
                    {
                        temp = PMList.OrderByDescending(x => x.PositionProfitLoss).ToList();
                    }

                    break;

            }
            PMList.Clear();
            foreach (var item in temp)
            {
                PMList.Add(item);
            }
        }
    }
}
