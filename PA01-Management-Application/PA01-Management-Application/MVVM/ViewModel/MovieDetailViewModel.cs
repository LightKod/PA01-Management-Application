using PA01_Management_Application.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public MovieDetailViewModel() 
        {

        }
    }
}
