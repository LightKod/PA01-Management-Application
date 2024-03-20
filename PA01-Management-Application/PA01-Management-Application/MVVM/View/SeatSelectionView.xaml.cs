using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.ViewModel;
using PA01_Management_Application.MVVM.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PA01_Management_Application.MVVM.View
{
    /// <summary>
    /// Interaction logic for SeatSelectionView.xaml
    /// </summary>
    public partial class SeatSelectionView : UserControl
    {
        SeatSelectionViewModel viewModel;

        Room room;

        public SeatSelectionView()
        {

            Debug.WriteLine($"Test");

            CreateTempRoom();

            viewModel = new();
            viewModel.room = room;
            viewModel.DisplayRoom = new(room);
            DataContext = viewModel;
            InitializeComponent();
        }


        void CreateTempRoom()
        {
            int col = 14;
            int row = 10;
            List<Seat> seats = new();

            MovieService service = new();

            for ( int x = 0; x < row; x++)
            {
                for(int y = 0; y < col; y++)
                {
                    string seatID = $"{MapToAlphabet(x)}{y}";
                    if (service.CheckIfBooked(BookingDataHolder.schedule.ScheduleId, seatID))
                    {
                        seats.Add(new Seat(seatID, SeatType.Booked));
                    }
                    else if (y == 3 || y == 10)
                    {
                        seats.Add(new Seat(seatID, SeatType.None));

                    }
                    else if (x > 6)
                    {
                        seats.Add(new Seat(seatID, SeatType.VIP));
                    }
                    else
                    {
                        seats.Add(new Seat(seatID, SeatType.Normal));
                    }
                    Debug.WriteLine(seatID);
                }
            }

            room = new("R01", "Room 01", seats, row, col);
        }

        char MapToAlphabet(int value)
        {
            if (value < 0 || value > 9)
            {
                throw new ArgumentOutOfRangeException("Value must be between 0 and 9.");
            }

            return (char)('A' + value);
        }
    }
}
