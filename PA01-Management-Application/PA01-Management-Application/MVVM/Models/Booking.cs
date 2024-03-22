using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? ScheduleId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? SeatId { get; set; }

    public double? Price { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual User? User { get; set; }
}
