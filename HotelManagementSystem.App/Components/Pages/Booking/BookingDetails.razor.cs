using Microsoft.AspNetCore.Components;

namespace HotelManagementSystem.App.Components.Pages.Booking;

public partial class BookingDetails
{
    [Parameter]
    public string BookingId { get; set; }

    private BookingResponseModel Booking = new();

    protected override async Task OnParametersSetAsync()
    {
        var result = await _bookingService.GetBookingDeatilsById(BookingId);
        if (result.IsSuccess)
        {
            Booking = result.Data;
        }
    }

    private Color GetRoomStatus(string status)
    {
        return status switch
        {
            "Confirmed" => Color.Success,
            "Cancelled" => Color.Error,
            _ => Color.Warning
        };
    }

    private async Task ConfirmBooking(string bookingId)
    {
        //var result = await _bookingService.ConfirmBookingAsync(bookingId);
        //if (result.IsSuccess)
        //{
        //    // Optionally, send an email to the customer
        string userSubject = "Booking Confirmation";
        string userBody = $@"
                <h1>Dear {Booking.CustomerName},</h1>
                <p>Your booking with ID <strong>{Booking.BookingId}</strong> has been confirmed.</p>
                <p>Room Type: {Booking.RoomNumber}</p>
                <p>Booking Date: {Booking.CheckInDate}</p>
                <p>Thank you for choosing our service!</p>
                <p>For more information, please visit: <a href='{Booking.BookingId}'>Booking Details</a></p>
                <p>Best regards,<br>Your Company</p>";

        // Send email to user
        await _emailService.SendEmail(userSubject, userBody);

        //    // Refresh the booking details
        //    var bookingResult = await _bookingService.GetBookingByIdAsync(bookingId);
        //    if (bookingResult.IsSuccess)
        //    {
        //        Booking = bookingResult.Data;
        //    }
        //}
    }

    private async Task CancelBooking(string bookingId)
    {
        string userSubject = "Room Unavailable";
        string userBody = $@"
                <h1>Dear {Booking.CustomerName},</h1>
                <p>We regret to inform you that the <strong>{Booking.CheckInDate}</strong> room is not available for your selected dates.</p>
                <p>Reason: {Booking.CheckInDate}</p>
                <p>We suggest the following alternative: <strong>{Booking.CheckInDate}</strong>.</p>
                <p>For more information, please visit: <a href='{Booking.CheckInDate}'>Booking Details</a></p>
                <p>Best regards,<br>Your Company</p>";

        // Send email to user
        await _emailService.SendEmail(userSubject, userBody);
    }

    private async Task Back()
    {
        _goto.NavigateTo("/customer-bookings");
    }
}