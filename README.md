# CRUD de Clientes en C# (ASP.NET Core MVC)

Proyecto web en **C# / ASP.NET Core 8 (MVC)** que implementa el **CRUD** (Crear, Leer, Actualizar, Eliminar) de la tabla **Clientes**, con un mínimo de 10 campos.

## Campos de la tabla Clientes

1. `Id` (PK, autoincremental)
2. `Nombre`
3. `Apellido`
4. `Cedula` (Cédula / RUC)
5. `Email`
6. `Telefono`
7. `Direccion`
8. `Ciudad`
9. `Pais`
10. `FechaNacimiento`
11. `FechaRegistro`
12. `Activo`
13. `Observaciones`

## Estructura

```
CrudClientes/
├── Controllers/ClientesController.cs   # Acciones CRUD
├── Models/Cliente.cs                   # Modelo con validaciones
├── Data/ApplicationDbContext.cs        # DbContext (EF Core)
├── Views/Clientes/                     # Vistas Razor (Index, Create, Edit, Details, Delete)
├── Scripts/Clientes.sql                # Tabla + procedimientos almacenados (SQL Server)
├── Program.cs                          # Configuración y EnsureCreated
├── appsettings.json                    # Cadena de conexión (LocalDB)
└── README.md
```

## Requisitos

- .NET SDK 8.0
- SQL Server LocalDB (incluido con Visual Studio) o SQL Server

## Ejecución

```bash
cd CrudClientes
dotnet run
```

Abrir en el navegador la URL indicada (por defecto `https://localhost:5001` o `http://localhost:5000`) y navegar a `/Clientes`.

La base de datos `CrudClientesDb` se crea automáticamente en LocalDB al iniciar la aplicación (`EnsureCreated`).

## Procedimientos almacenados (SQL Server)

En `Scripts/Clientes.sql` se incluyen los procedimientos almacenados para el CRUD:
- `sp_Cliente_Insertar`
- `sp_Cliente_Actualizar`
- `sp_Cliente_Eliminar`
- `sp_Cliente_ObtenerPorId`
- `sp_Cliente_Listar`

Para usarlos, ejecutar el script en SQL Server Management Studio (SSMS) sobre la base `CrudClientesDb`.
