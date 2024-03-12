using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.ViewModel;
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
            int col = 7;
            int row = 7;
            List<Seat> seats = new();

            for (int y = 0; y < col; y++)
            {
                for(int x = 0; x < row; x++)
                {
                    if(x == 3)
                    {
                        seats.Add(new Seat($"{x}-{y}", SeatType.None));

                    }
                    else if (y > 3)
                    {
                        seats.Add(new Seat($"{x}-{y}", SeatType.VIP));
                    }
                    else
                    {
                        seats.Add(new Seat($"{x}-{y}", SeatType.Normal));
                    }
                    Debug.WriteLine($"{x}-{y}");
                }
            }

            room = new("R01", "Room 01", seats, row, col);
        }
    }
}
