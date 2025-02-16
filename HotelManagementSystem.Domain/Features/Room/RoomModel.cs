namespace HotelManagementSystem.Domain.Features.Room;

public class RoomModel
{
    public int RoomId { get; set; }
    public string RoomNumber { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Status { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
}