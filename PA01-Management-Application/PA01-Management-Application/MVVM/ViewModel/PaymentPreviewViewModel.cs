using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class PaymentPreviewViewModel : BaseViewModel
    {
        private ObservableCollection<Item> orderItems { get; set; }

        float totalPrice {  get; set; }

        public float TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }


        float discount { get; set; }

        public float Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }


        float finalPrice { get; set; }

        public float FinalPrice
        {
            get { return finalPrice; }
            set
            {
                finalPrice = value;
                OnPropertyChanged(nameof(FinalPrice));
            }
        }



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

        public RelayCommand ApplyCouponCommand {  get; set; }

        public PaymentPreviewViewModel()
        {
            ApplyCouponCommand = new(ApplyCouponCommandExecute);

            OrderItems = new ObservableCollection<Item>();
            AddSeatData();
            AddFoodData();

            FinalPrice = TotalPrice;
        }

        private void ApplyCouponCommandExecute(object obj)
        {
            if(obj is TextBox textBox)
            {
                if (textBox.IsReadOnly) return;

                string messageBoxText = "Apply coupon successfully";
                string caption = "Success";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result;


                string txt = textBox.Text;
                if(txt == "HAPPYBIRTHDAY")
                {
                    textBox.IsReadOnly = true;
                    Discount = TotalPrice * 0.1f;
                    FinalPrice = TotalPrice - Discount;
                }
                else
                {
                    messageBoxText = "Apply coupon failed";
                    caption = "Fail";
                }

                result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }
        }

        void AddSeatData()
        {
            string seats = "Seats: ";
            float ticketPrice = 0;
            if (BookingDataHolder.seats != null)
            {
                for (int i = 0; i < BookingDataHolder.seats.Count; i++)
                {
                    seats += BookingDataHolder.seats[i].Name;
                    ticketPrice += BookingDataHolder.seats[i].GetPrice();
                    if (i != BookingDataHolder.seats.Count - 1)
                    {
                        seats += ", ";
                    }

                }
            }
            OrderItems.Add(new Item(seats, ticketPrice));
            TotalPrice += ticketPrice;
        }

        void AddFoodData()
        {
            if (BookingDataHolder.foods != null)
            {
                for (int i = 0; i < BookingDataHolder.foods.Count; i++)
                {
                    var pair = BookingDataHolder.foods[i];
                    if (pair.Value == 0) continue;
                    float price = pair.Value * (float)pair.Key.Price;
                    OrderItems.Add(new Item(pair.Key.FoodName, price));
                    TotalPrice += price;
                }
            }
        }
    }
}
