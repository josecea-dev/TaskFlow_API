using System.Text.Json.Serialization;

namespace TaskFlow_API.Models
{
    public class Usuario
    {
        public int Id_usuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public List<Proyecto> proyecto { get; set; }
        [JsonIgnore]
        public List<Tarea> tarea { get; set; }
    }
}
