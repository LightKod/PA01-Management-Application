using System;
using System.ComponentModel;

namespace PA01_Management_Application.MVVM.Model
{
    public class Booking : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _bookingId;
        public int BookingId
        {
            get { return _bookingId; }
            set { _bookingId = value; OnPropertyChanged(nameof(BookingId)); }
        }

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged(nameof(UserId)); }
        }

        private int _scheduleId;
        public int ScheduleId
        {
            get { return _scheduleId; }
            set { _scheduleId = value; OnPropertyChanged(nameof(ScheduleId)); }
        }

        private int _seatId;
        public int SeatId
        {
            get { return _seatId; }
            set { _seatId = value; OnPropertyChanged(nameof(SeatId)); }
        }

        private float _price;
        public float Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        private int _seatStatus;
        public int SeatStatus
        {
            get { return _seatStatus; }
            set { _seatStatus = value; OnPropertyChanged(nameof(SeatStatus)); }
        }

        private DateTime _bookingDate;
        public DateTime BookingDate
        {
            get { return _bookingDate; }
            set { _bookingDate = value; OnPropertyChanged(nameof(BookingDate)); }
        }

        private string _comboFood;
        public string ComboFood
        {
            get { return _comboFood; }
            set { _comboFood = value; OnPropertyChanged(nameof(ComboFood)); }
        }

        private string _movie;
        public string Movie
        {
            get { return _movie; }
            set { _movie = value; OnPropertyChanged(nameof(Movie)); }
        }
    }
}
