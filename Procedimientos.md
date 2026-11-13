# Procedimientos para crear el CRUD de Clientes en C# (ASP.NET Core)

## 1. Crear el proyecto
```bash
dotnet new mvc -n CrudClientes -o CrudClientes
cd CrudClientes
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.11
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.11
dotnet add package Microsoft.Data.SqlClient --version 5.2.2
```

## 2. Crear el modelo (Models/Cliente.cs)
Definir la clase `Cliente` con los campos requeridos (mínimo 10) y validaciones con DataAnnotations (`[Required]`, `[StringLength]`, `[EmailAddress]`).

## 3. Crear el DbContext (Data/ApplicationDbContext.cs)
Heredar de `DbContext`, exponer `DbSet<Cliente> Clientes`.

## 4. Crear la interfaz del repositorio (Data/IClienteRepository.cs)
Definir los métodos: `GetAllAsync`, `GetByIdAsync`, `CreateAsync`, `UpdateAsync`, `DeleteAsync`.

## 5. Implementar los repositorios
- `EfClienteRepository`: implementación con EF Core (SQLite por defecto).
- `StoredProcedureClienteRepository`: implementación con ADO.NET que invoca los procedimientos almacenados `sp_Cliente_*` (SQL Server).

## 6. Configurar Program.cs
- Registrar el `DbContext`.
- Registrar el repositorio según `DataAccess:Mode` (EFCore o StoredProcedures).
- Llamar a `EnsureCreated()` en desarrollo.

## 7. Crear el controlador (Controllers/ClientesController.cs)
Implementar las acciones Index (con búsqueda y paginación), Details, Create, Edit y Delete.

## 8. Crear las vistas Razor (Views/Clientes/)
`Index.cshtml`, `Create.cshtml`, `Edit.cshtml`, `Details.cshtml`, `Delete.cshtml`.

## 9. Agregar enlace en el menú (_Layout.cshtml)
Añadir el enlace "Clientes" en la barra de navegación.

## 10. Configurar la base de datos (appsettings.json)
Cadena de conexión SQLite (`Data Source=CrudClientesDb.sqlite`) y, opcionalmente, SQL Server (`SqlServerConnection`).

## 11. Crear la tabla y procedimientos almacenados (Scripts/Clientes.sql)
Ejecutar el script en SQL Server Management Studio para crear la tabla `Clientes` y los procedimientos `sp_Cliente_Insertar`, `sp_Cliente_Actualizar`, `sp_Cliente_Eliminar`, `sp_Cliente_ObtenerPorId`, `sp_Cliente_Listar`.

## 12. Ejecutar
```bash
dotnet run
```
Abrir `http://localhost:5000/Clientes`.

## Notas
- Para usar los procedimientos almacenados, cambiar `"DataAccess:Mode"` a `"StoredProcedures"` y configurar `SqlServerConnection` apuntando a un SQL Server.
- El repositorio público del proyecto: https://github.com/jc02vergara/crud-clientes-csharp
