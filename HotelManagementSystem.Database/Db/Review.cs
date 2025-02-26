namespace HotelManagementSystem.Database.Db;

public partial class Review
{
    public string ReviewId { get; set; } = null!;

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }
}
