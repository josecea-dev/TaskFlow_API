using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services.Auth
{
    public class PasswordService
    {
        private readonly PasswordHasher<Usuario> _hash;

        public PasswordService()
        {
            _hash = new PasswordHasher<Usuario>();
        }
        public string passwordHasher(Usuario usuario, string password)
        {
            return _hash.HashPassword(usuario, password);
        }
        public bool passwordValida(Usuario usuario, string passwordHash, string password)
        {
            var resultado = _hash.VerifyHashedPassword(usuario, passwordHash, password);

            return resultado == PasswordVerificationResult.Success;
        }
    }
}
