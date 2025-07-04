using EmpleadosApi.Data;
using Microsoft.EntityFrameworkCore;
using ApiEmpleados_Backend.Application.Services;
using ApiEmpleados_Backend.Domain.Ports;
using ApiEmpleados_Backend.Hubs;
using ApiEmpleados_Backend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
builder.Services.AddControllers();
/*builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("EmpleadosDb")); */
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IMessageRepository, FileMessageRepository>();
builder.Services.AddScoped<ChatService>();

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // URL de tu app Angular
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

var app = builder.Build();

// Middleware
/* if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} */

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp"); // Habilitar CORS

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");

// Inicializar la base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        DbInitializer.Seed(app);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
