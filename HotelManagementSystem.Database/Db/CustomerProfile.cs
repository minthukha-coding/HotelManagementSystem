using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class CustomerProfile
{
    public string ProfileId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
