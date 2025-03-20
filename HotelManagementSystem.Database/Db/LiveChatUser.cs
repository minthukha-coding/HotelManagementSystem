using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class LiveChatUser
{
    public string LiveChatUserId { get; set; } = null!;

    public string? ConnectionId { get; set; }

    public string? UserId { get; set; }

    public int? IsActive { get; set; }
}
