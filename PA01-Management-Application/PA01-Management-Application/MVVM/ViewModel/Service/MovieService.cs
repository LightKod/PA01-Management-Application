using Microsoft.EntityFrameworkCore;
using PA01_Management_Application.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA01_Management_Application.MVVM.ViewModel.Service
{
    internal class MovieService
    {
        // thêm RunTime vào dữ liệu

        private readonly MovieManagementContext _context = new MovieManagementContext();


        //HomePage
        //Lấy danh sách film đang được chiếu lấy tất cả (10 phim) schedular
        //Lấy danh sách film có popularity top 10

        public List<Movie> GetTop10PopularMovies()
        {
            var top10Movies = _context.Movies.OrderByDescending(m => m.Popularity)
                                            .Take(10)
                                            .ToList();

            return top10Movies;
        }


        //MoveDetailPage
        //Lấy Film by ID
        public Movie GetMovieById(int movieId)
        {
            // Truy vấn cơ sở dữ liệu để lấy bộ phim theo ID
            return _context.Movies.FirstOrDefault(m => m.MovieId == movieId);
        }

        //Giúp gán dô Film
        // lấy Tất cả list movie từ dataBase
        public List<Movie> GetAllMovies()
        {
            using (var context = new MovieManagementContext())
            {
                return context.Movies.ToList();
            }
        }
        //lấy danh sách thể loại theo film (By movie id)
        public async Task<string[]> GetGenresByMovieIdAsync(int movieId)
        {
            var genres = await _context.Movies
                .Where(m => m.MovieId == movieId)
                .SelectMany(m => m.Genres.Select(g => g.GenreName))
                .ToArrayAsync();

            return genres;
        }
        //lấy danh sách diễn viên theo id(id)
        public async Task<string[]> GetActorsByMovieIdAsync(int movieId)
        {
            var actors = await _context.Movies
                .Where(m => m.MovieId == movieId)
                .SelectMany(m => m.Actors.Select(a => a.Name))
                .ToArrayAsync();

            return actors;
        }

        public async Task<Person[]> GetPersonActorsByMovieIdAsync(int movieId)
        {
            var actors = await _context.Movies
                .Where(m => m.MovieId == movieId)
                .SelectMany(m => m.Actors)
                .ToArrayAsync();

            return actors;
        }
        // lấy danh sách đạo diễn theo film (by Movie id)
        public async Task<string?> GetDirectorNameByMovieIdAsync(int movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.MovieId == movieId);

            if (movie != null)
            {
                return movie.Director?.Name;
            }

            return null;
        }


        //Search 
        //Search các phim có chứa chuỗi params 
        //Search các film có chứa chuỗi tên film,tên của đạo diễn,tên của diễn viên ,phát hành từ năm ,thời lượng từ

        //Thêm xóa sửa
        // xóa film by ID

        //xóa toàn bộ genres thuộc về 1 movie (by Movie ID)
        //xóa toàn bộ diễn viên thuộc về 1 movie (by Movie ID)
        //xóa toàn bộ  đạo diễn thuộc về 1 movie (by Movie ID)

        //add lại genres từ 1 mảng string 
        //add lại diễn viên từ 1 mảng string 
        //add lại đạo diễn từ 1 mảng string 


        //thêm 1 Movie vào dataBase

        //lấy movie by id 

        //Lấy tất cả tên của thể loại Movie
        //Lấy tất cả tên của Person



        //Lấy ListSchedular theo MovieId và theo ngày
        public List<Schedule> GetSchedulesByMovieIdAndDate(int movieId, DateOnly date)
        {
            var schedules = _context.Schedules
                                    .Where(s => s.MovieId == movieId && s.ScheduleDate == date)
                                    .ToList();

            return schedules;
        }

        //lấy tên cinema từ roomId
        public string GetCinemaNameByRoomId(int? roomId)
        {
            if (roomId.HasValue)
            {
                // Tìm Room theo roomId
                var room = _context.Rooms.FirstOrDefault(r => r.RoomId == roomId.Value); // Sử dụng roomId.Value để truy cập giá trị int

                if (room != null)
                {
                    // Tìm Cinema theo CinemaId của Room
                    var cinema = _context.Cinemas.FirstOrDefault(c => c.CinemaId == room.CinemaId);

                    if (cinema != null)
                    {
                        // Trả về CinemaName
                        return cinema.CinemaName;
                    }
                }
            }

            // Trả về null nếu không tìm thấy hoặc nếu thông tin không đầy đủ
            return null;
        }





    }
}
