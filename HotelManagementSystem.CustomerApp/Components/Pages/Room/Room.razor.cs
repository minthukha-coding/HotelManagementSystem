using HotelManagementSystem.Domain.Features.Booking;
using HotelManagementSystem.Shared;
using HotelManagementSystem.Shared.Services.JwtService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace HotelManagementSystem.CustomerApp.Components.Pages.Room;

public partial class Room
{
    [Inject] ILogger<Room> _logger { get; set; }
    [Inject] JwtAuthStateProviderService AuthStateProvider { get; set; }

    private List<RoomModel> Rooms { get; set; }

    private bool _isAuthenticated;

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

        if (token is not null)
        {
            _goto.NavigateTo($"/book/{roomId}");
            return;
        }

        await JS.InvokeVoidAsync("notiflixNotify.error", "Pls firstly login for booking.");
        _goto.NavigateTo("/login");
    }
}
