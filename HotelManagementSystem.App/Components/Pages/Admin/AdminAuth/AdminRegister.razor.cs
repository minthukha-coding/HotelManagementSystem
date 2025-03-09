namespace HotelManagementSystem.App.Components.Pages.Admin.AdminAuth;

public partial class AdminRegister
{
    private MudForm form;
    private string username;
    private string password;
    private string email;

    private async Task<Result<UserModel>> HandleValidSubmit()
    {
        var model = new Result<UserModel>();

        var reqModel = new UserModel
        {
            Username = username,
            PasswordHash = password,
            Email = email,
        };

        model = await _userService.Register(reqModel);

        if (model!.IsSuccess)
        {
            await JS.InvokeVoidAsync("notiflixNotify.success", "Register successful!");
            _goto.NavigateTo("/");
        }
        else
        {
            await JS.InvokeVoidAsync("notiflixNotify.error", "Error! Something went wrong.");
            _goto.NavigateTo("/");
        }

        return model;
    }
}
