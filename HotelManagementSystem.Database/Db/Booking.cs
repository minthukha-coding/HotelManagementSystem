using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Booking
{
    public string BookingId { get; set; } = null!;

    public string CustomerId { get; set; } = null!;

    public string RoomId { get; set; } = null!;

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
