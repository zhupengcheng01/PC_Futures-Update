using PC_Futures.Models;
using PC_Futures.ViewModel;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// QuotesTabControl.xaml 的交互逻辑
    /// </summary>
    public partial class QuotesTabControl : UserControl
    {

        private ScoketManager _scoketManager;
        private System.Timers.Timer myTimer;
        private QuotesTabControlViewModel _viewModel;

        public QuotesTabControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as QuotesTabControlViewModel;
            if (_viewModel != null)
            {
                _viewModel.ContractChanged -= _viewModel_ContractChanged;
                _viewModel.ContractChanged += _viewModel_ContractChanged;
                Init();
                if (_viewModel.SetSelectItemAction == null)
                {
                    _viewModel.SetSelectItemAction = s => { SetSelectItem(s); };
                }
                _scoketManager = ScoketManager.GetInstance();
                myTimer = new System.Timers.Timer(200);
                myTimer.Elapsed += myTimer_Elapsed;
                myTimer.Start();

            }
        }

        private void _viewModel_ContractChanged(object sender, EventArgs e)
        {
            SysCodeModel model = sender as SysCodeModel;
            if (model != null)
            {
                ScrollIntoView(model);
            }

        }

        private void Init()
        {
            foreach (var item in TradeInfoHelper.ExchangeDisplayList)
            {
                TabItem tabItem = new TabItem();
                tabItem.Header = item;
                tabItem.Style = FindResource("QuotesTabItemStyle") as Style;
                QuotesDataGrid uscDataGird = new QuotesDataGrid();
                if (CommHelper.ContractModelGroupList.ContainsKey(item))
                {
                    uscDataGird.quotesDataGrid.ItemsSource = CommHelper.ContractModelGroupList[item];
                }
                tabItem.Content = uscDataGird;
                AllFuturesQuota.Items.Add(tabItem);
            }


            //HKEx.quotesDataGrid.ItemsSource = _viewModel.HKExList;
            //CMEENG.quotesDataGrid.ItemsSource = _viewModel.CMEENGList;
            //CMEMET.quotesDataGrid.ItemsSource = _viewModel.CMEMETList;
            //CMEFEX.quotesDataGrid.ItemsSource = _viewModel.CMEFEXList;
            //CMEIDX.quotesDataGrid.ItemsSource = _viewModel.CMEIDXList;
            //CMEFRM.quotesDataGrid.ItemsSource = _viewModel.CMEFRMList;
            //EUREX.quotesDataGrid.ItemsSource = _viewModel.EUREXList;
            //SGXA50.quotesDataGrid.ItemsSource = _viewModel.SGXA50List;

            SetFirstSelectItem();
        }
        private void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var trademodel = TradeQuotesViewModel.GetInstance(null);
                if (ServerStatusInfoHelper.Instace().IsReattached)
                {
                    #region 行情重连之后执行
                    ServerStatusInfoHelper.Instace().IsReattached = false;
                    var mainViewModel = MainViewModel.GetInstance();
                    if (mainViewModel.SelectItemViewModel != null)
                    {
                        if (Application.Current != null) //判断不为空
                        {
                            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(
                            () =>
                            {
                                mainViewModel.SelectItemViewModel.Tick.EachDealList.Clear();
                            }));
                        }
                        _scoketManager.SendSubscribeInfo(7, mainViewModel.SelectItemViewModel.ContractCode);
                    }
                    var clearDealList = mainViewModel.QuotesSubscribed.ToList();
                    foreach (var item in clearDealList)
                    {
                        var futuresModel = _viewModel.FuturesViewModelList.FirstOrDefault(o => !string.IsNullOrEmpty(o.ExchangeDisplay) && string.Equals(o.ContractCode, item));
                        if (futuresModel != null)
                        {
                            if (futuresModel.Tick.EachDealList.Count == 0)
                                continue;
                            if (Application.Current != null) //判断不为空
                            {
                                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(
                                () =>
                                {
                                    futuresModel.Tick.EachDealList.Clear();
                                }));
                            }
                        }
                    }
                    mainViewModel.QuotesSubscribed.Clear();

                    LogHelper.Info("重连之后行情订阅:" + string.Join(",", mainViewModel.SubscribedStocks));
                    _scoketManager.SendSubscribeInfo(7, mainViewModel.SubscribedStocks);
                    if (mainViewModel.SelectItemViewModel != null)
                    {
                        if (Application.Current != null) //判断不为空
                        {
                            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(
                            () =>
                            {
                                //if (trademodel.RealTimeVisibility == Visibility.Visible || trademodel.KLineVisibility == Visibility.Visible)
                                //{
                                //    mainViewModel.SelectItemViewModel.Tick.SetEachDealList();
                                //}
                            }));
                        }
                    }
                    #endregion
                }

                if (trademodel.IsInternationalCheck)
                {
                    int rowIndex = -1;
                    //int i = 0;
                    List<string> subList = new List<string>();
                    List<string> viewList = new List<string>();
                    var mainViewModel = MainViewModel.GetInstance();
                    this.AllFuturesQuota.Dispatcher.Invoke(new Action(() =>
                    {
                        TabItem tabItem = AllFuturesQuota.Items[_viewModel.SelectedIndex] as TabItem;
                        var dataGrid = (tabItem.Content as QuotesDataGrid).quotesDataGrid;
                        if (dataGrid.Items.Count > 0)
                        {
                            for (int a = 0; a < dataGrid.Items.Count; a++)
                            {
                                var item = dataGrid.Items[a];
                                var Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);
                                if (Row != null)
                                {
                                    if (Row.TransformToVisual(dataGrid).Transform(new Point(0, 0)).Y + Row.ActualHeight >= dataGrid.ActualHeight)
                                    {
                                        rowIndex = a;
                                        break;
                                    }
                                }
                            }
                            if (rowIndex == -1)
                            {
                                rowIndex = dataGrid.Items.Count - 1;
                                if (rowIndex == -1)
                                {
                                    return;
                                }
                            }
                            int j = 0;
                            do
                            {
                                if (rowIndex < 0)
                                {
                                    break;
                                }
                                var Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
                                if (Row != null)
                                {
                                    FuturesViewModel stock = Row.DataContext as FuturesViewModel;
                                    if (stock != null)
                                    {
                                        if (!mainViewModel.QuotesSubscribed.Contains(stock.ContractCode) && !mainViewModel.SubscribedStocks.Contains(stock.ContractCode))
                                        {
                                            subList.Add(stock.ContractCode);
                                            mainViewModel.QuotesSubscribed.Add(stock.ContractCode);
                                        }
                                        viewList.Add(stock.ContractCode);
                                    }
                                }
                                rowIndex--;
                                j++;
                            } while (j < 28);
                        }
                    }));
                    if (subList.Count > 0)
                    {
                        LogHelper.Info("行情订阅:" + string.Join(",", subList));
                        _scoketManager.SendSubscribeInfo(7, subList);
                    }
                    FuturesViewModel selectModel = mainViewModel.SelectItemViewModel;
                    List<string> unSubList = new List<string>();
                    List<string> tempList = mainViewModel.QuotesSubscribed.ToList();
                    if (tempList.Count > 0)
                    {
                        foreach (var item in tempList)
                        {
                            if (!string.IsNullOrEmpty(item) && !viewList.Contains(item) && !mainViewModel.SubscribedStocks.Contains(item) && (selectModel == null || !string.Equals(selectModel.ContractCode, item)))
                            {
                                mainViewModel.QuotesSubscribed.Remove(item);
                                unSubList.Add(item);
                            }
                        }
                    }
                    if (unSubList.Count > 0)
                    {
                        LogHelper.Info("行情反订阅:" + string.Join(",", unSubList));
                        _scoketManager.SendSubscribeInfo(8, unSubList);
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.Info("StockQuota：myTimer_Elapsed" + ex.ToString());
            }

        }

        public void ScrollIntoView(SysCodeModel stock)
        {
            int i = 0;
            foreach (var item in CommHelper.ContractModelGroupList)
            {
                FuturesViewModel hkexmodel = item.Value.FirstOrDefault(o => string.Equals(o.ContractCode, stock.SystemName));
                if (hkexmodel != null)
                {
                    _viewModel.SelectedIndex = i;
                    TabItem tabItem = AllFuturesQuota.Items[i] as TabItem;
                    var dataGrid = (tabItem.Content as QuotesDataGrid).quotesDataGrid;
                    dataGrid.ScrollIntoView(hkexmodel);
                    dataGrid.SelectedItem = hkexmodel;
                    break;
                }
                i++;
            }
        }

        public void SetFirstSelectItem()
        {
            if (AllFuturesQuota.Items.Count > 0)
            {
                //_viewModel.SelectedIndex = 0;
                TabItem tabItem = AllFuturesQuota.Items[0] as TabItem;
                (tabItem.Content as QuotesDataGrid).quotesDataGrid.SelectedItem = CommHelper.ContractModelGroupList[tabItem.Header.ToString()][0];
            }
        }

        #region 注释内容
        public void SetSelectItem(int index)
        {
            TabItem tabItem = AllFuturesQuota.Items[index] as TabItem;
            (tabItem.Content as QuotesDataGrid).quotesDataGrid.SelectedItem = null;
            (tabItem.Content as QuotesDataGrid).quotesDataGrid.SelectedItem = CommHelper.ContractModelGroupList[tabItem.Header.ToString()][0];
        }


        #endregion
    }
}
