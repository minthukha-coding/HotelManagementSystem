using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? NotificationDate { get; set; }

    public string SentVia { get; set; } = null!;

    public virtual User? User { get; set; }
}
