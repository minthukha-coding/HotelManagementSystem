﻿using HotelManagementSystem.Shared.Services.JwtService;
using Microsoft.AspNetCore.Components;

namespace HotelManagementSystem.App.Components.Pages.Room;

public partial class EditRoom
{
    [Parameter]
    public string RoomId { get; set; }

    private RoomModel roomModel = new RoomModel();

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
