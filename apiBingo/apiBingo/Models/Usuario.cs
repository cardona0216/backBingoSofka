using System.ComponentModel.DataAnnotations;

namespace apiBingo.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        public string Rol { get; set; } = "Usuario"; // Rol por defecto es "Usuario"

       
    }
}
