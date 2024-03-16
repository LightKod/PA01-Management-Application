using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class MovieDetailViewModel : BaseViewModel
    {
        private Film _film;
        public  Film Film{ 
            get { return _film; }
            set 
            {
                _film = value;
                OnPropertyChanged(nameof(Film));
            } 
        }

        public RelayCommand BookClickCommand { get; set; }

        public MovieDetailViewModel() 
        {
            BookClickCommand = new(BookClickCommandExecute);
        }

        private void BookClickCommandExecute(object obj)
        {
            (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = new SeatSelectionViewModel();
        }
    }
}
