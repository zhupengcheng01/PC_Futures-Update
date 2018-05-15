using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// StockTicker.xaml 的交互逻辑
    /// </summary>
    public partial class StockTicker : UserControl
    {
        Storyboard _flash;
        bool _firstTime = true;
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(StockTicker),
            new PropertyMetadata(0.0, ValueChanged));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty PrecyProperty =
            DependencyProperty.Register(
            "Precy",
            typeof(int),
            typeof(StockTicker),
            null);
        public int Precy
        {
            get { return (int)GetValue(PrecyProperty); }
            set { SetValue(PrecyProperty, value); }
        }
        public StockTicker()
        {
            InitializeComponent();
            _flash = (Storyboard)Resources["_sbFlash"];
        }
        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as StockTicker).ValueChanged(e);
        }

        private void ValueChanged(DependencyPropertyChangedEventArgs e)
        {
            var value = (double)e.NewValue;
            var oldValue = (double)e.OldValue;
            if (double.IsNaN(value))
            {
                this._flash.Stop();
                this._root.Background = new SolidColorBrush(Colors.Transparent);
                //this._txtChange.Foreground = this._txtValue.Foreground;
                return;
            }
            
            var change = oldValue == 0 || double.IsNaN(oldValue) ? 0 : (value - oldValue) / oldValue;
            string lformat = "F" + Precy;
            this._txtValue.Text = value.ToString(lformat);//value.ToString("F2");
            //this._txtChange.Text = change.ToString();

            // update flash color
            var ca = this._flash.Children[0] as ColorAnimation;

            // update symbol
            if (change == 0)
            {
                //this._txtChange.Foreground = this._txtValue.Foreground;
            }
            else if (change > 0.0001)
            {
                ca.From = Colors.Red;
            }
            else if (change < 0.0001)
            {
                ca.From = Colors.Green;
            }
            // flash new value (but not right after the control was created)
            if (!this._firstTime && (Math.Abs(change) > 0))
            {
                this._flash.Begin();
            }
            this._firstTime = false;
        }
    }
}
