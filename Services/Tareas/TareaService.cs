using Microsoft.EntityFrameworkCore;
using System.Runtime;
using TaskFlow_API.Data;
using TaskFlow_API.DTO;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services.Tareas
{
    public class TareaService : ITareaService
    {
        private readonly AppDBContext _context;

        public TareaService(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<TareaDTO>> Get()
        {
            return await _context.tareas
                .Select(t => new TareaDTO 
                {
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    Estado = t.Estado,
                    Prioridad = t.Prioridad,
                    Fecha_vencimiento = t.Fecha_vencimiento,
                    Nombre_proyecto = t.proyecto.Nombre,
                    Nombre_usuario = t.usuario.Nombre
                })
                .ToListAsync();
        }
        public async Task<Tarea> Post(TareaDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Prioridad) || string.IsNullOrWhiteSpace(dto.Titulo) || string.IsNullOrWhiteSpace(dto.Estado))
                throw new ArgumentNullException();


            //validar si realmente existe el id del proyecto o de usuario
            if (await _context.proyectos.FindAsync(dto.Id_Proyecto) == null || await _context.usuarios.FindAsync(dto.Id_Usuario) == null)
            {
                return null;
            }

            var post = new Tarea 
            {
                Prioridad = dto.Prioridad,
                Titulo = dto.Titulo,
                Estado = dto.Estado,
                Descripcion = dto.Descripcion,
                Fecha_vencimiento = dto.Fecha_vencimiento,
                Id_Proyecto = dto.Id_Proyecto,
                Id_Usuario = dto.Id_Usuario
            };

            await _context.tareas.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }
        public async Task<Tarea> Put(int id, TareaDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Prioridad) || string.IsNullOrWhiteSpace(dto.Titulo) || string.IsNullOrWhiteSpace(dto.Estado))
                throw new ArgumentNullException();


            //nuevamente, se valida si realmente existe el id del proyecto o de usuario
            if (await _context.proyectos.FindAsync(dto.Id_Proyecto) == null || await _context.usuarios.FindAsync(dto.Id_Usuario) == null)
            {
                return null;
            }

            //se valida que el ID de tarea realmente exista
            var put = await _context.tareas.FindAsync(id);

            if (put == null)
                return null;

            put.Prioridad = dto.Prioridad;
            put.Titulo = dto.Titulo;
            put.Estado = dto.Estado;
            put.Descripcion = dto.Descripcion;
            put.Fecha_vencimiento = dto.Fecha_vencimiento;
            put.Id_Proyecto = dto.Id_Proyecto;
            put.Id_Usuario = dto.Id_Usuario;

            await _context.SaveChangesAsync();

            return put;
        }
        public async Task<Tarea> Delete(int id)
        {
            var delete = await _context.tareas.FindAsync(id);

            if (delete == null)
                return null;

            _context.tareas.Remove(delete);
            await _context.SaveChangesAsync();

            return delete;
        }
    }
}
