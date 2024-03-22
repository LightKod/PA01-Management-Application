using PA01_Management_Application.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel.Service
{
    internal class ReportService
    {
        // Lấy Booking Vé theo tuần 
        // Lấy giá Booking food theo tuần

        // Lấy giá Booing vé theo tháng
        //Lấy giá booking food theo tháng

        // Lấy giá Booing vé theo năm 
        //Lấy giá booking food theo năm

        //lấy giá Booking vé  date to date
        //lấy giá booking food date to date

        private readonly MovieManagementContext _context = new MovieManagementContext();

        // Lấy danh sách các booking của một bộ phim trong khoảng thời gian từ dateStart đến dateEnd, và trả về danh sách các đối tượng { date: price: }
        public async Task<List<(DateTime? date, double? price)>> GetBookingsByMovieIdAndDateRangeAsync(int movieId, DateTime dateStart, DateTime dateEnd)
        {
            var bookings = _context.Bookings
                .Where(b => b.Schedule != null && b.Schedule.MovieId == movieId && b.BookingDate >= dateStart && b.BookingDate <= dateEnd)
                .GroupBy(b => b.BookingDate)
                .Select(group => new { date = group.Key, price = group.Sum(b => b.Price) })
                .ToList();
            //Debug.WriteLine(bookings);
            //var bookings2 = _context.Bookings.Where(b => b.Schedule != null && b.Schedule.MovieId == movieId && b.BookingDate >= dateStart && b.BookingDate <= dateEnd).ToList();
            var foodBookings = _context.BookingFoods
              .Where(bf => bf.Schedule != null && bf.Schedule.MovieId == movieId && bf.BookingDate >= dateStart && bf.BookingDate <= dateEnd)
              .GroupBy(bf => bf.BookingDate)
              .Select(group => new { date = group.Key, price = group.Sum(bf => bf.Price) })
              .ToList();

            var combinedBookings = bookings
                .Concat(foodBookings)
                .GroupBy(b => b.date)
                .Select(group => new { date = group.Key, price = group.Sum(b => b.price) })
                .ToList();

            return combinedBookings.Select(b => (b.date, b.price)).ToList();
            //return bookings.Select(b => (b.date, b.price)).ToList();
        }

        public async Task<List<(DateTime? date, double? price)>> GetBookingsByMovieId(int movieId)
        {
            var bookings = _context.Bookings
                .Where(b => b.Schedule != null && b.Schedule.MovieId == movieId)
                .GroupBy(b => b.BookingDate)
                .Select(group => new { date = group.Key, price = group.Sum(b => b.Price) })
                .ToList();

            var foodBookings = _context.BookingFoods
              .Where(bf => bf.Schedule != null && bf.Schedule.MovieId == movieId)
              .GroupBy(bf => bf.BookingDate)
              .Select(group => new { date = group.Key, price = group.Sum(bf => bf.Price) })
              .ToList();

            var combinedBookings = bookings
                .Concat(foodBookings)
                .GroupBy(b => b.date)
                .Select(group => new { date = group.Key, price = group.Sum(b => b.price) })
                .ToList();

            return combinedBookings.Select(b => (b.date, b.price)).ToList();
        }

    }
}
