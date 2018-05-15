using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Utilities;

namespace PC_Futures
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Current.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }
        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                LogHelper.Info("崩溃错误信息：" + e.Exception.ToString());
                //MessageBox.Show("当前应用程序遇到一些错误，操作已经终止" + Environment.NewLine + e.Exception.ToString());
                //MessageBox.Show("系统出现异常", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                LogHelper.Info("崩溃错误异常信息：" + ex.ToString());
                //MessageBox.Show("当前应用程序遇到一些错误，将要退出！" + ex.ToString());
                //MessageBox.Show("系统有异常", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                LogHelper.Info("非UI线程抛出的未处理消息" + e.ExceptionObject.ToString());
                //MessageBox.Show("当前应用程序遇到一些问题，操作已经终止" + Environment.NewLine + e.ExceptionObject.ToString());
                //MessageBox.Show("当前系统出现异常", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                LogHelper.Info("非UI线程抛出的未处理异常消息" + ex.ToString());
                //MessageBox.Show("当前应用程序遇到一些问题，将要退出！" + ex.ToString());
                //MessageBox.Show("当前系统有异常", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        protected override void OnStartup(StartupEventArgs e)
        {

            AutoUpdater.SetAppName("外盘期货风控端");
            string autoUpdateAddr = ConfigurationManager.AppSettings["AutoUpdateAddr"];
            AutoUpdater.Start(autoUpdateAddr);

            Application.Current.StartupUri = new Uri("/PC_Futures.ANXINYI;component/Login.xaml", UriKind.Relative);

        }

    }
}
