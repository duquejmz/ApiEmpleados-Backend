using EmpleadosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosApi.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Aplica migraciones pendientes
            context.Database.Migrate();

            if (!context.Empleados.Any())
            {
                context.Empleados.AddRange(
                    new Empleado
                    {
                        Nombre = "Juan Pérez",
                        Correo = "juan.perez@example.com",
                        Cargo = "Desarrollador",
                        Departamento = "TI",
                        Telefono = "3001234567",
                        FechaIngreso = new DateTime(2022, 3, 15),
                        Activo = true
                    },
                    new Empleado
                    {
                        Nombre = "Ana Gómez",
                        Correo = "ana.gomez@example.com",
                        Cargo = "Diseñadora",
                        Departamento = "Marketing",
                        Telefono = "3019876543",
                        FechaIngreso = new DateTime(2023, 1, 10),
                        Activo = true
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
