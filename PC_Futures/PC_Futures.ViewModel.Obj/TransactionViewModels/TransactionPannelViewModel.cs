using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC_Futures.ViewModels
{
  public  class TransactionPannelViewModel: ObservableObject
    {

        private PositionViewModel _PositionViewModel;

        public PositionViewModel Position
        {
            get { return _PositionViewModel; }
            set
            {
                if (_PositionViewModel != value)
                {
                    _PositionViewModel = value;
                    RaisePropertyChanged("Position");
                }
            }
        }

        private OrderCancelViewModel _OrderCancelViewModel;

        public OrderCancelViewModel OrderCancel
        {
            get { return _OrderCancelViewModel; }
            set
            {
                if (_OrderCancelViewModel != value)
                {
                    _OrderCancelViewModel = value;
                    RaisePropertyChanged("OrderCancel");
                }
            }
        }


        public TransactionPannelViewModel()
        {
            Position =  PositionViewModel.Instance();
            OrderCancel =  OrderCancelViewModel.Instance();
        }
    }
}
