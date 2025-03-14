using HotelManagementSystem.Shared.Services.JwtService;
using Microsoft.AspNetCore.Components;

namespace HotelManagementSystem.App.Components.Pages.Booking;

public partial class BookingDetails
{
    [Parameter]
    public string BookingId { get; set; }

    private BookingResponseModel Booking = new();

    [Inject] JwtAuthStateProviderService AuthStateProvider { get; set; }

    private bool _isAuthenticated;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                var authState = await AuthStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                _isAuthenticated = user.Identity!.IsAuthenticated;

                if (!user.Identity.IsAuthenticated)
                {
                    _goto.NavigateTo("/");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

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
        await JS.InvokeVoidAsync("manageLoading", "show");

        var reqModel = new BookingModel
        {
            BookingId = bookingId
        };

        var result = await _bookingService.BookingConfirm(reqModel);
        //if (result.IsSuccess)
        //{
        //    // Optionally, send an email to the customer
        string userSubject = "Booking Confirmation";
        string userBody = $@"
                <h1>Dear {Booking.CustomerName},</h1>
                <p>Your booking with ID <strong>{Booking.BookingId}</strong> has been confirmed.</p>
                <p>Room Type: {Booking.Category}</p>
                <p>Booking Date: {Booking.CheckInDate}</p>
                <p>Thank you for choosing our service!</p>
                <p>For more information, please visit: <a href=''>Booking Details</a></p>
                <p>Best regards,<br>Hotel Myaungmya</p>";

        // Send email to user
        await _emailService.SendEmail(userSubject, userBody, result.Data!.CustomerEmail);

        await JS.InvokeVoidAsync("notiflixNotify.success", "Booking Confirmation successful!");
        _goto.NavigateTo("/customer-bookings");
        await JS.InvokeVoidAsync("manageLoading", "remove");

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
        await JS.InvokeVoidAsync("manageLoading", "show");

        string userSubject = "Room Unavailable";
        string userBody = $@"
                <h1>Dear {Booking.CustomerName},</h1>
                <p>We regret to inform you that the <strong>{Booking.CheckInDate}</strong> room is not available for your selected dates.</p>
                <p>Reason: {Booking.CheckInDate}</p>
                <p>We suggest the following alternative: <strong></strong>.</p>
                <p>For more information, please visit: <a href=''>Booking Details</a></p>
                <p>Best regards,<br>Hotel Myaungmya</p>";

        // Send email to user
        //await _emailService.SendEmail(userSubject, userBody);
        await JS.InvokeVoidAsync("notiflixNotify.error", "CancelBooking successful!");
        _goto.NavigateTo("/customer-bookings");
        await JS.InvokeVoidAsync("manageLoading", "remove");

    }

    private async Task Back()
    {
        await JS.InvokeVoidAsync("manageLoading", "show");
        _goto.NavigateTo("/customer-bookings");
        await JS.InvokeVoidAsync("manageLoading", "remove");
    }
}