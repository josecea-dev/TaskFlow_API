using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Data;
using TaskFlow_API.DTO;
using TaskFlow_API.Models;
using TaskFlow_API.Services.Auth;

namespace TaskFlow_API.Services.Usuarios
{
    public class UsuarioService: IUsuarioService
    {
        private readonly AppDBContext _context;
        private readonly PasswordService _hasher;

        public UsuarioService(AppDBContext context, PasswordService hasher)
        {
            _context = context;
            _hasher = hasher;
        }
        public async Task<List<UsuarioDTO>> Get()
        {
            return await _context.usuarios
                .Select(u => new UsuarioDTO 
                {
                    Nombre = u.Nombre,
                    Email = u.Email
                })
                .ToListAsync();
        }
        public async Task<Usuario> Post(UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Rol) || string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentNullException();

            var post = new Usuario 
            {
                Email = dto.Email,
                Rol = dto.Rol,
                Nombre = dto.Nombre,
               
            };

            post.PasswordHash = _hasher.passwordHasher(post, dto.password);



            await _context.usuarios.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }
        public async Task<Usuario> Put(int id, UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Rol) || string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.password))
                throw new ArgumentNullException();

            var put = await _context.usuarios.FindAsync(id);

            if (put == null)
                return null;

            put.Rol = dto.Rol;
            put.Email = dto.Email;
            put.Nombre = dto.Nombre;

            put.PasswordHash = _hasher.passwordHasher(put, dto.password);

            await _context.SaveChangesAsync();

            return put;
        }
        public async Task<Usuario> Delete(int id)
        {
            var delete = await _context.usuarios.FindAsync(id);

            if (delete == null)
            {
                return null;
            }

            _context.usuarios.Remove(delete);
            await _context.SaveChangesAsync();

            return delete;
        }
    }
}
