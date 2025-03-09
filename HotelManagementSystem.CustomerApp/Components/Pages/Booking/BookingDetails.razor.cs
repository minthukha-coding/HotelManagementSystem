namespace HotelManagementSystem.CustomerApp.Components.Pages.Booking;

public partial class BookingDetails
{
    [Parameter]
    public BookingModel bookingModel { get; set; }

    [Parameter]
    public RoomModel room { get; set; }

    private decimal totalPrice;

    protected override void OnParametersSet()
    {
        if (bookingModel.CheckInDate.HasValue && bookingModel.CheckOutDate.HasValue)
        {
            totalPrice = (bookingModel.CheckOutDate.Value - bookingModel.CheckInDate.Value).Days * room.Price;
        }
    }

    private void ConfirmBooking()
    {
        _goto.NavigateTo("/booking-success");
    }
}