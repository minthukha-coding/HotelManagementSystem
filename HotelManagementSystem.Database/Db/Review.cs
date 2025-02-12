using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Review
{
    public string ReviewId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string RoomId { get; set; } = null!;

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
