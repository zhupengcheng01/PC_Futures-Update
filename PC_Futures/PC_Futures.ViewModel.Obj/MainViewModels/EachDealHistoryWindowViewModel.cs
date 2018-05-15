using MicroMvvm;
using PC_Futures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PC_Futures.ViewModel
{
    public class EachDealHistoryWindowViewModel : ObservableObject
    {
        private FuturesTickViewModel _tick;
        private HttpRequestContractHelper httpHelper = new HttpRequestContractHelper();
        private int pageSize = 500;
        private string currtime = string.Empty;
        //0最新 1上翻 2下翻
        //private int type = 0;
        public EachDealHistoryWindowViewModel(FuturesTickViewModel tick)
        {
            _tick = tick;
            Action EachDealHistoryHandle = EachDealHistoryHandleAction;
            EachDealHistoryHandle.BeginInvoke(null, null);
            QueryIsEnabled = true;
            PreviousIsEnabled = true;
            NextIsEnabled = true;
        }
        private void EachDealHistoryHandleAction()
        {
            EachDealHistoryList = httpHelper.GetEachDealHistory(_tick.FuturesViewModel.ContractCode, _tick.FuturesViewModel.ProductCode, pageSize).ConvertAll(b => new EachDealHistoryDetailViewModel(b, _tick)).OrderBy(o => o.OrderByTime).ToList();
        }
        private List<EachDealHistoryDetailViewModel> _EachDealHistoryList;
        public List<EachDealHistoryDetailViewModel> EachDealHistoryList
        {
            get
            {
                return _EachDealHistoryList;
            }
            set
            {
                if (_EachDealHistoryList == value)
                    return;
                _EachDealHistoryList = value;
                RaisePropertyChanged(nameof(EachDealHistoryList));
            }
        }
        private DateTime _DayDate = DateTime.Now.Date;
        public DateTime DayDate
        {
            get
            {
                return _DayDate;
            }
            set
            {
                if (_DayDate != value)
                {
                    _DayDate = value;
                    RaisePropertyChanged(nameof(DayDate));
                }
            }
        }
        private string _TrrigerTime = DateTime.Now.ToString("HH:mm:ss");

        public string TrrigerTime
        {
            get { return _TrrigerTime; }
            set
            {
                if (value != _TrrigerTime)
                {
                    if (value.ToString() == "1.00:00:00")
                    {
                        return;
                    }
                    else if (value.ToString() == "-01:00:00")
                    {
                        value = "23:00:00";
                    }
                    _TrrigerTime = value;
                    RaisePropertyChanged("TrrigerTime");
                }
            }

        }
        private bool _QueryIsEnabled;
        public bool QueryIsEnabled
        {
            get
            {
                return _QueryIsEnabled;
            }
            set
            {
                if (_QueryIsEnabled != value)
                {
                    _QueryIsEnabled = value;
                    RaisePropertyChanged(nameof(QueryIsEnabled));
                }
            }
        }
        private bool _PreviousIsEnabled;
        public bool PreviousIsEnabled
        {
            get
            {
                return _PreviousIsEnabled;
            }
            set
            {
                if (_PreviousIsEnabled != value)
                {
                    _PreviousIsEnabled = value;
                    RaisePropertyChanged(nameof(PreviousIsEnabled));
                }
            }
        }
        private bool _NextIsEnabled;
        public bool NextIsEnabled
        {
            get
            {
                return _NextIsEnabled;
            }
            set
            {
                if (_NextIsEnabled != value)
                {
                    _NextIsEnabled = value;
                    RaisePropertyChanged(nameof(NextIsEnabled));
                }
            }
        }
        /// <summary>
        ///  查询
        /// </summary>
        public ICommand QueryCommand { get { return new RelayCommand(QueryExecuteChanged, QueryCanExecuteChanged); } }
        public void QueryExecuteChanged()
        {
            QueryIsEnabled = false;
            currtime = DayDate.ToString("yyyy-MM-dd") + " " + TrrigerTime + ".000";
            EachDealHistoryList = httpHelper.GetEachDealHistory(_tick.FuturesViewModel.ContractCode, _tick.FuturesViewModel.ProductCode, pageSize, currtime, 2).ConvertAll(b => new EachDealHistoryDetailViewModel(b, _tick)).OrderBy(o => o.OrderByTime).ToList();
            QueryIsEnabled = true;
        }
        public bool QueryCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 上一页
        /// </summary>
        public ICommand PreviousCommand { get { return new RelayCommand(PreviousExecuteChanged, PreviousCanExecuteChanged); } }
        public void PreviousExecuteChanged()
        {
            if (EachDealHistoryList.Count > 0)
            {
                PreviousIsEnabled = false;
                currtime = EachDealHistoryList.FirstOrDefault().Time;
                var list = httpHelper.GetEachDealHistory(_tick.FuturesViewModel.ContractCode, _tick.FuturesViewModel.ProductCode, pageSize, currtime, 1);
                PreviousIsEnabled = true;
                if(list.Count==0)
                {
                    MessageBox.Show("已是第一页了！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                EachDealHistoryList = list.ConvertAll(b => new EachDealHistoryDetailViewModel(b, _tick)).OrderBy(o => o.OrderByTime).ToList();
            }
            else
            {
                MessageBox.Show("当前集合为空，无法查询上一页！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
        public bool PreviousCanExecuteChanged()
        {
            return true;
        }
        /// <summary>
        /// 下一页
        /// </summary>
        public ICommand NextCommand { get { return new RelayCommand(NextExecuteChanged, NextCanExecuteChanged); } }
        public void NextExecuteChanged()
        {
            if(EachDealHistoryList.Count>0)
            {
                NextIsEnabled = false;
                currtime = EachDealHistoryList.LastOrDefault().Time;
                var list = httpHelper.GetEachDealHistory(_tick.FuturesViewModel.ContractCode, _tick.FuturesViewModel.ProductCode, pageSize, currtime, 2);
                NextIsEnabled = true;
                if (list.Count == 0)
                {
                    MessageBox.Show("已是最后一页了！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                EachDealHistoryList = list.ConvertAll(b => new EachDealHistoryDetailViewModel(b, _tick)).OrderBy(o => o.OrderByTime).ToList();
            }
            else
            {
                MessageBox.Show("当前集合为空，无法查询下一页！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
        public bool NextCanExecuteChanged()
        {
            return true;
        }
    }
}
