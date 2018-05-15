using MicroMvvm;
using PC_Futures.Models;
using PC_Futures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PC_Futures
{
    public class TradeQuotesViewModel : ObservableObject
    {
        private MainViewModel _mainVM;
        //private FuturesViewModel _aa;
        private static TradeQuotesViewModel _instance;
        public int PageIndex = 0;
        private int selectMinute = 0;
        private bool IsBack = false;
        private List<int> stepList = new List<int>();
        public event EventHandler SetDataGridStyleHandler;
        private EnumBarType emumTemp;
        public static TradeQuotesViewModel GetInstance(MainViewModel mainVM)
        {
            if (_instance == null)
            {
                _instance = new TradeQuotesViewModel(mainVM);
            }
            return _instance;
        }
        
        public TradeQuotesViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;
            stepList.Add(1);
        }
        #region K线和grid显示隐藏
        private bool _isOptionalCheck;
        public bool IsOptionalCheck
        {
            get
            {
                return _isOptionalCheck;
            }
            set
            {
                _isOptionalCheck = value;
                if (_isOptionalCheck)
                {
                    OptionalVisibility = Visibility.Visible;
                    IsInternationalCheck = false;
                    IsQuoteCheck = true;
                    ReSetOptionalSelectItem();
                    stepList.Clear();
                    stepList.Add(1);
                }
                else
                {
                    OptionalVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(nameof(IsOptionalCheck));
            }
        }
        private Visibility _optionalVisibility = Visibility.Collapsed;
        public Visibility OptionalVisibility
        {
            get
            {
                return _optionalVisibility;
            }
            set
            {
                if (_optionalVisibility != value)
                {
                    _optionalVisibility = value;
                    RaisePropertyChanged(nameof(OptionalVisibility));
                }
            }
        }
        public int InternationalCheckType { get; set; }
        private bool _isInternationalCheck = true;
        public bool IsInternationalCheck
        {
            get
            {
                return _isInternationalCheck;
            }
            set
            {
                _isInternationalCheck = value;
                if (_isInternationalCheck)
                {
                    InternationalVisibility = Visibility.Visible;
                    IsQuoteCheck = true;
                    if(InternationalCheckType==0)
                    {
                        QuotesTabControlViewModel.GetInstance(null).SelectedIndex = 0;
                        QuotesTabControlViewModel.GetInstance(null).SetSelectItem(0);
                    }
                    else
                    {
                        InternationalCheckType = 0;
                    }
                    stepList.Clear();
                    stepList.Add(1);
                }
                else
                {
                    InternationalVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(nameof(IsInternationalCheck));
            }
        }
        private Visibility _internationalVisibility;
        public Visibility InternationalVisibility
        {
            get
            {
                return _internationalVisibility;
            }
            set
            {
                if (_internationalVisibility != value)
                {
                    _internationalVisibility = value;
                    RaisePropertyChanged(nameof(InternationalVisibility));
                }
            }
        }

        private bool _isQuoteCheck = true;
        public bool IsQuoteCheck
        {
            get
            {
                return _isQuoteCheck;
            }
            set
            {
                _isQuoteCheck = value;
                if (_isQuoteCheck)
                {
                    if (IsInternationalCheck)
                    {
                        if (stepList.Contains(1))
                        {
                            stepList.Remove(1);
                        }
                        stepList.Add(1);
                        InternationalVisibility = Visibility.Visible;
                    }
                    else
                    {
                        OptionalVisibility = Visibility.Visible;
                    }
                    KLineVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(nameof(IsQuoteCheck));
            }
        }
 
            private bool _isRealTimeCheck;
        public bool IsRealTimeCheck
        {
            get
            {
                return _isRealTimeCheck;
            }
            set
            {
                _isRealTimeCheck = value;
                if (_isRealTimeCheck)
                {
                    if (stepList.Contains(2))
                    {
                        stepList.Remove(2);
                    }
                    stepList.Add(2);
                    RealTimeVisibility = Visibility.Visible;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    // RealTimeChart = new RealTimeChartDataModel(_mainVM.SelectItemViewModel);
                    Action RefreshHandel = FsChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);
                    KLineVisibility = Visibility.Collapsed;
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
                else
                {
                    RealTimeVisibility = Visibility.Collapsed;
                    PageIndex = 0;
                    RealTimeChart.StopUpdate();
                }
                RaisePropertyChanged(nameof(IsRealTimeCheck));
            }
        }
        private bool _isContractDetailCheck;
        public bool IsContractDetailCheck
        {
            get
            {
                return _isContractDetailCheck;
            }
            set
            {
                _isContractDetailCheck = value;
                if (_isContractDetailCheck)
                {
                    if (stepList.Contains(3))
                    {
                        stepList.Remove(3);
                    }
                    stepList.Add(3);
                    ContractDetailVisibility = Visibility.Visible;
                    ContractDetailInfoViewModel = new ContractDetailInfoViewModel(_mainVM.SelectItemViewModel);
                    KLineVisibility = Visibility.Collapsed;
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
                else
                {
                    ContractDetailVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(nameof(IsContractDetailCheck));
            }
        }
        #region 异步打开图表
        private void FsChartAction()
        {
            RealTimeChart = new RealTimeChartDataModel(_mainVM.SelectItemViewModel);
        }

        private void KChartAction()
        {
            switch (emumTemp)
            {
                case EnumBarType.BAR_TYPE_DAY:
                 CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_DAY, 0);
                    break;
                case EnumBarType.BAR_TYPE_WEEK:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_WEEK, 0);
                    break;
                case EnumBarType.BAR_TYPE_MONTH:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MONTH, 0);
                    break;
                case EnumBarType.BAR_TYPE_MIN1:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN1, 1);
                    break;
                case EnumBarType.BAR_TYPE_MIN3:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN3, 3);
                    break;
                case EnumBarType.BAR_TYPE_MIN5:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN5, 5);
                    break;
                case EnumBarType.BAR_TYPE_MIN15:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN15, 15);
                    break;
                case EnumBarType.BAR_TYPE_MIN30:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN30,30);
                    break;
                case EnumBarType.BAR_TYPE_MIN60:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN60, 60);
                    break;
                case EnumBarType.BAR_TYPE_HOUR2:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_HOUR2, 120);
                    break;
                case EnumBarType.BAR_TYPE_HOUR4:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_HOUR4, 240);
                    break;
                case EnumBarType.BAR_FS_OtherMin:
                    CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_FS_OtherMin,selectMinute);
                    break;
            }
        }


        public void PageIndexAdd()
        {
            Action RefreshHandel = FsChartAction;
            RefreshHandel.BeginInvoke(null, RefreshHandel);
        }




        #endregion
        private Visibility _realTimeVisibility = Visibility.Collapsed;
        public Visibility RealTimeVisibility
        {
            get
            {
                return _realTimeVisibility;
            }
            set
            {
                if (_realTimeVisibility != value)
                {
                    _realTimeVisibility = value;
                    if(_realTimeVisibility==Visibility.Visible)
                    {
                        if(_mainVM.SelectItemViewModel!=null)
                        {
                            if(_mainVM.SelectItemViewModel.Tick.EachDealList.Count==0)
                            {
                                _mainVM.SelectItemViewModel.Tick.SetEachDealList();
                            }
                            
                        }
                    }
                    RaisePropertyChanged(nameof(RealTimeVisibility));
                }
            }
        }
        private Visibility _contractDetailVisibility = Visibility.Collapsed;
        public Visibility ContractDetailVisibility
        {
            get
            {
                return _contractDetailVisibility;
            }
            set
            {
                if (_contractDetailVisibility != value)
                {
                    _contractDetailVisibility = value;
                    RaisePropertyChanged(nameof(ContractDetailVisibility));
                }
            }
        }
        private bool _isDayKLineCheck;
        public bool IsDayKLineCheck
        {
            get
            {
                return _isDayKLineCheck;
            }
            set
            {
                _isDayKLineCheck = value;
                if (_isDayKLineCheck)
                {
     
                    if (stepList.Contains(4))
                    {
                        stepList.Remove(4);
                    }
                    stepList.Add(4);
                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    KLineVisibility = Visibility.Visible;
                    emumTemp = EnumBarType.BAR_TYPE_DAY;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_DAY,0);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);

                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(nameof(IsDayKLineCheck));
            }
        }
        private bool _isWeekKLineCheck;
        public bool IsWeekKLineCheck
        {
            get
            {
                return _isWeekKLineCheck;
            }
            set
            {
                _isWeekKLineCheck = value;
                if (_isWeekKLineCheck)
                {
                    if (stepList.Contains(5))
                    {
                        stepList.Remove(5);
                    }
                    stepList.Add(5);
                    KLineVisibility = Visibility.Visible;
                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_WEEK;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_WEEK,0);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }
        private bool _isMouthKLineCheck;
        public bool IsMouthKLineCheck
        {
            get
            {
                return _isMouthKLineCheck;
            }
            set
            {
                _isMouthKLineCheck = value;
                if (_isMouthKLineCheck)
                {
                    if (stepList.Contains(6))
                    {
                        stepList.Remove(6);
                    }
                    stepList.Add(6);
                    KLineVisibility = Visibility.Visible;
                    FuturesDataModel fu = new FuturesDataModel();
                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_MONTH;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MONTH,0);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }
        private bool _isOneMinKLineCheck;
        public bool IsOneMinKLineCheck
        {
            get
            {
                return _isOneMinKLineCheck;
            }
            set
            {
                _isOneMinKLineCheck = value;
                if (_isOneMinKLineCheck)
                {
                    if (stepList.Contains(7))
                    {
                        stepList.Remove(7);
                    }
                    stepList.Add(7);
                    KLineVisibility = Visibility.Visible;
                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_MIN1;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    // CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN1,1);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);

                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
              
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }
        private bool _isThreeMinKLineCheck;
        public bool IsThreeMinKLineCheck
        {
            get
            {
                return _isThreeMinKLineCheck;
            }
            set
            {
                _isThreeMinKLineCheck = value;
                if (_isThreeMinKLineCheck)
                {
                    if (stepList.Contains(8))
                    {
                        stepList.Remove(8);
                    }
                    stepList.Add(8);
                    KLineVisibility = Visibility.Visible;
                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_MIN3;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN3,3);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
          
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }
        private bool _isFiveMinKLineCheck;
        public bool IsFiveMinKLineCheck
        {
            get
            {
                return _isFiveMinKLineCheck;
            }
            set
            {
                _isFiveMinKLineCheck = value;
                if (_isFiveMinKLineCheck)
                {
                    if (stepList.Contains(9))
                    {
                        stepList.Remove(9);
                    }
                    stepList.Add(9);
                    KLineVisibility = Visibility.Visible;
                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_MIN5;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN5,5);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);

                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
          
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }
        private bool _isFifteenMinKLineCheck;
        public bool IsFifteenMinKLineCheck
        {
            get
            {
                return _isFifteenMinKLineCheck;
            }
            set
            {
                _isFifteenMinKLineCheck = value;
                if (_isFifteenMinKLineCheck)
                {
                    if (stepList.Contains(10))
                    {
                        stepList.Remove(10);
                    }
                    stepList.Add(10);
                    KLineVisibility = Visibility.Visible;

                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_MIN15;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN15,15);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
        
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }
        private bool _isThirtyMinKLineCheck;
        public bool IsThirtyMinKLineCheck
        {
            get
            {
                return _isThirtyMinKLineCheck;
            }
            set
            {
                _isThirtyMinKLineCheck = value;
                if (_isThirtyMinKLineCheck)
                {
                    if (stepList.Contains(11))
                    {
                        stepList.Remove(11);
                    }
                    stepList.Add(11);
                    KLineVisibility = Visibility.Visible;

                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_MIN30;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN30, 30);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
           
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }
        private bool _isSixtyMinKLineCheck;
        public bool IsSixtyMinKLineCheck
        {
            get
            {
                return _isSixtyMinKLineCheck;
            }
            set
            {
                _isSixtyMinKLineCheck = value;
                if (_isSixtyMinKLineCheck)
                {
                    if (stepList.Contains(12))
                    {
                        stepList.Remove(12);
                    }
                    stepList.Add(12);
                    KLineVisibility = Visibility.Visible;
                    if (CuotesTabControlViewModel!=null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_MIN60;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN60, 60);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
             
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }
        private bool _isTwoHoursKLineCheck;
        public bool IsTwoHoursKLineCheck
        {
            get
            {
                return _isTwoHoursKLineCheck;
            }
            set
            {
                _isTwoHoursKLineCheck = value;
                if (_isTwoHoursKLineCheck)
                {
                    if (stepList.Contains(13))
                    {
                        stepList.Remove(13);
                    }
                    stepList.Add(13);
                    KLineVisibility = Visibility.Visible;
                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_HOUR2;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_HOUR2, 120);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
      
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }
        private bool _isFourHoursKLineCheck;
        public bool IsFourHoursKLineCheck
        {
            get
            {
                return _isFourHoursKLineCheck;
            }
            set
            {
                _isFourHoursKLineCheck = value;
                if (_isFourHoursKLineCheck)
                {
                    if (stepList.Contains(14))
                    {
                        stepList.Remove(14);
                    }
                    stepList.Add(14);
                    KLineVisibility = Visibility.Visible;

                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        CuotesTabControlViewModel = null;
                    }
                    emumTemp = EnumBarType.BAR_TYPE_HOUR4;
                    MainViewModel.GetInstance().ISShow = Visibility.Visible;
                    //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_HOUR4,240);
                    Action RefreshHandel = KChartAction;
                    RefreshHandel.BeginInvoke(null, RefreshHandel);
                    OptionalVisibility = Visibility.Collapsed;
                    InternationalVisibility = Visibility.Collapsed;
                }
          
                RaisePropertyChanged(nameof(IsWeekKLineCheck));
            }
        }

        private bool _IsOtherMinKLineCheck;
        public bool IsOtherMinKLineCheck
        {
            get
            {
                return _IsOtherMinKLineCheck;
            }
            set
            {
                _IsOtherMinKLineCheck = value;
                if (_IsOtherMinKLineCheck)
                {
                    if (stepList.Contains(15))
                    {
                        stepList.Remove(15);
                    }
                    stepList.Add(15);
                    if (CuotesTabControlViewModel != null)
                    {
                        CuotesTabControlViewModel.StopKCharTimeUpdate();
                        CuotesTabControlViewModel.StopTimeUpdate();
                        CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                        //CuotesTabControlViewModel = null;
                    }
                    if (IsBack)
                    {
                        if(selectMinute>0)
                        {
                            emumTemp = EnumBarType.BAR_FS_OtherMin;
                            KLineVisibility = Visibility.Visible;
                            MainViewModel.GetInstance().ISShow = Visibility.Visible;
                            //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_FS_OtherMin, selectMinute);
                            Action RefreshHandel = KChartAction;
                            RefreshHandel.BeginInvoke(null, RefreshHandel);
                            OptionalVisibility = Visibility.Collapsed;
                            InternationalVisibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        KLineVisibility = Visibility.Visible;
                        if (selectMinute==0)
                        {
                            emumTemp = EnumBarType.BAR_TYPE_MIN1;
                           // CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_TYPE_MIN1, 1);
                            selectMinute = 1;
                            MainViewModel.GetInstance().ISShow = Visibility.Visible;
                            Action RefreshHandel = KChartAction;
                            RefreshHandel.BeginInvoke(null, RefreshHandel);
                        }
    
                        OptionalVisibility = Visibility.Collapsed;
                        InternationalVisibility = Visibility.Collapsed;

                        OtherMinWin omw = new OtherMinWin();
                        omw.ShowDialog();
                        if (omw.DialogResult == true)
                        {
                            emumTemp = EnumBarType.BAR_FS_OtherMin;
                            //CuotesTabControlViewModel = new CreateMultiPaneStockChartsViewModel(_mainVM.SelectItemViewModel, EnumBarType.BAR_FS_OtherMin, omw.MinValue);
                            selectMinute = omw.MinValue;
                            Action RefreshHandel = KChartAction;
                            RefreshHandel.BeginInvoke(null, RefreshHandel);
                        }
                    }
                   
                }
             
                RaisePropertyChanged(nameof(IsOtherMinKLineCheck));
            }
        }

        private Visibility _kLineVisibility = Visibility.Collapsed;
        public Visibility KLineVisibility
        {
            get
            {
                return _kLineVisibility;
            }
            set
            {
                if (_kLineVisibility != value)
                {
                    _kLineVisibility = value;
                    if (_kLineVisibility == Visibility.Visible)
                    {
                        if (_mainVM.SelectItemViewModel != null)
                        {
                            if (_mainVM.SelectItemViewModel.Tick.EachDealList.Count == 0)
                            {
                                _mainVM.SelectItemViewModel.Tick.SetEachDealList();
                            }

                        }
                    }
                    else
                    {
                        //不在K线界面   需要关闭K线计时器
                        if (CuotesTabControlViewModel != null)
                        {
                            CuotesTabControlViewModel.StopKCharTimeUpdate();
                            CuotesTabControlViewModel.StopTimeUpdate();
                            CuotesTabControlViewModel.StopmyTimerIsAddBarUpdate();
                            CuotesTabControlViewModel = null;
                        }
                    }
                    RaisePropertyChanged(nameof(KLineVisibility));
                }
            }
        }
        #endregion

        public Action SetOptionalListAction
        {
            get; set;
        }
        public void SetOptionalList()
        {
            SetOptionalListAction?.Invoke();
        }
        public Action ReSetOptionalSelectItemAction
        {
            get; set;
        }
        public void ReSetOptionalSelectItem()
        {
            ReSetOptionalSelectItemAction?.Invoke();
        }
        public void SetDataGridStyle(int type)
        {
            SetDataGridStyleHandler?.Invoke(type, EventArgs.Empty);
        }

        private QuotesTabControlViewModel _quotesTabControlViewModel;
        public QuotesTabControlViewModel QuotesTabControlViewModel
        {
            get
            {
                if (_quotesTabControlViewModel == null)
                {
                    _quotesTabControlViewModel = QuotesTabControlViewModel.GetInstance(_mainVM);
                }
                return _quotesTabControlViewModel;
            }
        }

        private CreateMultiPaneStockChartsViewModel _createMultiPaneStockChartsViewModel;
        /// <summary>
        /// K线
        /// </summary>
        public CreateMultiPaneStockChartsViewModel CuotesTabControlViewModel
        {
            get
            {
                //if (_createMultiPaneStockChartsViewModel == null)
                //{
                //    _createMultiPaneStockChartsViewModel = CreateMultiPaneStockChartsViewModel.GetInstance();
                //}
                return _createMultiPaneStockChartsViewModel;
            }
            set
            {
                if(_createMultiPaneStockChartsViewModel!=value)
                {
                    _createMultiPaneStockChartsViewModel = value;
                    RaisePropertyChanged(nameof(CuotesTabControlViewModel));
                }
            }
        }

        private RealTimeChartDataModel _realTimeChart;
        /// <summary>
        /// 分时
        /// </summary>
        public RealTimeChartDataModel RealTimeChart
        {
            get
            {
                //if (_createMultiPaneStockChartsViewModel == null)
                //{
                //    _createMultiPaneStockChartsViewModel = CreateMultiPaneStockChartsViewModel.GetInstance();
                //}
                return _realTimeChart;
            }
            set
            {
                if (_realTimeChart != value)
                {
                    _realTimeChart = value;
                    RaisePropertyChanged(nameof(RealTimeChart));
                }
            }
        }
        private ContractDetailInfoViewModel _ContractDetailInfoViewModel;
        /// <summary>
        /// 合约背景资料
        /// </summary>
        public ContractDetailInfoViewModel ContractDetailInfoViewModel
        {
            get
            {
                return _ContractDetailInfoViewModel;
            }
            set
            {
                if (_ContractDetailInfoViewModel != value)
                {
                    _ContractDetailInfoViewModel = value;
                    RaisePropertyChanged(nameof(ContractDetailInfoViewModel));
                }
            }
        }

        private List<FuturesViewModel> _OptionalList = new List<FuturesViewModel>();
        /// <summary>
        /// 自选
        /// </summary>
        public List<FuturesViewModel> OptionalList
        {
            get
            {
                return _OptionalList;
            }
            set
            {
                _OptionalList = value;
                RaisePropertyChanged(nameof(OptionalList));
            }
        }
        /// <summary>
        /// 后退
        /// </summary>
        public ICommand BackCommand { get { return new RelayCommand(BackExecuteChanged, BackCanExecuteChanged); } }
        public void BackExecuteChanged()
        {
            BackButtonAction();
        }
        public bool BackCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 起始页
        /// </summary>
        public ICommand HomeCommand { get { return new RelayCommand(HomeExecuteChanged, HomeCanExecuteChanged); } }
        public void HomeExecuteChanged()
        {
            IsInternationalCheck = true;

        }
        public bool HomeCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 放大
        /// </summary>
        public ICommand EnlargeCommand { get { return new RelayCommand(EnlargeExecuteChanged, EnlargeCanExecuteChanged); } }
        public void EnlargeExecuteChanged()
        {
            SetDataGridStyle(1);
        }
        public bool EnlargeCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 缩小
        /// </summary>
        public ICommand NarrowCommand { get { return new RelayCommand(NarrowExecuteChanged, NarrowCanExecuteChanged); } }
        public void NarrowExecuteChanged()
        {
            SetDataGridStyle(2);
        }
        public bool NarrowCanExecuteChanged()
        {
            return true;
        }

        public void AddOptionalData(OptionalContractModel item)
        {
            int seq = TradeInfoHelper.OptionalModelList.Count;
            var datamodel = TradeInfoHelper.ContractModelList.FirstOrDefault(o => string.Equals(o.contractCode, item.contract_id));
            if (datamodel != null)
            {
                var viewModel = new FuturesViewModel(datamodel);
                viewModel.Seq = seq;
                viewModel.IsOptionalStock = true;
                viewModel.OptionalSerialNumber = item.serial_number;
                QuotationEntity quotesmodel = null;
                lock (TradeInfoHelper.SubscribedContractList)
                {
                    quotesmodel = TradeInfoHelper.SubscribedContractList.FirstOrDefault(o => string.Equals(o.cd, item.contract_id));
                }
                if (quotesmodel != null)
                {
                    ScoketManager.GetInstance().UpdateModelInfo(viewModel, quotesmodel);
                }
                OptionalList.Add(viewModel);
                SetOptionalList();
            }
        }
        public void DelOptionalData(OptionalContractModel item)
        {
            var datamodel = OptionalList.FirstOrDefault(o => string.Equals(o.OptionalSerialNumber, item.serial_number));
            if (datamodel != null)
            {
                OptionalList.Remove(datamodel);
                for (int i = 0; i < OptionalList.Count; i++)
                {
                    OptionalList[i].Seq = i + 1;
                }
                SetOptionalList();
            }
        }

        public void BackButtonAction()
        {
            if(stepList.Count>0)
            {
                int lastIndex = stepList.LastOrDefault();
                stepList.Remove(lastIndex);
                lastIndex = stepList.LastOrDefault();
                switch (lastIndex)
                {
                    case 1:
                        IsQuoteCheck = true;
                        break;
                    case 2:
                        IsRealTimeCheck = true;
                        break;
                    case 3:
                        IsContractDetailCheck = true;
                        break;
                    case 4:
                        IsDayKLineCheck = true;
                        break;
                    case 5:
                        IsWeekKLineCheck = true;
                        break;
                    case 6:
                        IsMouthKLineCheck = true;
                        break;
                    case 7:
                        IsOneMinKLineCheck = true;
                        break;
                    case 8:
                        IsThreeMinKLineCheck = true;
                        break;
                    case 9:
                        IsFiveMinKLineCheck = true;
                        break;
                    case 10:
                        IsFifteenMinKLineCheck = true;
                        break;
                    case 11:
                        IsThirtyMinKLineCheck = true;
                        break;
                    case 12:
                        IsSixtyMinKLineCheck = true;
                        break;
                    case 13:
                        IsTwoHoursKLineCheck = true;
                        break;
                    case 14:
                        IsFourHoursKLineCheck = true;
                        break;
                    case 15:
                        IsBack = true;
                        IsOtherMinKLineCheck = true;
                        IsBack = false;
                        break;
                    default:
                        break;
                }
              
            }
        }
    }
}
