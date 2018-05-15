using PC_Futures.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// UCPosition.xaml 的交互逻辑
    /// </summary>
    public partial class UCPositionAll : UserControl
    {
        public UCPositionAll()
        {
            InitializeComponent();
        }
        bool isContractCode = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().Sorting("ContractCode", isContractCode);
            isContractCode = !isContractCode;
        }

        bool isDirection=false;
        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().Sorting("Direction", isDirection);
            isDirection = !isDirection;
        }

        bool isOpenPrice = false;
        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().Sorting("OpenPrice", isOpenPrice);
            isOpenPrice = !isOpenPrice;
        }

        bool isPositionVolume = false;
        private void Border_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().Sorting("PositionVolume", isPositionVolume);
            isPositionVolume = !isPositionVolume;
        }
        bool isAbleVolume = false;
        private void Border_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().Sorting("AbleVolume", isAbleVolume);
            isAbleVolume = !isAbleVolume;
        }
        bool isPositionProfitLoss = false;
        private void Border_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().Sorting("PositionProfitLoss", isPositionProfitLoss);
            isPositionProfitLoss = !isPositionProfitLoss;
        }

        bool PositionProfitLossJB = false;
        private void Border_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().Sorting("PositionProfitLossJB", PositionProfitLossJB);
            PositionProfitLossJB = !PositionProfitLossJB;
        }
        bool UseMargin = false;
        private void Border_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().Sorting("UseMargin", UseMargin);
            UseMargin = !UseMargin;
        }

        bool ContractCode = false;
        private void Border_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().DetSorting("ContractCode", ContractCode);
            ContractCode = !ContractCode;
        }
        bool Direction = false;
        private void Border_MouseLeftButtonDown_9(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().DetSorting("Direction", Direction);
            Direction = !Direction;
        }
       bool OpenPrice=false;
        private void Border_MouseLeftButtonDown_10(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().DetSorting("OpenPrice", OpenPrice);
            OpenPrice = !OpenPrice;
        }
        bool isdetAbleVolume = false;
        private void Border_MouseLeftButtonDown_11(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().DetSorting("AbleVolume", isdetAbleVolume);
            isdetAbleVolume = !isdetAbleVolume;
        }
       bool PositionProfitLoss=false;
        private void Border_MouseLeftButtonDown_12(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().DetSorting("PositionProfitLoss", PositionProfitLoss);
            PositionProfitLoss = !PositionProfitLoss;
        }
        bool ShadowTradeId = false;
        private void Border_MouseLeftButtonDown_13(object sender, MouseButtonEventArgs e)
        {
            PositionAllViewModel.Instance().DetSorting("ShadowTradeId", ShadowTradeId);
            ShadowTradeId = !ShadowTradeId;
        }
    }
}
