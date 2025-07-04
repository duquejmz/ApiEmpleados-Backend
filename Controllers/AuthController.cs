using Microsoft.AspNetCore.Mvc;
using ApiEmpleados_Backend.Domain.Entities;
using EmpleadosApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiEmpleados_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // En un caso real, aquí iría el hasheo de la contraseña
                // y la validación de si el email ya existe.
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    return Conflict("A user with this email already exists.");
                }

                // Asignar un nombre por defecto si es nulo
                user.Name = user.Name ?? "Default Name";
                // Aquí deberías hashear la contraseña antes de guardarla
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                
                // Evitar devolver la contraseña hasheada en la respuesta
                user.PasswordHash = null; 
                return Ok(user);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "A database error occurred while registering the user.");
                return StatusCode(500, "An internal database error occurred. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while registering the user.");
                return StatusCode(500, "An unexpected internal server error occurred. Please try again later.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email && u.PasswordHash == loginUser.PasswordHash);
                
                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }
                
                // Evitar devolver la contraseña hasheada en la respuesta
                user.PasswordHash = null;
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during the login process.");
                return StatusCode(500, "An unexpected internal server error occurred. Please try again later.");
            }
        }
    }
}