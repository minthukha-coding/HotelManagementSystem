using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Booking
{
    public string BookingId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string RoomId { get; set; } = null!;

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public string BookingStatus { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
