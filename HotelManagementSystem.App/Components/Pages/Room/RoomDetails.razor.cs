using HotelManagementSystem.Shared.Services.JwtService;
using Microsoft.AspNetCore.Components;

namespace HotelManagementSystem.App.Components.Pages.Room;

public partial class RoomDetails
{
    [Parameter]
    public string roomId { get; set; }

    private RoomModel? room;

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
                    _goto.NavigateTo("/");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    protected override async Task OnParametersSetAsync()
    {
        await LoadRoomDetails();
    }

    private async Task LoadRoomDetails()
    {
        var result = await _roomService.GetRoomByIdAsync(roomId);
        if (result.IsSuccess)
        {
            room = result.Data;
        }
        else
        {
            // Handle error (e.g., show a message or redirect)
            Navigation.NavigateTo("/rooms");
        }
    }

    private async void BackToRooms()
    {
        await JS.InvokeVoidAsync("manageLoading", "show");

        Navigation.NavigateTo("/rooms");

        await JS.InvokeVoidAsync("manageLoading", "remove");
    }

    private async Task DeleteRoom()
    {
        await JS.InvokeVoidAsync("manageLoading", "show");

        var result = await _roomService.DeleteRoomAsync(roomId);
        if (result.IsSuccess)
        {
            await JS.InvokeVoidAsync("manageLoading", "remove");

            // Optionally, show a success message
            Navigation.NavigateTo("/rooms");
        }
        else
        {
            // Handle error (e.g., show an error message)
        }
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
