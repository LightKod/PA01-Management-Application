using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.View;
using System;
using System.Windows.Input;

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

        public ICommand ShowReportPageCommand { get; private set; }

        public AdminViewModel()
        {
            ShowReportPageCommand = new RelayCommand(ShowReportPage);
        }

        private void ShowReportPage(object parameter)
        {
            CurrentAdminView = new ReportPageView(); 
        }
    }
}
