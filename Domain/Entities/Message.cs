namespace ApiEmpleados_Backend.Domain.Entities;

public class Message
{
    public required string User { get; set; }
    public required string Content { get; set; }
    public DateTime Timestamp { get; set; }
}
