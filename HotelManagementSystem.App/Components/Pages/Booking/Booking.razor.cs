namespace HotelManagementSystem.App.Components.Pages.Booking;

public partial class Booking
{
    private List<BookingResponseModel> Bookings = new();

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
   
        var reqModel = new BookingModel
        {
            BookingId = bookingId
        };
        
        var result = await _bookingService.BookingConfirm(reqModel);

        if(result.IsSuccess)
        {
            string userSubject = "Booking Confirmation";

            string userBody = $@"
                             <h1>Dear {result.Data!.CustomerName},</h1>
                             <p>Your booking with ID <strong>{result.Data.BookingId}</strong> has been confirmed.</p>
                             <p>Room Type: {result.Data.RoomType}</p>
                             <p>Booking Date: {result.Data.BookingDate:yyyy-MM-dd}</p>
                             <p>Thank you for choosing our service!</p>
                             <p>For more information, please visit: <a href=''>Booking Details</a></p>
                             <p>Best regards,<br>Hotel Myaungmya</p>";
            var toMail = result.Data!.CustomerEmail;
            await _emailService.SendEmail(userSubject, userBody, toMail);
            await JS.InvokeVoidAsync("manageLoading", "remove");
            _goto.NavigateTo("/customer-bookings", true);
            await JS.InvokeVoidAsync("notiflixNotify.success", "Booking Confirmation successful!");
        }
        else
        {
            //var failResponseMessage = result.Message.ToString();
            await JS.InvokeVoidAsync("notiflixNotify.error", result.Message.ToString());
            await JS.InvokeVoidAsync("manageLoading", "remove");
        }
    }

    private async Task CancelBooking(string bookingId)
    {
        await JS.InvokeVoidAsync("manageLoading", "show");

        string userSubject = "Room Unavailable";
        //string userBody = $@"
        //    <h1>Dear {CustomerName},</h1>
        //    <p>We regret to inform you that the <strong>{RoomType}</strong> room is not available for your selected dates.</p>
        //    <p>Reason: {Reason}</p>
        //    <p>We suggest the following alternative: <strong>{AlternativeRoom}</strong>.</p>
        //    <p>For more information, please visit: <a href='{BookingDetailsUrl}'>Booking Details</a></p>
        //    <p>Best regards,<br>Hotel Myaungmya</p>";

        //await _emailService.SendEmail(userSubject, userBody);
        await JS.InvokeVoidAsync("notiflixNotify.success", "CancelBooking successful!");
        _goto.NavigateTo("/customer-bookings");
        await JS.InvokeVoidAsync("manageLoading", "remove");
    }

}