using TaskFlow_API.DTO;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services.Tareas
{
    public interface ITareaService
    {
        public Task<List<TareaDTO>> Get();
        public Task<Tarea> Post(TareaDTO dto);
        public Task<Tarea> Put(int id, TareaDTO dto);
        public Task<Tarea> Delete(int id);
    }
}
