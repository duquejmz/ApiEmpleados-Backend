using Microsoft.EntityFrameworkCore;
using EmpleadosApi.Models;

namespace EmpleadosApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
    }
}
