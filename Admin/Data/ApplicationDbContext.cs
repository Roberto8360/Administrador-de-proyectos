using Microsoft.EntityFrameworkCore;
using Admin.Models; 
namespace Admin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Proyecto> Proyectos { get; set; }

        public DbSet<Modulo> Modulos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReporteProyectoDto>().HasNoKey();
        }

    }
}
