using System.Text.RegularExpressions;

namespace HotelManagementSystem.App.Components.Pages.Admin.AdminAuth;

public partial class AdminLogin
{
    private LoginModel loginModel = new();

    private string EmailValidation(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return "Email is required.";

        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!regex.IsMatch(email))
            return "Please enter a valid email address.";

        return null;
    }
    private async Task<Result<UserModel>?> Login()
    {
        var model = new Result<UserModel>();

        // await JS.InvokeVoidAsync("notiflixLoadingPulse.show");
        await JS.InvokeVoidAsync("manageLoading", "show");
        model = await _userService.Login(loginModel.Email, loginModel.Password);
        // await JS.InvokeVoidAsync("notiflixLoadingRemove.show");
        await JS.InvokeVoidAsync("manageLoading", "remove");

        if (model!.IsSuccess)
        {
            await JS.InvokeVoidAsync("notiflixNotify.success", "Login successful!");
            await Task.Delay(1000);
            _goto.NavigateTo("/rooms");

            model = new Result<UserModel>
            {
                IsSuccess = true,
                Data = new UserModel
                {
                    UserId = model.Data!.UserId,
                    Username = model.Data.Username,
                    Email = model.Data.Email,
                    PasswordHash = model.Data.PasswordHash,
                    Role = model.Data.Role,
                    CreatedAt = model.Data.CreatedAt
                }
            };
        }
        else
        {
            await JS.InvokeVoidAsync("notiflixNotify.error", "Error! Something went wrong.");
            _goto.NavigateTo("/");
        }

        return model;
    }

    private class LoginModel
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
