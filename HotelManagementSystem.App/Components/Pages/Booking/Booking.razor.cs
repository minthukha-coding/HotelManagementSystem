namespace HotelManagementSystem.App.Components.Pages.Booking;

public partial class Booking
{
    private List<BookingResponseModel> Bookings = new();

    protected override async Task OnInitializedAsync()
    {
        var result = await _bookingService.GetAllBookinAsync();
        if (result.IsSuccess)
        {
            Bookings = result.Data;
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

    private async void ViewBookingDetails(string bookingId)
    {
        try
        {
            await JS.InvokeVoidAsync("manageLoading", "show");
            _goto.NavigateTo($"/booking-details/{bookingId}");
            await JS.InvokeVoidAsync("manageLoading", "remove");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task ConfirmBooking(string bookingId)
    {
        await JS.InvokeVoidAsync("manageLoading", "show");
   
        string userSubject = "Booking Confirmation";

        string userBody = $@"
                <h1>Dear ,</h1>
                <p>Your booking with ID <strong></strong> has been confirmed.</p>
                <p>Room Type: </p>
                <p>Booking Date: </p>
                <p>Thank you for choosing our service!</p>
                <p>For more information, please visit: <a href=''>Booking Details</a></p>
                <p>Best regards,<br>Hotel Myaungmya</p>";

        await _emailService.SendEmail(userSubject, userBody);

        await JS.InvokeVoidAsync("notiflixNotify.success", "Booking Confirmation successful!");
        _goto.NavigateTo("/customer-bookings");
        await JS.InvokeVoidAsync("manageLoading", "remove");
    }

    private async Task CancelBooking(string bookingId)
    {
        await JS.InvokeVoidAsync("manageLoading", "show");

        string userSubject = "Room Unavailable";
        string userBody = $@"
                <h1>Dear ,</h1>
                <p>We regret to inform you that the <strong></strong> room is not available for your selected dates.</p>
                <p>Reason: </p>
                <p>We suggest the following alternative: <strong></strong>.</p>
                <p>For more information, please visit: <a href=''>Booking Details</a></p>
                <p>Best regards,<br>Hotel Myaungmya</p>";

        await _emailService.SendEmail(userSubject, userBody);
        await JS.InvokeVoidAsync("notiflixNotify.success", "CancelBooking successful!");
        _goto.NavigateTo("/customer-bookings");
        await JS.InvokeVoidAsync("manageLoading", "remove");
    }
}