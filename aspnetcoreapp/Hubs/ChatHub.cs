using Microsoft.AspNetCore.SignalR;

namespace aspnetcoreapp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine($"Received message {user} {message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}