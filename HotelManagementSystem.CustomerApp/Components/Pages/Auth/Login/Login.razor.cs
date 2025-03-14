using HotelManagementSystem.Shared.Services;
using HotelManagementSystem.Shared.Services.JwtService;
using System.Text.RegularExpressions;

namespace HotelManagementSystem.CustomerApp.Components.Pages.Auth.Login;

public partial class Login
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

    private async Task LoginMethod()
    {
        await form.Validate();

        if (form.IsValid)
        {
            await JS.InvokeVoidAsync("manageLoading", "show");

            var result = await _customerAuthServices.Login(loginModel.Email, loginModel.Password);

            await JS.InvokeVoidAsync("manageLoading", "remove");

            if (result!.IsSuccess)
            {
                var token = result.Data!.Token;
                await _localStorage.SetItemAsync("authToken", token);
                _authStateProvider.NotifyUserAuthentication(token);
                await JS.InvokeVoidAsync("notiflixNotify.success", "Login successful!");
                _goto.NavigateTo("/");
            }
            else
            {
                await JS.InvokeVoidAsync("notiflixNotify.error", "Error! Something went wrong.");
            }
        }
    }
}
