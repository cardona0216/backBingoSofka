using apiBingo.Models;

namespace apiBingo.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> RegistrarUsuarioAsync(string nombreUsuario, string contrasena, string correo);
        Task<Usuario> LoginUsuarioAsync(string nombreUsuario, string contrasena);
    }
}
