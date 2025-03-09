using System.Text.RegularExpressions;

namespace HotelManagementSystem.CustomerApp.Components.Pages.Auth.Register;

public partial class Register
{
    private MudForm form;
    private string fullname;
    private string password;
    private string email;
    private string phoneNumber;
    private string address;

    private string EmailValidation(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return "Email is required.";

        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!regex.IsMatch(email))
            return "Please enter a valid email address.";

        return null;
    }

    private async Task HandleValidSubmit()
    {
        await form.Validate();

        if (form.IsValid)
        {
            var reqModel = new CustomerModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                FullName = fullname,
                Password = password,
                Address = address
            };

            var result = await _customerServices.Register(reqModel);

            if (result!.IsSuccess)
            {
                await JS.InvokeVoidAsync("notiflixNotify.success", "Register successful!");
                _goto.NavigateTo("/");
            }
            else
            {
                await JS.InvokeVoidAsync("notiflixNotify.error", "Error! Something went wrong.");
            }
        }
    }
}
