using Microsoft.AspNetCore.SignalR.Client;

namespace HotelManagementSystem.App;

public class ChatService
{
    private HubConnection _hubConnection;
    private string _userType;

    public event Action<string, string, string> OnMessageReceived;

    public async Task StartConnectionAsync(string baseUrl, string userType)
    {
        _userType = userType;
        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{baseUrl}/chatHub")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<string, string, string>("ReceiveMessage", (senderType, senderName, message) =>
        {
            OnMessageReceived?.Invoke(senderType, senderName, message);
        });

        await _hubConnection.StartAsync();
        await _hubConnection.SendAsync("RegisterUser", userType);
    }

    public async Task SendMessageAsync(string userName, string message)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.SendAsync("SendMessage", _userType, userName, message);
        }
    }

    public async Task StopConnectionAsync()
    {
        if (_hubConnection != null)
        {
            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();
        }
    }
}