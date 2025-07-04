using System.Text.Json;
using ApiEmpleados_Backend.Domain.Entities;
using ApiEmpleados_Backend.Domain.Ports;

namespace ApiEmpleados_Backend.Infrastructure.Repositories;

public class FileMessageRepository : IMessageRepository
{
    private readonly string _storagePath = Path.Combine(AppContext.BaseDirectory, "ChatHistory");

    public FileMessageRepository()
    {
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(string roomCode)
    {
        var filePath = Path.Combine(_storagePath, $"{roomCode}.json");
        if (!File.Exists(filePath))
        {
            return Enumerable.Empty<Message>();
        }

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Message>>(json) ?? new List<Message>();
    }

    public async Task SaveMessageAsync(string roomCode, Message message)
    {
        var messages = (await GetMessagesAsync(roomCode)).ToList();
        messages.Add(message);
        
        var filePath = Path.Combine(_storagePath, $"{roomCode}.json");
        var json = JsonSerializer.Serialize(messages, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }
}
