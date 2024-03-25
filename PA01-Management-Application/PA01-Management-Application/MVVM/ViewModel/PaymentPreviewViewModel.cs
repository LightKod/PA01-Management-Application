using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

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


        string seatString { get; set; }

        public string SeatString
        {
            get { return seatString; }
            set
            {
                seatString = value;
                OnPropertyChanged(nameof(SeatString));
            }
        }

        Movie movie;
        public Movie Movie
        {
            get {
                return movie;
            }
            set
            {
                movie = value;
                OnPropertyChanged(nameof(Movie));
            }
        }

        string cinemaName { get; set; }

        public string CinemaName
        {
            get { return cinemaName; }
            set
            {
                cinemaName = value;
                OnPropertyChanged(nameof(CinemaName));
            }
        }


        Schedule schedule;
        public Schedule Schedule
        {
            get
            {
                return schedule;
            }
            set
            {
                schedule = value;
                OnPropertyChanged(nameof(Schedule));
            }
        }

        public RelayCommand ApplyCouponCommand {  get; set; }
        public RelayCommand PayClickCommand { get; }


        public PaymentPreviewViewModel()
        {
            ApplyCouponCommand = new(ApplyCouponCommandExecute);
            PayClickCommand = new RelayCommand(PayClickCommandExecute);

            Schedule = null;
            Schedule = BookingDataHolder.schedule;

            Movie = null;
            Movie = BookingDataHolder.movie;


            OrderItems = new ObservableCollection<Item>();
            AddSeatData();
            AddFoodData();

            FinalPrice = TotalPrice;
            GetCinemaName();


        }

        async void GetCinemaName()
        {
            MovieManagementContext context = new();
            Models.Room room = context.Rooms.FirstOrDefault((Models.Room r) => r.RoomId == schedule.RoomId);
            CinemaName = context.Cinemas.FirstOrDefault((Cinema x) => x.CinemaId == room.CinemaId).CinemaName;
        }

        private void PayClickCommandExecute(object obj)
        {
            MovieManagementContext context = new();
            List<Seat> selectedSeats = BookingDataHolder.seats;
            var foods = BookingDataHolder.foods;
            int scheduleID = BookingDataHolder.schedule.ScheduleId;

            foreach (Seat seat in selectedSeats)
            {
                PA01_Management_Application.MVVM.Models.Booking booking = new();
                booking.SeatId = seat.Name;
                //booking.UserId = 1;
                booking.ScheduleId = scheduleID;
                booking.Price = seat.GetPrice();

                context.Bookings.Add(booking);
            }

            foreach(var food in foods)
            {
                if (food.Value == 0) continue;
                for (int i = 0; i < food.Value; i++)
                {
                    BookingFood bFood = new();
                    bFood.FoodId = food.Key.FoodId;
                    bFood.Price = food.Key.Price;
                    bFood.ScheduleId = scheduleID;
                    context.BookingFoods.Add(bFood);    
                }
               
            }

            if(Discount > 0)
            {
                PA01_Management_Application.MVVM.Models.Booking booking = new();
                booking.SeatId = "DISCOUNT";
                //booking.UserId = 1;
                booking.ScheduleId = scheduleID;
                booking.Price = -discount;

                context.Bookings.Add(booking);
            }
            

            context.SaveChanges();


            string messageBoxText = "Payment Success";
            string caption = "Success";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = new HomeViewModel();

        }

        private void ApplyCouponCommandExecute(object obj)
        {
            if(obj is TextBox textBox)
            {
                if (textBox.IsReadOnly) return;

                MovieManagementContext context = new();

                string messageBoxText = "Apply coupon successfully";
                string caption = "Success";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result;


                string txt = textBox.Text;
                Voucher voucher = context.Vouchers.FirstOrDefault(x => x.VoucherCode == txt);

                if(txt == "HAPPYBIRTHDAY" && DateTime.Now.Day == UserData.userData.Birthday.Value.Day 
                    && DateTime.Now.Month == UserData.userData.Birthday.Value.Month)
                {
                    textBox.IsReadOnly = true;
                    Discount = TotalPrice * 0.2f;
                    FinalPrice = TotalPrice - Discount;
                }else if (voucher != null)
                {
                    textBox.IsReadOnly = true;
                    Discount = TotalPrice * ((float)voucher.VoucherValue / 100f);
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
            SeatString = seats;
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
