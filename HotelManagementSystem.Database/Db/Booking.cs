using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? RoomId { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public string? Status { get; set; }

    public DateTime? BookingDate { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Room? Room { get; set; }

    public virtual User? User { get; set; }
}
