using CrudClientes.Data;
using CrudClientes.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudClientes.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _repo;

        public ClientesController(IClienteRepository repo)
        {
            _repo = repo;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            const int pageSize = 5;
            var clientes = await _repo.GetAllAsync(search);
            var total = clientes.Count;
            var items = clientes.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            return View(items);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _repo.GetByIdAsync(id.Value);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Cedula,Email,Telefono,Direccion,Ciudad,Pais,FechaNacimiento,Activo,Observaciones")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _repo.CreateAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _repo.GetByIdAsync(id.Value);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Cedula,Email,Telefono,Direccion,Ciudad,Pais,FechaNacimiento,FechaRegistro,Activo,Observaciones")] Cliente cliente)
        {
            if (id != cliente.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _repo.UpdateAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var cliente = await _repo.GetByIdAsync(id.Value);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
