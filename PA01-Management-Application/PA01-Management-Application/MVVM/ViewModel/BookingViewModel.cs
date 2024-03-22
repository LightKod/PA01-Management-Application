using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PA01_Management_Application.MVVM.Models;
using System.Globalization;

namespace PA01_Management_Application.MVVM.ViewModel
{

    public class BookingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Booking> _bookings;
        public ObservableCollection<Booking> Bookings
        {
            get { return _bookings; }
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }

        public BookingViewModel()
        {
            Bookings = new ObservableCollection<Booking>();
            GenerateSampleData(100); // Generate 100 sample bookings
        }

        private void GenerateSampleData(int count)
        {
            Random random = new Random();
            DateTime startDate = new DateTime(2021, 1, 1); // Start date set to 2021
            DateTime endDate = new DateTime(2024, 12, 31); // End date set to 2024

            string[] comboFoods = { "Popcorn", "Nachos", "Hot Dog", "Pizza", "Combo Meal" };
            string[] movies = { "Action Movie", "Comedy Movie", "Drama Movie", "Sci-Fi Movie", "Horror Movie" };

            for (int i = 0; i < count; i++)
            {
                int userId = random.Next(1, 100);
                int scheduleId = random.Next(1, 50);
                int seatId = random.Next(1, 200);
                float price = (float)random.NextDouble() * 100;
                int seatStatus = random.Next(1, 4);
                string comboFood = comboFoods[random.Next(comboFoods.Length)];
                string movie = movies[random.Next(movies.Length)];

                // Generating booking date
                DateTime bookingDate = startDate.AddDays(random.Next(0, (endDate - startDate).Days))
                                                   .AddHours(random.Next(0, 24))
                                                   .AddMinutes(random.Next(0, 60));

                //Booking booking = new Booking
                //{
                //    BookingId = i + 1,
                //    UserId = userId,
                //    ScheduleId = scheduleId,
                //    SeatId = seatId,
                //    Price = price,
                //    SeatStatus = seatStatus,
                //    ComboFood = comboFood,
                //    Movie = movie,
                //    BookingDate = bookingDate
                //};

                //Bookings.Add(booking);
            }
        }

        public void AddBooking(Booking booking)
        {
            Bookings.Add(booking);
        }

        public void DeleteBooking(Booking booking)
        {
            if (Bookings.Contains(booking))
            {
                Bookings.Remove(booking);
            }
        }

        public void SaveBookingChanges(Booking updatedBooking)
        {
            // Find the booking in the collection
            var existingBooking = Bookings.FirstOrDefault(b => b.BookingId == updatedBooking.BookingId);
            if (existingBooking != null)
            {
                // Update booking information
                existingBooking.UserId = updatedBooking.UserId;
                existingBooking.ScheduleId = updatedBooking.ScheduleId;
                //existingBooking.SeatId = updatedBooking.SeatId;
                existingBooking.Price = updatedBooking.Price;
                //existingBooking.SeatStatus = updatedBooking.SeatStatus;
                existingBooking.BookingDate = updatedBooking.BookingDate;

                OnPropertyChanged(nameof(Bookings));
            }
            else
            {
                AddBooking(updatedBooking);
            }

            Console.WriteLine("Booking changes saved.");
        }
    }
}
