using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int? CinemaId { get; set; }

    public virtual Cinema? Cinema { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
