using apiBingo.Models;

namespace apiBingo.Interfaces
{
    public interface IJugadorService
    {
        Task<List<Jugador>> ObtenerJugadoresAsync();
        Task<Jugador> CrearJugadorAsync(string nombre, int usuarioId);
    }
}
