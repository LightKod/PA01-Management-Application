using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PA01_Management_Application.MVVM.ViewModel
{
    class FilmManagementViewModel : ObservableObject
    {
        // Everytime this view model is called, this list should be refreshed by fetching from database
        private ObservableCollection<Film> _filmList;

        public ObservableCollection<Film> FilmList
        {
            get { return _filmList; }
            set
            {
                _filmList = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand EditFilmCommand { get; set; }
        public RelayCommand DeleteFilmCommand { get; set; }
        public RelayCommand AddFilmCommand { get; set; }

        public FilmManagementViewModel()
        {
            FilmList = new ObservableCollection<Film>();
            for (int i = 0; i < 10; i++)
            {
                FilmList.Add(new Film
                    (
                        $"Shrek {i}",
                        [$"{(i % 2 == 0 ? "Action" : "Romance")}", $"{(i % 3 == 0 ? "Horror" : "Comedy")}", $"{(i % 5 == 0 ? "Sci-fi" : "Slice of life")}"],
                        i % 2 == 0 ? (120 - i) : (120 + i),
                        i % 2 == 0 ? (5.0 + i * 0.1) : (5.0 - i * 0.1),
                        i % 2 == 0 ? "https://upload.wikimedia.org/wikipedia/vi/b/bb/Spy_×_Family_Code_White_Movie_Teaser_Visual.png" : "https://www.elle.vn/wp-content/uploads/2023/12/06/560540/poster-Mai-scaled.jpg",
                        "Videos/video.mp4",
                        [],
                        i % 2 == 0 ? ["Pickle", $"{(i % 3 == 0 ? "Adam" : "Eve")}"] : ["Doggy", $"{(i % 3 == 0 ? "Martin" : "Lucas")}"],
                        i % 2 == 0 ? ["Meo", $"{(i % 3 == 0 ? "Dwight" : "Jess")}", $"{(i % 2 == 0 ? "Matt" : "Jason")}"] : ["Argenti", $"{(i % 3 == 0 ? "Loucha" : "Pela")}"],
                        i % 2 == 0 ? ["Star", $"{(i % 3 == 0 ? "WeebBro" : "ILoveMyHand")}"] : ["Brighter Star", $"{(i % 3 == 0 ? "WhoAreYou" : "Idk")}"]
                    )
                );
            }

            EditFilmCommand = new RelayCommand(o =>
            {
                if (o is Film filmToEdit)
                {
                    var addEditFilmView = new AddEditFilmView();
                    var addEditFilmViewModel = new AddEditFilmViewModel(true, filmToEdit);
                    addEditFilmView.DataContext = addEditFilmViewModel;
                    addEditFilmView.ShowDialog();

                    if (addEditFilmViewModel.IsValidForm)
                    {
                        var index = FilmList.IndexOf(filmToEdit);
                        FilmList.RemoveAt(index);
                        FilmList.Insert(index, addEditFilmViewModel.BuildFilm());
                    }
                }
            });

            DeleteFilmCommand = new RelayCommand(o =>
            {
                var result = MessageBox.Show("Are you sure you want to delete this film?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (o is Film film)
                    {
                        FilmList.Remove(film);
                    }
                }
            });

            AddFilmCommand = new RelayCommand(o =>
            {
                var addEditFilmView = new AddEditFilmView();
                var addEditFilmViewModel = new AddEditFilmViewModel();
                addEditFilmView.DataContext = addEditFilmViewModel;
                addEditFilmView.ShowDialog();

                if (addEditFilmViewModel.IsValidForm)
                {
                    FilmList.Add(addEditFilmViewModel.BuildFilm());
                }
            });
        }
    }
}
