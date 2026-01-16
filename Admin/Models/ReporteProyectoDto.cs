namespace Admin.Models
{
    public class ReporteProyectoDto
    {
        public int ProyectoId { get; set; }
        public string? ProyectoNombre { get; set; }

        public List<ModuloDto> Modulos { get; set; } = new();
    }

    public class ModuloDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public double Horas { get; set; }
    }
}