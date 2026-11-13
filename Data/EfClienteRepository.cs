using CrudClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudClientes.Data
{
    public class EfClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public EfClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllAsync(string? search)
        {
            var query = _context.Clientes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim().ToLower();
                query = query.Where(c =>
                    c.Nombre.ToLower().Contains(search) ||
                    c.Apellido.ToLower().Contains(search) ||
                    c.Cedula.ToLower().Contains(search) ||
                    (c.Email != null && c.Email.ToLower().Contains(search)));
            }
            return await query.OrderByDescending(c => c.Id).ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<int> CreateAsync(Cliente cliente)
        {
            cliente.FechaRegistro = DateTime.Now;
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente.Id;
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
