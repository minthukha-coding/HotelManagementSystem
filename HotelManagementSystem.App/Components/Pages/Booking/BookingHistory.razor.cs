using HotelManagementSystem.Database.Db;

namespace HotelManagementSystem.App.Components.Pages.Booking;

public partial class BookingHistory
{
    private List<BookingModel> bookings = new();

    protected override async Task OnInitializedAsync()
    {
        var result = await _bookingService.GetAllBookingHistory();
        if (result.IsSuccess)
        {
            bookings = result.Data;
        }
    }

    private void EditBooking(string id)
    {
        // Navigate to edit page
        //_.NavigateTo($"/bookings/edit/{id}");
    }

    private async Task DeleteBooking(string id)
    {
        //await BookingService.DeleteBookingAsync(id);
        //bookings = await BookingService.GetBookingsAsync();
    }
}
