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
    class HomeViewModel : BaseViewModel
    {
        MovieService service = new();
        public ObservableCollection<FilmSet> FilmSets { get; set; }

        public RelayCommand MovieDetailCommand { get; set; }
        public MovieDetailViewModel MovieDetailVM{ get; set; }

        public HomeViewModel()
        {
            FilmSets = new ObservableCollection<FilmSet>();

            GetMovieSets();

            MovieDetailVM = new MovieDetailViewModel();
            MovieDetailCommand = new RelayCommand(o =>
            {
                if(o is Film selectedFilm)
                {
                    BookingDataHolder.movieID = selectedFilm.FilmID;
                    (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = MovieDetailVM;
                }
            });
        }

        public async Task GetMovieSets()
        {
            var screeningMovieList = service.GetAllScreeningMovies();
            ObservableCollection<Film> screeningFilmList = new ObservableCollection<Film>();

            foreach (var movie in screeningMovieList)
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

                screeningFilmList.Add(f);
            };

            FilmSets.Add(new FilmSet()
            {
                SetTitle = "Currently Showing",
                FilmList = screeningFilmList
            });

            var popularMovieList = service.GetTop10PopularMovies();
            ObservableCollection<Film> popularFilmList = new ObservableCollection<Film>();

            foreach (var movie in popularMovieList)
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

                popularFilmList.Add(f);
            };

            FilmSets.Add(new FilmSet()
            {
                SetTitle = "Popular Shows",
                FilmList = popularFilmList
            });
        }
    }
}
