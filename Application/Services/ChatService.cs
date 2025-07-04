using ApiEmpleados_Backend.Domain.Ports;
using EmpleadosApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpleados_Backend.Application.Services
{
    public class ChatService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly AppDbContext _context; // Añadir DbContext

        // Modificar constructor para inyectar AppDbContext
        public ChatService(IRoomRepository roomRepository, IMessageRepository messageRepository, AppDbContext context)
        {
            _roomRepository = roomRepository;
            _messageRepository = messageRepository;
            _context = context;
        }

        public async Task<string?> CreateRoomAsync(string name, int creatorId)
        {
            // 1. Validar que el usuario creador exista
            var creator = await _context.Users.FirstOrDefaultAsync(u => u.Id == creatorId);
            if (creator == null)
            {
                // Considera lanzar una excepción o manejar el caso de usuario no encontrado
                return null; 
            }

            // 2. Crear la sala a través del repositorio
            var room = await _roomRepository.CreateRoomAsync(name, creatorId);

            // 3. La asociación se maneja por EF Core a través de la clave foránea CreatorId
            // No es necesario agregarla manualmente a la colección si las navegaciones están bien configuradas.
            // El repositorio ya guarda los cambios.

            return room.Code;
        }

        public async Task<bool> JoinRoomAsync(string roomCode)
        {
            var room = await _roomRepository.GetRoomByCodeAsync(roomCode);
            return room != null;
        }
    }
}
