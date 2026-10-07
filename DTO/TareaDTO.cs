namespace TaskFlow_API.DTO
{
    public class TareaDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Prioridad { get; set; }
        public DateTime Fecha_vencimiento { get; set; }

        //relaciones
        public int Id_Proyecto { get; set; }
        public int Id_Usuario { get; set; }


        //atributos adicionales
        public string Nombre_proyecto { get; set; }
        public string Nombre_usuario { get; set; }

    }
}
