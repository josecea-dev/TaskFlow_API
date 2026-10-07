using TaskFlow_API.DTO;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services.Usuarios
{
    public interface IUsuarioService
    {
        public Task<List<UsuarioDTO>> Get();
        public Task<Usuario> Post(UsuarioDTO dto);
        public Task<Usuario> Put(int id, UsuarioDTO dto);
        public Task<Usuario> Delete(int id);
    }
}
