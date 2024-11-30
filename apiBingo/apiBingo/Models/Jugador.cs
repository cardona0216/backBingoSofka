using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace apiBingo.Models
{
    public class Jugador
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int UsuarioId { get; set; } // Relación con el usuario
        public Usuario Usuario { get; set; }

        [Required]
        public string TarjetonSerialized { get; set; }  // Guardar la representación en string

        // Propiedad no mapeada
        [NotMapped]
        public List<List<int>> Tarjeton
        {
            get
            {
                return JsonSerializer.Deserialize<List<List<int>>>(TarjetonSerialized);
            }
            set
            {
                TarjetonSerialized = JsonSerializer.Serialize(value);
            }
        }

        // Nueva propiedad para almacenar los números seleccionados
        [NotMapped]
        public HashSet<int> NumerosSeleccionados { get; set; } = new HashSet<int>();

        public Jugador()
        {
            Tarjeton = GenerarTarjeton();
        }

        private List<List<int>> GenerarTarjeton()
        {
            var random = new Random();
            var numerosUsados = new HashSet<int>();
            var matriz = new List<List<int>>();

            // Definir los rangos para cada letra (B, I, N, G, O)
            var rangos = new Dictionary<int, Tuple<int, int>>
    {
        { 0, new Tuple<int, int>(1, 15) },   // B: 1-15
        { 1, new Tuple<int, int>(16, 30) },  // I: 16-30
        { 2, new Tuple<int, int>(31, 45) },  // N: 31-45
        { 3, new Tuple<int, int>(46, 60) },  // G: 46-60
        { 4, new Tuple<int, int>(61, 75) }   // O: 61-75
    };

            // Inicializamos las filas (5 en total)
            for (int i = 0; i < 5; i++)
            {
                var fila = new List<int>();

                // Generamos un número aleatorio para cada columna en el rango correspondiente
                for (int j = 0; j < 5; j++)
                {
                    int num;
                    do
                    {
                        var rango = rangos[j];
                        num = random.Next(rango.Item1, rango.Item2 + 1); // Genera un número en el rango correspondiente
                    } while (numerosUsados.Contains(num));

                    fila.Add(num);
                    numerosUsados.Add(num); // Marcar el número como usado
                }

                // Agregamos la fila a la matriz
                matriz.Add(fila);
            }

            // Devolvemos la matriz generada
            return matriz;
        }
    }
}
