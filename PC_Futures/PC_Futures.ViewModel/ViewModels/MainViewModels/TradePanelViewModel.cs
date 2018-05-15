using MicroMvvm;
using PC_Futures.ViewModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Utilities;

namespace PC_Futures
{
    public class TradePanelViewModel : ObservableObject
    {
        private MainViewModel _mainVM;
        private static TradePanelViewModel _instance;
        public static TradePanelViewModel GetInstance(MainViewModel mainVM)
        {
            if (_instance == null)
            {
                _instance = new TradePanelViewModel(mainVM);
            }
            return _instance;
        }
        public TradePanelViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;
            TransactionViewModel = TransactionViewModel.Instance();
            TransactionPannel = new TransactionPannelViewModel();
            ConditionBill = UCConditionBillViewModel.Instance();
            PositionAll = PositionAllViewModel.Instance();
            OrderCancel = OrderCancelViewModel.Instance();
            TodayTrader = TodayTraderViewModels.Instance();
            FundsViewModel = FundsViewModel.GetInstance();
        }

        #region 属性
        #region 显示隐藏控件
        private bool _isTradeCheck = true;
        public bool IsTradeCheck
        {
            get
            {
                return _isTradeCheck;
            }
            set
            {
                if (_isTradeCheck != value)
                {
                    _isTradeCheck = value;
                    if (_isTradeCheck)
                    {
                        PanelVisibility = Visibility.Visible;
                        TradeDetailVisibility = Visibility.Visible;
                    }
                    else
                    {
                        TradeDetailVisibility = Visibility.Collapsed;
                    }
                    RaisePropertyChanged(nameof(IsTradeCheck));
                }
            }
        }
        private Visibility _tradeDetailVisibility;
        public Visibility TradeDetailVisibility
        {
            get
            {
                return _tradeDetailVisibility;
            }
            set
            {
                if (_tradeDetailVisibility != value)
                {
                    _tradeDetailVisibility = value;
                    RaisePropertyChanged(nameof(TradeDetailVisibility));
                }
            }
        }
        private bool _isOrderCheck;
        public bool IsOrderCheck
        {
            get
            {
                return _isOrderCheck;
            }
            set
            {
                if (_isOrderCheck != value)
                {
                    _isOrderCheck = value;
                    if (_isOrderCheck)
                    {
                        PanelVisibility = Visibility.Visible;
                        OrderDetailVisibility = Visibility.Visible;
                    }
                    else
                    {
                        OrderDetailVisibility = Visibility.Collapsed;
                    }
                    RaisePropertyChanged(nameof(IsOrderCheck));
                }
            }
        }
        private Visibility _orderDetailVisibility = Visibility.Collapsed;
        public Visibility OrderDetailVisibility
        {
            get
            {
                return _orderDetailVisibility;
            }
            set
            {
                if (_orderDetailVisibility != value)
                {
                    _orderDetailVisibility = value;
                    RaisePropertyChanged(nameof(OrderDetailVisibility));
                }
            }
        }
        private bool _isDealCheck;
        public bool IsDealCheck
        {
            get
            {
                return _isDealCheck;
            }
            set
            {
                if (_isDealCheck != value)
                {
                    _isDealCheck = value;
                    if (_isDealCheck)
                    {
                        PanelVisibility = Visibility.Visible;
                        DealDetailVisibility = Visibility.Visible;
                    }
                    else
                    {
                        DealDetailVisibility = Visibility.Collapsed;
                    }
                    RaisePropertyChanged(nameof(IsDealCheck));
                }
            }
        }
        private Visibility _dealDetailVisibility = Visibility.Collapsed;
        public Visibility DealDetailVisibility
        {
            get
            {
                return _dealDetailVisibility;
            }
            set
            {
                if (_dealDetailVisibility != value)
                {
                    _dealDetailVisibility = value;
                    RaisePropertyChanged(nameof(DealDetailVisibility));
                }
            }
        }
        private bool _isPositionCheck;
        public bool IsPositionCheck
        {
            get
            {
                return _isPositionCheck;
            }
            set
            {
                if (_isPositionCheck != value)
                {
                    _isPositionCheck = value;
                    if (_isPositionCheck)
                    {
                        PanelVisibility = Visibility.Visible;
                        PositionDetailVisibility = Visibility.Visible;
                    }
                    else
                    {
                        PositionDetailVisibility = Visibility.Collapsed;
                    }
                    RaisePropertyChanged(nameof(IsPositionCheck));
                }
            }
        }
        private Visibility _positionDetailVisibility = Visibility.Collapsed;
        public Visibility PositionDetailVisibility
        {
            get
            {
                return _positionDetailVisibility;
            }
            set
            {
                if (_positionDetailVisibility != value)
                {
                    _positionDetailVisibility = value;
                    RaisePropertyChanged(nameof(PositionDetailVisibility));
                }
            }
        }
        private bool _isConditionCheck;
        public bool IsConditionCheck
        {
            get
            {
                return _isConditionCheck;
            }
            set
            {
                if (_isConditionCheck != value)
                {
                    _isConditionCheck = value;
                    if (_isConditionCheck)
                    {
                        PanelVisibility = Visibility.Visible;
                        ConditionDetailVisibility = Visibility.Visible;
                    }
                    else
                    {
                        ConditionDetailVisibility = Visibility.Collapsed;
                    }
                    RaisePropertyChanged(nameof(IsConditionCheck));
                }
            }
        }
        private Visibility _conditionDetailVisibility = Visibility.Collapsed;
        public Visibility ConditionDetailVisibility
        {
            get
            {
                return _conditionDetailVisibility;
            }
            set
            {
                if (_conditionDetailVisibility != value)
                {
                    _conditionDetailVisibility = value;
                    RaisePropertyChanged(nameof(ConditionDetailVisibility));
                }
            }
        }
        private bool _isQueryCheck;
        public bool IsQueryCheck
        {
            get
            {
                return _isQueryCheck;
            }
            set
            {
                if (_isQueryCheck != value)
                {
                    _isQueryCheck = value;
                    if (_isQueryCheck)
                    {
                        PanelVisibility = Visibility.Collapsed;
                        QueryDetailVisibility = Visibility.Visible;
                    }
                    else
                    {
                        QueryDetailVisibility = Visibility.Collapsed;
                    }
                    RaisePropertyChanged(nameof(IsQueryCheck));
                }
            }
        }
        private Visibility _queryDetailVisibility = Visibility.Collapsed;
        public Visibility QueryDetailVisibility
        {
            get
            {
                return _queryDetailVisibility;
            }
            set
            {
                if (_queryDetailVisibility != value)
                {
                    _queryDetailVisibility = value;
                    RaisePropertyChanged(nameof(QueryDetailVisibility));
                }
            }
        }
        private bool _isTransferCheck;
        public bool IsTransferCheck
        {
            get
            {
                return _isTransferCheck;
            }
            set
            {
                if (_isTransferCheck != value)
                {
                    _isTransferCheck = value;
                    if (_isTransferCheck)
                    {
                        PanelVisibility = Visibility.Collapsed;
                        TransferDetailVisibility = Visibility.Visible;
                    }
                    else
                    {
                        TransferDetailVisibility = Visibility.Collapsed;
                    }
                    RaisePropertyChanged(nameof(IsTransferCheck));
                }
            }
        }
        private Visibility _transferDetailVisibility = Visibility.Collapsed;
        public Visibility TransferDetailVisibility
        {
            get
            {
                return _transferDetailVisibility;
            }
            set
            {
                if (_transferDetailVisibility != value)
                {
                    _transferDetailVisibility = value;
                    RaisePropertyChanged(nameof(TransferDetailVisibility));
                }
            }
        }
        private Visibility _panelVisibility;
        public Visibility PanelVisibility
        {
            get
            {
                return _panelVisibility;
            }
            set
            {
                if (_panelVisibility != value)
                {
                    _panelVisibility = value;
                    RaisePropertyChanged(nameof(PanelVisibility));
                }
            }
        }
        private bool _isHideTradeCheck;
        public bool IsHideTradeCheck
        {
            get
            {
                return _isHideTradeCheck;
            }
            set
            {
                if (_isHideTradeCheck != value)
                {
                    _isHideTradeCheck = value;
                    if (_isHideTradeCheck)
                    {
                        HideTradeDetailVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        HideTradeDetailVisibility = Visibility.Visible;
                    }
                    RaisePropertyChanged(nameof(IsHideTradeCheck));
                }
            }
        }
        private Visibility _hideTradeDetailVisibility;
        public Visibility HideTradeDetailVisibility
        {
            get
            {
                return _hideTradeDetailVisibility;
            }
            set
            {
                if (_hideTradeDetailVisibility != value)
                {
                    _hideTradeDetailVisibility = value;
                    RaisePropertyChanged(nameof(HideTradeDetailVisibility));
                }
            }
        }

        private bool _isMinTradeCheck;
        public bool IsMinTradeCheck
        {
            get
            {
                return _isMinTradeCheck;
            }
            set
            {
                if (_isMinTradeCheck != value)
                {
                    _isMinTradeCheck = value;
                    if (_isMinTradeCheck)
                    {
                        MinTradeDetailVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MinTradeDetailVisibility = Visibility.Visible;
                    }
                    RaisePropertyChanged(nameof(IsMinTradeCheck));
                }
            }
        }
        private Visibility _minTradeDetailVisibility;
        public Visibility MinTradeDetailVisibility
        {
            get
            {
                return _minTradeDetailVisibility;
            }
            set
            {
                if (_minTradeDetailVisibility != value)
                {
                    _minTradeDetailVisibility = value;
                    RaisePropertyChanged(nameof(MinTradeDetailVisibility));
                }
            }
        }
        #endregion TodayTraderViewModels
        /// <summary>
        /// 委托
        /// </summary>
        private TodayTraderViewModels _TodayTraderViewModels;
        public TodayTraderViewModels TodayTrader
        {
            get
            {
                return _TodayTraderViewModels;
            }
            set
            {
                if (_TodayTraderViewModels != value)
                {
                    _TodayTraderViewModels = value;
                    RaisePropertyChanged("TodayTrader");
                }
            }
        }
        /// <summary>
        /// 委托
        /// </summary>
        private OrderCancelViewModel _OrderCancel;
        public OrderCancelViewModel OrderCancel
        {
            get
            {
                return _OrderCancel;
            }
            set
            {
                if (_OrderCancel != value)
                {
                    _OrderCancel = value;
                    RaisePropertyChanged("OrderCancel");
                }
            }
        }
        /// <summary>
        /// 条件单
        /// </summary>
        private UCConditionBillViewModel _ConditionBillViewModel;
        public UCConditionBillViewModel ConditionBill
        {
            get
            {
                return _ConditionBillViewModel;
            }
            set
            {
                if (_ConditionBillViewModel != value)
                {
                    _ConditionBillViewModel = value;
                    RaisePropertyChanged("ConditionBill");
                }
            }
        }

        /// <summary>
        /// 持仓
        /// </summary>
        private PositionAllViewModel _PositionAllViewModel;
        public PositionAllViewModel PositionAll
        {
            get
            {
                return _PositionAllViewModel;
            }
            set
            {
                if (_PositionAllViewModel != value)
                {
                    _PositionAllViewModel = value;
                    RaisePropertyChanged("PositionAll");
                }
            }
        }
        /// <summary>
        /// 资金
        /// </summary>
        private FundsViewModel _FundsViewModel;
        public FundsViewModel FundsViewModel
        {
            get
            {
                return _FundsViewModel;
            }
            set
            {
                if (_FundsViewModel != value)
                {
                    _FundsViewModel = value;
                    RaisePropertyChanged("FundsViewModel");
                }
            }
        }

        private TransactionViewModel _transactionViewModel;
        /// <summary>
        /// 下单面板
        /// </summary>
        public TransactionViewModel TransactionViewModel
        {
            get

            {
                if (_transactionViewModel == null)
                {
                    _transactionViewModel = TransactionViewModel.Instance();
                }
                return _transactionViewModel;
            }
            set
            {
                if (_transactionViewModel != value)
                {
                    _transactionViewModel = value;
                    RaisePropertyChanged(nameof(TransactionViewModel));
                }
            }
        }

        private TransactionPannelViewModel _TransactionPannelViewModel;
        /// <summary>
        /// 持仓委托集合
        /// </summary>
        public TransactionPannelViewModel TransactionPannel
        {
            get
            {
                if (_TransactionPannelViewModel == null)
                {
                    _TransactionPannelViewModel = new TransactionPannelViewModel();
                }
                return _TransactionPannelViewModel;
            }
            set
            {
                if (_TransactionPannelViewModel != value)
                {
                    _TransactionPannelViewModel = value;
                    RaisePropertyChanged("TransactionPannel");
                }
            }
        }
        #endregion
        public ICommand LockingCommand { get { return new RelayCommand(LockingExecuteChanged, LockingCanExecuteChanged); } }
        public void LockingExecuteChanged()
        {
            TradeLoginViewModel.GetInstance(null).SetLockInfo();
            _mainVM.TradeLoginVisibility = Visibility.Visible;
            ContractVariety.IsLoginSuccess = false;
           
        }
        public bool LockingCanExecuteChanged()
        {
            return true;
        }
        public ICommand CloseTradeCommand { get { return new RelayCommand(CloseTradeExecuteChanged, CloseTradeCanExecuteChanged); } }
        public void CloseTradeExecuteChanged()
        {
            if (MessageBox.Show("关闭交易系统？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TradeLoginViewModel.GetInstance(null).SetCloseInfo();
                ContractVariety.IsLoginSuccess = false;
                _mainVM.TradeVisibility = Visibility.Collapsed;
                _mainVM.TradeLoginVisibility = Visibility.Visible;
            }

        }
        public bool CloseTradeCanExecuteChanged()
        {
            return true;
        }

        public ICommand ParamSetCommand { get { return new RelayCommand(ParamSetExecuteChanged, ParamSetCanExecuteChanged); } }
        public void ParamSetExecuteChanged()
        {
            //ParameterSetting setWin = new ParameterSetting();
            //setWin.ShowDialog();
        }
        public bool ParamSetCanExecuteChanged()
        {
            return true;
        }
    }
}
