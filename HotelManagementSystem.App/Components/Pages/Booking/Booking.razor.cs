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

    private void ViewBookingDetails(string bookingId)
    {
        try
        {
            _goto.NavigateTo($"/booking-details/{bookingId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task ConfirmBooking(string bookingId)
    {
        //var result = await _bookingService.ConfirmBookingAsync(bookingId);
        //if (result.IsSuccess)
        //{
        //    // Optionally, send an email to the customer
        //    await _emailService.SendConfirmationEmailAsync(bookingId);

        //    // Refresh the bookings list
        //    var bookingsResult = await _bookingService.GetAllBookinAsync();
        //    if (bookingsResult.IsSuccess)
        //    {
        //        Bookings = bookingsResult.Data;
        //    }
        //}
    }

    private async Task CancelBooking(string bookingId)
    {
        //var result = await _bookingService.CancelBookingAsync(bookingId);
        //if (result.IsSuccess)
        //{
        //    // Optionally, send an email to the customer
        //    await _emailService.SendCancellationEmailAsync(bookingId);

        //    // Refresh the bookings list
        //    var bookingsResult = await _bookingService.GetAllBookinAsync();
        //    if (bookingsResult.IsSuccess)
        //    {
        //        Bookings = bookingsResult.Data;
        //    }
        //}
    }
}
