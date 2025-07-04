using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpleadosApi.Data;
using EmpleadosApi.Models;

namespace EmpleadosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpleadosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            Console.WriteLine("Endpoint GetEmpleados fue alcanzado.");
            try
            {
                return await _context.Empleados.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetEmpleados: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // GET: api/empleados/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            Console.WriteLine($"Endpoint GetEmpleado con id: {id} fue alcanzado.");
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);

                if (empleado == null)
                {
                    Console.WriteLine($"Empleado con id: {id} no encontrado.");
                    return NotFound();
                }

                return empleado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetEmpleado con id {id}: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // POST: api/empleados
        [HttpPost]
        public async Task<ActionResult<Empleado>> CreateEmpleado(Empleado empleado)
        {
            Console.WriteLine("Endpoint CreateEmpleado fue alcanzado.");
            try
            {
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("Modelo de estado inválido.");
                    return BadRequest(ModelState);
                }

                if (empleado.FechaIngreso > DateTime.Now)
                {
                    Console.WriteLine("La fecha de ingreso no puede ser futura.");
                    return BadRequest("La fecha de ingreso no puede ser futura.");
                }

                _context.Empleados.Add(empleado);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Empleado con id: {empleado.Id} creado exitosamente.");
                return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Id }, empleado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CreateEmpleado: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // PUT: api/empleados/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpleado(int id, Empleado empleado)
        {
            Console.WriteLine($"Endpoint UpdateEmpleado con id: {id} fue alcanzado.");
            try
            {
                if (id != empleado.Id)
                {
                    Console.WriteLine("ID no coincide con el empleado.");
                    return BadRequest("ID no coincide con el empleado.");
                }

                if (!ModelState.IsValid)
                {
                    Console.WriteLine("Modelo de estado inválido.");
                    return BadRequest(ModelState);
                }

                if (empleado.FechaIngreso > DateTime.Now)
                {
                    Console.WriteLine("La fecha de ingreso no puede ser futura.");
                    return BadRequest("La fecha de ingreso no puede ser futura.");
                }

                var empleadoExistente = await _context.Empleados.FindAsync(id);
                if (empleadoExistente == null)
                {
                    Console.WriteLine($"Empleado con id: {id} no encontrado.");
                    return NotFound();
                }

                // Actualiza campos
                empleadoExistente.Nombre = empleado.Nombre;
                empleadoExistente.Correo = empleado.Correo;
                empleadoExistente.Cargo = empleado.Cargo;
                empleadoExistente.Departamento = empleado.Departamento;
                empleadoExistente.Telefono = empleado.Telefono;
                empleadoExistente.FechaIngreso = empleado.FechaIngreso;
                empleadoExistente.Activo = empleado.Activo;

                await _context.SaveChangesAsync();

                Console.WriteLine($"Empleado con id: {id} actualizado exitosamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateEmpleado con id {id}: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // DELETE: api/empleados/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            Console.WriteLine($"Endpoint DeleteEmpleado con id: {id} fue alcanzado.");
            try
            {
                var empleado = await _context.Empleados.FindAsync(id);

                if (empleado == null)
                {
                    Console.WriteLine($"Empleado con id: {id} no encontrado.");
                    return NotFound();
                }

                _context.Empleados.Remove(empleado);
                await _context.SaveChangesAsync();

                Console.WriteLine($"Empleado con id: {id} eliminado exitosamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteEmpleado con id {id}: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
