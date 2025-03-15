namespace HotelManagementSystem.App.Components.Pages.Chat;

public partial class AdminChat
{
    private string UserName = "Hotel Myaungmya Admin";
    private string Message;
    private List<(string SenderType, string User, string Text)> Messages = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _chatService.OnMessageReceived += async (senderType, user, message) =>
            {
                await InvokeAsync(() =>
                {
                    Messages.Add((senderType, user, message));
                    StateHasChanged();
                });
            };
            await _chatService.StartConnectionAsync("https://localhost:7014", "Admin");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task SendMessage()
    {
        try
        {
            await _chatService.SendMessageAsync(UserName, Message);
            Message = string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await _chatService.StopConnectionAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}