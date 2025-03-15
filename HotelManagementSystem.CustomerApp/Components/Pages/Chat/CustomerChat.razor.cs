using HotelManagementSystem.Shared.Services.JwtService;
using HotelManagementSystem.Shared;

namespace HotelManagementSystem.CustomerApp.Components.Pages.Chat;

public partial class CustomerChat
{
    private string UserName { get; set; }
    private string Message;
    private List<(string SenderType, string User, string Text)> Messages = new();

    [Inject] JwtAuthStateProviderService AuthStateProvider { get; set; }
    private bool _isAuthenticated;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                await JS.InvokeVoidAsync("manageLoading", "show");

                var authState = await AuthStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                _isAuthenticated = user.Identity!.IsAuthenticated;

                if (!user.Identity.IsAuthenticated)
                {
                    await JS.InvokeVoidAsync("manageLoading", "remove");
                    _goto.NavigateTo("/login");
                    return;
                }
                var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");

                var userId = _DevCode.GetUserIdFromToken(token);

                var customer = await _customerInfoService.GetCustomerData(userId);
                if (customer.IsSuccess)
                {
                    UserName = customer.Data!.FullName;
                }
                await JS.InvokeVoidAsync("manageLoading", "remove");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        ChatService.OnMessageReceived += async (senderType, user, message) =>
        {
            await InvokeAsync(() =>
            {
                Messages.Add((senderType, user, message));
                StateHasChanged();
            });
        };

        //await ChatService.StartConnectionAsync("https://localhost:7114", "Customer");
        await ChatService.StartConnectionAsync("https://localhost:7014", "Customer");
    }

    private async Task SendMessage()
    {
        await ChatService.SendMessageAsync(UserName, Message);
        Message = string.Empty;
    }

    public async ValueTask DisposeAsync()
    {
        await ChatService.StopConnectionAsync();
    }
}