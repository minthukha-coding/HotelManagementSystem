namespace HotelManagementSystem.CustomerApp.Components.Pages.Room;

public partial class Room
{
    [Inject] ILogger<Room> _logger { get; set; }

    private List<RoomModel> Rooms { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await LoadRooms();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task LoadRooms()
    {
        var result = await _roomService.GetAllRoomForUserRoomListing();
        if (result.IsSuccess)
        {
            Rooms = result.Data;
        }
        else
        {
        }
    }

    private Booking _booking = new Booking();

    private async Task BookRoom(string roomId)
    {
        _goto.NavigateTo($"/booking/{roomId}");
        // var result = await _bookingService.BookRoom(roomId);
        // if (result.IsSuccess)
        // {

        // }
        // else
        // {
        // }
    }
}
