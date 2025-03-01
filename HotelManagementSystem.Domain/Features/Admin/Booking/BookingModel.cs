namespace HotelManagementSystem.Domain.Features.Booking
{
    public class BookingModel
    {
        public string BookingId { get; set; }
        public string CustomerName { get; set; } // From User table
        public string Phone { get; set; }       // From User table (assuming you have a Phone field)
        public string RoomNumber { get; set; }  // From Room table
        public string RoomId { get; set; }  // From Room table
        public string Category { get; set; }    // From Room table
        public decimal Price { get; set; }      // From Room table
        public DateTime? CheckInDate { get; set; } // From Booking table
        public DateTime? CheckOutDate { get; set; } // From Booking table
        public string Status { get; set; }      // From Booking table
    }
}
