using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Models;
using PA01_Management_Application.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class AppWindowViewModel : ObservableObject
    {
        // Commands to change CurrentView (see AppWindow.xaml, the part with RadioButtons and their Command property)
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand SearchViewCommand { get; set; }
        public RelayCommand AccountViewCommand { get; set; }
        public RelayCommand ReportViewCommand { get; set; }
        public RelayCommand AdminViewCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand VoucherCommand { get; set; }

        // Possible view models that can be called from this screen
        public HomeViewModel HomeVM { get; set; }
        public SearchViewModel SearchVM { get; set; }
        public AccountViewModel AccountVM { get; set; }
        public ReportViewModel ReportVM { get; set; }
        public AdminViewModel AdminVM { get; set; }
        public LoginPageViewModel LoginPageVM { get; set; }
        public RegisterViewModel RegisterVM { get; set; }
        public VoucherManagementViewModel VoucherManagementVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        public AppWindowViewModel()
        {
            // Initialize views and their view models
            HomeVM = new HomeViewModel();
            SearchVM = new SearchViewModel();
            AccountVM = new AccountViewModel();
            AdminVM = new AdminViewModel();
            ReportVM = new ReportViewModel();
            LoginPageVM = new LoginPageViewModel();
            RegisterVM = new RegisterViewModel();
            VoucherManagementVM = new VoucherManagementViewModel();
            CurrentView = HomeVM;

            // Handle the command calls
            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            SearchViewCommand = new RelayCommand(o =>
            {
                CurrentView = SearchVM;
            });

            AccountViewCommand = new RelayCommand(o =>
            {
                if (UserData.userData != null)
                {
                    CurrentView = AccountVM;

                }
                else
                {
                    CurrentView = LoginPageVM;
                }
            });

            ReportViewCommand = new RelayCommand(o =>
            {
                CurrentView = ReportVM;
            });

            AdminViewCommand = new RelayCommand(o =>
            {
                CurrentView = AdminVM;
            });

            SearchCommand = new RelayCommand(o =>
            {
                CurrentView = SearchVM;
                SearchVM.SearchByName();
            });

            VoucherCommand = new RelayCommand(o =>
            {
                CurrentView = VoucherManagementVM;
            });
            IsAdmin = true;
        }
    }
}