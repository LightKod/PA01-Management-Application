using PA01_Management_Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class AppWindowViewModel : ObservableObject
    {
        // Commands to change CurrentView (see AppWindow.xaml, the part with RadioButtons and their Command property)
        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand SearchViewCommand { get; set; }
        public RelayCommand AccountViewCommand { get; set; }
        public RelayCommand ReportViewCommand { get; set; }

        public RelayCommand SearchCommand { get; set; }

        // Possible view models that can be called from this screen
        public HomeViewModel HomeVM { get; set; }

        public SearchViewModel SearchVM { get; set; }
        public AccountViewModel AccountVM { get; set; }
        public ReportViewModel ReportVM { get; set; }

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
            ReportVM = new ReportViewModel();

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
                CurrentView = AccountVM;
            });

            SearchCommand = new RelayCommand(o =>
            {
                CurrentView = SearchVM;
                SearchVM.SearchByName();
            });

            ReportViewCommand = new RelayCommand(o =>
            {
                CurrentView = ReportVM;
            });

            IsAdmin = true;
        }
    }
}
