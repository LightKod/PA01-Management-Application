using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PA01_Management_Application.MVVM.Models;

public partial class MovieManagementContext : DbContext
{
    public MovieManagementContext()
    {
    }

    public MovieManagementContext(DbContextOptions<MovieManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingFood> BookingFoods { get; set; }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-1P79QFBT; Trusted_Connection=Yes; Initial Catalog=MovieManagement; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__booking__5DE3A5B144A3039D");

            entity.ToTable("booking");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.SeatId)
                .HasMaxLength(50)
                .HasColumnName("seat_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK__booking__schedul__571DF1D5");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__booking__user_id__5629CD9C");
        });

        modelBuilder.Entity<BookingFood>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__bookingF__5DE3A5B19BA079D4");

            entity.ToTable("bookingFood");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Food).WithMany(p => p.BookingFoods)
                .HasForeignKey(d => d.FoodId)
                .HasConstraintName("FK__bookingFo__FoodI__5DCAEF64");

            entity.HasOne(d => d.Schedule).WithMany(p => p.BookingFoods)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK__bookingFo__sched__5CD6CB2B");

            entity.HasOne(d => d.User).WithMany(p => p.BookingFoods)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__bookingFo__user___5BE2A6F2");
        });

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.CinemaId).HasName("PK__cinemas__56628778A34EF0B1");

            entity.ToTable("cinemas");

            entity.Property(e => e.CinemaId)
                .ValueGeneratedNever()
                .HasColumnName("cinema_id");
            entity.Property(e => e.CinemaName)
                .HasMaxLength(255)
                .HasColumnName("cinema_name");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("PK__Food__856DB3EB75D45253");

            entity.ToTable("Food");

            entity.Property(e => e.FoodName).HasMaxLength(255);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__genres__18428D42B3FB1659");

            entity.ToTable("genres");

            entity.HasIndex(e => e.GenreName, "UQ__genres__1E98D151B487ED12").IsUnique();

            entity.Property(e => e.GenreId)
                .ValueGeneratedNever()
                .HasColumnName("genre_id");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .HasColumnName("genre_name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__movies__83CDF7499ADE7DEA");

            entity.ToTable("movies");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("movie_id");
            entity.Property(e => e.Adult).HasColumnName("adult");
            entity.Property(e => e.BackdropPath)
                .HasMaxLength(255)
                .HasColumnName("backdrop_path");
            entity.Property(e => e.DirectorId).HasColumnName("director_id");
            entity.Property(e => e.OriginalLanguage)
                .HasMaxLength(2)
                .HasColumnName("original_language");
            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(255)
                .HasColumnName("original_title");
            entity.Property(e => e.Overview).HasColumnName("overview");
            entity.Property(e => e.Popularity).HasColumnName("popularity");
            entity.Property(e => e.PosterPath)
                .HasMaxLength(255)
                .HasColumnName("poster_path");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Video).HasColumnName("video");
            entity.Property(e => e.VoteAverage).HasColumnName("vote_average");
            entity.Property(e => e.VoteCount).HasColumnName("vote_count");

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK__movies__director__4222D4EF");

            entity.HasMany(d => d.Actors).WithMany(p => p.MoviesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieActor",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_act__actor__45F365D3"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_act__movie__44FF419A"),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorId").HasName("PK__movie_ac__DB7FB33264377860");
                        j.ToTable("movie_actor");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");
                        j.IndexerProperty<int>("ActorId").HasColumnName("actor_id");
                    });

            entity.HasMany(d => d.Genres).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_gen__genre__4CA06362"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__movie_gen__movie__4BAC3F29"),
                    j =>
                    {
                        j.HasKey("MovieId", "GenreId").HasName("PK__movie_ge__B249DF9DAEC9C707");
                        j.ToTable("movie_genres");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");
                        j.IndexerProperty<int>("GenreId").HasColumnName("genre_id");
                    });
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC27354C981A");

            entity.ToTable("Person");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__room__19675A8A9A976353");

            entity.ToTable("room");

            entity.Property(e => e.RoomId)
                .ValueGeneratedNever()
                .HasColumnName("room_id");
            entity.Property(e => e.CinemaId).HasColumnName("cinema_id");

            entity.HasOne(d => d.Cinema).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.CinemaId)
                .HasConstraintName("FK__room__cinema_id__4F7CD00D");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__schedule__C46A8A6F83CCD4DE");

            entity.ToTable("schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.ScheduleDate).HasColumnName("schedule_date");
            entity.Property(e => e.ScheduleStart).HasColumnName("schedule_start");

            entity.HasOne(d => d.Movie).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__schedule__movie___52593CB8");

            entity.HasOne(d => d.Room).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__schedule__room_i__534D60F1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F55C6FCDF");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .HasColumnName("avatar");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Point).HasColumnName("point");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
