using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }
}
