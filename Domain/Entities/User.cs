namespace ApiEmpleados_Backend.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public ICollection<Room> CreatedRooms { get; set; } = new List<Room>();
}
