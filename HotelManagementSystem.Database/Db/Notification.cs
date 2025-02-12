using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Notification
{
    public string NotificationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string NotificationType { get; set; } = null!;

    public DateTime? SentAt { get; set; }

    public virtual User User { get; set; } = null!;
}
