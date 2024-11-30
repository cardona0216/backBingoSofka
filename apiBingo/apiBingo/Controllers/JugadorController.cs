using apiBingo.Interfaces;
using apiBingo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiBingo.Controllers
{
    public class JugadorController : ControllerBase
    {

        private readonly IJugadorService _jugadorService;

        public JugadorController(IJugadorService jugadorService)
        {
            _jugadorService = jugadorService;
        }

        // Endpoint para obtener todos los jugadores
        [HttpGet("ObtenerJugadores")]
        public async Task<ActionResult<List<Jugador>>> ObtenerJugadores()
        {
            var jugadores = await _jugadorService.ObtenerJugadoresAsync();
            return Ok(jugadores);
        }

        [HttpPost("crearJugador")]
        public async Task<ActionResult<Jugador>> CrearJugador([FromBody] CrearJugadorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || request.UsuarioId <= 0)
                return BadRequest("Datos inválidos.");

            var jugador = await _jugadorService.CrearJugadorAsync(request.Nombre, request.UsuarioId);
            return CreatedAtAction(nameof(ObtenerJugadores), new { id = jugador.Id }, jugador);
        }



        // Clase para recibir datos en el request
        public class CrearJugadorRequest
        {
            public string Nombre { get; set; }
            public int UsuarioId { get; set; }
        }
    }
}
