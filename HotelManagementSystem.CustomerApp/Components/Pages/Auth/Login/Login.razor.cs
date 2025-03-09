namespace HotelManagementSystem.CustomerApp.Components.Pages.Auth.Login;

public partial class Login
{
    private LoginModel loginModel = new();

    private async Task LoginMethod()
    {
        await JS.InvokeVoidAsync("manageLoading", "show");

        var result = await _customerServices.Login(loginModel.Email, loginModel.Password);

        await JS.InvokeVoidAsync("manageLoading", "remove");

        if (result!.IsSuccess)
        {
            await JS.InvokeVoidAsync("localStorage.setItem", "authToken", result.Data.Token);
            await JS.InvokeVoidAsync("notiflixNotify.success", "Login successful!");
            _goto.NavigateTo("/rooms");
        }
        else
        {
            await JS.InvokeVoidAsync("notiflixNotify.error", "Error! Something went wrong.");
        }
    }
}
