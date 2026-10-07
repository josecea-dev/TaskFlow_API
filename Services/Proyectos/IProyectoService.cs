using TaskFlow_API.DTO;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services.Proyectos
{
    public interface IProyectoService
    {
        public Task<List<ProyectoDTO>> Get();
        public Task<Proyecto> Post(ProyectoDTO dto);
        public Task<Proyecto> Put(int id, ProyectoDTO dto);
        public Task<Proyecto> Delete(int id);
    }
}
