using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace HotelManagementSystem.Shared.Services.SignalRService;

public class ChatHub : Hub
{
    private static ConcurrentDictionary<string, string> _connectionIdMap = new();

    public override async Task OnConnectedAsync()
    {
        try
        {
            await Clients.Caller.SendAsync("ReceiveSystemMessage", "Connected to chat server.");
            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        try
        {
            await base.OnDisconnectedAsync(exception);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task RegisterUser(string connectionId,string userType)
    {
        try
        {
            await Clients.All.SendAsync("UserRegistered", Context.ConnectionId, userType);
            Console.WriteLine($"User {Context.ConnectionId} registered as {userType}");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task SendMessage(string senderType, string senderName, string message)
    {
        try
        {
            await Clients.All.SendAsync("ReceiveMessage", senderType, senderName, message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task NotifyTyping(string user) 
    {
        try
        {
            await Clients.Others.SendAsync("ReceiveTypingNotification", user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}