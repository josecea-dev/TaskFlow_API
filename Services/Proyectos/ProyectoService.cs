using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Data;
using TaskFlow_API.DTO;
using TaskFlow_API.Models;

namespace TaskFlow_API.Services.Proyectos
{
    public class ProyectoService: IProyectoService
    {
        private readonly AppDBContext _context;

        public ProyectoService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProyectoDTO>> Get()
        {
            return await _context.proyectos
                .Select(p => new ProyectoDTO
                {
                    Id_proyecto = p.Id_proyecto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Nombre_usuario = p.usuario.Nombre,
                    Total_tarea = p.tarea.Count,
                    fecha_registro = p.fecha_registro
                })
                .ToListAsync();
        }
        public async Task<Proyecto> Post(ProyectoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                throw new ArgumentNullException();
            }

            //validar si existe el ID proporcionado para el usuario
            if (await _context.usuarios.FindAsync(dto.Usuario_Id) == null)
            {
                return null;
            }

            var post = new Proyecto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Usuario_Id = dto.Usuario_Id
            };


            await _context.proyectos.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;   
        }
        public async Task<Proyecto> Put(int id, ProyectoDTO dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                throw new ArgumentNullException();
            }

            //validar que existe el usuario
            if (await _context.usuarios.FindAsync(dto.Usuario_Id) == null)
            {
                return null;
            }

            var put = await _context.proyectos.FindAsync(id);

            //validar la existencia del ID del proyecto
            if (put == null)
            {
                return null;
            }

            put.Descripcion = dto.Descripcion;
            put.Nombre = dto.Nombre;
            put.Usuario_Id = dto.Usuario_Id;

            await _context.SaveChangesAsync();

            return put;

        }
        public async Task<Proyecto> Delete(int id)
        {
            var delete = await _context.proyectos.FindAsync(id);

            if (delete == null)
            {
                return null;
            }

            _context.proyectos.Remove(delete);

            await _context.SaveChangesAsync();

            return delete;
        }
    }
}
