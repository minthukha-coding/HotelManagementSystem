using HotelManagementSystem.Shared.Services.JwtService;

namespace HotelManagementSystem.CustomerApp.Components.Pages.Booking;

public partial class BookingDetails
{
    [Parameter]
    public BookingModel bookingModel { get; set; }

    [Parameter]
    public RoomModel room { get; set; }

    private decimal totalPrice;

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
                    _goto.NavigateTo("/login");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

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