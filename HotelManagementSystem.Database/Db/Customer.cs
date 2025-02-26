namespace HotelManagementSystem.Database.Db;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
