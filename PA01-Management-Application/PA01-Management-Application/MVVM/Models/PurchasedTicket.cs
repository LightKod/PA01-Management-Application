using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.Models
{
    public class PurchasedTicket
    {
        public int BookingCode { get; set; }
        public string FilmName { get; set; }
        public string FilmGenres { get; set; }
        public int? FilmDuration { get; set; }
        public double? FilmRating { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public TimeOnly? Time { get; set; }
        public string Cinema { get; set; }
        public string CinemaLocation { get; set; }
        public string Seat { get; set; }
        public double? Price { get; set; }
        public string FilmPoster { get; set; }
    }
}
