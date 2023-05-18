using ApiContactos.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ApiContacto.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // Aquí Agregamos Todos Los modelos que se creen
        public DbSet<Contacto> Contactos { get; set; }
    }
}
