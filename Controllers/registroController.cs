using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static PracticaApi.model;

namespace PracticaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class registroController : ControllerBase
    {
        private readonly AppDB _context;

        public registroController(AppDB context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] Usuario.registroDTO dto)
        {
            // Validar si ya existe el correo
            var existe = await _context.Usuarios.AnyAsync(u => u.correo == dto.correo);
            if (existe)
            {
                return BadRequest(new { mensaje = "El correo ya está registrado." });
            }

            // Crear la entidad Usuario basada en tu modelo
            var nuevoUsuario = new Usuario
            {
                nombre = dto.nombre,
                correo = dto.correo,
                claveHash = dto.clave, // En un entorno real, recuerda aplicar hashing (ej. BCrypt)
                disponible = true,
                aniosExperiencia = 0
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario registrado exitosamente." });
        }
    }
}