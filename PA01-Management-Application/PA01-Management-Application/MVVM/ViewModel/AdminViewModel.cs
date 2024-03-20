using PA01_Management_Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class AdminViewModel : ObservableObject
    {
        private object _currentAdminView;

        public object CurrentAdminView
        {
            get { return _currentAdminView; }
            set
            {
                _currentAdminView = value;
                OnPropertyChanged();
            }
        }

        public AdminViewModel()
        {
        }
    }
}
