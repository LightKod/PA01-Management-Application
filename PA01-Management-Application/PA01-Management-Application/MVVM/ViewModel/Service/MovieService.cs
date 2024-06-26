﻿using Microsoft.EntityFrameworkCore;
using PA01_Management_Application.MVVM.Model;
using PA01_Management_Application.MVVM.Models;
using System.Diagnostics;

namespace PA01_Management_Application.MVVM.ViewModel.Service
{
    internal class MovieService
    {
        // thêm RunTime vào dữ liệu

        private readonly MovieManagementContext _context = new MovieManagementContext();


        //HomePage
        //Lấy danh sách film đang được chiếu lấy tất cả (10 phim) schedular
        public List<Movie> GetAllScreeningMovies()
        {
            using (var context = new MovieManagementContext())
            {
                return context.Movies.Take(10).ToList();
            }
        }

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

        public async Task<PA01_Management_Application.MVVM.Models.Person[]> GetPersonActorsByMovieIdAsync(int movieId)
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

        public async Task<PA01_Management_Application.MVVM.Models.Person?> GetDirectorByMovieIdAsync(int movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.MovieId == movieId);


            return movie.Director;
        }


        //Search 
        //Search các phim có chứa chuỗi params
        public List<Movie> SearchMoviesByTitle(string searchString)
        {
            // Chuyển đổi chuỗi tìm kiếm thành chữ thường để so sánh không phân biệt chữ hoa chữ thường
            string searchLower = searchString.ToLower();

            // Lọc danh sách phim theo tiêu đề chứa chuỗi tìm kiếm
            var matchedMovies = _context.Movies
                                        .Where(m => m.OriginalTitle.ToLower().Contains(searchLower))
                                        .ToList();

            return matchedMovies;
        }
        //Search các film có chứa chuỗi tên film,tên của đạo diễn,tên của diễn viên ,phát hành từ năm ,thời lượng từ
        public List<Movie> SearchMovies(string searchString, string directorName, string actorName, int releaseYear)
        {
            // Chuyển đổi chuỗi tìm kiếm thành chữ thường để so sánh không phân biệt chữ hoa chữ thường
            string searchLower = searchString.ToLower();
            string directorLower = directorName.ToLower();
            string actorLower = actorName.ToLower();

            // Lọc danh sách phim dựa trên các tiêu chí tìm kiếm
            var matchedMovies = _context.Movies
                                        .Where(m => m.OriginalTitle.ToLower().Contains(searchLower) &&
                                                    m.ReleaseDate.HasValue &&
                                                    m.ReleaseDate.Value.Year >= releaseYear &&
                                                    m.Director.Name.ToLower().Contains(directorLower) &&
                                                    m.Actors.Any(actor => actor.Name.ToLower().Contains(actorLower)))
                                        .ToList();

            return matchedMovies;
        }

        //Thêm xóa sửa
        // xóa film by ID
        public bool DeleteMovieById(int movieId)
        {
            // Tìm bộ phim theo ID
            var movieToDelete = _context.Movies.FirstOrDefault(m => m.MovieId == movieId);

            if (movieToDelete != null)
            {
                // Xóa bộ phim từ DbSet
                _context.Movies.Remove(movieToDelete);

                // Lưu các thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                return true; // Trả về true nếu xóa thành công
            }
            else
            {
                return false; // Trả về false nếu không tìm thấy bộ phim
            }
        }

        //xóa toàn bộ genres thuộc về 1 movie (by Movie ID)
        public bool DeleteGenresByMovieId(int movieId)
        {
            // Tìm bộ phim theo ID
            var movie = _context.Movies.Include(m => m.Genres).FirstOrDefault(m => m.MovieId == movieId);

            if (movie != null)
            {
                // Xóa tất cả các thể loại của bộ phim
                movie.Genres.Clear();
                // Lưu các thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                return true; // Trả về true nếu xóa thành công
            }
            else
            {
                return false; // Trả về false nếu không tìm thấy bộ phim
            }
        }

        //xóa toàn bộ diễn viên thuộc về 1 movie (by Movie ID)
        public bool RemoveAllActorsFromMovie(int movieId)
        {
            // Tìm bộ phim theo ID
            var movie = _context.Movies.Include(m => m.Actors).FirstOrDefault(m => m.MovieId == movieId);

            if (movie != null)
            {
                // Xóa tất cả các diễn viên liên quan đến bộ phim
                movie.Actors.Clear();
                Debug.WriteLine("đã xóa");

                // Lưu các thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                return true; // Trả về true nếu xóa thành công
            }
            else
            {
                return false; // Trả về false nếu không tìm thấy bộ phim
            }
        }

        //xóa toàn bộ  đạo diễn thuộc về 1 movie (by Movie ID)
        public void DeleteDirectorsByMovieId(int movieId)
        {
            // Tìm bộ phim theo Movie ID
            var movie = _context.Movies
                .Include(m => m.Director) // Bao gồm thông tin đạo diễn của bộ phim
                .FirstOrDefault(m => m.MovieId == movieId);

            if (movie != null)
            {
                // Xóa tất cả các đạo diễn của bộ phim
                movie.Director = null;
                movie.DirectorId = null;
                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }
        }


        //add lại genres từ 1 mảng string
        public bool AddGenresToMovie(int movieId, string[] genreNames)
        {
            // Tìm bộ phim theo ID
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == movieId);

            if (movie != null)
            {
                // Lấy hoặc tạo mới các thể loại từ mảng chuỗi genreNames
                var genres = new List<Genre>();
                foreach (var genreName in genreNames)
                {
                    // Kiểm tra xem thể loại đã tồn tại trong cơ sở dữ liệu chưa
                    var existingGenre = _context.Genres.FirstOrDefault(g => g.GenreName == genreName);
                    if (existingGenre != null)
                    {
                        genres.Add(existingGenre); // Sử dụng thể loại đã tồn tại
                    }
                    else
                    {
                        // Tạo mới thể loại nếu không tồn tại
                        var newGenre = new Genre { GenreName = genreName };
                        genres.Add(newGenre);
                    }
                }

                // Thêm các thể loại vào bộ phim
                foreach (var genre in genres)
                {
                    movie.Genres.Add(genre);
                }

                // Lưu các thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                return true; // Trả về true nếu thêm thành công
            }
            else
            {
                return false; // Trả về false nếu không tìm thấy bộ phim
            }
        }

        //add lại diễn viên từ 1 mảng string 
        public void AddActorsToMovie(int movieId, string[] actorNames)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == movieId);

            if (movie != null)
            {
                foreach (var actorName in actorNames)
                {
                    // Tìm kiếm diễn viên trong cơ sở dữ liệu bằng tên
                    var existingActor = _context.People.FirstOrDefault(p => p.Name == actorName);

                    // Nếu diễn viên đã tồn tại, thêm vào phim
                    if (existingActor != null)
                    {
                        movie.Actors.Add(existingActor);
                    }
                    // Nếu diễn viên chưa tồn tại, tạo mới và thêm vào phim
                    else
                    {
                        var newActor = new PA01_Management_Application.MVVM.Models.Person { Name = actorName };
                        movie.Actors.Add(newActor);
                    }
                }

                _context.SaveChanges();
            }
        }

        //add lại đạo diễn từ 1 mảng string 
        public void AddDirectorToMovie(int movieId, string directorName)
        {
            // Kiểm tra xem đạo diễn đã tồn tại trong cơ sở dữ liệu chưa
            var director = _context.People.FirstOrDefault(p => p.Name == directorName);


            // Tìm bộ phim cần thêm đạo diễn
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == movieId);
            if (movie != null)
            {
                // Liên kết đạo diễn với bộ phim
                movie.Director = director;
                _context.SaveChanges();
            }
            else
            {
                // Xử lý trường hợp không tìm thấy bộ phim
                // (ví dụ: throw exception hoặc log thông báo lỗi)
            }
        }
        //Update 1 Movie
        public void UpdateMovie(int movieId, Film film)
        {
            // Tìm bộ phim cần cập nhật trong cơ sở dữ liệu
            var movieToUpdate = _context.Movies.FirstOrDefault(m => m.MovieId == movieId);

            if (movieToUpdate != null)
            {
                // Cập nhật các thuộc tính của bộ phim
                movieToUpdate.Title = film.FilmName;
                movieToUpdate.OriginalTitle = film.FilmName;
                movieToUpdate.Overview = film.FilmDescription;
                movieToUpdate.VoteAverage = film.FilmRating;
                movieToUpdate.RunTime = film.FilmDuration;
                movieToUpdate.BackdropPath = film.FilmBanner[0];
                movieToUpdate.PosterPath = film.FilmPoster;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }
        }

        //thêm 1 Movie vào dataBase
        public int AddMovieToDatabase(Film film)
        {
            // Tạo một đối tượng Movie mới
            var newMovie = new Movie
            {
                Title = film.FilmName,
                OriginalTitle = film.FilmName,
                Overview = film.FilmDescription,
                ReleaseDate = DateOnly.FromDateTime(DateTime.Today),
                VoteAverage = film.FilmRating,
                VoteCount = 1,
                RunTime = film.FilmDuration,
                BackdropPath = film.FilmBanner[0],
                PosterPath = film.FilmPoster
            };

            // Thêm đối tượng Movie mới vào DbSet của DbContext
            _context.Movies.Add(newMovie);

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            return newMovie.MovieId;
        }
        //lấy movie by id (Có ở trên rồi)

        //Lấy tất cả tên của thể loại Movie (có ở trên rồi)
        public List<string> GetAllGenreNames()
        {
            // Truy vấn tất cả các tên của Genre từ cơ sở dữ liệu
            var genreNames = _context.Genres.Select(p => p.GenreName).ToList();

            return genreNames;
        }

        //Lấy tất cả tên của Person
        public List<string> GetAllPersonNames()
        {
            // Truy vấn tất cả các tên của Person từ cơ sở dữ liệu
            var personNames = _context.People.Select(p => p.Name).ToList();

            return personNames;
        }


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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="seatId"></param>
        /// <returns>True: booked | False: unbooked</returns>
        public bool CheckIfBooked(int scheduleId, string seatId)
        {
            bool check = _context.Bookings.FirstOrDefault(x => x.ScheduleId == scheduleId && x.SeatId == seatId) != null;
            Debug.WriteLine(check);
            return check;
        }

        public void DeleteMovie(int movieId)
        {
            // Xác định phim cần xóa
            Movie movieToDelete = _context.Movies
                                .Include(m => m.Director)
                                .Include(m => m.Genres)
                                .Include(m => m.Actors)
                                .Include(m => m.Schedules)
                                .FirstOrDefault(m => m.MovieId == movieId);

            if (movieToDelete == null)
            {
                // Phim không tồn tại trong cơ sở dữ liệu
                return;
            }

            // Xóa tất cả các lịch chiếu liên quan đến phim
            foreach (var schedule in movieToDelete.Schedules.ToList())
            {
                var scheduleToDelete = _context.Schedules.Include(m => m.Bookings).FirstOrDefault(m => m.ScheduleId == schedule.ScheduleId);
                // Xóa tất cả các đặt vé liên quan đến lịch chiếu
                foreach (var booking in scheduleToDelete.Bookings.ToList())
                {

                    var bookingToDelete = _context.Bookings.Include(m => m.Schedule).FirstOrDefault(m => m.BookingId == booking.BookingId);

                    if (bookingToDelete != null)
                    {
                        _context.Bookings.Remove(bookingToDelete);
                    }
                    Debug.WriteLine("aaaa");
                }
                foreach (var bookingFood in schedule.BookingFoods.ToList())
                {
                    _context.BookingFoods.Remove(bookingFood);
                }



            }

            foreach (var schedule in movieToDelete.Schedules.ToList())
            {
                _context.Schedules.Remove(schedule);
            }
            // Xóa các liên kết với đạo diễn, diễn viên và thể loại
            movieToDelete.Director = null;
            movieToDelete.Actors.Clear();
            movieToDelete.Genres.Clear();
            movieToDelete.Schedules.Clear();
            // Xóa phim khỏi cơ sở dữ liệu
            _context.Movies.Remove(movieToDelete);

            // Lưu các thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();
        }


    }
}
