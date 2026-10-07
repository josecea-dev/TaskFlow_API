namespace TaskFlow_API.DTO
{
    public class ProyectoDTO
    {
        public int Id_proyecto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime fecha_registro { get; set; }

        //parametros de relaciones
        public int Usuario_Id { get; set; }

        public string Nombre_usuario { get;set; }
        public int Total_tarea { get; set; }

    }
}
