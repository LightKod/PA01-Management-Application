using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Avatar { get; set; }

    public string? Fullname { get; set; }

    public DateOnly? Birthday { get; set; }

    public int? Gender { get; set; }

    public string? Email { get; set; }

    public string? City { get; set; }

    public int? Rules { get; set; }

    public string? Phone { get; set; }

    public int? Point { get; set; }

    public virtual ICollection<BookingFood> BookingFoods { get; set; } = new List<BookingFood>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
