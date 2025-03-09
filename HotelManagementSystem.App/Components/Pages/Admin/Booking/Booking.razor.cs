namespace HotelManagementSystem.App.Components.Pages.Admin.Booking;

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

    private MudBlazor.Color GetRoomStatus(string status)
    {
        return status switch
        {
            "Confirmed" => Color.Success,
            _ => Color.Warning
        };
    }
}
