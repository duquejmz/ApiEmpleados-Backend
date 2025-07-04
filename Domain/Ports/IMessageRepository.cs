using ApiEmpleados_Backend.Domain.Entities;

namespace ApiEmpleados_Backend.Domain.Ports;

public interface IMessageRepository
{
    Task SaveMessageAsync(string roomCode, Message message);
    Task<IEnumerable<Message>> GetMessagesAsync(string roomCode);
}
