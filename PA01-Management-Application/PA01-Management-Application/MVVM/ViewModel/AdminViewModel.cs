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

        public FilmManagementViewModel FilmManagementVM { get; set; }
        public VoucherManagementViewModel VoucherManagementVM { get; set; }
        public ICommand ShowReportPageCommand { get; private set; }
        public RelayCommand VoucherCommand { get; private set; }
        public RelayCommand FilmManagementCommand { get; set; }

        public AdminViewModel()
        {
            FilmManagementVM = new FilmManagementViewModel();
            VoucherManagementVM = new VoucherManagementViewModel();
            FilmManagementCommand = new RelayCommand(o =>
            {
                CurrentAdminView = FilmManagementVM;
            });
            VoucherCommand = new RelayCommand(o =>
            {
                CurrentAdminView = VoucherManagementVM;
            });
            ShowReportPageCommand = new RelayCommand(ShowReportPage);
        }

        private void ShowReportPage(object parameter)
        {
            CurrentAdminView = new ReportPageView();
        }

    }
}
