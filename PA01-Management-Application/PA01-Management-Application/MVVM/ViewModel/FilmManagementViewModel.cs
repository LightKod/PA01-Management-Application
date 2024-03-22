using PA01_Management_Application.Core;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.View;
using PA01_Management_Application.MVVM.ViewModel.Service;
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
        MovieService service = new();

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
            GetAllFilms();

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
                        Film newFilm = addEditFilmViewModel.BuildFilm();

                        service.DeleteGenresByMovieId(filmToEdit.FilmID);
                        service.DeleteDirectorsByMovieId(filmToEdit.FilmID);
                        service.RemoveAllActorsFromMovie(filmToEdit.FilmID);
                        service.UpdateMovie(filmToEdit.FilmID, newFilm);
                        service.AddGenresToMovie(filmToEdit.FilmID, newFilm.FilmGenres);
                        service.AddDirectorToMovie(filmToEdit.FilmID, newFilm.Directors[0]);
                        service.AddActorsToMovie(filmToEdit.FilmID, newFilm.Stars);

                        var index = FilmList.IndexOf(filmToEdit);
                        FilmList.RemoveAt(index);
                        FilmList.Insert(index, newFilm);
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
                    Film newFilm = addEditFilmViewModel.BuildFilm();

                    int id = service.AddMovieToDatabase(newFilm);
                    service.AddGenresToMovie(id, newFilm.FilmGenres);
                    service.AddDirectorToMovie(id, newFilm.Directors[0]);
                    service.AddActorsToMovie(id, newFilm.Stars);

                    newFilm.FilmID = id;
                    FilmList.Add(newFilm);
                }
            });
        }

        private async Task GetAllFilms()
        {
            var movies = service.GetAllMovies();
            foreach (var movie in movies)
            {
                Film f = new();

                f.FilmID = movie.MovieId;
                f.FilmName = movie.Title;
                f.FilmGenres = await service.GetGenresByMovieIdAsync(movie.MovieId);
                f.FilmDuration = movie.RunTime.Value;
                f.FilmRating = movie.VoteAverage.Value;
                f.FilmTrailer = "Videos/video.mp4";
                f.FilmBanner = new string[] { movie.BackdropPath };
                f.FilmPoster = movie.PosterPath;
                f.Directors = new string[] { await service.GetDirectorNameByMovieIdAsync(movie.MovieId) };
                f.Stars = await service.GetActorsByMovieIdAsync(movie.MovieId);
                f.FilmDescription = movie.Overview;

                FilmList.Add(f);
            }
        }
    }
}
