using PC_Futures.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// ConditionBill.xaml 的交互逻辑
    /// </summary>
    public partial class UCConditionBill : UserControl
    {
        public UCConditionBill()
        {
            InitializeComponent();
        }
        bool ContractCode = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UCConditionBillViewModel.Instance().Sorting("ContractCode", ContractCode);
               ContractCode = !ContractCode;
        }
        bool Direction = false;
        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            UCConditionBillViewModel.Instance().Sorting("Direction", Direction);
            Direction = !Direction;
        }
       bool OpenOffset=false;
        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            UCConditionBillViewModel.Instance().Sorting("OpenOffset", OpenOffset);
            OpenOffset = !OpenOffset;
        }
        bool Status = false;
        private void Border_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            UCConditionBillViewModel.Instance().Sorting("Status", Status);
            Status = !Status;
        }
        bool TrrigerCond = false;
        private void Border_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            UCConditionBillViewModel.Instance().Sorting("TrrigerCond", TrrigerCond);
            TrrigerCond = !TrrigerCond;
        }
      bool  OrderPrice=false;
        private void Border_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            UCConditionBillViewModel.Instance().Sorting("OrderPrice", OrderPrice);
            OrderPrice = !OrderPrice;
        }
        bool OrderVolume = false;
        private void Border_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            UCConditionBillViewModel.Instance().Sorting("OrderVolume", OrderVolume);
            OrderVolume = !OrderVolume;

        }
        bool CreateTime = false;
        private void Border_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            UCConditionBillViewModel.Instance().Sorting("CreateTime", CreateTime);
            CreateTime = !CreateTime;
        }
        bool TrrigerTime = false;
        private void Border_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            UCConditionBillViewModel.Instance().Sorting("TrrigerTime", TrrigerTime);
            TrrigerTime = !TrrigerTime;
        }
    }
}
