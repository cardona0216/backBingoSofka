using apiBingo.Data;
using apiBingo.Interfaces;
using apiBingo.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System;
using System.Text;

namespace apiBingo.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly BingoDbContext _context;

        public UsuarioService(BingoDbContext context)
        {
            _context = context;
        }


        // Método para registrar un nuevo usuario
        public async Task<Usuario> RegistrarUsuarioAsync(string nombreUsuario, string contrasena, string correo)
        {
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario || u.Correo == correo);

            if (usuarioExistente != null)
                throw new Exception("El usuario o correo ya está registrado");

            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                Correo = correo,
                Password = HashPassword(contrasena)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        // Método para hacer login de un usuario
        public async Task<Usuario> LoginUsuarioAsync(string nombreUsuario, string contrasena)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null || !VerifyPassword(contrasena, usuario.Password))
                throw new Exception("Usuario o contraseña incorrectos");

            return usuario;
        }

        // Método para hashear la contraseña
        private string HashPassword(string contrasena)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Método para verificar la contraseña
        private bool VerifyPassword(string contrasena, string hashedPassword)
        {
            var hashedInputPassword = HashPassword(contrasena);
            return hashedInputPassword == hashedPassword;
        }
    }
}
