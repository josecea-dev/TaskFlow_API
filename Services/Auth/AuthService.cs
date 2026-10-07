using TaskFlow_API.Data;
using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TaskFlow_API.Services.Auth
{
    public class AuthService: IAuthService
    {
        //acceso a la bd, atributos y tablas
        private readonly AppDBContext _context;

        //acceso a la clase que convierte y valida contraseñas hash
        private readonly PasswordService _passwordService;

        //acceso a la configuracion para hacer uso de la clave secreta del JWT
        private readonly IConfiguration _configuration;

        public AuthService(AppDBContext context, PasswordService passwordService, IConfiguration configuration)
        {
            _context = context;
            _passwordService = passwordService;
            _configuration = configuration;
        }
        public async Task<string> Login(LoginDTO dto)
        {
            //validamos que el usuario no mande datos vacios
            if (string.IsNullOrWhiteSpace(dto.Password) || string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentNullException();

            //validamos que el usuario realmente exista mediante su correo
            var usuario = await _context.usuarios.FirstOrDefaultAsync(u => u.Email ==  dto.Email);

            if (usuario == null)
            {
                return null;
            }

            //validamos que la contraseña del usuario sea la correcta
            if (!_passwordService.passwordValida(usuario, usuario.PasswordHash, dto.Password))
            {
                return null;
            }

            //creamos los claims para el JWT
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id_usuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            //creamos nuestro sello

            var JwtKey = _configuration["Jwt:Key"] 
                ?? throw new InvalidOperationException("JWT key isn't configured.");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtKey)
            );

            //creamos las credenciales
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            //finalmente creamos el JWT
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            //retornamos el JSON Web Token ya creado al usuario con una duración de expiración de 2 horas
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
