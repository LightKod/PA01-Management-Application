using Microsoft.EntityFrameworkCore;
using PA01_Management_Application.MVVM.Models;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.ViewModel.Service
{
    public interface IPurchasedTicketService
    {
        List<PurchasedTicket> GetPurchasedTicketsForUser(int userId, int page, int pageSize);
        Task<int> GetTotalPurchasedTicketsCount(int userId);
    }

    public class PurchasedTicketService : IPurchasedTicketService
    {
        private readonly MovieManagementContext _context;

        public PurchasedTicketService(MovieManagementContext context)
        {
            _context = context;
        }

        public List<PurchasedTicket> GetPurchasedTicketsForUser(int userId, int page, int pageSize)
        {
            int startIndex = (page - 1) * pageSize;

            var bookings = _context.Bookings
                .Where(b => b.UserId == userId)

                .Skip(startIndex)
                .Take(pageSize)
                .Select(b => new PurchasedTicket
                {
                    BookingCode = b.BookingId,
                    FilmPoster = b.Schedule.Movie.PosterPath,
                    FilmName = b.Schedule.Movie.Title,
                    FilmGenres = string.Join(", ", b.Schedule.Movie.Genres.Select(g => g.GenreName)),
                    FilmDuration = b.Schedule.Movie.RunTime,
                    FilmRating = b.Schedule.Movie.VoteAverage,
                    ReleaseDate = b.Schedule.Movie.ReleaseDate,
                    Time = b.Schedule.ScheduleStart,
                    Cinema = b.Schedule.Room.Cinema.CinemaName,
                    CinemaLocation = b.Schedule.Room.Cinema.CinemaName,
                    Seat = b.SeatId,
                    Price = b.Price
                })
                .ToList();

            return bookings;
        }

        public async Task<int> GetTotalPurchasedTicketsCount(int userId)
        {
            return await _context.Bookings.CountAsync(b => b.UserId == userId);
        }
    }
}
