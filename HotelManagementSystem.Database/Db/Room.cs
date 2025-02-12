using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Room
{
    public string RoomId { get; set; } = null!;

    public string RoomNumber { get; set; } = null!;

    public string RoomCategory { get; set; } = null!;

    public string? Description { get; set; }

    public decimal PricePerNight { get; set; }

    public string AvailabilityStatus { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
