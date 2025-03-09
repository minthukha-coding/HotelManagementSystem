using System.Text.RegularExpressions;
using static MudBlazor.CategoryTypes;

namespace HotelManagementSystem.CustomerApp.Components.Pages.Auth.Login;

public partial class Login
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

    private async Task LoginMethod()
    {
        await JS.InvokeVoidAsync("manageLoading", "show");

        var result = await _customerServices.Login(loginModel.Email, loginModel.Password);

        await JS.InvokeVoidAsync("manageLoading", "remove");

        if (result!.IsSuccess)
        {
            await JS.InvokeVoidAsync("localStorage.setItem", "authToken", result.Data.Token);
            await JS.InvokeVoidAsync("notiflixNotify.success", "Login successful!");
            _goto.NavigateTo("/");
        }
        else
        {
            await JS.InvokeVoidAsync("notiflixNotify.error", "Error! Something went wrong.");
        }
    }
}
