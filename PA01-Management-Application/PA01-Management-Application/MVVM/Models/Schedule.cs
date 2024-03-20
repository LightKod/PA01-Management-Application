using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? MovieId { get; set; }

    public int? RoomId { get; set; }

    public DateOnly? ScheduleDate { get; set; }

    public TimeOnly? ScheduleStart { get; set; }

    public virtual ICollection<BookingFood> BookingFoods { get; set; } = new List<BookingFood>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Movie? Movie { get; set; }

    public virtual Room? Room { get; set; }
}
