using System.Diagnostics;
using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
       
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerProyectos()
        {
            var proyectos = _context.Proyectos
                .Select(p => new
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Empresa = p.Empresa
                })
                .ToList();

            return Json(proyectos);
        }

        [HttpPost]
        public IActionResult Create(string nombre, string empresa)
        {
           
            var proyecto = new Proyecto
            {
                Nombre = nombre,
                Empresa = empresa,
                Estado = true,
                FechaCreacion = DateTime.Now
            };
            _context.Proyectos.Add(proyecto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AgregarModulo(string nombre, double horas, int proyectoId)
        {
            var modulo = new Modulo
            {
                Nombre = nombre,
                Horas = horas,
                ProyectoId = proyectoId
            };


            _context.Modulos.Add(modulo);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult ActualizarProyecto(int id, string nombre, string empresa)
        {
            var proyecto = _context.Proyectos.FirstOrDefault(p => p.Id == id);

            if (proyecto == null)
            {
                return Json(new { success = false, message = "El proyecto no existe" });
            }

            proyecto.Nombre = nombre;
            proyecto.Empresa = empresa;

            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult EliminarProyecto(int id)
        {
            var proyecto = _context.Proyectos.Find(id);
            if (proyecto == null) return Json(new { success = false });

            _context.Proyectos.Remove(proyecto);
            _context.SaveChanges();
            return Json(new { success = true });
        }


        [HttpGet]
        public JsonResult ObtenerModulosPorProyecto(int idProyecto)
        {
            var modulos = _context.Modulos
                .Where(m => m.ProyectoId == idProyecto)
                .Select(m => new {
                    id = m.Id,
                    nombre = m.Nombre
                })
                .ToList();

            return Json(modulos);
        }

    }
}
