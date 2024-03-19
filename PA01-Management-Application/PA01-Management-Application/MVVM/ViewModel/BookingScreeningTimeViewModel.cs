using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Models;
using PA01_Management_Application.MVVM.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PA01_Management_Application.MVVM.ViewModel
{
    internal class BookingScreeningTimeViewModel : ObservableObject
    {
        public class DateInfo
        {
            public string Day { get; set; }
            public string DayOfWeek { get; set; }
            public string Month { get; set; }
        }
        public class CinemaDetailsViewModel
        {
            public string CinemaName { get; set; }
            public string TheaterName { get; set; }
            public ObservableCollection<Schedule> TimeButtons { get; set; }
        }
        private ObservableCollection<string> _cityList;
        public ObservableCollection<Schedule> Schedules { get; set; }
        private readonly MovieService _movieService = new MovieService();

        public ObservableCollection<string> CityList
        {
            get { return _cityList; }
            set
            {
                _cityList = value;
                OnPropertyChanged(nameof(CityList));
            }
        }

        private ObservableCollection<DateInfo> _dateList;
        public ObservableCollection<DateInfo> DateList
        {
            get { return _dateList; }
            set
            {
                _dateList = value;
                OnPropertyChanged(nameof(DateList));
            }
        }

        private ObservableCollection<CinemaDetailsViewModel> _cinemaDetailsCollection;
        public ObservableCollection<CinemaDetailsViewModel> CinemaDetailsCollection
        {
            get { return _cinemaDetailsCollection; }
            set
            {
                _cinemaDetailsCollection = value;
                OnPropertyChanged(nameof(CinemaDetailsCollection));
            }
        }
        private DateInfo _selectedDate;
        public DateInfo SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private Schedule _selectedCinemaDetailsViewModel;
        public Schedule SelectedCinemaDetailsViewModel
        {
            get { return _selectedCinemaDetailsViewModel; }
            set
            {
                _selectedCinemaDetailsViewModel = value;
                OnPropertyChanged(nameof(SelectedCinemaDetailsViewModel));
            }
        }

        private Movie movie;
        public Movie Movie
        {
            get { return movie; }
            set
            {
                movie = value;
                OnPropertyChanged(nameof(Movie));
            }
        }

        public RelayCommand DateButtonClickCommand { get; private set; }
        public RelayCommand CinemaDetailsClickCommand { get; set; }

        public BookingScreeningTimeViewModel()
        {
            // Khởi tạo CityList
            CityList = new ObservableCollection<string>
            {
                "Hồ Chí Minh",
                "Hà Nội",
                "Đà Nẵng",
                "Cần Thơ",
                "Đồng Nai"
            };

            // Khởi tạo DateList
            DateList = new ObservableCollection<DateInfo>
            {
                new DateInfo { Day = "15", DayOfWeek = "Mon", Month = "03" },
                new DateInfo { Day = "16", DayOfWeek = "Tue", Month = "03" },
                new DateInfo { Day = "17", DayOfWeek = "Wed", Month = "03" },
                new DateInfo { Day = "18", DayOfWeek = "Thu", Month = "03" },
                new DateInfo { Day = "19", DayOfWeek = "Fri", Month = "03" },

                // Thêm các đối tượng dữ liệu khác nếu cần
            };
            //Lấy ListSchedular từ movieId và DateOnly

            // Khởi tạo CinemaDetailsCollection
            CinemaDetailsCollection = new ObservableCollection<CinemaDetailsViewModel>();

            // Thêm dữ liệu vào CinemaDetailsCollection


            DateButtonClickCommand = new RelayCommand(ExecuteDateButtonClick);
            CinemaDetailsClickCommand = new RelayCommand(ExecuteCinemaDetailsClick);

            Movie = null;
            Movie = BookingDataHolder.movie;
        }
        private void ExecuteDateButtonClick(object parameter)
        {
            // Xử lý logic khi DateButton được click
            SelectedDate = parameter as DateInfo;
            int movieId = BookingDataHolder.movieID;
            Debug.WriteLine(SelectedDate.Day.ToString());
            DateOnly selectedDate = new DateOnly(2024, int.Parse(SelectedDate.Month), int.Parse(SelectedDate.Day));
            // Call the method to get schedules for the selected movie ID and date
            Schedules = new ObservableCollection<Schedule>(_movieService.GetSchedulesByMovieIdAndDate(movieId, selectedDate));
            Debug.WriteLine("Number of schedules: " + Schedules.Count);
            CinemaDetailsCollection.Clear();
            foreach (var schedule in Schedules)
            {

                string cinemaName = _movieService.GetCinemaNameByRoomId(schedule.RoomId); // Lấy tên rạp từ ID của phòng chiếu

                var existingCinemaDetails = CinemaDetailsCollection.FirstOrDefault(c => c.CinemaName == cinemaName);
                if (existingCinemaDetails == null)
                {
                    var newCinemaDetails = new CinemaDetailsViewModel
                    {
                        CinemaName = cinemaName,
                        TheaterName = "Rạp", // Có thể thay đổi theo yêu cầu
                    };
                    newCinemaDetails.TimeButtons = new ObservableCollection<Schedule>();
                    newCinemaDetails.TimeButtons.Add(schedule); // Thêm lịch chiếu vào TimeButtons
                    CinemaDetailsCollection.Add(newCinemaDetails);
                }
                else
                {
                    existingCinemaDetails.TimeButtons.Add(schedule); // Thêm lịch chiếu vào TimeButtons của CinemaDetailsViewModel tương ứng
                }
            }
        }
        private void ExecuteCinemaDetailsClick(object parameter)
        {
            // Xử lý logic khi CinemaDetailsViewModel được click
            // Sử dụng biến cinemaDetails ở đây để thực hiện các thao tác cần thiết
            Debug.WriteLine("aaaaa");

            SelectedCinemaDetailsViewModel = parameter as Schedule;

            BookingDataHolder.schedule = SelectedCinemaDetailsViewModel;

            Debug.WriteLine(SelectedCinemaDetailsViewModel.ScheduleStart.ToString());

            (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = new SeatSelectionViewModel();
        }


    }
}
