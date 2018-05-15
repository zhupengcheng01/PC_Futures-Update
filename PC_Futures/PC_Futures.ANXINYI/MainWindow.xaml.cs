using PC_Futures.Models;
using PC_Futures.ViewModel;
using PC_Futures.WebScoket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Utilities;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScoketManager _scoketManager;
        private MainViewModel viewModel;
        private Size _size = new Size(1300, 900);
        private Point _previousMousePoint;
        private int _divideValue = 400;
        private int _offSetValue = 150;
        private System.Timers.Timer myTimer;
        private bool IsFsOrKChar = false;
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;

            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            log.Source = new BitmapImage(new Uri("\\Resources\\Images\\TitleLog\\TitleImg.png", UriKind.Relative));//strImagePath 就绝对路径
            viewModel = MainViewModel.GetInstance();
            DataContext = viewModel;
            _scoketManager = ScoketManager.GetInstance();
            if (viewModel.TradeQuotesViewModel.SetOptionalListAction == null)
            {
                viewModel.TradeQuotesViewModel.SetOptionalListAction = () => { this.SetOptionalList(); };
            }
            //if (viewModel.TradeQuotesViewModel.ReSetOptionalSelectItemAction == null)
            //{
            //    viewModel.TradeQuotesViewModel.ReSetOptionalSelectItemAction = () => { this.ReSetOptionalSelectItem(); };
            //}
            var resizer = new ControlResizer(border, new Thickness(1), 0, null);
            //设置事件
            resizer.Resize += resizer_Resize;
            LoadParameter();
        }

        private void resizer_Resize(object sender, ControlResizeEventArgs e)
        { //左右拉伸
            if (e.LeftDirection.HasValue)
            {
                return;
            }
            //上下拉伸
            if (e.TopDirection.HasValue)
            {
                var value = border.Height + e.VerticalChange;
                if (value > border.MinHeight)
                {
                    //if (e.TopDirection.Value)
                    //Canvas.SetTop(rect, Canvas.GetTop(rect) - e.VerticalChange);
                    border.Height = value;
                }
            }
        }


        /// <summary>
        /// 加载参数设置
        /// </summary>
        private void LoadParameter()
        {
            //加载止盈止损
            // CommParameterSetting.StopLossModel.ADelegateSecond = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "ADelegateSecond", IniHelper.parameterSetting)) ? 0 : int.Parse(IniHelper.ProfileReadValue("CheckFullStop", "ADelegateSecond", IniHelper.parameterSetting));
            CommParameterSetting.StopLossModel.BuyNum = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "BuyNum", IniHelper.parameterSetting)) ? 0 : int.Parse(IniHelper.ProfileReadValue("CheckFullStop", "BuyNum", IniHelper.parameterSetting));
            //   CommParameterSetting.StopLossModel.CkADelegate = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "CkADelegate", IniHelper.parameterSetting)) ? false : bool.Parse(IniHelper.ProfileReadValue("CheckFullStop", "CkADelegate", IniHelper.parameterSetting));
            CommParameterSetting.StopLossModel.CkBuy = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "CkBuy", IniHelper.parameterSetting)) ? false : bool.Parse(IniHelper.ProfileReadValue("CheckFullStop", "CkBuy", IniHelper.parameterSetting));
            //  CommParameterSetting.StopLossModel.CkReADelegate = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "CkReADelegate", IniHelper.parameterSetting)) ? false : bool.Parse(IniHelper.ProfileReadValue("CheckFullStop", "CkReADelegate", IniHelper.parameterSetting));
            CommParameterSetting.StopLossModel.CkSell = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "CkSell", IniHelper.parameterSetting)) ? false : bool.Parse(IniHelper.ProfileReadValue("CheckFullStop", "CkSell", IniHelper.parameterSetting));
            //   CommParameterSetting.StopLossModel.DateValidity = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "DateValidity", IniHelper.parameterSetting)) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : IniHelper.ProfileReadValue("CheckFullStop", "DateValidity", IniHelper.parameterSetting);
            // CommParameterSetting.StopLossModel.ReADelegateAllSecond = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "ReADelegateAllSecond", IniHelper.parameterSetting)) ? 0 : int.Parse(IniHelper.ProfileReadValue("CheckFullStop", "ReADelegateAllSecond", IniHelper.parameterSetting));
            CommParameterSetting.StopLossModel.SellNum = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "SellNum", IniHelper.parameterSetting)) ? 0 : int.Parse(IniHelper.ProfileReadValue("CheckFullStop", "SellNum", IniHelper.parameterSetting));
            // CommParameterSetting.StopLossModel.StopLoss = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "StopLoss", IniHelper.parameterSetting)) ? 0 : int.Parse(IniHelper.ProfileReadValue("CheckFullStop", "StopLoss", IniHelper.parameterSetting));
            CommParameterSetting.StopLossModel.StopLossDelegation = IniHelper.ProfileReadValue("CheckFullStop", "StopLossDelegation", IniHelper.parameterSetting);
            if (string.IsNullOrEmpty(CommParameterSetting.StopLossModel.StopLossDelegation))
            {

                CommParameterSetting.StopLossModel.StopLossDelegation = "市价";
            }
            CommParameterSetting.StopLossModel.StopLossPrice = IniHelper.ProfileReadValue("CheckFullStop", "StopLossPrice", IniHelper.parameterSetting);
            if (string.IsNullOrEmpty(CommParameterSetting.StopLossModel.StopLossPrice))
            {

                CommParameterSetting.StopLossModel.StopLossPrice = "最新价";
            }
            //  CommParameterSetting.StopLossModel.StopProfit = string.IsNullOrEmpty(IniHelper.ProfileReadValue("CheckFullStop", "StopProfit", IniHelper.parameterSetting)) ? 0 : int.Parse(IniHelper.ProfileReadValue("CheckFullStop", "StopProfit", IniHelper.parameterSetting));
            CommParameterSetting.StopLossModel.StopProfitDelegation = IniHelper.ProfileReadValue("CheckFullStop", "StopProfitDelegation", IniHelper.parameterSetting);
            //  CommParameterSetting.StopLossModel.StopProfitPrice = IniHelper.ProfileReadValue("CheckFullStop", "StopProfitPrice", IniHelper.parameterSetting);
            //加载自动止盈止损 自动盈亏是用；分割行，分割列；
            string AutoStopLossModel = IniHelper.ProfileReadValue("AutoCheckFullStop", "AutoStopLoss", IniHelper.parameterSetting);
            // public static List<AutoStopLossModel> AutoStopLossModel = new List<AutoStopLossModel>();
            if (!string.IsNullOrEmpty(AutoStopLossModel))
            {
                string[] ModelCount = AutoStopLossModel.Split(';');

                for (int i = 0; i < ModelCount.Length; i++)
                {
                    string[] paras = ModelCount[i].Split(',');
                    AutoStopLossModel aspl = new AutoStopLossModel();
                    aspl.Variety = paras[0];
                    aspl.Agreement = paras[1];
                    aspl.Direction = paras[2];
                    aspl.FloatingProfitAndLoss = string.IsNullOrEmpty(paras[3]) ? 0 : int.Parse(paras[3]);
                    aspl.StopLossPotion = string.IsNullOrEmpty(paras[4]) ? 0 : int.Parse(paras[4]);
                    aspl.StopProfitPotion = string.IsNullOrEmpty(paras[5]) ? 0 : int.Parse(paras[5]);
                    CommParameterSetting.AutoStopLossModel.Add(aspl);
                    if (!ContractVariety.ContracPostionID.ContainsKey(aspl.Agreement))
                    {
                        ContractVariety.ContracPostionID.Add(aspl.Agreement, new List<string>());
                    }
                }
            }

            //下单版设置

            CommParameterSetting.OrderVersion.BeforeOrderEnter = string.IsNullOrEmpty(IniHelper.ProfileReadValue("OrderVersion", "BeforeOrderEnter", IniHelper.parameterSetting)) ? true : bool.Parse(IniHelper.ProfileReadValue("OrderVersion", "BeforeOrderEnter", IniHelper.parameterSetting));
            CommParameterSetting.OrderVersion.DefaultLot = string.IsNullOrEmpty(IniHelper.ProfileReadValue("OrderVersion", "DefaultLot", IniHelper.parameterSetting)) ? 0 : int.Parse(IniHelper.ProfileReadValue("OrderVersion", "DefaultLot", IniHelper.parameterSetting));
            CommParameterSetting.OrderVersion.MaxLot = string.IsNullOrEmpty(IniHelper.ProfileReadValue("OrderVersion", "MaxLot", IniHelper.parameterSetting)) ? 0 : int.Parse(IniHelper.ProfileReadValue("OrderVersion", "MaxLot", IniHelper.parameterSetting));
            //快捷键设置
            CommParameterSetting.ShortcutKey.BuyOpen = IniHelper.ProfileReadValue("ShortcutKey", "BuyOpen", IniHelper.parameterSetting);
            CommParameterSetting.ShortcutKey.Clearance = IniHelper.ProfileReadValue("ShortcutKey", "Clearance", IniHelper.parameterSetting);
            CommParameterSetting.ShortcutKey.ClosingBuy = IniHelper.ProfileReadValue("ShortcutKey", "ClosingBuy", IniHelper.parameterSetting);
            CommParameterSetting.ShortcutKey.ClosingSell = IniHelper.ProfileReadValue("ShortcutKey", "ClosingSell", IniHelper.parameterSetting);
            CommParameterSetting.ShortcutKey.LockMenu = string.IsNullOrEmpty(IniHelper.ProfileReadValue("ShortcutKey", "LockMenu", IniHelper.parameterSetting)) ? false : bool.Parse(IniHelper.ProfileReadValue("ShortcutKey", "LockMenu", IniHelper.parameterSetting)); ;
            CommParameterSetting.ShortcutKey.Revoke = IniHelper.ProfileReadValue("ShortcutKey", "Revoke", IniHelper.parameterSetting);
            CommParameterSetting.ShortcutKey.SellOpen = IniHelper.ProfileReadValue("ShortcutKey", "SellOpen", IniHelper.parameterSetting);

            CommParameterSetting.ShortcutKey.IntBuyOpen = Convert.ToInt32(IniHelper.ProfileReadValue("ShortcutKey", "IntBuyOpen", IniHelper.parameterSetting));
            CommParameterSetting.ShortcutKey.IntClearance = Convert.ToInt32(IniHelper.ProfileReadValue("ShortcutKey", "IntClearance", IniHelper.parameterSetting));
            CommParameterSetting.ShortcutKey.IntClosingBuy = Convert.ToInt32(IniHelper.ProfileReadValue("ShortcutKey", "IntClosingBuy", IniHelper.parameterSetting));
            CommParameterSetting.ShortcutKey.IntClosingSell = Convert.ToInt32(IniHelper.ProfileReadValue("ShortcutKey", "IntClosingSell", IniHelper.parameterSetting));
            CommParameterSetting.ShortcutKey.IntRevoke = Convert.ToInt32(IniHelper.ProfileReadValue("ShortcutKey", "IntRevoke", IniHelper.parameterSetting));
            CommParameterSetting.ShortcutKey.IntSellOpen = Convert.ToInt32(IniHelper.ProfileReadValue("ShortcutKey", "IntSellOpen", IniHelper.parameterSetting));

            //消息提示
            CommParameterSetting.MessageAlert.OrderAlert = IniHelper.ProfileReadValue("MessageAlert", "OrderAlert", IniHelper.parameterSetting);
            CommParameterSetting.MessageAlert.RevokeAlert = IniHelper.ProfileReadValue("MessageAlert", "RevokeAlert", IniHelper.parameterSetting);
            CommParameterSetting.MessageAlert.TradeAlert = IniHelper.ProfileReadValue("MessageAlert", "TradeAlert", IniHelper.parameterSetting);

            ContractVariety.effectivityLenth = string.IsNullOrEmpty(IniHelper.ProfileReadValue("effectivity", "effectivityLenth", IniHelper.parameterSetting)) ? 100.00 : double.Parse(IniHelper.ProfileReadValue("effectivity", "effectivityLenth", IniHelper.parameterSetting));

        }
        #region 主窗口功能
        private void UIElement_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _previousMousePoint = e.GetPosition(this);
        }

        private void DockPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var v = e.GetPosition(this);
                bool b = Math.Abs(v.X - _previousMousePoint.X) >= 10 || Math.Abs(v.Y - _previousMousePoint.Y) >= 10;

                if (WindowState == WindowState.Maximized && b)
                {
                    this.Top = v.Y;
                    if (v.X > 0 && v.X <= _divideValue)
                    {
                        this.Left = 10;
                    }
                    else if (v.X > _divideValue && v.X <= MaxWidth / 2)
                    {
                        this.Left = _offSetValue;
                    }
                    else if (v.X > MaxWidth / 2 && v.X < MaxWidth - _divideValue)
                    {
                        if (MaxWidth - _size.Width >= _offSetValue)
                        {
                            this.Left = MaxWidth - _size.Width - _offSetValue;
                        }
                        else
                        {
                            this.Left = MaxWidth - _size.Width;
                        }
                    }
                    else
                    {
                        this.Left = MaxWidth - _size.Width;
                    }
                    SwitchMaxAndNomal();
                }

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    try
                    {
                        this.DragMove();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        private void MainWindow_OnStateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                StateChange.Source = new BitmapImage(new Uri("/PC_Futures;component/Resources/Images/ico_system_restore.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.WindowState == WindowState.Normal)
            {
                StateChange.Source = new BitmapImage(new Uri("/PC_Futures;component/Resources/Images/ico_system_maximize.png", UriKind.RelativeOrAbsolute));
            }
        }
        private void SwitchMaxAndNomal()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;

            }
            else if (this.WindowState == WindowState.Normal)
            {
                _size = RenderSize;
                this.WindowState = WindowState.Maximized;
            }
        }

        private void MainWindow_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var v = e.GetPosition(this);

            if (v.Y < 30)
            {
                SwitchMaxAndNomal();
            }
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState.Equals(WindowState.Normal))
            {
                _size = e.NewSize;
            }
        }

        private void State_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;
            switch (button.Name)
            {
                case "Max":
                    SwitchMaxAndNomal();
                    break;
                case "Min":
                    WindowState = WindowState.Minimized;
                    break;
                case "Close":
                    if (MessageBox.Show(this, "您确定要退出吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {

                        Close();
                    }
                    break;
            }
        }
        #endregion

        private void AuthCode_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            viewModel.TradeLoginViewModel.CreateImageSource();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string UserCode = ((System.Windows.FrameworkElement)(sender)).DataContext.ToString();
            viewModel.TradeLoginViewModel.DeleteUserChanged(UserCode);
        }


        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (ContractVariety.IsLoginSuccess)
            {
                if ((int)e.Key == CommParameterSetting.ShortcutKey.IntBuyOpen && CommParameterSetting.ShortcutKey.IntBuyOpen != 0)
                {
                    if (viewModel.SelectItemViewModel == null)
                    {
                        MessageBox.Show("请锁定合约", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    TransactionViewModel.Instance().SJBuyExecuteChanged();
                }
                else if ((int)e.Key == CommParameterSetting.ShortcutKey.IntClearance && CommParameterSetting.ShortcutKey.IntClearance != 0)
                {
                    if (viewModel.SelectItemViewModel == null)
                    {
                        MessageBox.Show("请锁定合约", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    PositionViewModel.Instance().TradeAllExecuteChanged();
                }
                else if ((int)e.Key == CommParameterSetting.ShortcutKey.IntClosingBuy && CommParameterSetting.ShortcutKey.IntClosingBuy != 0)
                {
                    if (viewModel.SelectItemViewModel == null)
                    {
                        MessageBox.Show("请锁定合约", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    TransactionViewModel.Instance().IsByClose = false;
                    TransactionViewModel.Instance().IsSellClose = true;
                    TransactionViewModel.Instance().BuyExecuteChanged();
                }
                else if ((int)e.Key == CommParameterSetting.ShortcutKey.IntClosingSell && CommParameterSetting.ShortcutKey.IntClosingSell != 0)
                {
                    if (viewModel.SelectItemViewModel == null)
                    {
                        MessageBox.Show("请锁定合约", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    TransactionViewModel.Instance().IsSellClose = true;
                    TransactionViewModel.Instance().IsByClose = false;
                    TransactionViewModel.Instance().SellExecuteChanged();
                }
                else if ((int)e.Key == CommParameterSetting.ShortcutKey.IntRevoke && CommParameterSetting.ShortcutKey.IntRevoke != 0)
                {
                    if (viewModel.SelectItemViewModel == null)
                    {
                        MessageBox.Show("请锁定合约", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    OrderCancelViewModel.Instance().SelectedItem = OrderCancelViewModel.Instance().Delegations.FirstOrDefault(x => x.ContractID == viewModel.SelectItemViewModel.ContractCode);
                    if (OrderCancelViewModel.Instance().SelectedItem == null) return;
                    OrderCancelViewModel.Instance().OrderCancelExecuteChanged();
                }
                else if ((int)e.Key == CommParameterSetting.ShortcutKey.IntSellOpen && CommParameterSetting.ShortcutKey.IntSellOpen != 0)
                {
                    if (viewModel.SelectItemViewModel == null)
                    {
                        MessageBox.Show("请锁定合约", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    TransactionViewModel.Instance().SJSellExecuteChanged();
                }
                //  Window_KeyUp(sender, e);
            }
            if (UserInfoHelper.IsHaveLogin)
            {
                Window_KeyUp(sender, e);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myTimer = new System.Timers.Timer(200);
            myTimer.Elapsed += myTimer_Elapsed;
            myTimer.Start();
        }
        public void SetOptionalList()
        {
            Optional.quotesDataGrid.ItemsSource = viewModel.TradeQuotesViewModel.OptionalList.ToList();
        }
        public void ReSetOptionalSelectItem()
        {
            if (viewModel.TradeQuotesViewModel.OptionalList.Count > 0)
            {
                Optional.quotesDataGrid.SelectedItem = null;
                Optional.quotesDataGrid.SelectedItem = viewModel.TradeQuotesViewModel.OptionalList[0];
            }
        }
        private void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var trademodel = viewModel.TradeQuotesViewModel;
                if (trademodel.IsOptionalCheck && trademodel.OptionalList.Count > 0)
                {
                    int rowIndex = -1;
                    //int i = 0;
                    List<string> subList = new List<string>();
                    List<string> viewList = new List<string>();
                    this.Optional.Dispatcher.Invoke(new Action(() =>
                    {
                        #region 自选
                        if (Optional.quotesDataGrid.Items.Count > 0)
                        {
                            for (int a = 0; a < Optional.quotesDataGrid.Items.Count; a++)
                            {
                                var item = Optional.quotesDataGrid.Items[a];
                                var Row = (DataGridRow)Optional.quotesDataGrid.ItemContainerGenerator.ContainerFromItem(item);
                                if (Row != null)
                                {
                                    if (Row.TransformToVisual(Optional.quotesDataGrid).Transform(new Point(0, 0)).Y + Row.ActualHeight >= Optional.quotesDataGrid.ActualHeight)
                                    {
                                        rowIndex = a;
                                        break;
                                    }
                                }
                            }
                            if (rowIndex == -1)
                            {
                                rowIndex = Optional.quotesDataGrid.Items.Count - 1;
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
                                var Row = (DataGridRow)Optional.quotesDataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
                                if (Row != null)
                                {
                                    FuturesViewModel stock = Row.DataContext as FuturesViewModel;
                                    if (stock != null)
                                    {
                                        if (!viewModel.QuotesSubscribed.Contains(stock.ContractCode) && !viewModel.SubscribedStocks.Contains(stock.ContractCode))
                                        {
                                            subList.Add(stock.ContractCode);
                                            viewModel.QuotesSubscribed.Add(stock.ContractCode);
                                        }
                                        viewList.Add(stock.ContractCode);
                                    }
                                }
                                rowIndex--;
                                j++;
                            } while (j < 28);
                        }
                        #endregion
                    }));
                    if (subList.Count > 0)
                    {
                        LogHelper.Info("自选股订阅:" + string.Join(",", subList));
                        _scoketManager.SendSubscribeInfo(7, subList);
                    }
                    FuturesViewModel selectModel = viewModel.SelectItemViewModel;
                    List<string> unSubList = new List<string>();
                    List<string> tempList = viewModel.QuotesSubscribed.ToList();
                    if (tempList.Count > 0)
                    {
                        foreach (var item in tempList)
                        {
                            if (!string.IsNullOrEmpty(item) && !viewList.Contains(item) && !viewModel.SubscribedStocks.Contains(item) && (selectModel == null || !string.Equals(selectModel.ContractCode, item)))
                            {
                                viewModel.QuotesSubscribed.Remove(item);
                                unSubList.Add(item);
                            }
                        }
                    }
                    if (unSubList.Count > 0)
                    {
                        LogHelper.Info("自选股反订阅:" + string.Join(",", unSubList));
                        _scoketManager.SendSubscribeInfo(8, unSubList);
                    }
                }



            }
            catch (Exception ex)
            {

                LogHelper.Info("Optional：myTimer_Elapsed" + ex.ToString());
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TradeLoginViewModel.GetInstance(null).LoginExecuteChanged();
            }
        }
        private int FsIndex = 0;
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    if (TradeQuotesViewModel.GetInstance(null).IsRealTimeCheck == true)
            //    {
            //        //打开K线
            //        TradeQuotesViewModel.GetInstance(null).IsDayKLineCheck = true;
            //    }
            //    else if (TradeQuotesViewModel.GetInstance(null).IsDayKLineCheck || TradeQuotesViewModel.GetInstance(null).IsFifteenMinKLineCheck || TradeQuotesViewModel.GetInstance(null).IsFiveMinKLineCheck
            //        || TradeQuotesViewModel.GetInstance(null).IsFourHoursKLineCheck || TradeQuotesViewModel.GetInstance(null).IsMouthKLineCheck || TradeQuotesViewModel.GetInstance(null).IsOneMinKLineCheck
            //        || TradeQuotesViewModel.GetInstance(null).IsSixtyMinKLineCheck || TradeQuotesViewModel.GetInstance(null).IsThirtyMinKLineCheck || TradeQuotesViewModel.GetInstance(null).IsThreeMinKLineCheck
            //        || TradeQuotesViewModel.GetInstance(null).IsTwoHoursKLineCheck || TradeQuotesViewModel.GetInstance(null).IsWeekKLineCheck)
            //    {
            //        //打开分时
            //        TradeQuotesViewModel.GetInstance(null).IsRealTimeCheck = true;
            //    }
            //    //e.Handled = true;
            //}
            //if (e.Key == Key.Up)
            //{
            //    if (TradeQuotesViewModel.GetInstance(null).IsRealTimeCheck == true)
            //    {
            //        if (TradeQuotesViewModel.GetInstance(null).PageIndex < 5)
            //        {
            //            TradeQuotesViewModel.GetInstance(null).PageIndex++;
            //            TradeQuotesViewModel.GetInstance(null).RealTimeChart.StopUpdate();
            //            TradeQuotesViewModel.GetInstance(null).PageIndexAdd();

            //        }
            //    }
            //}
            //if (e.Key == Key.Down)
            //{
            //    if (TradeQuotesViewModel.GetInstance(null).IsRealTimeCheck == true)
            //    {
            //        if (TradeQuotesViewModel.GetInstance(null).PageIndex > 0)
            //        {
            //            TradeQuotesViewModel.GetInstance(null).PageIndex--;
            //            if (TradeQuotesViewModel.GetInstance(null).PageIndex == 0)
            //            {
            //                TradeQuotesViewModel.GetInstance(null).RealTimeChart.StartUpdate();
            //            }
            //            TradeQuotesViewModel.GetInstance(null).RealTimeChart.StopUpdate();
            //            TradeQuotesViewModel.GetInstance(null).PageIndexAdd();

            //        }
            //    }
            //}
            //e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(this, "您确定要退出吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            if (this.check.IsChecked == true)
            {
                border.Height = 27;
            }
            else
            {
                border.Height = 320;

            }
        }
    }
}
