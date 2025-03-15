using Microsoft.AspNetCore.SignalR;

namespace HotelManagementSystem.App;

public class ChatHub : Hub
{
    private static Dictionary<string, string> _connectedUsers = new();

    public override async Task OnConnectedAsync()
    {
        try
        {
            string userId = Context.ConnectionId;
            _connectedUsers[userId] = "Unknown";
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
            _connectedUsers.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task RegisterUser(string userType)
    {
        try
        {
            _connectedUsers[Context.ConnectionId] = userType;
            await Clients.All.SendAsync("UserRegistered", Context.ConnectionId, userType);
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

}
