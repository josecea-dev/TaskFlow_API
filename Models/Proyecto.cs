using System.Text.Json.Serialization;

namespace TaskFlow_API.Models
{
    public class Proyecto
    {
        public int Id_proyecto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime fecha_registro { get; set; }

        //en el contexto de este proyecto, cada proyecto está designado a un único usuario
        public int Usuario_Id { get; set; }
        [JsonIgnore]
        public Usuario usuario { get; set; }

        //otra relacion 
        [JsonIgnore]
        public List<Tarea> tarea { get; set; }
    }
}
