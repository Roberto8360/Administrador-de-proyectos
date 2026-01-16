using Admin.Models;

namespace Admin.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Empresa { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<Modulo> Modulos { get; set; } = new List<Modulo>();
    }
}


