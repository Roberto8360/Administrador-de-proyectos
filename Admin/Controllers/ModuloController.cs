using System.Diagnostics;
using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class ModuloController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ModuloController> _logger;

        public ModuloController(ApplicationDbContext context, ILogger<ModuloController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult CargarModulosdeProyecto(int id)
        {

            var proyecto = _context.Proyectos
                .Include(p => p.Modulos)
                .FirstOrDefault(p => p.Id == id);

            if (proyecto == null)
            {
                return new JsonResult(new { error = "Proyecto no encontrado" });
            }

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

            return new JsonResult(dto);
        }

        [HttpPost]
        public JsonResult AgregarModulo(int proyectoId, string nombre, int horas)
        {
            if (string.IsNullOrWhiteSpace(nombre) || horas < 1)
            {
                return Json(new { success = false });
            }

            var proyecto = _context.Proyectos.FirstOrDefault(p => p.Id == proyectoId);
            if (proyecto == null)
            {
                return Json(new { success = false });
            }

            var nuevoModulo = new Modulo
            {
                Nombre = nombre,
                Horas = horas,
                ProyectoId = proyectoId
            };

            _context.Modulos.Add(nuevoModulo);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult EliminarModulo(int id)
        {
            var modulo = _context.Modulos.Find(id);
            if (modulo == null) return Json(new { success = false });

            _context.Modulos.Remove(modulo);
            _context.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult ActualizarModulo(int id, string nombre, int horas)
        {
            var modulo = _context.Modulos.FirstOrDefault(p => p.Id == id);

            if (modulo == null)
            {
                return Json(new { success = false, message = "El modulo no existe" });
            }

            modulo.Nombre = nombre;
            modulo.Horas = horas;

            _context.SaveChanges();

            return Json(new { success = true });

        }
    }
}
