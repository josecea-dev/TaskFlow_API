using Microsoft.EntityFrameworkCore;
using TaskFlow_API.Models;

namespace TaskFlow_API.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Proyecto> proyectos { get; set; }
        public DbSet<Tarea> tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //TABLA: Usuario

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id_usuario);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .ToTable(t => t.HasCheckConstraint
                (
                    "CH_USUARIO_ROL",
                    "[Rol] IN ('Administrador', 'Usuario')"
                ));

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .IsRequired();


            //TABLA: PROYECTO 

            modelBuilder.Entity<Proyecto>()
                .HasKey(p => p.Id_proyecto);

            modelBuilder.Entity<Proyecto>()
                .HasOne(p => p.usuario)
                .WithMany(p => p.proyecto)
                .HasForeignKey(p => p.Usuario_Id);

            modelBuilder.Entity<Proyecto>()
                .Property(p => p.fecha_registro)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Proyecto>()
                .Property(p => p.Descripcion)
                .IsRequired(false);

            modelBuilder.Entity<Proyecto>()
                .Property(p => p.Nombre)
                .IsRequired();


            //TABLA: TAREA

            modelBuilder.Entity<Tarea>()
                .HasKey(t => t.Id_tarea);

            modelBuilder.Entity<Tarea>()
                .Property(t => t.Descripcion)
                .IsRequired(false);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.usuario)
                .WithMany(t => t.tarea)
                .HasForeignKey(t => t.Id_Usuario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.proyecto)
                .WithMany(t => t.tarea)
                .HasForeignKey(t => t.Id_Proyecto)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Tarea>()
                .Property(t => t.Fecha_vencimiento)
                .IsRequired();

            modelBuilder.Entity<Tarea>()
                .ToTable(t => t.HasCheckConstraint
                (
                    "CH_TAREA_ESTADO",
                    "[Estado] IN ('Pendiente', 'En proceso', 'Finalizado')"
                ));

            modelBuilder.Entity<Tarea>()
                .ToTable(t => t.HasCheckConstraint
                (
                    "CH_TAREA_PRIORIDAD",
                    "[Prioridad] IN ('Baja', 'Media', 'Alta')"
                ));
                
        }
    }
}
