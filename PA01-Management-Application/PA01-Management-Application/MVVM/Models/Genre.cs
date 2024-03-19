using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
