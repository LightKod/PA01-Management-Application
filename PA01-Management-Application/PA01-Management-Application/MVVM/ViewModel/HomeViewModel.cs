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
    class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<FilmSet> FilmSets { get; set; }

        public RelayCommand MovieDetailCommand { get; set; }
        public MovieDetailViewModel MovieDetailVM{ get; set; }

        public HomeViewModel()
        {
            FilmSets = new ObservableCollection<FilmSet>();

            for (int i = 0; i < 5; i++)
            {
                ObservableCollection<Film> filmList = new ObservableCollection<Film>();

                for (int j = 0; j < 10; j++)
                {
                    filmList.Add(new Film
                    (
                        $"Shrek {i}-{j}",
                       [$"Genre {i}-{j}"],
                        69 + i + j,
                        4.96,
                        "https://upload.wikimedia.org/wikipedia/vi/b/bb/Spy_×_Family_Code_White_Movie_Teaser_Visual.png",
                         "Videos/video.mp4",
                        [], ["Romeo", "Juliet"], [], []
                    ));
                }

                FilmSets.Add(new FilmSet()
                {
                    SetTitle = $"Set {i + 1}",
                    FilmList = filmList
                });
            }

            MovieDetailVM = new MovieDetailViewModel();
            MovieDetailCommand = new RelayCommand(o =>
            {
                if(o is Film selectedFilm)
                {
                    MovieDetailVM.Film = selectedFilm;
                    (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = MovieDetailVM;
                }
            });
        }
    }
}
