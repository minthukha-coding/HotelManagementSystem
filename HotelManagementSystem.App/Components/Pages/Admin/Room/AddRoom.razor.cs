namespace HotelManagementSystem.App.Components.Pages.Admin.Room;

public partial class AddRoom
{
    private RoomModel roomModel = new RoomModel();
    private IReadOnlyList<IBrowserFile>? uploadedFiles;

    // private void OnFileUpload(InputFileChangeEventArgs e)
    // {
    //     uploadedFiles = e.GetMultipleFiles();
    // }

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
                Snackbar.Add($"Error adding room: {result.Message}", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/rooms");
    }
}
