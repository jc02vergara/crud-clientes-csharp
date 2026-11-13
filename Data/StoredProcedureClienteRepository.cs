using CrudClientes.Models;
using Microsoft.Data.SqlClient;

namespace CrudClientes.Data
{
    /// <summary>
    /// Implementación del repositorio que invoca los procedimientos almacenados
    /// (sp_Cliente_*) mediante ADO.NET. Requiere SQL Server y la cadena de conexión
    /// "SqlServerConnection" en appsettings.json.
    /// </summary>
    public class StoredProcedureClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public StoredProcedureClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Cliente>> GetAllAsync(string? search)
        {
            var lista = new List<Cliente>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_Cliente_Listar", conn) { CommandType = System.Data.CommandType.StoredProcedure };
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var c = Map(reader);
                if (!string.IsNullOrWhiteSpace(search))
                {
                    var s = search.Trim().ToLower();
                    if (!(c.Nombre.ToLower().Contains(s) || c.Apellido.ToLower().Contains(s) ||
                          c.Cedula.ToLower().Contains(s) || (c.Email ?? "").ToLower().Contains(s)))
                        continue;
                }
                lista.Add(c);
            }
            return lista;
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_Cliente_ObtenerPorId", conn) { CommandType = System.Data.CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync()) return Map(reader);
            return null;
        }

        public async Task<int> CreateAsync(Cliente cliente)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_Cliente_Insertar", conn) { CommandType = System.Data.CommandType.StoredProcedure };
            AddParams(cmd, cliente, includeId: false);
            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_Cliente_Actualizar", conn) { CommandType = System.Data.CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", cliente.Id);
            AddParams(cmd, cliente, includeId: false);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("sp_Cliente_Eliminar", conn) { CommandType = System.Data.CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        private static void AddParams(SqlCommand cmd, Cliente c, bool includeId)
        {
            cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
            cmd.Parameters.AddWithValue("@Cedula", c.Cedula);
            cmd.Parameters.AddWithValue("@Email", c.Email);
            cmd.Parameters.AddWithValue("@Telefono", (object?)c.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Direccion", (object?)c.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Ciudad", (object?)c.Ciudad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pais", (object?)c.Pais ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FechaNacimiento", (object?)c.FechaNacimiento ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Activo", c.Activo);
            cmd.Parameters.AddWithValue("@Observaciones", (object?)c.Observaciones ?? DBNull.Value);
        }

        private static Cliente Map(SqlDataReader r)
        {
            return new Cliente
            {
                Id = Convert.ToInt32(r["Id"]),
                Nombre = r["Nombre"].ToString() ?? "",
                Apellido = r["Apellido"].ToString() ?? "",
                Cedula = r["Cedula"].ToString() ?? "",
                Email = r["Email"].ToString() ?? "",
                Telefono = r["Telefono"] == DBNull.Value ? null : r["Telefono"].ToString(),
                Direccion = r["Direccion"] == DBNull.Value ? null : r["Direccion"].ToString(),
                Ciudad = r["Ciudad"] == DBNull.Value ? null : r["Ciudad"].ToString(),
                Pais = r["Pais"] == DBNull.Value ? null : r["Pais"].ToString(),
                FechaNacimiento = r["FechaNacimiento"] == DBNull.Value ? null : Convert.ToDateTime(r["FechaNacimiento"]),
                FechaRegistro = Convert.ToDateTime(r["FechaRegistro"]),
                Activo = Convert.ToBoolean(r["Activo"]),
                Observaciones = r["Observaciones"] == DBNull.Value ? null : r["Observaciones"].ToString()
            };
        }
    }
}
