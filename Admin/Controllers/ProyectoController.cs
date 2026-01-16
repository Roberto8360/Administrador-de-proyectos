using System.Diagnostics;
using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProyectoController> _logger;

        public ProyectoController(ApplicationDbContext context, ILogger<ProyectoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerProyectosActivos()
        {
            var proyectos = _context.Proyectos
                .Where(p => p.Estado)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    Modulos = p.Modulos.Select(m => new
                    {
                        m.Id,
                        m.Nombre,
                        m.Horas
                    }).ToList()
                })
                .ToList();

            return Json(proyectos);
        }
    }

}
