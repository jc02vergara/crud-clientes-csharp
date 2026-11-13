using CrudClientes.Models;

namespace CrudClientes.Data
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAllAsync(string? search);
        Task<Cliente?> GetByIdAsync(int id);
        Task<int> CreateAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
    }
}
