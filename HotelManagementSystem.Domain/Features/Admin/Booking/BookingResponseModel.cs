namespace HotelManagementSystem.App.Components.Pages.User.Booking
{
    public class BookingResponseModel
    {
        public string BookingId { get; set; } 
        public string Phone { get; set; } 
        public string CustomerName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
