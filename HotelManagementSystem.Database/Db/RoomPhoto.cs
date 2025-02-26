namespace HotelManagementSystem.Database.Db;

public partial class RoomPhoto
{
    public string PhotoId { get; set; } = null!;

    public string RoomId { get; set; } = null!;

    public string PhotoUrl { get; set; } = null!;

    public string? Description { get; set; }
}
