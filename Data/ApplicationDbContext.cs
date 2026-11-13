using CrudClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudClientes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; } = null!;
    }
}
