using MicroMvvm;
using Newtonsoft.Json;
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
    public class OrderCancelViewModel : ObservableObject
    {
        private ObservableCollection<DelegationModelViewModel> _Delegations = new ObservableCollection<DelegationModelViewModel>();
        public ObservableCollection<DelegationModelViewModel> Delegations
        {
            get { return _Delegations; }
            set
            {
                if (_Delegations != value)
                {
                    _Delegations = value;
                    RaisePropertyChanged("Delegations");
                }
            }

        }

        private DelegationModelViewModel _SelectedItem;
        public DelegationModelViewModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    SelectedItemTemp = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }

        }

        DelegationModelViewModel SelectedItemTemp = null;
        private ObservableCollection<DelegationModelViewModel> _KCDelegations = new ObservableCollection<DelegationModelViewModel>();
        public ObservableCollection<DelegationModelViewModel> KCDelegations
        {
            get { return _KCDelegations; }
            set
            {
                if (_KCDelegations != value)
                {
                    _KCDelegations = value;
                    RaisePropertyChanged("KCDelegations");
                }
            }

        }

        private DelegationModelViewModel _KCSelectedItem;
        public DelegationModelViewModel KCSelectedItem
        {
            get { return _KCSelectedItem; }
            set
            {
                if (_KCSelectedItem != value)
                {
                    _KCSelectedItem = value;
                    SelectedItemTemp = value;
                    RaisePropertyChanged("KCSelectedItem");
                }
            }

        }

        #region 构造函数
        private static OrderCancelViewModel _OrderCancelViewModel;

        public static OrderCancelViewModel Instance()
        {
            if (_OrderCancelViewModel == null)
            {
                _OrderCancelViewModel = new OrderCancelViewModel();
            }
            return _OrderCancelViewModel;
        }

        private OrderCancelViewModel()
        {
            //当日委托
            //ReqPotion rpd = new ReqPotion();
            //rpd.cmdcode = RequestCmdCode.SelectOrderCancel;
            //rpd.content = new ReqLoginName() { user_id = UserInfoHelper.UserId };

            //ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rpd));

        }
        #endregion


        #region 命令
        /// <summary>
        /// 撤单
        /// </summary>
        public ICommand OrderCancelCommand { get { return new RelayCommand(OrderCancelExecuteChanged, OrderCancelCanExecuteChanged); } }
        public void OrderCancelExecuteChanged()
        {
            if (SelectedItemTemp == null)
            {
                MessageBox.Show("请选择撤单项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ReqCannetOrderModel rcom = new ReqCannetOrderModel();
            rcom.cmdcode = RequestCmdCode.CannelOrderCode;
            rcom.content = new CannetOrderModel() { user_id = UserInfoHelper.UserId, order_id = SelectedItemTemp.OrderId, resource = (int)OperatorTradeType.OPERATOR_TRADE_PC };
            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rcom));


        }
        public bool OrderCancelCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 全部撤单
        /// </summary>
        public ICommand OrderCancelAllCommand { get { return new RelayCommand(OrderCancelAllExecuteChanged, OrderCancelAllCanExecuteChanged); } }
        public void OrderCancelAllExecuteChanged()
        {
            if (KCDelegations != null && KCDelegations.Count > 0)
            {
                foreach (DelegationModelViewModel item in KCDelegations)
                {
                    ReqCannetOrderModel rcom = new ReqCannetOrderModel();
                    rcom.cmdcode = RequestCmdCode.CannelOrderCode;
                    rcom.content = new CannetOrderModel() { user_id = UserInfoHelper.UserId, order_id = item.OrderId, resource = (int)OperatorTradeType.OPERATOR_TRADE_PC };
                    ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rcom));
                }
            }

        }
        public bool OrderCancelAllCanExecuteChanged()
        {
            return true;
        }

        public ICommand SelectionChangedCommand { get { return new RelayCommand(SelectionChangedExecuteChanged, SelectionChangedCanExecuteChanged); } }
        public void SelectionChangedExecuteChanged()
        {
            if (SelectedItem == null) return;
            SelectedItem.ContractID = SelectedItem.ContractCode;
            TransactionViewModel.Instance().SelectFutures(SelectedItem.ContractID);

        }
        public bool SelectionChangedCanExecuteChanged()
        {
            return true;
        }
        public ICommand SelectionChangedKCCommand { get { return new RelayCommand(SelectionChangedKCExecuteChanged, SelectionChangedKCCanExecuteChanged); } }
        public void SelectionChangedKCExecuteChanged()
        {
            if (KCSelectedItem == null) return;
            KCSelectedItem.ContractID = KCSelectedItem.ContractCode;
            TransactionViewModel.Instance().SelectFutures(KCSelectedItem.ContractID);

        }
        public bool SelectionChangedKCCanExecuteChanged()
        {
            return true;
        }

        public ICommand ExportCommand { get { return new RelayCommand(ExportExecute, ExportCanExecuteChanged); } }
        public void ExportExecute()
        {
            ExprotHelper.ExportToExcelTemp(Delegations, "委托单", 2);
        }
        public bool ExportCanExecuteChanged()
        {
            return true;
        }
        #endregion

        internal void Sorting(string name, bool isDesc)
        {
            List<DelegationModelViewModel> temp = null;
            switch (name)
            {
                case "ContractCode":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.ContractCode).ToList(); ;
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.ContractCode).ToList();
                    }

                    break;
                case "Direction":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.Direction).ToList();
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.Direction).ToList();
                    }

                    break;
                case "OpenOffset":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.OpenOffset).ToList();
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.OpenOffset).ToList();
                    }

                    break;
                case "OrderStatus":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.OrderStatus).ToList();
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.OrderStatus).ToList();
                    }

                    break;
                case "OrderPrice":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.OrderPrice).ToList();
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.OrderPrice).ToList();
                    }

                    break;
                case "OrderVolume":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.OrderVolume).ToList();
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.OrderVolume).ToList();
                    }

                    break;
                case "TradeVolume":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.TradeVolume).ToList();
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.TradeVolume).ToList();
                    }

                    break;
                case "LeftVolume":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.LeftVolume).ToList();
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.LeftVolume).ToList();
                    }

                    break;

                case "OrderTime":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.OrderTime).ToList();
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.OrderTime).ToList();
                    }

                    break;

                case "ShadowOrderID":
                    if (isDesc)
                    {
                        temp = Delegations.OrderBy(x => x.ShadowOrderID).ToList();
                    }
                    else
                    {
                        temp = Delegations.OrderByDescending(x => x.ShadowOrderID).ToList();
                    }

                    break;
            }

            Delegations.Clear();
            foreach (var item in temp)
            {
                Delegations.Add(item);
            }
        }
    }
}
