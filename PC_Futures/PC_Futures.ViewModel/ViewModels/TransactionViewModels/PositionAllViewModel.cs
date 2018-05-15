using Futures.Enum;
using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.Models;
using PC_Futures.ViewModel.Comm;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Utilities;

namespace PC_Futures.ViewModel
{
    public class PositionAllViewModel : ObservableObject
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
                    if (_SelectedItem != null)
                        TransactionViewModel.Instance().SelectFutures(_SelectedItem.ContractId, _SelectedItem.AbleVolume);
                }
            }

        }

        private ObservableCollection<PotionDetailModelViewModel> _PMListdet = new ObservableCollection<PotionDetailModelViewModel>();
        public ObservableCollection<PotionDetailModelViewModel> DetPMList
        {
            get { return _PMListdet; }
            set
            {
                if (_PMListdet != value)
                {
                    _PMListdet = value;
                    RaisePropertyChanged("DetPMList");
                }
            }

        }

        private PotionDetailModelViewModel _detSelectedItem;
        public PotionDetailModelViewModel DetSelectedItem
        {
            get { return _detSelectedItem; }
            set
            {
                if (_detSelectedItem != value)
                {
                    _detSelectedItem = value;
                    RaisePropertyChanged("DetSelectedItem");
                }
            }

        }

        #region 构造函数
        private static PositionAllViewModel _PositionViewModel;

        public static PositionAllViewModel Instance()
        {
            if (_PositionViewModel == null)
            {
                _PositionViewModel = new PositionAllViewModel();
            }
            return _PositionViewModel;
        }
        private PositionAllViewModel()
        {
            //LoadData();
        }
        #endregion
        #region 自定义方法
        private void LoadData()
        {
            ReqPotion rpd = new ReqPotion();
            rpd.cmdcode = RequestCmdCode.SelectPotionDetialCode;
            rpd.content = new ReqLoginName() { user_id = UserInfoHelper.UserId };
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rpd));
        }

        /// <summary>
        /// 提供行情信息 显示要交易的期货行情的信息
        /// </summary>
        /// <param name="futures"></param>
        public void SendMarket(QuotationEntity futures)
        {
            if (futures == null) return;
            if (PMList.Count == 0) return;
            List<PotionDetailModelViewModel> items = PMList.Where(x => x.ContractId == futures.cd).ToList();
            VarietyModel vm = null;
            ParitiesModel pm = null;
            lock (DetPMList)
            {
                foreach (PotionDetailModelViewModel itemde in DetPMList)
                {
                    if (itemde.ContractCode == futures.cd)
                    {

                        string[] values = futures.cd.Split(' ');
                        if (values.Length != 3)
                        {
                            return;
                        }
                        string varietie = values[1];
                        if (ContractVariety.Varieties.ContainsKey(varietie))
                        {
                            vm = ContractVariety.Varieties[varietie];
                        }
                        if (vm == null) continue;
                        if (itemde.Direction == "B")
                        {
                            if (futures.lp > 0)
                            {
                                itemde.PositionProfitLoss = (futures.lp - itemde.OpenPrice) * itemde.PositionVolume * vm.multiple;//合约乘数
                            }
                            else if (futures.lp == 0 && futures.pslp > 0)
                            {
                                itemde.PositionProfitLoss = (futures.pslp - itemde.OpenPrice) * itemde.PositionVolume * vm.multiple;//合约乘数
                            }
                            else if (futures.lp == 0 && futures.pslp == 0) continue;

                        }
                        else
                        {

                            if (futures.lp > 0)
                            {
                                itemde.PositionProfitLoss = (itemde.OpenPrice - futures.lp) * itemde.PositionVolume * vm.multiple;//合约乘数
                            }
                            else if (futures.lp == 0 && futures.pslp > 0)
                            {
                                itemde.PositionProfitLoss = (itemde.OpenPrice - futures.pslp) * itemde.PositionVolume * vm.multiple;//合约乘数
                            }
                            else if (futures.lp == 0 && futures.pslp == 0) continue;
                        }
                    }
                }
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        string[] values = futures.cd.Split(' ');
                        if (values.Length != 3)
                        {
                            return;
                        }
                        string varietie = values[1];
                        if (ContractVariety.Varieties.ContainsKey(varietie))
                        {
                            vm = ContractVariety.Varieties[varietie];
                        }
                        if (vm == null) return;

                        //  if (item.Direction == "B")
                        item.PositionProfitLoss = Math.Round(DetPMList.Where(x => x.ContractId == item.ContractId && x.Direction == item.Direction).Sum(x => x.PositionProfitLoss), vm.precision);//合约乘数
                        if (ContractVariety.Parities.ContainsKey(vm.currency_code))
                        {
                            pm = ContractVariety.Parities[vm.currency_code];
                        }
                        if (pm == null) return;
                        // if (item.Direction == "B")
                        item.PositionProfitLossJB = DetPMList.Where(x => x.ContractId == item.ContractId && x.Direction == item.Direction).Sum(x => x.PositionProfitLoss) * pm.exchange_rate;//合约乘数*汇率    
                                                                                                                                                                                             // item.PositionProfitLossJB = (item.OpenPrice - futures.lastPrice) * item.PositionVolume * vm.multiple * pm.exchange_rate;//合约乘数*汇率    
                    }
                    AutoStopLossComm.StartAutoStopLoss(futures);
                }
            }
        }

        #endregion

        public ICommand SelectionChangedCommand { get { return new RelayCommand(SelectionChangedExecuteChanged, SelectionChangedCanExecuteChanged); } }
        public void SelectionChangedExecuteChanged()
        {
            if (SelectedItem == null) return;
            TransactionViewModel.Instance().SelectFutures(SelectedItem.ContractId);

        }
        public bool SelectionChangedCanExecuteChanged()
        {
            return true;
        }


        public ICommand SelectionChangedDeCommand { get { return new RelayCommand(SelectionChangedDeExecuteChanged, SelectionChangedDeCanExecuteChanged); } }
        public void SelectionChangedDeExecuteChanged()
        {
            if (DetSelectedItem == null) return;
            TransactionViewModel.Instance().SelectFutures(DetSelectedItem.ContractId);

        }
        public bool SelectionChangedDeCanExecuteChanged()
        {
            return true;
        }
        #region 命令
        public ICommand ExportCommand { get { return new RelayCommand(ExportExecute, ExportCanExecuteChanged); } }
        public void ExportExecute()
        {
            //ExprotHelper.ExportToExcelTemp(DetPMList, "持仓明细", 4);
        }
        public bool ExportCanExecuteChanged()
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
                MessageBox.Show("请选择平仓项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("可平手数不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TransactionViewModel.Instance().SelectFutures(SelectedItem.ContractId, SelectedItem.PositionVolume - ckNum);
            AutoStopLossComm.OpenCloseing(SelectedItem, ckNum);
        }
        public bool QuicklyUnwindCanExecuteChanged()
        {
            return true;
        }

        /// <summary>
        /// 快捷锁仓
        /// </summary>
        public ICommand LockingCommand { get { return new RelayCommand(LockingExecuteChanged, LockingCanExecuteChanged); } }
        public void LockingExecuteChanged()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("请选择快捷锁仓", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                MessageBox.Show("请选择快捷反手项", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("可平手数不足!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AutoStopLossComm.OpenCloseing(SelectedItem, ckNum);
            AutoStopLossComm.OpenCloseing(SelectedItem, ckNum, false);
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
            //CheckFullStop cfs = new CheckFullStop(SelectedItem);
            //cfs.Owner = ContractVariety.main;
            //cfs.ShowDialog();

        }
        public bool CheckFullStopCanExecuteChanged()
        {
            return true;
        }

        #endregion

        public void Sorting(string name, bool isDesc)
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
                case "PositionProfitLossJB":
                    if (isDesc)
                    {
                        temp = PMList.OrderBy(x => x.PositionProfitLossJB).ToList();
                    }
                    else
                    {
                        temp = PMList.OrderByDescending(x => x.PositionProfitLossJB).ToList();
                    }

                    break;

            }
            PMList.Clear();
            foreach (var item in temp)
            {
                PMList.Add(item);
            }
        }

        public void DetSorting(string name, bool isDesc)
        {
            List<PotionDetailModelViewModel> temp = null;
            switch (name)
            {
                case "ContractCode":
                    if (isDesc)
                    {
                        temp = DetPMList.OrderBy(x => x.ContractCode).ToList();
                    }
                    else
                    {
                        temp = DetPMList.OrderByDescending(x => x.ContractCode).ToList();
                    }

                    break;
                case "Direction":
                    if (isDesc)
                    {
                        temp = DetPMList.OrderBy(x => x.Direction).ToList();
                    }
                    else
                    {
                        temp = DetPMList.OrderByDescending(x => x.Direction).ToList();
                    }

                    break;
                case "PositionVolume":
                    if (isDesc)
                    {
                        temp = DetPMList.OrderBy(x => x.PositionVolume).ToList();
                    }
                    else
                    {
                        temp = DetPMList.OrderByDescending(x => x.PositionVolume).ToList();
                    }

                    break;
                case "AbleVolume":
                    if (isDesc)
                    {
                        temp = DetPMList.OrderBy(x => x.AbleVolume).ToList();
                    }
                    else
                    {
                        temp = DetPMList.OrderByDescending(x => x.AbleVolume).ToList();
                    }

                    break;
                case "OpenPrice":
                    if (isDesc)
                    {
                        temp = DetPMList.OrderBy(x => x.OpenPrice).ToList();
                    }
                    else
                    {
                        temp = DetPMList.OrderByDescending(x => x.OpenPrice).ToList();
                    }

                    break;
                case "ShadowTradeId":
                    if (isDesc)
                    {
                        temp = DetPMList.OrderBy(x => x.ShadowTradeId).ToList();
                    }
                    else
                    {
                        temp = DetPMList.OrderByDescending(x => x.ShadowTradeId).ToList();
                    }

                    break;
                case "PositionProfitLoss":
                    if (isDesc)
                    {
                        temp = DetPMList.OrderBy(x => x.PositionProfitLoss).ToList();
                    }
                    else
                    {
                        temp = DetPMList.OrderByDescending(x => x.PositionProfitLoss).ToList();
                    }

                    break;


            }
            DetPMList.Clear();
            foreach (var item in temp)
            {
                DetPMList.Add(item);
            }
        }
    }
}
