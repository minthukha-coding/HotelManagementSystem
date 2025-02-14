using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }
}
