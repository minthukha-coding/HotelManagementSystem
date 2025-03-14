using Microsoft.AspNetCore.Components;

namespace HotelManagementSystem.App.Components.Pages.Room;

public partial class RoomBookingDetails
{
    [Parameter]
    public string roomId { get; set; }

    private List<CustomBookingRoomDateRange> bookedDates = new();

    private async Task LoadRoomBookingDates()
    {
        var result = await _roomService.GetRoomBookingDatesAsync(roomId);
        if (result.IsSuccess && result.Data != null)
        {
            bookedDates = result.Data;
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        await LoadRoomBookingDates();
    }
}