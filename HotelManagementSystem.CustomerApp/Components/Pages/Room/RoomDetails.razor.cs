namespace HotelManagementSystem.CustomerApp.Components.Pages.Room;

public partial class RoomDetails
{
    [Parameter] public string RoomId { get; set; }
    private RoomModel room;
    private MudForm form;
    private bool isValid;

    public BookingModel bookingModel = new BookingModel();

    protected override async Task OnInitializedAsync()
    {
        var result = await _roomService.GetRoomByIdAsync(RoomId);
        if (result.IsSuccess)
        {
            room = result.Data!;
            bookingModel.RoomId = RoomId;
        }
    }

    private async Task SubmitBooking()
    {
        var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
        if (!string.IsNullOrEmpty(token))
        {
            // Get the user ID from the token
            var userId = GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                bookingModel.NumberOfDays = (bookingModel.CheckOutDate - bookingModel.CheckInDate)?.Days ?? 0;
                bookingModel.CustomerId = userId;

                var result = await _bookingService.BookRoom(bookingModel);
                if (result.IsSuccess)
                {
                    await JS.InvokeVoidAsync("notiflixNotify.success", "Booking Success");
                    _goto.NavigateTo("/booking/confirmation");
                }
            }
            else
            {
                await JS.InvokeVoidAsync("notiflixNotify.error", "User ID not found in token.");
            }
        }
        else
        {
            await JS.InvokeVoidAsync("notiflixNotify.error", "Token not found in localStorage.");
        }
    }

    private void UpdateCheckOutMin(ChangeEventArgs e)
    {
        if (DateTime.TryParse(e.Value?.ToString(), out DateTime newDate))
        {
            bookingModel.CheckInDate = newDate;
            StateHasChanged();
        }
    }

    private bool IsCheckInDateValid()
    {
        if (!bookingModel.CheckInDate.HasValue) return false;
        return bookingModel.CheckInDate.Value >= DateTime.Today;
    }

    private bool IsCheckOutDateValid()
    {
        if (!bookingModel.CheckOutDate.HasValue) return false;
        if (!bookingModel.CheckInDate.HasValue) return false;
        return bookingModel.CheckOutDate.Value > bookingModel.CheckInDate.Value;
    }

    private string GetCheckInErrorMessage()
    {
        if (!bookingModel.CheckInDate.HasValue) return "Check-in date is required";
        if (bookingModel.CheckInDate.Value < DateTime.Today) return "Check-in date cannot be in the past";
        return string.Empty;
    }

    private string GetCheckOutErrorMessage()
    {
        if (!bookingModel.CheckOutDate.HasValue) return "Check-out date is required";
        if (bookingModel.CheckInDate.HasValue && bookingModel.CheckOutDate <= bookingModel.CheckInDate)
            return "Check-out date must be after check-in date";
        return string.Empty;
    }

    private bool IsFormValid()
    {
        return IsCheckInDateValid() && IsCheckOutDateValid();
    }

    public string GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (tokenHandler.CanReadToken(token))
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Extract the user ID from the token
            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
        }

        return null;
    }

}
