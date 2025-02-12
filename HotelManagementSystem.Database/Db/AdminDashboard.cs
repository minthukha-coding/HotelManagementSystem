using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class AdminDashboard
{
    public string DashboardId { get; set; } = null!;

    public int TotalBookings { get; set; }

    public decimal TotalRevenue { get; set; }

    public int ActiveCustomers { get; set; }

    public DateTime? LastUpdated { get; set; }
}
