using HotelManagementSystem.Shared.Services.JwtService;
using HotelManagementSystem.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace HotelManagementSystem.App.Components.Pages.AdminAuth;

public partial class AdminLogin
{
    private MudForm form;

    private LoginModel loginModel = new();

    [Inject] JwtAuthStateProviderService _authStateProvider { get; set; }
    [Inject] LocalStorageService _localStorage { get; set; }

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
        await form.Validate();
        var model = new Result<UserModel>();

        if (form.IsValid)
        {
            // await JS.InvokeVoidAsync("notiflixLoadingPulse.show");
            await JS.InvokeVoidAsync("manageLoading", "show");
            var result = await _userService.Login(loginModel.Email, loginModel.Password);
            // await JS.InvokeVoidAsync("notiflixLoadingRemove.show");
            await JS.InvokeVoidAsync("manageLoading", "remove");

            if (result!.IsSuccess)
            {
                var token = result.Data!.Token;
                await _localStorage.SetItemAsync("authToken", token);
                _authStateProvider.NotifyUserAuthentication(token);
                await JS.InvokeVoidAsync("notiflixNotify.success", "Login successful!");
                await Task.Delay(1000);
                _goto.NavigateTo("/rooms");
            }
            else
            {
                await JS.InvokeVoidAsync("notiflixNotify.error", result.Message);
                _goto.NavigateTo("/");
            }

            return Result<UserModel>.SuccessResult("Login success");
        }
        return model;
    }

    private class LoginModel
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
