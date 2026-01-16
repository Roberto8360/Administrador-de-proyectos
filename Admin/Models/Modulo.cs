namespace Admin.Models
{
    public class Modulo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public double Horas { get; set; }

        public int ProyectoId { get; set; }

        public Proyecto? Proyecto { get; set; }
    }
}
