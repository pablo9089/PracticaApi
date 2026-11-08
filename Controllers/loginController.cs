using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static PracticaApi.model;

namespace PracticaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class loginController : ControllerBase
    {
        private readonly AppDB _context;
        // Si tienes inyectado un servicio de JWT (ej. configJWT), puedes agregarlo aquí por constructor

        public loginController(AppDB context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Usuario.loginDTO dto)
        {
            // Buscar el usuario por correo
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.correo == dto.correo);

            if (usuario == null || usuario.claveHash != dto.clave)
            {
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
            }

            // Aquí puedes generar tu token JWT si ya lo configurasre con configJWT.cs
            return Ok(new { mensaje = "Login exitoso", usuarioId = usuario.id, nombre = usuario.nombre });
        }
    }
}