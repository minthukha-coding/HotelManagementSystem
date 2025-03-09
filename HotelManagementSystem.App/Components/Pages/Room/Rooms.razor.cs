namespace HotelManagementSystem.App.Components.Pages.Room;

public partial class Rooms
{
    private List<RoomModel> rooms = new List<RoomModel>();

    protected override async Task OnInitializedAsync()
    {
        await LoadRooms();
    }

    private async Task LoadRooms()
    {
        var result = await _roomService.GetRoomModelsAsync();
        if (result.IsSuccess)
        {
            rooms = result.Data;
        }
        else
        {
        }
    }

    private async void AddRoom()
    {
        await JS.InvokeVoidAsync("manageLoading", "show");
        _goto.NavigateTo("/rooms/add");
        await JS.InvokeVoidAsync("manageLoading", "remove");
    }

    private async void EditRoom(int roomId)
    {
        await JS.InvokeVoidAsync("manageLoading", "show");
        _goto.NavigateTo($"/rooms/edit/{roomId}");
        await JS.InvokeVoidAsync("manageLoading", "remove");
    }

    private async Task DeleteRoom(string roomId)
    {
        await JS.InvokeVoidAsync("manageLoading", "show");
        var result = await _roomService.DeleteRoomAsync(roomId);
        if (result.IsSuccess)
        {
            await LoadRooms();
            await JS.InvokeVoidAsync("manageLoading", "remove");
        }
        else
        {

        }
    }

    private async Task Detail(string roomId)
    {
        await JS.InvokeVoidAsync("manageLoading", "show");
        _goto.NavigateTo($"/rooms/details/{roomId}");
        await JS.InvokeVoidAsync("manageLoading", "remove");
    }

    private Color GetRoomStatus(string status)
    {
        return status switch
        {
            "Available" => Color.Success,
            "Booked" => Color.Warning,
            "Occupied" => Color.Error,
            _ => Color.Default
        };
    }
}