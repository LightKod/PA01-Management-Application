using PA01_Management_Application.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class PaymentPreviewViewModel : BaseViewModel
    {
        private ObservableCollection<Item> orderItems { get; set; }

        public ObservableCollection<Item> OrderItems {
            get
            {
                return orderItems;
            }
            set
            {
                orderItems = value;
                OnPropertyChanged(nameof(OrderItems));
            } 
        }

        public PaymentPreviewViewModel()
        {
            OrderItems = new ObservableCollection<Item>();
            OrderItems.Add(new("Seat: A1-A2", 1000));
            OrderItems.Add(new("Combo 2", 1000));
        }
    }
}
