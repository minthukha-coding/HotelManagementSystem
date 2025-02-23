using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class RoomPhoto
{
    public int PhotoId { get; set; }

    public int RoomId { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public string? Description { get; set; }
}
