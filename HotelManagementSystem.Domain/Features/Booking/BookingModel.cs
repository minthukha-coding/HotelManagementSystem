namespace HotelManagementSystem.Domain.Features.Booking;

public class BookingModel
{
    public string BookingId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerId { get; set; }
    public string Phone { get; set; }
    public string RoomNumber { get; set; }
    public string RoomId { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public string Status { get; set; }
    public int NumberOfDays { get; set; }
}