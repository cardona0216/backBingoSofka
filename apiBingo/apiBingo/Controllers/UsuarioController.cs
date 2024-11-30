using apiBingo.Interfaces;
using apiBingo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiBingo.Controllers
{
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        // Endpoint para registrar un usuario
        [HttpPost("registrar")]
        public async Task<ActionResult<Usuario>> RegistrarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var nuevoUsuario = await _usuarioService.RegistrarUsuarioAsync(usuario.NombreUsuario, usuario.Password, usuario.Correo);
                return Ok(nuevoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Endpoint para login de usuario
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> LoginUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var usuarioLogueado = await _usuarioService.LoginUsuarioAsync(usuario.NombreUsuario, usuario.Password);
                return Ok(usuarioLogueado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
