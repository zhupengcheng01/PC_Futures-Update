using PC_Futures.ViewModel;
using System;
using System.Collections;
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
using Utilities;

namespace PC_Futures.ANXINYI
{
    /// <summary>
    /// QuotesDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class QuotesDataGrid : UserControl
    {
        private MainViewModel _mainVM;
        public QuotesDataGrid()
        {
            InitializeComponent();
            quotesDataGrid.FontSize = 13;
            quotesDataGrid.RowHeight = 25;
            quotesDataGrid.ColumnHeaderHeight = 25;
        }
        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e == null || e.AddedItems.Count <= 0)
                return;
            if (_mainVM == null)
            {
                _mainVM = MainViewModel.GetInstance();
            }
            _mainVM.SelectItemViewModel = e?.AddedItems[0] as FuturesViewModel;
        }
        private void DataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           // TradeQuotesViewModel.GetInstance(null).IsRealTimeCheck = true;
            //_callBack?.OnMouseDoubleClick(sender, e);
        }
        private void DataGridHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ContextMenu_Loaded(object sender, RoutedEventArgs e)
        {
            ContextMenu item = sender as ContextMenu;
            if (item == null) return;
            FuturesViewModel stock = item.DataContext as FuturesViewModel;
            if (stock == null) return;
            var model = TradeInfoHelper.OptionalModelList.FirstOrDefault(o => string.Equals(o.contract_id, stock.ContractCode));
            if (model == null)
            {
                stock.IsOptionalStock = false;
            }
            else
            {
                stock.IsOptionalStock = true;
                stock.OptionalSerialNumber = model.serial_number;
            }
        }
        public void SetDataGridStyle()
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TradeQuotesViewModel.GetInstance(null).SetDataGridStyleHandler -= QuotesDataGrid_SetDataGridStyleHandler;
            TradeQuotesViewModel.GetInstance(null).SetDataGridStyleHandler += QuotesDataGrid_SetDataGridStyleHandler;
        }

        private void QuotesDataGrid_SetDataGridStyleHandler(object sender, EventArgs e)
        {
            if (!TradeQuotesViewModel.GetInstance(null).IsQuoteCheck)
                return;
            int? type = sender as int?;
            if (type != null)
            {
                if (type == 1)//放大
                {
                    if (quotesDataGrid.FontSize > 25)
                        return;
                    quotesDataGrid.ColumnHeaderHeight = quotesDataGrid.ColumnHeaderHeight + 1;
                    quotesDataGrid.RowHeight = quotesDataGrid.RowHeight + 1;
                    quotesDataGrid.FontSize = quotesDataGrid.FontSize + 1;
                    foreach (var item in quotesDataGrid.Columns)
                    {
                        item.Width = item.ActualWidth + 2;
                    }
                }
                else
                {
                    if (quotesDataGrid.FontSize <= 13)
                        return;
                    quotesDataGrid.ColumnHeaderHeight = quotesDataGrid.ColumnHeaderHeight - 1;
                    quotesDataGrid.RowHeight = quotesDataGrid.RowHeight - 1;
                    quotesDataGrid.FontSize = quotesDataGrid.FontSize - 1;
                    foreach (var item in quotesDataGrid.Columns)
                    {
                        item.Width = item.ActualWidth - 2;
                    }
                }
                //var aa = type;
                ////var bb= FindResource("StockSellStyle") as Style;
                //quotesDataGrid.fon
                //quotesDataGrid.Style.Setters.Add(new Setter(FontSizeProperty,25));
                //foreach (Setter item in quotesDataGrid.Style.Setters)
                //{
                //    if(item.Property.Name.Equals("FontSize"))
                //    {
                //        new Setter(FontSizeProperty, 25);
                //    }
                //}
                //var aa=  quotesDataGrid.Style.Setters.Count;
            }

        }
    }
}
