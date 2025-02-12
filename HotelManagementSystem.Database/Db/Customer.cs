using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public virtual User? User { get; set; }
}
