using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class Person
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Avatar { get; set; }

    public string? Biography { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Movie> MoviesNavigation { get; set; } = new List<Movie>();
}
