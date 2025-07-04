using ApiEmpleados_Backend.Domain.Ports;

namespace ApiEmpleados_Backend.Application.Services
{
    public class ChatService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMessageRepository _messageRepository;

        public ChatService(IRoomRepository roomRepository, IMessageRepository messageRepository)
        {
            _roomRepository = roomRepository;
            _messageRepository = messageRepository;
        }

        public async Task<string> CreateRoomAsync(string name, int creatorId)
        {
            var room = await _roomRepository.CreateRoomAsync(name, creatorId);
            return room.Code;
        }

        public async Task<bool> JoinRoomAsync(string roomCode)
        {
            var room = await _roomRepository.GetRoomByCodeAsync(roomCode);
            return room != null;
        }
    }
}
