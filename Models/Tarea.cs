using System.Text.Json.Serialization;

namespace TaskFlow_API.Models
{
    public class Tarea
    {
        public int Id_tarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Prioridad { get; set; }
        public DateTime Fecha_vencimiento { get; set; }

        public int Id_Proyecto { get; set; }
        public int Id_Usuario { get; set; }

        [JsonIgnore]
        public Proyecto proyecto { get; set; }

        [JsonIgnore]
        public Usuario usuario { get; set; }
    }
}
