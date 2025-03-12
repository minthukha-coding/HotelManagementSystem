using HotelManagementSystem.Shared.Services.JwtService;
using Microsoft.AspNetCore.Components;

namespace HotelManagementSystem.App.Components.Pages.Room;

public partial class AddRoom
{
    private RoomModel roomModel = new RoomModel();
    private IReadOnlyList<IBrowserFile>? uploadedFiles;

    // private void OnFileUpload(InputFileChangeEventArgs e)
    // {
    //     uploadedFiles = e.GetMultipleFiles();
    // }

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

    IList<IBrowserFile> _files = new List<IBrowserFile>();
    private void UploadFiles(IBrowserFile file)
    {
        _files.Add(file);
        //TODO upload the files to the server
    }

    private async Task SaveRoom()
    {
        try
        {
            await JS.InvokeVoidAsync("manageLoading", "show");

            // Save uploaded photos and add their paths to the roomModel
            if (_files != null)
            {
                roomModel.PhotoUrls = new List<RoomPhotoModel>();
                foreach (var file in _files)
                {
                    var photoUrl = await _roomService.UploadFileAsync(file);
                    roomModel.PhotoUrls.Add(new RoomPhotoModel { PhotoUrl = photoUrl });
                }
            }

            // Save the room data
            var result = await _roomService.AddRoomAsync(roomModel);
            if (result.IsSuccess)
            {
                await JS.InvokeVoidAsync("manageLoading", "remove");

                _goto.NavigateTo("/rooms");
            }
            else
            {
                await JS.InvokeVoidAsync("manageLoading", "remove");
            }
        }
        catch (Exception ex)
        {
            await JS.InvokeVoidAsync("manageLoading", "remove");
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/rooms");
    }
}
