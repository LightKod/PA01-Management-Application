using PA01_Management_Application.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static PA01_Management_Application.MVVM.View.BookingScreeningTime;

namespace PA01_Management_Application.MVVM.ViewModel
{
    internal class BookingScreeningTimeViewModel : ObservableObject
    {
        private ObservableCollection<string> _cityList;
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

        private CinemaDetailsViewModel _selectedCinemaDetailsViewModel;
        public CinemaDetailsViewModel SelectedCinemaDetailsViewModel
        {
            get { return _selectedCinemaDetailsViewModel; }
            set
            {
                _selectedCinemaDetailsViewModel = value;
                OnPropertyChanged(nameof(SelectedCinemaDetailsViewModel));
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
                new DateInfo { Day = "25", DayOfWeek = "Tues", Month = "02" },
                // Thêm các đối tượng dữ liệu khác nếu cần
            };

            // Khởi tạo CinemaDetailsCollection
            CinemaDetailsCollection = new ObservableCollection<CinemaDetailsViewModel>();

            // Thêm dữ liệu vào CinemaDetailsCollection
            var cinemaDetails = new CinemaDetailsViewModel
            {
                CinemaName = "CGV Aeon Tân Phú",
                TheaterName = "Rạp 27D",
                TimeButtons = new ObservableCollection<string>
                {
                    "17:20 PM",
                    "17:20 PM",
                    // Thêm các giá trị thời gian khác nếu cần
                }
            };

            CinemaDetailsCollection.Add(cinemaDetails);
            CinemaDetailsCollection.Add(cinemaDetails);

            DateButtonClickCommand = new RelayCommand(ExecuteDateButtonClick);
            CinemaDetailsClickCommand = new RelayCommand(ExecuteCinemaDetailsClick);


        }
        private void ExecuteDateButtonClick(object parameter)
        {
            // Xử lý logic khi DateButton được click
            SelectedDate = parameter as DateInfo;
            Debug.WriteLine(SelectedDate.Day.ToString());
        }
        private void ExecuteCinemaDetailsClick(object parameter)
        {
            // Xử lý logic khi CinemaDetailsViewModel được click
            // Sử dụng biến cinemaDetails ở đây để thực hiện các thao tác cần thiết
            Debug.WriteLine("aaaaa");

            SelectedCinemaDetailsViewModel = parameter as CinemaDetailsViewModel;
            Debug.WriteLine(SelectedCinemaDetailsViewModel.TheaterName.ToString());

        }


    }
}
