using System.Diagnostics;
using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class ReporteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReporteController> _logger;

        public ReporteController(ApplicationDbContext context, ILogger<ReporteController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult ObtenerDatosDelProyecto(int id)
        {
            // Buscar el proyecto
            var proyecto = _context.Proyectos
                .Include(p => p.Modulos) // Asegúrate que tienes esta relación
                .FirstOrDefault(p => p.Id == id);

            if (proyecto == null)
            {
                return NotFound(); // 404 si no existe el proyecto
            }

            // Mapear a DTO
            var dto = new ReporteProyectoDto
            {
                ProyectoId = proyecto.Id,
                ProyectoNombre = proyecto.Nombre,
                Modulos = proyecto.Modulos.Select(m => new ModuloDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Horas = m.Horas
                }).ToList()
            };

            return Json(dto); // Retornar el DTO como JSON
        }

    }
    
 }