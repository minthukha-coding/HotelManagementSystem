using HotelManagementSystem.App.Components.Pages.Shared.Dialogs;

namespace HotelManagementSystem.App.Components.Pages.Shared;

public class ShowDeleteConfirmation
{
    private async Task ShowDeleteConfirmationdfds()
    {
        var parameters = new DialogParameters();

        var options = new DialogOptions()
        {
            CloseButton = false,
            MaxWidth = MaxWidth.ExtraExtraLarge
        };

        //var dialog = DialogService.Show<ConfirmDeleteDialog>("Confirm Delete", parameters, options);
        //var result = await dialog.Result;

        //if (!result!.Canceled)
        //{
            //await DeleteRoom();
        //}
    }
}