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

        // Possible view models that can be called from this screen
        public HomeViewModel HomeVM { get; set; }

        public SearchViewModel SearchVM { get; set; }

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

        public AppWindowViewModel()
        {
            // Initialize views and their view models
            HomeVM = new HomeViewModel();
            SearchVM = new SearchViewModel();
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
        }
    }
}
