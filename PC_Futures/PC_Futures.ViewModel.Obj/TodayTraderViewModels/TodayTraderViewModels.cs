using MicroMvvm;
using Newtonsoft.Json;
using PC_Futures.Models;
using PC_Futures.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PC_Futures
{
    public class TodayTraderViewModels : ObservableObject
    {
        private ObservableCollection<TodayTraderModelViewModel> _TodayTraderList = new ObservableCollection<TodayTraderModelViewModel>();
        public ObservableCollection<TodayTraderModelViewModel> TodayTraderList
        {
            get { return _TodayTraderList; }
            set
            {
                if (_TodayTraderList != value)
                {
                    _TodayTraderList = value;
                    RaisePropertyChanged("TodayTraderList");
                }
            }

        }

        private ObservableCollection<TodayTraderModelViewModel> _TodayTraderListAll = new ObservableCollection<TodayTraderModelViewModel>();
        public ObservableCollection<TodayTraderModelViewModel> TodayTraderListALL
        {
            get { return _TodayTraderListAll; }
            set
            {
                if (_TodayTraderListAll != value)
                {
                    _TodayTraderListAll = value;
                    RaisePropertyChanged("TodayTraderListAll");
                }
            }

        }
        private TodayTraderModelViewModel _SelectedItem;
        public TodayTraderModelViewModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }

        }

        private TodayTraderModelViewModel _SelectedItemAll;
        public TodayTraderModelViewModel SelectedItemAll
        {
            get { return _SelectedItemAll; }
            set
            {
                if (_SelectedItemAll != value)
                {
                    _SelectedItemAll = value;
                    RaisePropertyChanged("SelectedItemAll");
                }
            }

        }
        private static TodayTraderViewModels _TodayTraderViewModels;

        public static TodayTraderViewModels Instance()
        {
            if (_TodayTraderViewModels == null)
            {
                _TodayTraderViewModels = new TodayTraderViewModels();
            }
            return _TodayTraderViewModels;
        }

        private TodayTraderViewModels()
        {
            //LoadData();

        }
        private void LoadData()
        {
            //成交明细
            ReqPotion rp = new ReqPotion();
            rp.cmdcode = RequestCmdCode.ToDayTradeCode;
            rp.content = new ReqLoginName()
            {
                user_id = UserInfoHelper.UserId
            };

            ScoketManager.GetInstance().SendTradeWSInfo(JsonConvert.SerializeObject(rp));
        }

    
        public ICommand SelectionChangedCommand { get { return new RelayCommand(SelectionChangedExecuteChanged, SelectionChangedCanExecuteChanged); } }
        public void SelectionChangedExecuteChanged()
        {
            if (SelectedItem == null) return;
            TransactionViewModel.Instance().SelectFutures(SelectedItem.ContractId);

        }
        public bool SelectionChangedCanExecuteChanged()
        {
            return true;
        }

        public ICommand SelectionChangedALLCommand { get { return new RelayCommand(SelectionChangedALLExecuteChanged, SelectionChangedALLCanExecuteChanged); } }
        public void SelectionChangedALLExecuteChanged()
        {
            if (SelectedItemAll == null) return;
            TransactionViewModel.Instance().SelectFutures(SelectedItemAll.ContractId);

        }
        public bool SelectionChangedALLCanExecuteChanged()
        {
            return true;
        }
        public ICommand ExportCommand { get { return new RelayCommand(ExportExecute, ExportCanExecuteChanged); } }
        public void ExportExecute()
        {
            ExprotHelper.ExportToExcelTemp(TodayTraderList, "当日成交", 3);
        }
        public bool ExportCanExecuteChanged()
        {
            return true;
        }

        internal void Sorting(string name, bool isDesc)
        {
            List<TodayTraderModelViewModel> temp = null;
            switch (name)
            {
                case "ContractCode":
                    if (isDesc)
                    {
                        temp = TodayTraderList.OrderBy(x => x.ContractCode).ToList();
                    }
                    else
                    {
                        temp = TodayTraderList.OrderByDescending(x => x.ContractCode).ToList();
                    }

                    break;
                case "Direction":
                    if (isDesc)
                    {
                        temp = TodayTraderList.OrderBy(x => x.Direction).ToList();
                    }
                    else
                    {
                        temp = TodayTraderList.OrderByDescending(x => x.Direction).ToList();
                    }

                    break;
                case "OpenOffset":
                    if (isDesc)
                    {
                        temp = TodayTraderList.OrderBy(x => x.OpenOffset).ToList();
                    }
                    else
                    {
                        temp = TodayTraderList.OrderByDescending(x => x.OpenOffset).ToList();
                    }

                    break;
                case "TradePrice":
                    if (isDesc)
                    {
                        temp = TodayTraderList.OrderBy(x => x.TradePrice).ToList();
                    }
                    else
                    {
                        temp = TodayTraderList.OrderByDescending(x => x.TradePrice).ToList();
                    }

                    break;
                case "ShadowOrderID":
                    if (isDesc)
                    {
                        temp = TodayTraderList.OrderBy(x => x.ShadowOrderID).ToList();
                    }
                    else
                    {
                        temp = TodayTraderList.OrderByDescending(x => x.ShadowOrderID).ToList();
                    }

                    break;
                case "TradeVolume":
                    if (isDesc)
                    {
                        temp = TodayTraderList.OrderBy(x => x.TradeVolume).ToList();
                    }
                    else
                    {
                        temp = TodayTraderList.OrderByDescending(x => x.TradeVolume).ToList();
                    }

                    break;
                case "TradeTime":
                    if (isDesc)
                    {
                        temp = TodayTraderList.OrderBy(x => x.TradeTime).ToList();
                    }
                    else
                    {
                        temp = TodayTraderList.OrderByDescending(x => x.TradeTime).ToList();
                    }

                    break;
                case "ShadowTradeID":
                    if (isDesc)
                    {
                        temp = TodayTraderList.OrderBy(x => x.ShadowTradeID).ToList();
                    }
                    else
                    {
                        temp = TodayTraderList.OrderByDescending(x => x.ShadowTradeID).ToList();
                    }

                    break;

            }
            TodayTraderList.Clear();
            foreach (var item in temp)
            {
                TodayTraderList.Add(item);
            }
        }

        internal void ALLSorting(string name, bool isDesc)
        {
            List<TodayTraderModelViewModel> temp = null;
            switch (name)
            {
                case "ContractCode":
                    if (isDesc)
                    {
                        temp = TodayTraderListALL.OrderBy(x => x.ContractCode).ToList();
                    }
                    else
                    {
                        temp = TodayTraderListALL.OrderByDescending(x => x.ContractCode).ToList();
                    }

                    break;
                case "Direction":
                    if (isDesc)
                    {
                        temp = TodayTraderListALL.OrderBy(x => x.Direction).ToList();
                    }
                    else
                    {
                        temp = TodayTraderListALL.OrderByDescending(x => x.Direction).ToList();
                    }

                    break;
                case "OpenOffset":
                    if (isDesc)
                    {
                        temp = TodayTraderListALL.OrderBy(x => x.OpenOffset).ToList();
                    }
                    else
                    {
                        temp = TodayTraderListALL.OrderByDescending(x => x.OpenOffset).ToList();
                    }

                    break;
                case "TradePrice":
                    if (isDesc)
                    {
                        temp = TodayTraderListALL.OrderBy(x => x.TradePrice).ToList();
                    }
                    else
                    {
                        temp = TodayTraderListALL.OrderByDescending(x => x.TradePrice).ToList();
                    }

                    break;
                case "OrderOrderref":
                    if (isDesc)
                    {
                        temp = TodayTraderListALL.OrderBy(x => x.OrderOrderref).ToList();
                    }
                    else
                    {
                        temp = TodayTraderListALL.OrderByDescending(x => x.OrderOrderref).ToList();
                    }

                    break;
                case "TradeVolume":
                    if (isDesc)
                    {
                        temp = TodayTraderListALL.OrderBy(x => x.TradeVolume).ToList();
                    }
                    else
                    {
                        temp = TodayTraderListALL.OrderByDescending(x => x.TradeVolume).ToList();
                    }

                    break;
                case "TradeTime":
                    if (isDesc)
                    {
                        temp = TodayTraderListALL.OrderBy(x => x.TradeTime).ToList();
                    }
                    else
                    {
                        temp = TodayTraderListALL.OrderByDescending(x => x.TradeTime).ToList();
                    }

                    break;
                case "ShadowTradeID":
                    if (isDesc)
                    {
                        temp = TodayTraderListALL.OrderBy(x => x.ShadowTradeID).ToList();
                    }
                    else
                    {
                        temp = TodayTraderListALL.OrderByDescending(x => x.ShadowTradeID).ToList();
                    }

                    break;

            }
            TodayTraderListALL.Clear();
            foreach (var item in temp)
            {
                TodayTraderListALL.Add(item);
            }
        }

    }
}
