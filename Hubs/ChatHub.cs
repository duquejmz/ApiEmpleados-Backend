using Microsoft.AspNetCore.SignalR;
using ApiEmpleados_Backend.Domain.Ports;
using ApiEmpleados_Backend.Domain.Entities;

namespace ApiEmpleados_Backend.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task SendMessage(string roomCode, string user, string message)
        {
            var chatMessage = new Message
            {
                User = user,
                Content = message,
                Timestamp = DateTime.UtcNow
            };

            await _messageRepository.SaveMessageAsync(roomCode, chatMessage);
            await Clients.Group(roomCode).SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddToGroup(string roomCode, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);

            var history = await _messageRepository.GetMessagesAsync(roomCode);
            foreach (var msg in history)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", msg.User, msg.Content);
            }

            await Clients.Group(roomCode).SendAsync("ReceiveMessage", "System", $"{userName} has joined the room.");
        }
    }
}
