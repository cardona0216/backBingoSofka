using apiBingo.Data;
using apiBingo.Interfaces;
using apiBingo.Models;
using Microsoft.EntityFrameworkCore;

namespace apiBingo.Services
{
    public class JugadorService : IJugadorService
    {
        

            private readonly BingoDbContext _context;

        public JugadorService(BingoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Jugador>> ObtenerJugadoresAsync()
        {
            return await _context.Jugadores.Include(j => j.Usuario).ToListAsync();
        }
        public async Task<Jugador> CrearJugadorAsync(string nombre, int usuarioId)
        {
            // Crea un nuevo jugador
            var jugador = new Jugador
            {
                Nombre = nombre,
                UsuarioId = usuarioId
            };

            // Guarda en la base de datos
            _context.Jugadores.Add(jugador);
            await _context.SaveChangesAsync();

            return jugador;

        }

        // Nuevo método para seleccionar un número en el tarjetón
        public async Task SeleccionarNumeroAsync(int jugadorId, int numero)
        {
            var jugador = await _context.Jugadores.FirstOrDefaultAsync(j => j.Id == jugadorId);
            if (jugador == null)
            {
                throw new ArgumentException("Jugador no encontrado.");
            }

            if (jugador.Tarjeton.Any(fila => fila.Contains(numero)))
            {
                jugador.NumerosSeleccionados.Add(numero);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Número no encontrado en el tarjetón.");
            }
        }

    }
}
