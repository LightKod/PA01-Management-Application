using System;
using System.Collections.Generic;

namespace PA01_Management_Application.MVVM.Models;

public partial class Food
{
    public int FoodId { get; set; }

    public string? FoodName { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? FoodIdDescription { get; set; }

    public double? Price { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<BookingFood> BookingFoods { get; set; } = new List<BookingFood>();
}
