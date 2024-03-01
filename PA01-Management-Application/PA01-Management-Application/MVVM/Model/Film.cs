using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.Model
{
    class Film
    {
        public string FilmName { get; set; }
        public string FilmGenre { get; set; }
        public int FilmDuration { get; set; }
        public double FilmRating { get; set; }
        public string FilmPoster { get; set; }
        public string FilmTrailer { get; set; }
    }
}
