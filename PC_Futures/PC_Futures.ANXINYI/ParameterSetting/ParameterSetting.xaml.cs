using PC_Futures.ViewModel;
using System.Windows;
using System.Windows.Input;
using Utilities;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// ParameterSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterSetting : Window
    {
        ParameterSetViewModel psvm = null;
        public ParameterSetting()
        {
            InitializeComponent();
            psvm = new ParameterSetViewModel();
            this.DataContext = psvm;
            if (psvm.CloseAction == null)
            {
                psvm.CloseAction = () => { this.Close(); };
            }
        }
    
        private void BuyOpen_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            psvm.ShortcutKey.IntBuyOpen = (int)e.Key;
            CommParameterSetting.ShortcutKey.IntBuyOpen = psvm.ShortcutKey.IntBuyOpen;
        }

        private void SellOpen_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            psvm.ShortcutKey.IntSellOpen = (int)e.Key;
            CommParameterSetting.ShortcutKey.IntSellOpen = psvm.ShortcutKey.IntSellOpen;
        }

        private void ClosingBuy_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            psvm.ShortcutKey.IntClosingBuy = (int)e.Key;
            CommParameterSetting.ShortcutKey.IntClosingBuy = psvm.ShortcutKey.IntClosingBuy;
        }

        private void ClosingSell_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            psvm.ShortcutKey.IntClosingSell = (int)e.Key;
            CommParameterSetting.ShortcutKey.IntClosingSell = psvm.ShortcutKey.IntClosingSell;
        }

        private void Revoke_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            psvm.ShortcutKey.IntRevoke = (int)e.Key;
            CommParameterSetting.ShortcutKey.IntRevoke = psvm.ShortcutKey.IntRevoke;
        }

        private void Clearance_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            psvm.ShortcutKey.IntClearance = (int)e.Key;
            CommParameterSetting.ShortcutKey.IntClearance = psvm.ShortcutKey.IntClearance;
        }

        //    private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        //    {
        //    }
        //    private void BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        //    {
        //    }
        //    /// <summary>
        //    /// 可供选择的数据列表
        //    /// </summary>        
        //    public ObservableCollection<DataItem> SelectionList
        //    {
        //        get { return _selectionList; }
        //        set { _selectionList = value; }
        //    }
        //    private ObservableCollection<DataItem> _selectionList = new ObservableCollection<DataItem>();

        //    private void Window_Loaded(object sender, RoutedEventArgs e)
        //    {


        //        SelectionList.Add(new DataItem() { Value = 0, ShowText = "买" });
        //        SelectionList.Add(new DataItem() { Value = 0, ShowText = "卖" });
        //        LoadPara();

        //    }
        //    private void LoadPara()
        //    {
        //      //  XctkADelegate.Value = CommParameterSetting.StopLossModel.ADelegateSecond;
        //        xctkBuy.Value = CommParameterSetting.StopLossModel.BuyNum;
        //      //  ckADelegate.IsChecked = CommParameterSetting.StopLossModel.CkADelegate;
        //        ckBuy.IsChecked = CommParameterSetting.StopLossModel.CkBuy;
        //       // ckReADelegate.IsChecked = CommParameterSetting.StopLossModel.CkReADelegate;
        //        ckSell.IsChecked = CommParameterSetting.StopLossModel.CkSell;
        //        //dpValidity.SelectedDate = Convert.ToDateTime(CommParameterSetting.StopLossModel.DateValidity);
        //      //  XctkReADelegate.Value = CommParameterSetting.StopLossModel.ReADelegateAllSecond;
        //        xctkSell.Value = CommParameterSetting.StopLossModel.SellNum;

        //        StopLossDelegation.Text = CommParameterSetting.StopLossModel.StopLossDelegation;
        //      //  xctkStopLoss.Value = CommParameterSetting.StopLossModel.StopLoss;
        //      //  xctkStopProfit.Value = CommParameterSetting.StopLossModel.StopProfit;
        //      //  StopProfitDelegation.Text = CommParameterSetting.StopLossModel.StopProfitDelegation;

        //        comStopLoss.Text = CommParameterSetting.StopLossModel.StopLossPrice;
        //       // comStopProfit.Text = CommParameterSetting.StopLossModel.StopProfitPrice;
        //        //加载自动止盈止损 自动盈亏是用；分割行，分割列；

        //        //下单版设置
        //        ckOrderEnter.IsChecked = CommParameterSetting.OrderVersion.BeforeOrderEnter;
        //        defultLot.Value = CommParameterSetting.OrderVersion.DefaultLot;
        //        maxLot.Value = CommParameterSetting.OrderVersion.MaxLot;
        //        //快捷键设置
        //        ckLockMenu.IsChecked = CommParameterSetting.ShortcutKey.LockMenu;
        //        BuyOpen.Text = CommParameterSetting.ShortcutKey.BuyOpen;
        //        Clearance.Text = CommParameterSetting.ShortcutKey.Clearance;
        //        ClosingBuy.Text = CommParameterSetting.ShortcutKey.ClosingBuy;
        //        ClosingSell.Text = CommParameterSetting.ShortcutKey.ClosingSell;
        //        Revoke.Text = CommParameterSetting.ShortcutKey.Revoke;
        //        SellOpen.Text = CommParameterSetting.ShortcutKey.SellOpen;
        //        //消息提示
        //        XDHint.Text = CommParameterSetting.MessageAlert.OrderAlert;
        //        CDHint.Text = CommParameterSetting.MessageAlert.RevokeAlert;
        //        CJHint.Text = CommParameterSetting.MessageAlert.TradeAlert;
        //    }


        //    private void Button_Click(object sender, RoutedEventArgs e)
        //    {
        //        try
        //        {
        //           // CommParameterSetting.StopLossModel.ADelegateSecond = XctkADelegate.Value == null ? 0 : (int)XctkADelegate.Value;
        //            CommParameterSetting.StopLossModel.BuyNum = xctkBuy.Value == null ? 0 : (int)xctkBuy.Value;
        //           // CommParameterSetting.StopLossModel.CkADelegate = ckADelegate.IsChecked == true ? true : false;
        //            CommParameterSetting.StopLossModel.CkBuy = ckBuy.IsChecked == true ? true : false;
        //          //  CommParameterSetting.StopLossModel.CkReADelegate = ckReADelegate.IsChecked == true ? true : false;
        //            CommParameterSetting.StopLossModel.CkSell = ckSell.IsChecked == true ? true : false;
        //            //  CommParameterSetting.StopLossModel.DateValidity = dpValidity.Text == null ? "0000-00-00 00:00:00" : Convert.ToDateTime(dpValidity.SelectedDate).ToString("yyyy-MM-dd HH:mm:ss");
        //          //  CommParameterSetting.StopLossModel.ReADelegateAllSecond = XctkReADelegate.Value == null ? 0 : (int)XctkReADelegate.Value;
        //            CommParameterSetting.StopLossModel.SellNum = xctkSell.Value == null ? 0 : (int)xctkSell.Value;
        //           // CommParameterSetting.StopLossModel.StopLoss = xctkStopLoss.Value == null ? 0 : (int)xctkStopLoss.Value;
        //            CommParameterSetting.StopLossModel.StopLossDelegation = StopLossDelegation.Text;
        //            CommParameterSetting.StopLossModel.StopLossPrice = comStopLoss.Text;
        //          //  CommParameterSetting.StopLossModel.StopProfit = xctkStopProfit.Value == null ? 0 : (int)xctkStopProfit.Value;
        //            //CommParameterSetting.StopLossModel.StopProfitDelegation = StopProfitDelegation.Text;
        //          //  CommParameterSetting.StopLossModel.StopProfitPrice = comStopProfit.Text;

        //          //  IniHelper.ProfileWriteValue("CheckFullStop", "ADelegateSecond", CommParameterSetting.StopLossModel.ADelegateSecond.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("CheckFullStop", "BuyNum", CommParameterSetting.StopLossModel.BuyNum.ToString(), IniHelper.parameterSetting);
        //           // IniHelper.ProfileWriteValue("CheckFullStop", "CkADelegate", CommParameterSetting.StopLossModel.CkADelegate.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("CheckFullStop", "CkBuy", CommParameterSetting.StopLossModel.CkBuy.ToString(), IniHelper.parameterSetting);
        //           // IniHelper.ProfileWriteValue("CheckFullStop", "CkReADelegate", CommParameterSetting.StopLossModel.CkReADelegate.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("CheckFullStop", "CkSell", CommParameterSetting.StopLossModel.CkSell.ToString(), IniHelper.parameterSetting);
        //            //IniHelper.ProfileWriteValue("CheckFullStop", "DateValidity", CommParameterSetting.StopLossModel.DateValidity.ToString(), IniHelper.parameterSetting);
        //            //IniHelper.ProfileWriteValue("CheckFullStop", "ReADelegateAllSecond", CommParameterSetting.StopLossModel.ReADelegateAllSecond.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("CheckFullStop", "SellNum", CommParameterSetting.StopLossModel.SellNum.ToString(), IniHelper.parameterSetting);
        //           // IniHelper.ProfileWriteValue("CheckFullStop", "StopLoss", CommParameterSetting.StopLossModel.StopLoss.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("CheckFullStop", "StopLossDelegation", CommParameterSetting.StopLossModel.StopLossDelegation.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("CheckFullStop", "StopLossPrice", CommParameterSetting.StopLossModel.StopLossPrice.ToString(), IniHelper.parameterSetting);
        //            //IniHelper.ProfileWriteValue("CheckFullStop", "StopProfit", CommParameterSetting.StopLossModel.StopProfit.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("CheckFullStop", "StopProfitDelegation", CommParameterSetting.StopLossModel.StopProfitDelegation.ToString(), IniHelper.parameterSetting);
        //            //IniHelper.ProfileWriteValue("CheckFullStop", "StopProfitPrice", CommParameterSetting.StopLossModel.StopProfitPrice.ToString(), IniHelper.parameterSetting);


        //            ////加载自动止盈止损 自动盈亏是用；分割行，分割列；
        //            //string AutoStopLossModel = IniHelper.ProfileReadValue("AutoCheckFullStop", "AutoStopLoss", IniHelper.parameterSetting);
        //            //// public static List<AutoStopLossModel> AutoStopLossModel = new List<AutoStopLossModel>();
        //            //if (!string.IsNullOrEmpty(AutoStopLossModel))
        //            //{
        //            //    string[] ModelCount = AutoStopLossModel.Split(';');
        //            //    for (int i = 0; i < ModelCount.Length; i++)
        //            //    {
        //            //        string[] paras = ModelCount[i].Split(',');
        //            //        AutoStopLossModel aspl = new AutoStopLossModel();
        //            //        aspl.Agreement = paras[0];
        //            //        aspl.Direction = paras[1];
        //            //        aspl.FloatingProfitAndLoss = string.IsNullOrEmpty(paras[2]) ? 0 : int.Parse(paras[2]);
        //            //        aspl.StopLossPotion = string.IsNullOrEmpty(paras[3]) ? 0 : int.Parse(paras[3]);
        //            //        aspl.StopProfitPotion = string.IsNullOrEmpty(paras[4]) ? 0 : int.Parse(paras[4]);
        //            //        CommParameterSetting.AutoStopLossModel.Add(aspl);
        //            //    }
        //            //}
        //            psvm.AutoStopLossModel();

        //            //if (grid.Items.Count > 0)
        //            //{
        //            //    string para = "";
        //            //    for (int i = 0; i < grid.Items.Count; i++)
        //            //    {
        //            //        DataRowView selectItem = grid.Items[i] as DataRowView;
        //            //        para += selectItem[0] + "," + selectItem[1] + "," + selectItem[2] + "," + selectItem[3] + "," + selectItem[2] + ";";
        //            //    }
        //            //    IniHelper.ProfileWriteValue("AutoCheckFullStop", "AutoStopLoss", para.TrimEnd(';'), IniHelper.parameterSetting);
        //            //    //foreach (AutoStopLossModelViewModel item in Aslmvm)
        //            //    //{
        //            //    //    para += item.Agreement + "," + item.Direction + "," + item.FloatingProfitAndLoss + "," + item.StopLossPotion + "," + item.StopProfitPotion + ";";
        //            //    //}



        //            //}
        //            //下单版设置
        //            CommParameterSetting.OrderVersion.BeforeOrderEnter = ckOrderEnter.IsChecked == true ? true : false;
        //            CommParameterSetting.OrderVersion.DefaultLot = defultLot.Value == null ? 0 : (int)defultLot.Value;
        //            CommParameterSetting.OrderVersion.MaxLot = maxLot.Value == null ? 0 : (int)maxLot.Value;
        //            IniHelper.ProfileWriteValue("OrderVersion", "BeforeOrderEnter", CommParameterSetting.OrderVersion.BeforeOrderEnter.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("OrderVersion", "DefaultLot", CommParameterSetting.OrderVersion.DefaultLot.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("OrderVersion", "MaxLot", CommParameterSetting.OrderVersion.MaxLot.ToString(), IniHelper.parameterSetting);
        //            //快捷键设置
        //            CommParameterSetting.ShortcutKey.LockMenu = ckLockMenu.IsChecked == true ? true : false;
        //            CommParameterSetting.ShortcutKey.BuyOpen = BuyOpen.Text;
        //            CommParameterSetting.ShortcutKey.Clearance = Clearance.Text;
        //            CommParameterSetting.ShortcutKey.ClosingBuy = ClosingBuy.Text;
        //            CommParameterSetting.ShortcutKey.ClosingSell = ClosingSell.Text;
        //            CommParameterSetting.ShortcutKey.Revoke = Revoke.Text;
        //            CommParameterSetting.ShortcutKey.SellOpen = SellOpen.Text;
        //            IniHelper.ProfileWriteValue("ShortcutKey", "LockMenu", CommParameterSetting.ShortcutKey.LockMenu.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("ShortcutKey", "BuyOpen", CommParameterSetting.ShortcutKey.BuyOpen.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("ShortcutKey", "Clearance", CommParameterSetting.ShortcutKey.Clearance.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("ShortcutKey", "ClosingBuy", CommParameterSetting.ShortcutKey.ClosingBuy.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("ShortcutKey", "ClosingSell", CommParameterSetting.ShortcutKey.ClosingSell.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("ShortcutKey", "Revoke", CommParameterSetting.ShortcutKey.Revoke.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("ShortcutKey", "SellOpen", CommParameterSetting.ShortcutKey.SellOpen.ToString(), IniHelper.parameterSetting);


        //            //消息提示
        //            CommParameterSetting.MessageAlert.OrderAlert = XDHint.Text;
        //            CommParameterSetting.MessageAlert.RevokeAlert = CDHint.Text;
        //            CommParameterSetting.MessageAlert.TradeAlert = CJHint.Text;
        //            IniHelper.ProfileWriteValue("MessageAlert", "OrderAlert", CommParameterSetting.MessageAlert.OrderAlert.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("MessageAlert", "RevokeAlert", CommParameterSetting.MessageAlert.RevokeAlert.ToString(), IniHelper.parameterSetting);
        //            IniHelper.ProfileWriteValue("MessageAlert", "TradeAlert", CommParameterSetting.MessageAlert.TradeAlert.ToString(), IniHelper.parameterSetting);

        //        }
        //        catch (Exception ex)
        //        {
        //            LogHelper.Info(ex.Message);
        //        }
        //        this.Close();
        //    }

        //    private void Button_Click_1(object sender, RoutedEventArgs e)
        //    {
        //        this.Close();
        //    }


    }
    //public class DataItem
    //{
    //    public int Value { get; set; }
    //    public string ShowText { get; set; }
    //}

}
