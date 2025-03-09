using Microsoft.AspNetCore.Components;

namespace HotelManagementSystem.App.Components.Pages.Room;

public partial class EditRoom
{
    [Parameter]
    public string RoomId { get; set; }

    private RoomModel roomModel = new RoomModel();

    protected override async Task OnParametersSetAsync()
    {
        var result = await _roomService.GetRoomModelsAsync();
        if (result.IsSuccess)
        {
            var room = result.Data.FirstOrDefault(r => r.RoomId == RoomId);
            if (room != null)
            {
                roomModel = room;
            }
            else
            {
                _goto.NavigateTo("/rooms");
            }
        }
        else
        {
        }
    }

    private async Task UpdateRoom()
    {

        var result = await _roomService.UpdateRoomAsync(roomModel);
        if (result.IsSuccess)
        {
            _goto.NavigateTo("/rooms");
        }
        else
        {
        }
    }

    private void Cancel()
    {
        _goto.NavigateTo("/rooms");
    }
}
