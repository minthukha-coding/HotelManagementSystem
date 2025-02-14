using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? SentAt { get; set; }

    public string Status { get; set; } = null!;
}
