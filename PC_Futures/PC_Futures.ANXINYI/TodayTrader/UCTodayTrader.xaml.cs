using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// UCTodayTrader.xaml 的交互逻辑
    /// </summary>
    public partial class UCTodayTrader : UserControl
    {
        public UCTodayTrader()
        {
            InitializeComponent();
        }
        bool ContractCode = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().Sorting("ContractCode", ContractCode);
            ContractCode = !ContractCode;
        }
        bool Direction = false;
        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().Sorting("Direction", Direction);
            Direction = !Direction;
        }
        bool OpenOffset = false;
        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().Sorting("OpenOffset", OpenOffset);
            OpenOffset = !OpenOffset;
        }
        bool TradePrice = false;
        private void Border_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().Sorting("TradePrice", TradePrice);
            TradePrice = !TradePrice;
        }
        bool ShadowOrderID = false;
        private void Border_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().Sorting("ShadowOrderID", ShadowOrderID);
            ShadowOrderID = !ShadowOrderID;
        }
        bool TradeVolume = false;
        private void Border_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().Sorting("TradeVolume", TradeVolume);
            TradeVolume = !TradeVolume;
        }
        bool TradeTime = false;
        private void Border_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().Sorting("TradeTime", TradeTime);
            TradeTime = !TradeTime;
        }
        bool ShadowTradeID=false;
        private void Border_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().Sorting("ShadowTradeID", ShadowTradeID);
            ShadowTradeID = !ShadowTradeID;
        }
        bool isContractCode = false;
        private void Border_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().ALLSorting("ContractCode", isContractCode);
            isContractCode = !isContractCode;
        }
        bool isDirection = false;
        private void Border_MouseLeftButtonDown_9(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().ALLSorting("Direction", isDirection);
            isDirection = !isDirection;
        }
        bool isOpenOffset = false;
        private void Border_MouseLeftButtonDown_10(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().ALLSorting("OpenOffset", isOpenOffset);
            isOpenOffset = !isOpenOffset;
        }
        bool isTradePrice = false;
        private void Border_MouseLeftButtonDown_11(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().ALLSorting("TradePrice", isTradePrice);
            isTradePrice = !isTradePrice;

        }
        bool OrderOrderref = false;
        private void Border_MouseLeftButtonDown_12(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().ALLSorting("OrderOrderref", OrderOrderref);
            OrderOrderref = !OrderOrderref;
        }
        bool isTradeVolume = false;
        private void Border_MouseLeftButtonDown_13(object sender, MouseButtonEventArgs e)
        {
            TodayTraderViewModels.Instance().ALLSorting("TradeVolume", isTradeVolume);
            isTradeVolume = !isTradeVolume;
        }
    }
}
