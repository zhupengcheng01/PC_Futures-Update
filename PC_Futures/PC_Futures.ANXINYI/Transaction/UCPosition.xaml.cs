using PC_Futures.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// UCPosition.xaml 的交互逻辑
    /// </summary>
    public partial class UCPosition : UserControl
    {
        public UCPosition()
        {
            InitializeComponent();
        }
        bool isContractCode = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PositionViewModel.Instance().Sorting("ContractCode", isContractCode);
            isContractCode = !isContractCode;
        }
        bool isDirection = false;
        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            PositionViewModel.Instance().Sorting("Direction", isDirection);
            isDirection = !isDirection;
        }

        bool isPositionVolume = false;

        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            PositionViewModel.Instance().Sorting("PositionVolume", isPositionVolume);
            isPositionVolume = !isPositionVolume;

        }
        bool isAbleVolume = false;
        private void Border_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            PositionViewModel.Instance().Sorting("AbleVolume", isAbleVolume);
            isAbleVolume = !isAbleVolume;
        }

        bool isOpenPrice = false;
        private void Border_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            PositionViewModel.Instance().Sorting("OpenPrice", isOpenPrice);
            isOpenPrice = !isOpenPrice;
        }
        bool isUseMargin = false;
        private void Border_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            PositionViewModel.Instance().Sorting("UseMargin", isUseMargin);
            isUseMargin = !isUseMargin;
        }
        bool isPositionProfitLoss = false;
        private void Border_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            PositionViewModel.Instance().Sorting("PositionProfitLoss", isPositionProfitLoss);
            isPositionProfitLoss = !isPositionProfitLoss;
        }
    }
}
