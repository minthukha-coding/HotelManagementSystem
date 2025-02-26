namespace HotelManagementSystem.Database.Db;

public partial class Room
{
    public string RoomId { get; set; } = null!;

    public string RoomNumber { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Status { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }
}
