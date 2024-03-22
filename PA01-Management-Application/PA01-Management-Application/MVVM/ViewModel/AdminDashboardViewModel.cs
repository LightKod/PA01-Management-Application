using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel
{
    public class AdminDashboardViewModel : BaseViewModel
    {
        ObservableCollection<MovieReportData> movieData;

        public ObservableCollection<MovieReportData> MovieData
        {
            get
            {
                return movieData;
            }
            set
            {
                movieData = value;
                OnPropertyChanged(nameof(MovieData));
            }
        }

        MovieManagementContext context = new();


        public AdminDashboardViewModel()
        {
            FetchData();
        }



        async void FetchData()
        {
            List<PA01_Management_Application.MVVM.Models.Booking> bookingData = context.Bookings.ToList();
            List<Schedule> schedules = context.Schedules.ToList();
            List<Movie> movies = context.Movies.ToList();
            MovieData = new();
            foreach (var booking in bookingData)
            {
                var sche = schedules.Find(x => x.ScheduleId == booking.ScheduleId);
                if (sche == null) return;
                MovieReportData existData = MovieData.FirstOrDefault(x => x.ID == sche.MovieId);

                if(existData != null)
                {
                    existData.TotalPrice += booking.Price.Value;
                    existData.TicketAmount += 1;
                }
                else
                {
                    Movie movie = movies.Find(x => x.MovieId == sche.MovieId); 

                    MovieReportData data = new();
                    data.ID = sche.MovieId.Value;
                    data.Name = movie.Title;
                    data.TotalPrice = booking.Price.Value;
                    data.TicketAmount = 1;
                    movieData.Add(data);
                }
            }

        }

    }
}
