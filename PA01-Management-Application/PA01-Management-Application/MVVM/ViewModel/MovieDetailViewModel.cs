using PA01_Management_Application.Core;
using PA01_Management_Application.DataManagers;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.Models;
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
        public RelayCommand OpenPersonWindowCommand { get; set; }

        MovieService service = new();

        public MovieDetailViewModel() 
        {
            BookClickCommand = new(BookClickCommandExecute);
            OpenPersonWindowCommand = new(OpenPersonWindowCommandExecute);
        }

        private void OpenPersonWindowCommandExecute(object obj)
        {
            if(obj is PA01_Management_Application.MVVM.Models.Person person)
            {
                PersonDetailWindow personView = new(person);
                personView.Show();
            }
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
            f.Duration = ConvertMinutesToHoursAndMinutes(movie.RunTime.Value);
            f.Certification = movie.Certification;
            f.ReleaseYear = movie.ReleaseDate.Value.Year.ToString();
            f.DirectorsObj = new PA01_Management_Application.MVVM.Models.Person[] { await service.GetDirectorByMovieIdAsync(movieID) };
            f.StarsObj = await service.GetPersonActorsByMovieIdAsync(movieID);
            if (string.IsNullOrEmpty(f.Certification)) f.Certification = "PG-13";
            f.SmallBannerText = $"{f.ReleaseYear} · {f.Certification} · {f.Duration}";
            Film = null;
            Film = f;

            BookingDataHolder.movie = movie;
            BookingDataHolder.film = Film;

        }

        string ConvertMinutesToHoursAndMinutes(int totalMinutes)
        {
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;

            string formattedTime = $"{hours}h {minutes:00}m";

            return formattedTime;
        }
        private void BookClickCommandExecute(object obj)
        {

            (Application.Current.MainWindow.DataContext as AppWindowViewModel).CurrentView = new BookingScreeningTimeViewModel();
        }
    }
}
