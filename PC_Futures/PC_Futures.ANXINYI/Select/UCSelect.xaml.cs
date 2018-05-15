using PC_Futures.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// UCSelect.xaml 的交互逻辑
    /// </summary>
    public partial class UCSelect : UserControl
    {
        DescriptViewModel dvm = null;
        public UCSelect()
        {
            InitializeComponent();
            dvm = DescriptViewModel.Instance();
            this.DataContext = dvm;
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            dvm.GotFocus();
        }

        private void TabItem_GotFocus_1(object sender, RoutedEventArgs e)
        {
            dvm.GotFocus1();
        }
    }
}
