using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.Models;
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
    class MovieDetailViewModel : BaseViewModel
    {
        private Film _film;
        public Film Film{ 
            get { return _film; }
            set 
            {
                _film = value;
                OnPropertyChanged(nameof(Film));
            } 
        }

        public RelayCommand BookClickCommand { get; set; }

        MovieService service = new();

        public MovieDetailViewModel() 
        {
            BookClickCommand = new(BookClickCommandExecute);
        }


        public async Task GetMovieDetail()
        {
            int movieID = BookingDataHolder.movieID;
            Movie movie = service.GetMovieById(BookingDataHolder.movieID);
            Film f = new();

            f.FilmName = movie.Title;
            f.FilmGenres = await service.GetGenresByMovieIdAsync(movieID);
            f.FilmRating = movie.VoteAverage.Value;
            f.FilmTrailer = "Videos/video.mp4";
            f.FilmBanner = new string[] { movie.BackdropPath };
            f.FilmPoster = movie.PosterPath;
            f.Directors = new string[] { await service.GetDirectorNameByMovieIdAsync(movieID) };
            f.Stars = await service.GetActorsByMovieIdAsync(movieID);
            f.FilmDescription = movie.Overview;

            Film = null;
            Film = f;

            BookingDataHolder.movie = movie;
            BookingDataHolder.film = Film;

        }

        private void BookClickCommandExecute(object obj)
        {

            (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = new BookingScreeningTimeViewModel();
        }
    }
}
