using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class BookingFood
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? ScheduleId { get; set; }

    public int? FoodId { get; set; }

    public double? Price { get; set; }

    public virtual Food? Food { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual User? User { get; set; }
}
