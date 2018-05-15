using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// UCTransaction.xaml 的交互逻辑
    /// </summary>
    public partial class UCTransaction : UserControl
    {
        public UCTransaction()
        {
            InitializeComponent();
            cd += Change;
            cd1 += Change1;
        }

        private void WatermarkComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;

        }

        private void TextBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            pop.PlacementTarget = price;
            if (pop.IsOpen)
            {
                pop.IsOpen = false;
            }
            else
            {
                pop.IsOpen = true;
            }

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            pop.IsOpen = false;
            string priceType = listPrice.SelectedItem.ToString();
            TransactionViewModel.Instance().TypeChangedCommandExecuteChanged(priceType);
        }

        private void listPrice2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            pop1.IsOpen = false;
            string priceType = listPrice2.SelectedItem.ToString();
            TransactionViewModel.Instance().TypeChangedCommandExecuteChanged(priceType);

        }

        private void price1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            pop1.PlacementTarget = price1;
            if (pop1.IsOpen)
            {
                pop1.IsOpen = false;
            }
            else
            {
                pop1.IsOpen = true;
            }

        }

        private void price2_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            pop2.PlacementTarget = price2;
            if (pop2.IsOpen)
            {
                pop2.IsOpen = false;
            }
            else
            {
                pop2.IsOpen = true;
            }
        }

        private void listPrice2_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            pop2.IsOpen = false;
            string priceType = listPrice23.SelectedItem.ToString();
            TransactionViewModel.Instance().TypeChangedCommandExecuteChanged(priceType);

        }

        private void price1_LostFocus(object sender, RoutedEventArgs e)
        {
            pop1.IsOpen = false;
        }

        private void price_LostFocus(object sender, RoutedEventArgs e)
        {
            pop.IsOpen = false;
        }

        private void price2_LostFocus(object sender, RoutedEventArgs e)
        {
            pop2.IsOpen = false;
        }

        public delegate void ChangeDelegate();
        public static ChangeDelegate cd = null;
        public void Change()
        {
            listPrice23.SelectedIndex = 0;
            listPrice.SelectedIndex = 0;
            listPrice2.SelectedIndex = 0;
        }
        public delegate void ChangeDelegate1();
        public static ChangeDelegate1 cd1 = null;
        public void Change1()
        {
            listPrice23.SelectedIndex = 1;
            listPrice.SelectedIndex = 1;
            listPrice2.SelectedIndex = 1;
        }

        private void price_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (listPrice.SelectedItem.ToString() == "限价")
            {
                try
                {
                    if (!string.IsNullOrEmpty(tb.Text) && Convert.ToDouble(tb.Text) <= 0)
                    {
                        MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("价格只能为大于0的数!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }
    }
}
