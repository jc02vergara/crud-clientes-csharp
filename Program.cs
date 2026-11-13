using CrudClientes.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext (EF Core / SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorio de datos: EF Core (por defecto) o Procedimientos Almacenados (SQL Server)
var dataMode = builder.Configuration["DataAccess:Mode"] ?? "EFCore";
if (dataMode.Equals("StoredProcedures", StringComparison.OrdinalIgnoreCase))
{
    var sqlConn = builder.Configuration.GetConnectionString("SqlServerConnection")
        ?? throw new InvalidOperationException("Falta la cadena SqlServerConnection");
    builder.Services.AddScoped<IClienteRepository>(_ => new StoredProcedureClienteRepository(sqlConn));
}
else
{
    builder.Services.AddScoped<IClienteRepository, EfClienteRepository>();
}

var app = builder.Build();

// Crear la base de datos automáticamente (desarrollo)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
