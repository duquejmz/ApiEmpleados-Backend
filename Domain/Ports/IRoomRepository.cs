using ApiEmpleados_Backend.Domain.Entities;

namespace ApiEmpleados_Backend.Domain.Ports;

public interface IRoomRepository
{
    Task<Room> CreateRoomAsync(string name, int creatorId);
    Task<Room?> GetRoomByCodeAsync(string code);
    // Otros métodos necesarios...
}
