using PC_Futures.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// UCOrderCancel.xaml 的交互逻辑
    /// </summary>
    public partial class UCOrderCancel : UserControl
    {
        public UCOrderCancel()
        {
            InitializeComponent();
        }
        bool isContractCode = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("ContractCode", isContractCode);
             isContractCode = !isContractCode;
        }
        bool isDirection = false;
        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("Direction", isDirection);
            isDirection = !isDirection;
        }
        bool isOpenOffset = false;
        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("OpenOffset", isOpenOffset);
            isOpenOffset = !isOpenOffset;
        }
        bool isOrderStatus = false;
        private void Border_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("OrderStatus", isOrderStatus);
            isOrderStatus = !isOrderStatus;
        }
        bool isOrderPrice = false;
        private void Border_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("OrderPrice", isOrderPrice);
            isOrderPrice = !isOrderPrice;
        }
        bool isOrderVolume = false;
        private void Border_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("OrderVolume", isOrderVolume);
            isOrderVolume = !isOrderVolume;
        }
        bool isTradeVolume = false;
        private void Border_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("TradeVolume", isTradeVolume);
            isTradeVolume = !isTradeVolume;
        }

        bool isLeftVolume = false;
        private void Border_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("LeftVolume", isLeftVolume);
            isLeftVolume = !isLeftVolume;
        }

        bool isOrderTime = false;
        private void Border_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("OrderTime", isOrderTime);
            isOrderTime = !isOrderTime;
        }

        bool isShadowOrderID = false;
        private void Border_MouseLeftButtonDown_9(object sender, MouseButtonEventArgs e)
        {
            OrderCancelViewModel.Instance().Sorting("ShadowOrderID", isShadowOrderID);
            isShadowOrderID = !isShadowOrderID;

        }
    }
}
