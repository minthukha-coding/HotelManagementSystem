using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
