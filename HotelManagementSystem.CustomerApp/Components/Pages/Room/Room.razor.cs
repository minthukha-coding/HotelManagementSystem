using HotelManagementSystem.Shared;
using Newtonsoft.Json.Linq;

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
   
    private async Task BookRoom(string roomId)
    {
        var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");

        if(token is not null)
        {
            _goto.NavigateTo($"/book/{roomId}");
            return;
        }

        await JS.InvokeVoidAsync("notiflixNotify.error", "Pls firstly login for booking.");
        _goto.NavigateTo("/login");
    }
}
