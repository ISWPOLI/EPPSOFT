
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/23/2015 01:12:53
-- Generated from EDMX file: E:\Kalfonso\Mis documentos\Visual Studio 2012\Projects\HitosReportes 2015-08-15\AccesoDatos\KonfirmaHitosModelo.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EPPSOFTRecolector];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Konfirma_HitosModelStoreContainer].[FK__Proyectos__Const__17036CC0]', 'F') IS NOT NULL
    ALTER TABLE [Konfirma_HitosModelStoreContainer].[Proyectos] DROP CONSTRAINT [FK__Proyectos__Const__17036CC0];
GO
IF OBJECT_ID(N'[dbo].[FK_Actividad_Proceso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Actividad] DROP CONSTRAINT [FK_Actividad_Proceso];
GO
IF OBJECT_ID(N'[dbo].[FK_Adjunto_RegistroActividad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adjunto] DROP CONSTRAINT [FK_Adjunto_RegistroActividad];
GO
IF OBJECT_ID(N'[dbo].[FK_Caso_Ciudad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Caso] DROP CONSTRAINT [FK_Caso_Ciudad];
GO
IF OBJECT_ID(N'[dbo].[FK_Caso_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Caso] DROP CONSTRAINT [FK_Caso_Cliente];
GO
IF OBJECT_ID(N'[dbo].[FK_Caso_Proceso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Caso] DROP CONSTRAINT [FK_Caso_Proceso];
GO
IF OBJECT_ID(N'[dbo].[FK_Ciudad_Departamento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ciudad] DROP CONSTRAINT [FK_Ciudad_Departamento];
GO
IF OBJECT_ID(N'[dbo].[FK_Departamento_Zona]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Departamento] DROP CONSTRAINT [FK_Departamento_Zona];
GO
IF OBJECT_ID(N'[dbo].[FK_RegistroActividad_Actividad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RegistroActividad] DROP CONSTRAINT [FK_RegistroActividad_Actividad];
GO
IF OBJECT_ID(N'[dbo].[FK_RegistroActividad_Caso]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RegistroActividad] DROP CONSTRAINT [FK_RegistroActividad_Caso];
GO
IF OBJECT_ID(N'[dbo].[FK_Usuario_Perfil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_Perfil];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Actividad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Actividad];
GO
IF OBJECT_ID(N'[dbo].[Adjunto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Adjunto];
GO
IF OBJECT_ID(N'[dbo].[Caso]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Caso];
GO
IF OBJECT_ID(N'[dbo].[Categoria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categoria];
GO
IF OBJECT_ID(N'[dbo].[Ciudad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ciudad];
GO
IF OBJECT_ID(N'[dbo].[Cliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cliente];
GO
IF OBJECT_ID(N'[dbo].[Constructoras]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Constructoras];
GO
IF OBJECT_ID(N'[dbo].[Departamento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departamento];
GO
IF OBJECT_ID(N'[dbo].[Perfil]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Perfil];
GO
IF OBJECT_ID(N'[dbo].[Proceso]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Proceso];
GO
IF OBJECT_ID(N'[dbo].[RegistroActividad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RegistroActividad];
GO
IF OBJECT_ID(N'[dbo].[Usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuario];
GO
IF OBJECT_ID(N'[dbo].[Zona]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Zona];
GO
IF OBJECT_ID(N'[Konfirma_HitosModelStoreContainer].[Proyectos]', 'U') IS NOT NULL
    DROP TABLE [Konfirma_HitosModelStoreContainer].[Proyectos];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Actividad'
CREATE TABLE [dbo].[Actividad] (
    [IdActividad] int IDENTITY(1,1) NOT NULL,
    [IdProceso] int  NOT NULL,
    [NombreActividad] nvarchar(90)  NOT NULL,
    [DuracionActividad] int  NULL,
    [EsHito] bit  NOT NULL
);
GO

-- Creating table 'Caso'
CREATE TABLE [dbo].[Caso] (
    [IdCaso] int IDENTITY(1,1) NOT NULL,
    [IdProceso] int  NOT NULL,
    [IdCliente] int  NOT NULL,
    [NoDeCaso] nvarchar(90)  NOT NULL,
    [NombreRadicador] nvarchar(90)  NOT NULL,
    [FechaInicioProceso] datetime  NOT NULL,
    [FechaOferta] datetime  NULL,
    [DestinacionPrestamo] nvarchar(90)  NULL,
    [CiudadOficinaUtilizacion] nvarchar(90)  NULL,
    [IdCiudadUtilizacion] int  NOT NULL,
    [MontoAprobado] nvarchar(90)  NULL,
    [PesosUVR] nvarchar(90)  NULL,
    [PresentaSubsidio] nvarchar(90)  NULL,
    [FolioMatricula] nvarchar(90)  NULL,
    [ObsercaionAprobarCreditoEInformar] nvarchar(500)  NULL,
    [NumeroDeTramite] nvarchar(90)  NULL
);
GO

-- Creating table 'Ciudad'
CREATE TABLE [dbo].[Ciudad] (
    [Id_ciudad] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(90)  NULL,
    [Codigo] int  NULL,
    [Coordenadas] varchar(70)  NULL,
    [IdDepartamento] int  NULL
);
GO

-- Creating table 'Cliente'
CREATE TABLE [dbo].[Cliente] (
    [IdCliente] int IDENTITY(1,1) NOT NULL,
    [NombreCliente] nvarchar(90)  NOT NULL,
    [TipoIdentificacion] nvarchar(90)  NOT NULL,
    [NumeroIdentificacion] nvarchar(90)  NOT NULL,
    [IdCiudad] int  NOT NULL,
    [FechaNacimiento] datetime  NOT NULL,
    [Edad] int  NOT NULL,
    [TelefonoFijo] nvarchar(90)  NULL,
    [TelefonoCelular] nvarchar(90)  NULL,
    [TelefonoFijo2] nvarchar(90)  NULL,
    [TelefonoCelular2] nvarchar(90)  NULL,
    [Direccion] nvarchar(90)  NULL,
    [CorreoElectronico] nvarchar(90)  NULL,
    [SueldoBasico] nvarchar(90)  NULL,
    [NombreCliente2] nvarchar(90)  NULL,
    [NumeroIdentificacionCliente2] nvarchar(90)  NULL
);
GO

-- Creating table 'Departamento'
CREATE TABLE [dbo].[Departamento] (
    [IdDepartamento] int IDENTITY(1,1) NOT NULL,
    [Codigo] int  NULL,
    [Nombre] varchar(200)  NOT NULL,
    [IdZona] tinyint  NULL,
    [Coordenadas] varchar(70)  NULL
);
GO

-- Creating table 'Proceso'
CREATE TABLE [dbo].[Proceso] (
    [IdProceso] int IDENTITY(1,1) NOT NULL,
    [NombreProceso] nvarchar(90)  NOT NULL
);
GO

-- Creating table 'RegistroActividad'
CREATE TABLE [dbo].[RegistroActividad] (
    [IdRegistroActividad] int IDENTITY(1,1) NOT NULL,
    [IdActividad] int  NOT NULL,
    [IdCaso] int  NOT NULL,
    [EstadoActividad] nvarchar(90)  NULL,
    [FechaVencimientoActividad] datetime  NULL,
    [usuarioResponsable] nvarchar(90)  NULL,
    [FechaCreacionActividad] datetime  NULL,
    [FechaInicioActividad] datetime  NULL,
    [FechaFinalizacionActividad] datetime  NULL,
    [TiempoDeEjecucion] bigint  NULL,
    [TiempoDeEspera] bigint  NULL,
    [DuracionTotal] bigint  NULL,
    [HitoFinalizado] bit  NULL
);
GO

-- Creating table 'Zona'
CREATE TABLE [dbo].[Zona] (
    [IdZona] tinyint IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(50)  NOT NULL
);
GO

-- Creating table 'Usuario'
CREATE TABLE [dbo].[Usuario] (
    [IdUsuario] int IDENTITY(1,1) NOT NULL,
    [NombreUsuario] varchar(20)  NOT NULL,
    [Contrasenia] varchar(100)  NOT NULL,
    [Habilitado] bit  NOT NULL,
    [IdPerfil] int  NOT NULL,
    [ContraseniaAsignada] bit  NOT NULL,
    [CorreoRegistro] varchar(200)  NULL,
    [NombreCompleto] varchar(200)  NULL
);
GO

-- Creating table 'Categoria'
CREATE TABLE [dbo].[Categoria] (
    [IdCategoria] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(90)  NOT NULL,
    [Color] varchar(50)  NOT NULL,
    [ValorMinimo] int  NOT NULL,
    [ValorMaximo] int  NOT NULL,
    [Activo] bit  NOT NULL
);
GO

-- Creating table 'Adjunto'
CREATE TABLE [dbo].[Adjunto] (
    [IdAdjunto] int IDENTITY(1,1) NOT NULL,
    [IdRegistroActividad] int  NOT NULL,
    [UrlAdjunto] nvarchar(250)  NULL,
    [TipoAdjunto] nvarchar(90)  NULL
);
GO

-- Creating table 'Perfil'
CREATE TABLE [dbo].[Perfil] (
    [IdPerfil] int IDENTITY(1,1) NOT NULL,
    [NombrePerfil] varchar(80)  NOT NULL
);
GO

-- Creating table 'Constructoras'
CREATE TABLE [dbo].[Constructoras] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [NIT] bigint  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [Correo_Electronico] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Proyectos'
CREATE TABLE [dbo].[Proyectos] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [Constructora] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdActividad] in table 'Actividad'
ALTER TABLE [dbo].[Actividad]
ADD CONSTRAINT [PK_Actividad]
    PRIMARY KEY CLUSTERED ([IdActividad] ASC);
GO

-- Creating primary key on [IdCaso] in table 'Caso'
ALTER TABLE [dbo].[Caso]
ADD CONSTRAINT [PK_Caso]
    PRIMARY KEY CLUSTERED ([IdCaso] ASC);
GO

-- Creating primary key on [Id_ciudad] in table 'Ciudad'
ALTER TABLE [dbo].[Ciudad]
ADD CONSTRAINT [PK_Ciudad]
    PRIMARY KEY CLUSTERED ([Id_ciudad] ASC);
GO

-- Creating primary key on [IdCliente] in table 'Cliente'
ALTER TABLE [dbo].[Cliente]
ADD CONSTRAINT [PK_Cliente]
    PRIMARY KEY CLUSTERED ([IdCliente] ASC);
GO

-- Creating primary key on [IdDepartamento] in table 'Departamento'
ALTER TABLE [dbo].[Departamento]
ADD CONSTRAINT [PK_Departamento]
    PRIMARY KEY CLUSTERED ([IdDepartamento] ASC);
GO

-- Creating primary key on [IdProceso] in table 'Proceso'
ALTER TABLE [dbo].[Proceso]
ADD CONSTRAINT [PK_Proceso]
    PRIMARY KEY CLUSTERED ([IdProceso] ASC);
GO

-- Creating primary key on [IdRegistroActividad] in table 'RegistroActividad'
ALTER TABLE [dbo].[RegistroActividad]
ADD CONSTRAINT [PK_RegistroActividad]
    PRIMARY KEY CLUSTERED ([IdRegistroActividad] ASC);
GO

-- Creating primary key on [IdZona] in table 'Zona'
ALTER TABLE [dbo].[Zona]
ADD CONSTRAINT [PK_Zona]
    PRIMARY KEY CLUSTERED ([IdZona] ASC);
GO

-- Creating primary key on [IdUsuario] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [PK_Usuario]
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC);
GO

-- Creating primary key on [IdCategoria] in table 'Categoria'
ALTER TABLE [dbo].[Categoria]
ADD CONSTRAINT [PK_Categoria]
    PRIMARY KEY CLUSTERED ([IdCategoria] ASC);
GO

-- Creating primary key on [IdAdjunto] in table 'Adjunto'
ALTER TABLE [dbo].[Adjunto]
ADD CONSTRAINT [PK_Adjunto]
    PRIMARY KEY CLUSTERED ([IdAdjunto] ASC);
GO

-- Creating primary key on [IdPerfil] in table 'Perfil'
ALTER TABLE [dbo].[Perfil]
ADD CONSTRAINT [PK_Perfil]
    PRIMARY KEY CLUSTERED ([IdPerfil] ASC);
GO

-- Creating primary key on [NIT] in table 'Constructoras'
ALTER TABLE [dbo].[Constructoras]
ADD CONSTRAINT [PK_Constructoras]
    PRIMARY KEY CLUSTERED ([NIT] ASC);
GO

-- Creating primary key on [Id], [Nombre], [Constructora] in table 'Proyectos'
ALTER TABLE [dbo].[Proyectos]
ADD CONSTRAINT [PK_Proyectos]
    PRIMARY KEY CLUSTERED ([Id], [Nombre], [Constructora] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdProceso] in table 'Actividad'
ALTER TABLE [dbo].[Actividad]
ADD CONSTRAINT [FK_Actividad_Proceso]
    FOREIGN KEY ([IdProceso])
    REFERENCES [dbo].[Proceso]
        ([IdProceso])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Actividad_Proceso'
CREATE INDEX [IX_FK_Actividad_Proceso]
ON [dbo].[Actividad]
    ([IdProceso]);
GO

-- Creating foreign key on [IdActividad] in table 'RegistroActividad'
ALTER TABLE [dbo].[RegistroActividad]
ADD CONSTRAINT [FK_RegistroActividad_Actividad]
    FOREIGN KEY ([IdActividad])
    REFERENCES [dbo].[Actividad]
        ([IdActividad])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RegistroActividad_Actividad'
CREATE INDEX [IX_FK_RegistroActividad_Actividad]
ON [dbo].[RegistroActividad]
    ([IdActividad]);
GO

-- Creating foreign key on [IdCiudadUtilizacion] in table 'Caso'
ALTER TABLE [dbo].[Caso]
ADD CONSTRAINT [FK_Caso_Ciudad]
    FOREIGN KEY ([IdCiudadUtilizacion])
    REFERENCES [dbo].[Ciudad]
        ([Id_ciudad])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Caso_Ciudad'
CREATE INDEX [IX_FK_Caso_Ciudad]
ON [dbo].[Caso]
    ([IdCiudadUtilizacion]);
GO

-- Creating foreign key on [IdCliente] in table 'Caso'
ALTER TABLE [dbo].[Caso]
ADD CONSTRAINT [FK_Caso_Cliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Caso_Cliente'
CREATE INDEX [IX_FK_Caso_Cliente]
ON [dbo].[Caso]
    ([IdCliente]);
GO

-- Creating foreign key on [IdProceso] in table 'Caso'
ALTER TABLE [dbo].[Caso]
ADD CONSTRAINT [FK_Caso_Proceso]
    FOREIGN KEY ([IdProceso])
    REFERENCES [dbo].[Proceso]
        ([IdProceso])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Caso_Proceso'
CREATE INDEX [IX_FK_Caso_Proceso]
ON [dbo].[Caso]
    ([IdProceso]);
GO

-- Creating foreign key on [IdCaso] in table 'RegistroActividad'
ALTER TABLE [dbo].[RegistroActividad]
ADD CONSTRAINT [FK_RegistroActividad_Caso]
    FOREIGN KEY ([IdCaso])
    REFERENCES [dbo].[Caso]
        ([IdCaso])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RegistroActividad_Caso'
CREATE INDEX [IX_FK_RegistroActividad_Caso]
ON [dbo].[RegistroActividad]
    ([IdCaso]);
GO

-- Creating foreign key on [IdDepartamento] in table 'Ciudad'
ALTER TABLE [dbo].[Ciudad]
ADD CONSTRAINT [FK_Ciudad_Departamento]
    FOREIGN KEY ([IdDepartamento])
    REFERENCES [dbo].[Departamento]
        ([IdDepartamento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Ciudad_Departamento'
CREATE INDEX [IX_FK_Ciudad_Departamento]
ON [dbo].[Ciudad]
    ([IdDepartamento]);
GO

-- Creating foreign key on [IdZona] in table 'Departamento'
ALTER TABLE [dbo].[Departamento]
ADD CONSTRAINT [FK_Departamento_Zona]
    FOREIGN KEY ([IdZona])
    REFERENCES [dbo].[Zona]
        ([IdZona])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Departamento_Zona'
CREATE INDEX [IX_FK_Departamento_Zona]
ON [dbo].[Departamento]
    ([IdZona]);
GO

-- Creating foreign key on [IdRegistroActividad] in table 'Adjunto'
ALTER TABLE [dbo].[Adjunto]
ADD CONSTRAINT [FK_Adjunto_RegistroActividad]
    FOREIGN KEY ([IdRegistroActividad])
    REFERENCES [dbo].[RegistroActividad]
        ([IdRegistroActividad])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Adjunto_RegistroActividad'
CREATE INDEX [IX_FK_Adjunto_RegistroActividad]
ON [dbo].[Adjunto]
    ([IdRegistroActividad]);
GO

-- Creating foreign key on [IdPerfil] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuario_Perfil]
    FOREIGN KEY ([IdPerfil])
    REFERENCES [dbo].[Perfil]
        ([IdPerfil])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuario_Perfil'
CREATE INDEX [IX_FK_Usuario_Perfil]
ON [dbo].[Usuario]
    ([IdPerfil]);
GO

-- Creating foreign key on [Constructora] in table 'Proyectos'
ALTER TABLE [dbo].[Proyectos]
ADD CONSTRAINT [FK__Proyectos__Const__17036CC0]
    FOREIGN KEY ([Constructora])
    REFERENCES [dbo].[Constructoras]
        ([NIT])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Proyectos__Const__17036CC0'
CREATE INDEX [IX_FK__Proyectos__Const__17036CC0]
ON [dbo].[Proyectos]
    ([Constructora]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------