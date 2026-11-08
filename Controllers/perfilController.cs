using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static PracticaApi.model;

namespace PracticaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class perfilController : ControllerBase
    {
        private readonly AppDB _context;

        public perfilController(AppDB context)
        {
            _context = context;
        }

        // Obtener perfil por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPerfil(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado." });
            }

            // Retornamos usando el perfilDTO que especificaste en tu modelo
            var perfil = new Usuario.perfilDTO(usuario.nombre, usuario.disponible, usuario.aniosExperiencia);

            return Ok(perfil);
        }

        // Actualizar perfil
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPerfil(int id, [FromBody] Usuario.perfilDTO dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado." });
            }

            usuario.nombre = dto.nombre;
            usuario.disponible = dto.disponible;
            usuario.aniosExperiencia = dto.aniosExperiencia;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Perfil actualizado correctamente." });
        }
    }
}