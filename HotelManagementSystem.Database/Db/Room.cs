using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public int? CategoryId { get; set; }

    public string? Status { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual RoomCategory? Category { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
