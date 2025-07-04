using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmpleadosApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmpleados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "Id", "Activo", "Cargo", "Correo", "Departamento", "FechaIngreso", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, true, "Desarrollador", "juan.perez@example.com", "TI", new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan Pérez", "3001234567" },
                    { 2, true, "Diseñadora", "ana.gomez@example.com", "Marketing", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana Gómez", "3019876543" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
