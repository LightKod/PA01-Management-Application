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
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace PA01_Management_Application.MVVM.ViewModel
{
    public class ShowBookingDetailViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Item> orderItems { get; set; }
        public ObservableCollection<Item> OrderItems
        {
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

        float totalPrice { get; set; }

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

        private string _moviePosterPath;
        public string MoviePosterPath
        {
            get { return _moviePosterPath; }
            set
            {
                _moviePosterPath = value;
                OnPropertyChanged(nameof(MoviePosterPath));
            }
        }

        private string _movieTitle;
        public string MovieTitle
        {
            get { return _movieTitle; }
            set
            {
                _movieTitle = value;
                OnPropertyChanged(nameof(MovieTitle));
            }
        }

        private string _cinemaName;
        public string CinemaName
        {
            get { return _cinemaName; }
            set
            {
                _cinemaName = value;
                OnPropertyChanged(nameof(CinemaName));
            }
        }

        private DateOnly? _scheduleDate;
        public DateOnly? ScheduleDate
        {
            get { return _scheduleDate; }
            set
            {
                _scheduleDate = value;
                OnPropertyChanged(nameof(ScheduleDate));
            }
        }

        private TimeOnly? _scheduleStart;
        public TimeOnly? ScheduleStart
        {
            get { return _scheduleStart; }
            set
            {
                _scheduleStart = value;
                OnPropertyChanged(nameof(ScheduleStart));
            }
        }

        private string _seatString;
        public string SeatString
        {
            get { return _seatString; }
            set
            {
                _seatString = value;
                OnPropertyChanged(nameof(SeatString));
            }
        }


        public void LoadBookingDetails(int bookingId, string cinemaName, string seats)
        {

            try
            {
                using (var context = new MovieManagementContext())
                {
                    var booking = context.Bookings
                                         .Include(b => b.Schedule.Movie)
                                         .FirstOrDefault(b => b.BookingId == bookingId);

                    if (booking != null)
                    {
                        MoviePosterPath = booking.Schedule.Movie.PosterPath;
                        MovieTitle = booking.Schedule.Movie.Title;
                        CinemaName = cinemaName;
                        ScheduleDate = booking.Schedule.ScheduleDate;
                        ScheduleStart = booking.Schedule.ScheduleStart;
                        SeatString = seats;
                        
                        OrderItems = new ObservableCollection<Item>();
                        AddSeatData(seats);
                        LoadFoodData(bookingId, booking.BookingDate);

                    }
                    else
                    {
                        MessageBox.Show("Booking not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void AddSeatData(string seats)
        {
            string[] seatNames = seats.Split(',');

            string formattedSeatString = "Seats: ";
            float totalTicketPrice = 0;

            foreach (string seatName in seatNames)
            {
                string trimmedSeatName = seatName.Trim();

                SeatType seatType;
                if (char.ToUpper(trimmedSeatName[0]) >= 'A' && char.ToUpper(trimmedSeatName[0]) <= 'G')
                {
                    seatType = SeatType.Normal;
                }
                else if (char.ToUpper(trimmedSeatName[0]) >= 'H' && char.ToUpper(trimmedSeatName[0]) <= 'J')
                {
                    seatType = SeatType.VIP;
                }
                else
                {
                    seatType = SeatType.None;
                }

                Seat seat = new Seat(trimmedSeatName, seatType);

                float ticketPrice = seat.GetPrice();

                formattedSeatString += trimmedSeatName;
                totalTicketPrice += ticketPrice;

                if (seatName != seatNames.Last())
                {
                    formattedSeatString += ", ";
                }
            }

            OrderItems.Add(new Item(formattedSeatString, totalTicketPrice));
            TotalPrice += totalTicketPrice;
            FinalPrice = TotalPrice;
        }

        void LoadFoodData(int bookingId, DateTime? bookingDate)
        {
            try
            {
                using (var context = new MovieManagementContext())
                {
                    var bookedFoods = context.BookingFoods
                                           .Where(bf => bf.BookingId == bookingId && bf.BookingDate == bookingDate)
                                           .ToList();

                    foreach (var bookedFood in bookedFoods)
                    {
                        var food = context.BookingFoods.FirstOrDefault(f => f.FoodId == bookedFood.FoodId);
                        if (food != null)
                        {
                            float price = (float)food.Price;
                            OrderItems.Add(new Item(bookedFood.Food.FoodName, price));
                            TotalPrice += price;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading food data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
