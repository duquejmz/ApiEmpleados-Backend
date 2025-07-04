using System.ComponentModel.DataAnnotations;

namespace EmpleadosApi.Models
{
	public class Empleado
	{
		public int Id { get; set; }

		[Required]
		public string Nombre { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string Correo { get; set; } = string.Empty;

		public string? Cargo { get; set; }
		public string? Departamento { get; set; }
		public string? Telefono { get; set; }

		[Required]
		public DateTime FechaIngreso { get; set; }

		public bool Activo { get; set; }
	}
}
