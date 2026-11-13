-- ============================================================
-- Script SQL: Tabla CLIENTES + Procedimientos almacenados CRUD
-- Motor: SQL Server
-- ============================================================

USE [CrudClientesDb];
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Clientes')
BEGIN
    CREATE TABLE [dbo].[Clientes]
    (
        [Id]                INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Nombre]            NVARCHAR(50)  NOT NULL,
        [Apellido]          NVARCHAR(50)  NOT NULL,
        [Cedula]            NVARCHAR(20)  NOT NULL,
        [Email]             NVARCHAR(100) NOT NULL,
        [Telefono]          NVARCHAR(20)  NULL,
        [Direccion]         NVARCHAR(150) NULL,
        [Ciudad]            NVARCHAR(60)  NULL,
        [Pais]              NVARCHAR(60)  NULL,
        [FechaNacimiento]   DATE          NULL,
        [FechaRegistro]     DATETIME      NOT NULL DEFAULT (GETDATE()),
        [Activo]            BIT           NOT NULL DEFAULT (1),
        [Observaciones]     NVARCHAR(200) NULL
    );
END
GO

-- ============================================================
-- Procedimiento: Insertar cliente
-- ============================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_Cliente_Insertar]
    @Nombre            NVARCHAR(50),
    @Apellido          NVARCHAR(50),
    @Cedula            NVARCHAR(20),
    @Email             NVARCHAR(100),
    @Telefono          NVARCHAR(20)  = NULL,
    @Direccion         NVARCHAR(150) = NULL,
    @Ciudad            NVARCHAR(60)  = NULL,
    @Pais              NVARCHAR(60)  = NULL,
    @FechaNacimiento   DATE          = NULL,
    @Activo            BIT           = 1,
    @Observaciones     NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Clientes]
        (Nombre, Apellido, Cedula, Email, Telefono, Direccion, Ciudad, Pais, FechaNacimiento, FechaRegistro, Activo, Observaciones)
    VALUES
        (@Nombre, @Apellido, @Cedula, @Email, @Telefono, @Direccion, @Ciudad, @Pais, @FechaNacimiento, GETDATE(), @Activo, @Observaciones);

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NuevoId;
END
GO

-- ============================================================
-- Procedimiento: Actualizar cliente
-- ============================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_Cliente_Actualizar]
    @Id                INT,
    @Nombre            NVARCHAR(50),
    @Apellido          NVARCHAR(50),
    @Cedula            NVARCHAR(20),
    @Email             NVARCHAR(100),
    @Telefono          NVARCHAR(20)  = NULL,
    @Direccion         NVARCHAR(150) = NULL,
    @Ciudad            NVARCHAR(60)  = NULL,
    @Pais              NVARCHAR(60)  = NULL,
    @FechaNacimiento   DATE          = NULL,
    @Activo            BIT           = 1,
    @Observaciones     NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Clientes]
    SET Nombre          = @Nombre,
        Apellido        = @Apellido,
        Cedula          = @Cedula,
        Email           = @Email,
        Telefono        = @Telefono,
        Direccion       = @Direccion,
        Ciudad          = @Ciudad,
        Pais            = @Pais,
        FechaNacimiento = @FechaNacimiento,
        Activo          = @Activo,
        Observaciones   = @Observaciones
    WHERE Id = @Id;
END
GO

-- ============================================================
-- Procedimiento: Eliminar cliente
-- ============================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_Cliente_Eliminar]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Clientes] WHERE Id = @Id;
END
GO

-- ============================================================
-- Procedimiento: Obtener cliente por Id
-- ============================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_Cliente_ObtenerPorId]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM [dbo].[Clientes] WHERE Id = @Id;
END
GO

-- ============================================================
-- Procedimiento: Listar todos los clientes
-- ============================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_Cliente_Listar]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM [dbo].[Clientes] ORDER BY Id DESC;
END
GO
