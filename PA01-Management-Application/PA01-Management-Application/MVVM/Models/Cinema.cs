using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class Cinema
{
    public int CinemaId { get; set; }

    public string? CinemaName { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
