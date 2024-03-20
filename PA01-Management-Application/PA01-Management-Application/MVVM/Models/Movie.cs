using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? Title { get; set; }

    public string? OriginalTitle { get; set; }

    public string? Overview { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public double? Popularity { get; set; }

    public double? VoteAverage { get; set; }

    public int? VoteCount { get; set; }

    public int? RunTime { get; set; }

    public bool? Adult { get; set; }

    public string? Certification { get; set; }

    public string? BackdropPath { get; set; }

    public string? PosterPath { get; set; }

    public bool? Video { get; set; }

    public string? OriginalLanguage { get; set; }

    public int? DirectorId { get; set; }

    public virtual Person? Director { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Person> Actors { get; set; } = new List<Person>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
