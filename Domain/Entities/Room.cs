namespace ApiEmpleados_Backend.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; } // Código de 6 dígitos
    public DateTime CreatedAt { get; set; }

    public int CreatorId { get; set; }
    public User? Creator { get; set; }

    public List<User> Users { get; set; } = new List<User>();
}
