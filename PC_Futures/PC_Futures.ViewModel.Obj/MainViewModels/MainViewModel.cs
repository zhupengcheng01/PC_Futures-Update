using MicroMvvm;
using PC_Futures.Models;
using PC_Futures.ViewModel;
using PC_Futures.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace PC_Futures
{
    public class MainViewModel : ObservableObject
    {
        private HttpRequestContractHelper httpHelper = new HttpRequestContractHelper();
        private static MainViewModel _instance;

        public static MainViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MainViewModel();
            }
            return _instance;
        }
        public MainViewModel()
        {
            httpHelper.GetVarietyList();
            httpHelper.loadBarSeries();
        }
        /// <summary>
        /// 进度条显示
        /// </summary>
        private Visibility _ISShow = Visibility.Collapsed;
        public Visibility ISShow
        {
            get { return _ISShow; }
            set
            {
                if (value != _ISShow)
                {
                    _ISShow = value;
                    RaisePropertyChanged("ISShow");
                }
            }
        }
        #region 属性
        #region 结构属性
        private Visibility _tradeVisibility;
        public Visibility TradeVisibility
        {
            get
            {
                return _tradeVisibility;
            }
            set
            {
                _tradeVisibility = value;
                RaisePropertyChanged(nameof(TradeVisibility));
                //RaisePropertyChanged(nameof(RowHeight));
            }
        }
        private Visibility _tradeLoginVisibility = Visibility.Visible;
        public Visibility TradeLoginVisibility
        {
            get
            {
                return _tradeLoginVisibility;
            }
            set
            {
                _tradeLoginVisibility = value;
                RaisePropertyChanged(nameof(TradeLoginVisibility));
                RaisePropertyChanged(nameof(TradePanelVisibility));
            }
        }
        public Visibility TradePanelVisibility
        {
            get
            {
                if (TradeLoginVisibility == Visibility.Visible)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }
        //public GridLength RowHeight
        //{
        //    get
        //    {
        //        if(TradeVisibility==Visibility.Visible)
        //        {
        //            return new GridLength(295, GridUnitType.Pixel);
        //        }
        //        else
        //        {
        //            return new GridLength(0, GridUnitType.Pixel);
        //        }
        //    }
        //}


        #endregion
        public ServerStatusInfoHelper ServerStatusInfoHelper
        {
            get
            {
                return ServerStatusInfoHelper.Instace();
            }
        }
        private TradeQuotesViewModel _tradeQuotesViewModel;
        public TradeQuotesViewModel TradeQuotesViewModel
        {
            get
            {
                if (_tradeQuotesViewModel == null)
                {
                    _tradeQuotesViewModel = TradeQuotesViewModel.GetInstance(this);
                }
                return _tradeQuotesViewModel;
            }
        }
        private TradeLoginViewModel _tradeLoginViewModel;
        public TradeLoginViewModel TradeLoginViewModel
        {
            get
            {
                if (_tradeLoginViewModel == null)
                {
                    _tradeLoginViewModel = TradeLoginViewModel.GetInstance(this);
                }
                return _tradeLoginViewModel;
            }
        }
        private TradePanelViewModel _tradePanelViewModel;
        public TradePanelViewModel TradePanelViewModel
        {
            get
            {
                return _tradePanelViewModel;
            }
            set
            {
                if (_tradePanelViewModel != value)
                {
                    _tradePanelViewModel = value;
                    RaisePropertyChanged(nameof(TradePanelViewModel));
                }
            }
        }

        private FuturesViewModel _selectItemViewModel;
        /// <summary>
        /// 选中的合约
        /// </summary>
        public FuturesViewModel SelectItemViewModel
        {
            get
            {
                return _selectItemViewModel;
            }
            set
            {
                if (_selectItemViewModel != value)
                {
                    _selectItemViewModel = value;
                    if (TradePanelViewModel != null)
                    {                     
                        TransactionViewModel.Instance().SetXamlValues(_selectItemViewModel);
                        UCTransaction.cd();
                    }
                    RaisePropertyChanged(nameof(SelectItemViewModel));
                }
            }
        }
        /// <summary>
        /// 品种和合约集合
        /// </summary>
        public Dictionary<string, List<SysCodeModel>> VarietyList = new Dictionary<string, List<SysCodeModel>>();
        /// <summary>
        /// 列表已注册行情
        /// </summary>
        public List<string> QuotesSubscribed = new List<string>();
        /// <summary>
        /// 已注册行情的持仓集合
        /// </summary>
        public List<string> SubscribedStocks = new List<string>();
        #endregion

        #region 标题属性
        private bool _SystemPopupIsOpen;
        public bool SystemPopupIsOpen
        {
            get
            {
                return _SystemPopupIsOpen;
            }
            set
            {
                _SystemPopupIsOpen = value;
                RaisePropertyChanged(nameof(SystemPopupIsOpen));
            }
        }
        private bool _PlatePopupIsOpen;
        public bool PlatePopupIsOpen
        {
            get
            {
                return _PlatePopupIsOpen;
            }
            set
            {
                _PlatePopupIsOpen = value;
                RaisePropertyChanged(nameof(PlatePopupIsOpen));
            }
        }
        public List<string> PlateList
        {
            get
            {
                return TradeInfoHelper.ExchangeDisplayList;
            }
        }
        private string _PlateSelectedItem;
        public string PlateSelectedItem
        {
            get
            {
                return _PlateSelectedItem;
            }
            set
            {
                _PlateSelectedItem = value;
                if (!string.IsNullOrEmpty(_PlateSelectedItem))
                {
                    PlatePopupIsOpen = false;
                    SetTabIndex(_PlateSelectedItem);

                }
                RaisePropertyChanged(nameof(PlateSelectedItem));
            }
        }
        #endregion


        #region Command事件
        public ICommand TradeCommand { get { return new RelayCommand(TradeExecuteChanged, TradeCanExecuteChanged); } }
        public void TradeExecuteChanged()
        {
            if (TradeVisibility == Visibility.Visible)
            {
                TradeVisibility = Visibility.Collapsed;
            }
            else
            {
                TradeVisibility = Visibility.Visible;
            }
        }
        public bool TradeCanExecuteChanged()
        {
            return true;
        }
        public ICommand ModifyPwdCommand { get { return new RelayCommand(ModifyPwdExecuteChanged, ModifyPwdCanExecuteChanged); } }
        public void ModifyPwdExecuteChanged()
        {
            if (UserInfoHelper.IsHaveLogin)
            {
                ModifyPwdWindow modifypwdWin = new ModifyPwdWindow();
                modifypwdWin.ShowDialog();
            }

        }
        public bool ModifyPwdCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 系统
        /// </summary>
        public ICommand SystemCommand { get { return new RelayCommand(SystemExecuteChanged, SystemCanExecuteChanged); } }
        public void SystemExecuteChanged()
        {
            SystemPopupIsOpen = true;
        }
        public bool SystemCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        public ICommand CloseWinCommand { get { return new RelayCommand(CloseWinExecuteChanged, CloseWinCanExecuteChanged); } }
        public void CloseWinExecuteChanged()
        {
            SystemPopupIsOpen = false;
        }
        public bool CloseWinCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 板块
        /// </summary>
        public ICommand PlateCommand { get { return new RelayCommand(PlateExecuteChanged, PlateCanExecuteChanged); } }
        public void PlateExecuteChanged()
        {
            PlateSelectedItem = null;
            PlatePopupIsOpen = true;

        }

        public ICommand ParamSetCommand { get { return new RelayCommand(ParamSetExecuteChanged, ParamSetCanExecuteChanged); } }
        public void ParamSetExecuteChanged()
        {
            if (UserInfoHelper.IsHaveLogin)
            {
                ParameterSetting setWin = new ParameterSetting();
                setWin.ShowDialog();
            }
        }
        public bool ParamSetCanExecuteChanged()
        {
            return true;
        }
        public ICommand SelectCommand { get { return new RelayCommand(SelectExecuteChanged, SelectCanExecuteChanged); } }
        public void SelectExecuteChanged()
        {
            if (UserInfoHelper.IsHaveLogin)
            {
                UCSelect setWin = new UCSelect();
                setWin.ShowDialog();
            }
        }
        public bool SelectCanExecuteChanged()
        {
            return true;
        }

        public bool PlateCanExecuteChanged()
        {
            return true;
        }
        #endregion

        private void SetTabIndex(string displayStr)
        {
            int index = 0;
            foreach (var item in TradeInfoHelper.ContractModelGroupList)
            {
                if (string.Equals(item.Key, displayStr))
                {
                    break;
                }
                index++;
            }
            var quoteViewModel = TradeQuotesViewModel.GetInstance(null);
            var tabViewModel = QuotesTabControlViewModel.GetInstance(null);
            if (quoteViewModel.IsInternationalCheck)
            {
                if (!quoteViewModel.IsQuoteCheck)
                {
                    quoteViewModel.IsQuoteCheck = true;
                }
            }
            else
            {
                quoteViewModel.InternationalCheckType = 1;
                quoteViewModel.IsInternationalCheck = true;
            }
            tabViewModel.SelectedIndex = index;

        }
    }
}
