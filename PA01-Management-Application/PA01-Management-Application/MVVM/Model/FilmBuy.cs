using System;
using System.ComponentModel;

namespace PA01_Management_Application.MVVM.Model
{
    public class FilmBuy : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _filmName;
        public string FilmName
        {
            get { return _filmName; }
            set { _filmName = value; OnPropertyChanged(nameof(FilmName)); }
        }

        private string[] _filmGenres;
        public string[] FilmGenres
        {
            get { return _filmGenres; }
            set { _filmGenres = value; OnPropertyChanged(nameof(FilmGenres)); }
        }

        private int _filmDuration;
        public int FilmDuration
        {
            get { return _filmDuration; }
            set { _filmDuration = value; OnPropertyChanged(nameof(FilmDuration)); }
        }

        private double _filmRating;
        public double FilmRating
        {
            get { return _filmRating; }
            set { _filmRating = value; OnPropertyChanged(nameof(FilmRating)); }
        }

        private string _filmPoster;
        public string FilmPoster
        {
            get { return _filmPoster; }
            set { _filmPoster = value; OnPropertyChanged(nameof(FilmPoster)); }
        }

        private string _filmTrailer;
        public string FilmTrailer
        {
            get { return _filmTrailer; }
            set { _filmTrailer = value; OnPropertyChanged(nameof(FilmTrailer)); }
        }

        private string[] _filmBanner;
        public string[] FilmBanner
        {
            get { return _filmBanner; }
            set { _filmBanner = value; OnPropertyChanged(nameof(FilmBanner)); }
        }

        private string[] _directors;
        public string[] Directors
        {
            get { return _directors; }
            set { _directors = value; OnPropertyChanged(nameof(Directors)); }
        }

        private string[] _writers;
        public string[] Writers
        {
            get { return _writers; }
            set { _writers = value; OnPropertyChanged(nameof(Writers)); }
        }

        private string[] _stars;
        public string[] Stars
        {
            get { return _stars; }
            set { _stars = value; OnPropertyChanged(nameof(Stars)); }
        }

        public FilmBuy(string filmName, string[] filmGenres, int filmDuration, double filmRating, string filmPoster, string filmTrailer, string[] filmBanner, string[] directors, string[] writers, string[] stars)
        {
            FilmName = filmName;
            FilmGenres = filmGenres;
            FilmDuration = filmDuration;
            FilmRating = filmRating;
            FilmPoster = filmPoster;
            FilmTrailer = filmTrailer;
            FilmBanner = filmBanner;
            Directors = directors;
            Writers = writers;
            Stars = stars;
        }
    }
}
