using PC_Futures.ViewModel;
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
using System.Windows.Shapes;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        LoginViewModel lvm = null;
        public Login()
        {
            InitializeComponent();
            lvm = new LoginViewModel();
            this.DataContext = lvm;
            if (lvm.CloseAction == null)
            {
                lvm.CloseAction = () => { this.Close(); };
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
                    //if (MessageBox.Show(this, "您确定要退出吗？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    //{

                    //    Close();
                    //}
                    Close();
                    break;
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
               
                this.WindowState = WindowState.Maximized;
            }
        }
    }
}
