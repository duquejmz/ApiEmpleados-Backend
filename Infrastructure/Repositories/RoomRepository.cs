using ApiEmpleados_Backend.Domain.Entities;
using ApiEmpleados_Backend.Domain.Ports;
using EmpleadosApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpleados_Backend.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Room> CreateRoomAsync(string name, int creatorId)
    {
        var room = new Room
        {
            Name = name,
            Code = GenerateRoomCode(),
            CreatedAt = DateTime.UtcNow,
            CreatorId = creatorId
        };
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<Room?> GetRoomByCodeAsync(string code)
    {
        return await _context.Rooms.FirstOrDefaultAsync(r => r.Code == code);
    }

    private string GenerateRoomCode()
    {
        return new Random().Next(100000, 999999).ToString();
    }
}
