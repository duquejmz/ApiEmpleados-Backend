using Microsoft.EntityFrameworkCore;
using EmpleadosApi.Models;
using ApiEmpleados_Backend.Domain.Entities;

namespace EmpleadosApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedRooms)
                .WithOne(r => r.Creator)
                .HasForeignKey(r => r.CreatorId);
        }
    }
}
