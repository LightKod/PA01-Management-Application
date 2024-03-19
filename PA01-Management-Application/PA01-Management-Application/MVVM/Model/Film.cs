using PA01_Management_Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.Model
{
    class Film : ObservableObject
    {
        public string FilmName { get; set; }
        public string[] FilmGenres { get; set; }
        public int FilmDuration { get; set; }
        public double FilmRating { get; set; }
        public string FilmPoster { get; set; }
        public string FilmDescription { get; set; }
        public string FilmTrailer { get; set; }
        public string[] FilmBanner { get; set; }
        public string[] Directors { get; set; }
        public string[] Writers { get; set; }
        public string[] Stars { get; set; }

        public Film(string filmName, string[] filmGenres, int filmDuration, double filmRating, string filmPoster, string filmTrailer, string[] filmBanner, string[] directors, string[] writers, string[] stars)
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
        public Film()
        {

        }
    }
}
