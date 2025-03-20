using Azure.Identity;
using HotelManagementSystem.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace HotelManagementSystem.App;

public class ChatService
{
    private HubConnection _hubConnection;
    private string _userType;

    public event Action<string, string, string> OnMessageReceived;
    public event Action<string> typingUsers;

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

        _hubConnection.On<string>("ReceiveTypingNotification", (userName) =>
        {
            typingUsers.Invoke(userName);
        });

        await _hubConnection.StartAsync();
        var connectionId = _DevCode.GetUlid();
        await _hubConnection.SendAsync("RegisterUser", connectionId,userType);
    }

    public async Task SendMessageAsync(string userName, string message)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.SendAsync("SendMessage", _userType, userName, message);
        }
    }

    public async Task NotifyTypingAsync(string userName)
    {
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            await _hubConnection.SendAsync("NotifyTyping", userName);
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