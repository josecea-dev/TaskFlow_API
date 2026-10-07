using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id_usuario);
                    table.CheckConstraint("CH_USUARIO_ROL", "[Rol] IN ('Administrador', 'Usuario')");
                });

            migrationBuilder.CreateTable(
                name: "proyectos",
                columns: table => new
                {
                    Id_proyecto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Usuario_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proyectos", x => x.Id_proyecto);
                    table.ForeignKey(
                        name: "FK_proyectos_usuarios_Usuario_Id",
                        column: x => x.Usuario_Id,
                        principalTable: "usuarios",
                        principalColumn: "Id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tareas",
                columns: table => new
                {
                    Id_tarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioridad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: false),
                    Id_Usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tareas", x => x.Id_tarea);
                    table.CheckConstraint("CH_TAREA_ESTADO", "[Estado] IN ('Pendiente', 'En proceso', 'Finalizado')");
                    table.CheckConstraint("CH_TAREA_PRIORIDAD", "[Prioridad] IN ('Baja', 'Media', 'Alta')");
                    table.ForeignKey(
                        name: "FK_tareas_proyectos_Id_Proyecto",
                        column: x => x.Id_Proyecto,
                        principalTable: "proyectos",
                        principalColumn: "Id_proyecto");
                    table.ForeignKey(
                        name: "FK_tareas_usuarios_Id_Usuario",
                        column: x => x.Id_Usuario,
                        principalTable: "usuarios",
                        principalColumn: "Id_usuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_proyectos_Usuario_Id",
                table: "proyectos",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tareas_Id_Proyecto",
                table: "tareas",
                column: "Id_Proyecto");

            migrationBuilder.CreateIndex(
                name: "IX_tareas_Id_Usuario",
                table: "tareas",
                column: "Id_Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_Email",
                table: "usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tareas");

            migrationBuilder.DropTable(
                name: "proyectos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
