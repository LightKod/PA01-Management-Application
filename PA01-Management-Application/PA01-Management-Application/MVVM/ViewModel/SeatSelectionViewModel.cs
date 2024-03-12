using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class SeatSelectionViewModel : BaseViewModel
    {
        public Room room; //Point of ref, dont change
        private Room _displayRoom; //The view will be displaying this

        private List<Seat> _sampleSeats;

        public List<Seat> SampleSeats
        {
            get { return _sampleSeats; }
            set 
            { 
                _sampleSeats = value;
                OnPropertyChanged(nameof(SampleSeats));
            }
        }

        public Room DisplayRoom
        {
            get { return _displayRoom; }
            set
            {
                _displayRoom = value;
                OnPropertyChanged(nameof(DisplayRoom));
            }
        }
        public RelayCommand ButtonClickCommand { get; }
        public RelayCommand ConfirmClickCommand { get; }
        public SeatSelectionViewModel() 
        {
            CreateSampleSeat();
            ButtonClickCommand = new RelayCommand(ButtonClickCommandExecute);
            ConfirmClickCommand = new RelayCommand(ConfirmClickCommandExecute);
        }

        private void ButtonClickCommandExecute(object parameter)
        {
            if (parameter is Button button)
            {
                Seat SelectedSeat = button.DataContext as Seat;

                if (SelectedSeat != null)
                {
                    Room cpyRoom = new(DisplayRoom);

                    SeatType baseType = room.Seats.Find(x => x.Name == SelectedSeat.Name).Type;

                    SelectedSeat.Type = SelectedSeat.Type == SeatType.Picked ? baseType : SeatType.Picked;

                    int index = DisplayRoom.Seats.FindIndex(x => x.Name == SelectedSeat.Name);
                    cpyRoom.Seats[index] = SelectedSeat;

                    //Force update (For some reason??)
                    DisplayRoom = null;
                    DisplayRoom = cpyRoom;

                    Debug.WriteLine($"{SelectedSeat.Name} - {SelectedSeat.Type}");

                }
                else
                {
                    Debug.WriteLine("Null");
                }

            }
        }

        private void ConfirmClickCommandExecute(object parameter)
        {
            (Application.Current.MainWindow.DataContext as MainViewModel).SelectedViewModel = new FoodSelectionViewModel(room, DisplayRoom);
        }

        void CreateSampleSeat()
        {
            SampleSeats = new();
            for (int i = 0; i < 4; i++)
            {
                SampleSeats.Add(new Seat(" ", (SeatType)i));
            }
        }
    }
}
