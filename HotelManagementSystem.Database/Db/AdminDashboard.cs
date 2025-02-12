using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class AdminDashboard
{
    public int? TotalBookings { get; set; }

    public int? TotalCustomers { get; set; }

    public decimal? TotalRevenue { get; set; }
}
