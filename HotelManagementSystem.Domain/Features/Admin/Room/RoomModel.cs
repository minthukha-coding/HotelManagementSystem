namespace HotelManagementSystem.Domain.Features.Room;

public class RoomModel
{
    public string RoomId { get; set; }
    public string RoomNumber { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Status { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public List<RoomPhotoModel> PhotoUrls { get; set; } = new List<RoomPhotoModel>();
}

public class RoomPhotoModel
{
    public int PhotoId { get; set; }
    public int RoomId { get; set; }
    public string PhotoUrl { get; set; } = null!;
    public string? Description { get; set; }
}