using HotelManagementSystem.Shared.Services.JwtService;

namespace HotelManagementSystem.CustomerApp.Components.Pages.Booking;

public partial class BookingDetails
{
    [Parameter]

    public string BookingId { get; set; }   

    public BookingResponseModel bookingModel = new BookingResponseModel();

    public RoomModel room = new RoomModel();

    private decimal totalPrice;

    [Inject] JwtAuthStateProviderService AuthStateProvider { get; set; }

    private bool _isAuthenticated;

    protected override async Task OnInitializedAsync()
    {
        bookingModel.BookingId = BookingId;
        var result = await _bookingService.GetBookingDeatilsById(BookingId);
        if (result.IsSuccess)
        {
            bookingModel = result.Data!;
        }
    }

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

    //protected override void OnParametersSet()
    //{
    //if (bookingModel.CheckInDate.HasValue && bookingModel.CheckOutDate.HasValue)
    //{
    //    totalPrice = (bookingModel.CheckOutDate.Value - bookingModel.CheckInDate.Value).Days * room.Price;
    //}
    //}

    private async void ClickOk()
    {
        await JS.InvokeVoidAsync("manageLoading", "show");
        _goto.NavigateTo("/");
        await JS.InvokeVoidAsync("manageLoading", "remove");
    }
}