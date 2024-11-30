using apiBingo.Models;

using Microsoft.EntityFrameworkCore;

namespace apiBingo.Data
{
    public class BingoDbContext : DbContext
    {
        public BingoDbContext(DbContextOptions<BingoDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }

    }
}
