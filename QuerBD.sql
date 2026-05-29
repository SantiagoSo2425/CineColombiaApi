-- ============================================================
-- CineColombia - Script Mini BD para módulo Nueva Venta
-- SQL Server 2012+
-- ============================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'CineColombia')
    DROP DATABASE CineColombia;
GO

CREATE DATABASE CineColombia;
GO

USE CineColombia;
GO

-- ─── CATÁLOGOS BASE ──────────────────────────────────────────

CREATE TABLE [CIUDAD] (
  [id_ciudad] int PRIMARY KEY NOT NULL,
  [id_departamento] int NOT NULL,
  [nombre] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [GENERO] (
  [id_genero] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [TIPO_CLIENTE] (
  [id_tipo_cliente] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL,
  [descripcion] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [TIPO_DOCUMENTO] (
  [id_tipo_doc] int PRIMARY KEY NOT NULL,
  [codigo] nvarchar(255) NOT NULL,
  [descripcion] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [IDIOMA] (
  [id_idioma] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL,
  [codigo] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [FORMATO] (
  [id_formato] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [ROL] (
  [id_rol] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL,
  [descripcion] nvarchar(255)
)
GO

CREATE TABLE [PROFESION] (
  [id_profesion] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [TEATRO] (
  [id_teatro] int PRIMARY KEY NOT NULL,
  [id_ciudad] int NOT NULL,
  [nombre] nvarchar(255) NOT NULL,
  [direccion] nvarchar(255) NOT NULL,
  [telefono] nvarchar(255),
  [email] nvarchar(255),
  [activo] bit NOT NULL,
  [registrado_por] int NULL,
  [fecha_registro] datetime NOT NULL
)
GO

CREATE TABLE [SILLA] (
  [id_silla] int PRIMARY KEY NOT NULL,
  [id_sala] int NOT NULL,
  [id_tipo_silla] int NOT NULL,
  [fila] nchar NOT NULL,
  [numero] int NOT NULL,
  [estado] int NOT NULL,
  [registrado_por] int NOT NULL,
  [fecha_registro] datetime NOT NULL
)
GO

CREATE TABLE [PELICULA] (
  [id_pelicula] int PRIMARY KEY NOT NULL,
  [id_genero] int NOT NULL,
  [id_clasificacion] int NOT NULL,
  [titulo_original] nvarchar(255) NOT NULL,
  [nombre_oferta] nvarchar(255) NOT NULL,
  [resumen] nvarchar(255) NOT NULL,
  [anio_estreno] date NOT NULL,
  [trailer_link] nvarchar(255),
  [duracion_min] int NOT NULL,
  [registrado_por] int NOT NULL,
  [fecha_registro] datetime NOT NULL
)
GO

CREATE TABLE [PELICULA_IDIOMA] (
  [id_pelicula_idioma] int PRIMARY KEY NOT NULL,
  [id_pelicula] int NOT NULL,
  [id_idioma] int NOT NULL,
  [es_original] bit NOT NULL
)
GO

CREATE TABLE [PELICULA_FORMATO] (
  [id_pelicula_formato] int PRIMARY KEY NOT NULL,
  [id_pelicula] int NOT NULL,
  [id_formato] int NOT NULL
)
GO

CREATE TABLE [PELICULA_PRODUCTORA] (
  [id_pelicula_productora] int PRIMARY KEY NOT NULL,
  [id_pelicula] int NOT NULL,
  [id_productora] int NOT NULL
)
GO

CREATE TABLE [PELICULA_DISTRIBUIDORA] (
  [id_pelicula_distribuidora] int PRIMARY KEY NOT NULL,
  [id_pelicula] int NOT NULL,
  [id_distribuidora] int NOT NULL
)
GO

CREATE TABLE [FUNCION] (
  [id_funcion] int PRIMARY KEY NOT NULL,
  [id_sala] int NOT NULL,
  [id_pelicula] int NOT NULL,
  [id_idioma] int NOT NULL,
  [id_formato] int NOT NULL,
  [fecha_funcion] date NOT NULL,
  [hora_inicio] datetime NOT NULL,
  [hora_fin] datetime NOT NULL,
  [precio_base] decimal NOT NULL,
  [estado] bit NOT NULL,
  [registrado_por] int NOT NULL,
  [fecha_registro] datetime NOT NULL
)
GO

CREATE TABLE [CLIENTE] (
  [id_cliente] int PRIMARY KEY NOT NULL,
  [id_tipo_cliente] int NOT NULL,
  [id_tipo_doc] int NOT NULL,
  [num_documento] nvarchar(255) NOT NULL,
  [nombres] nvarchar(255) NOT NULL,
  [apellidos] nvarchar(255) NOT NULL,
  [email] nvarchar(255),
  [activo] bit NOT NULL,
  [registrado_por] int NOT NULL,
  [fecha_registro] datetime NOT NULL
)
GO

CREATE TABLE [DIRECCION_CLIENTE] (
  [id_direccion_cli] int PRIMARY KEY NOT NULL,
  [id_cliente] int NOT NULL,
  [id_ciudad] int NOT NULL,
  [direccion] nvarchar(255) NOT NULL,
  [activo] bit NOT NULL
)
GO

CREATE TABLE [TELEFONO_CLIENTE] (
  [id_telefono] int PRIMARY KEY NOT NULL,
  [id_cliente] int NOT NULL,
  [id_tipo_telefono] int NOT NULL,
  [numero] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [TARJETA_FIDELIZACION] (
  [id_tarjeta] int PRIMARY KEY NOT NULL,
  [id_cliente] int NOT NULL,
  [numero_tarjeta] nvarchar(255) NOT NULL,
  [fecha_emision] date NOT NULL,
  [fecha_vencimiento] date NOT NULL,
  [puntos_acumulados] decimal NOT NULL,
  [descuento_porcentaje] decimal NOT NULL,
  [estado] bit NOT NULL,
  [registrado_por] int NOT NULL,
  [fecha_registro] datetime NOT NULL
)
GO

CREATE TABLE [EMPLEADO] (
  [id_empleado] int PRIMARY KEY NOT NULL,
  [codigo_empleado] nvarchar(255) UNIQUE NOT NULL,
  [id_teatro] int NOT NULL,
  [id_tipo_doc] int NOT NULL,
  [num_documento] nvarchar(255) NOT NULL,
  [nombres] nvarchar(255) NOT NULL,
  [apellidos] nvarchar(255) NOT NULL,
  [fecha_ingreso] date NOT NULL,
  [activo] bit NOT NULL,
  [registrado_por] int,
  [fecha_registro] datetime NOT NULL
)
GO

CREATE TABLE [TELEFONO_EMPLEADO] (
  [id_telefono_emp] int PRIMARY KEY NOT NULL,
  [id_empleado] int NOT NULL,
  [id_tipo_telefono] int NOT NULL,
  [numero] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [TIPO_TELEFONO] (
  [id_tipo_telefono] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [DIRECCION_EMPLEADO] (
  [id_direccion_emp] int PRIMARY KEY NOT NULL,
  [id_empleado] int NOT NULL,
  [id_ciudad] int NOT NULL,
  [direccion] nvarchar(255) NOT NULL,
  [activo] bit NOT NULL
)
GO

CREATE TABLE [EMPLEADO_PROFESION] (
  [id_emp_profesion] int PRIMARY KEY NOT NULL,
  [id_empleado] int NOT NULL,
  [id_profesion] int NOT NULL
)
GO

CREATE TABLE [USUARIO_SISTEMA] (
  [id_usuario] int PRIMARY KEY NOT NULL,
  [id_empleado] int NOT NULL,
  [id_rol] int NOT NULL,
  [username] nvarchar(255) NOT NULL,
  [password_hash] nvarchar(255) NOT NULL,
  [activo] bit NOT NULL,
  [ultimo_login] datetime,
  [registrado_por] int NOT NULL,
  [fecha_registro] datetime NOT NULL
)
GO

CREATE TABLE [VENTA] (
  [id_venta] int PRIMARY KEY NOT NULL,
  [id_cliente] int,
  [id_empleado] int NOT NULL,
  [id_metodo_pago] int NOT NULL,
  [fecha_hora] datetime NOT NULL,
  [subtotal] decimal NOT NULL,
  [total_descuento] decimal NOT NULL,
  [total_venta] decimal NOT NULL,
  [estado] bit NOT NULL
)
GO

CREATE TABLE [BOLETICA] (
  [id_boletica] int PRIMARY KEY NOT NULL,
  [id_venta] int NOT NULL,
  [id_funcion] int NOT NULL,
  [estado] int NOT NULL
)
GO

CREATE TABLE [PRODUCTORA] (
  [id_productora] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL,
  [id_pais] int
)
GO

CREATE TABLE [DISTRIBUIDORA] (
  [id_distribuidora] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL,
  [id_pais] int
)
GO

CREATE TABLE [TIPO_SALA] (
  [id_tipo_sala] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [TIPO_SILLA] (
  [id_tipo_silla] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL,
  [precio_base] decimal NOT NULL
)
GO

CREATE TABLE [CLASIFICACION] (
  [id_clasificacion] int PRIMARY KEY NOT NULL,
  [codigo] nvarchar(255) NOT NULL,
  [descripcion] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [METODO_PAGO] (
  [id_metodo_pago] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [SALA] (
  [id_sala] int PRIMARY KEY NOT NULL,
  [id_teatro] int NOT NULL,
  [id_tipo_sala] int NOT NULL,
  [nombre_sala] nvarchar(255) NOT NULL,
  [capacidad_total] int NOT NULL,
  [activo] bit NOT NULL,
  [registrado_por] int NOT NULL,
  [fecha_registro] datetime NOT NULL
)
GO

CREATE TABLE [PAIS] (
  [id_pais] int PRIMARY KEY NOT NULL,
  [nombre] nvarchar(255) NOT NULL,
  [codigo] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [DEPARTAMENTO] (
  [id_departamento] int PRIMARY KEY NOT NULL,
  [id_pais] int NOT NULL,
  [nombre] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [BOLETICA_SILLA] (
  [id_boletica_silla] int PRIMARY KEY NOT NULL,
  [id_boletica] int NOT NULL,
  [id_silla] int NOT NULL,
  [precio_unitario] decimal NOT NULL,
  [descuento] decimal NOT NULL DEFAULT (0),
  [precio_final] decimal NOT NULL,
  [estado] int NOT NULL
)
GO

CREATE UNIQUE INDEX [SILLA_index_0] ON [SILLA] ("id_sala", "fila", "numero")
GO

CREATE UNIQUE INDEX [PELICULA_IDIOMA_index_1] ON [PELICULA_IDIOMA] ("id_pelicula", "id_idioma")
GO

CREATE UNIQUE INDEX [PELICULA_FORMATO_index_2] ON [PELICULA_FORMATO] ("id_pelicula", "id_formato")
GO

CREATE UNIQUE INDEX [PELICULA_PRODUCTORA_index_3] ON [PELICULA_PRODUCTORA] ("id_pelicula", "id_productora")
GO

CREATE UNIQUE INDEX [PELICULA_DISTRIBUIDORA_index_4] ON [PELICULA_DISTRIBUIDORA] ("id_pelicula", "id_distribuidora")
GO

CREATE UNIQUE INDEX [FUNCION_index_5] ON [FUNCION] ("id_sala", "fecha_funcion", "hora_inicio")
GO

CREATE UNIQUE INDEX [CLIENTE_index_6] ON [CLIENTE] ("id_tipo_doc", "num_documento")
GO

CREATE UNIQUE INDEX [TARJETA_FIDELIZACION_index_7] ON [TARJETA_FIDELIZACION] ("id_cliente")
GO

CREATE UNIQUE INDEX [EMPLEADO_index_8] ON [EMPLEADO] ("id_tipo_doc", "num_documento")
GO

CREATE UNIQUE INDEX [EMPLEADO_PROFESION_index_9] ON [EMPLEADO_PROFESION] ("id_empleado", "id_profesion")
GO

CREATE UNIQUE INDEX [USUARIO_SISTEMA_index_10] ON [USUARIO_SISTEMA] ("id_empleado")
GO

CREATE UNIQUE INDEX [USUARIO_SISTEMA_index_11] ON [USUARIO_SISTEMA] ("username")
GO

CREATE UNIQUE INDEX [BOLETICA_SILLA_index_12] ON [BOLETICA_SILLA] ("id_boletica", "id_silla")
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'CIUDAD',
@level2type = N'Column', @level2name = 'id_ciudad';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'GENERO',
@level2type = N'Column', @level2name = 'id_genero';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TIPO_CLIENTE',
@level2type = N'Column', @level2name = 'id_tipo_cliente';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TIPO_DOCUMENTO',
@level2type = N'Column', @level2name = 'id_tipo_doc';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'CC, CE, TI, PEP, PA',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TIPO_DOCUMENTO',
@level2type = N'Column', @level2name = 'codigo';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'IDIOMA',
@level2type = N'Column', @level2name = 'id_idioma';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'es, en, fr, de...',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'IDIOMA',
@level2type = N'Column', @level2name = 'codigo';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'FORMATO',
@level2type = N'Column', @level2name = 'id_formato';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = '2D, 3D, IMAX, 4DX',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'FORMATO',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'ROL',
@level2type = N'Column', @level2name = 'id_rol';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Administrador, Cajero, Supervisor',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'ROL',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PROFESION',
@level2type = N'Column', @level2name = 'id_profesion';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TEATRO',
@level2type = N'Column', @level2name = 'id_teatro';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Empleado que registró el teatro',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TEATRO',
@level2type = N'Column', @level2name = 'registrado_por';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'SILLA',
@level2type = N'Column', @level2name = 'id_silla';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PELICULA',
@level2type = N'Column', @level2name = 'id_pelicula';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'URL del tráiler — generalmente en idioma original',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PELICULA',
@level2type = N'Column', @level2name = 'trailer_link';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PELICULA_IDIOMA',
@level2type = N'Column', @level2name = 'id_pelicula_idioma';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = '1 = idioma original de la película',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PELICULA_IDIOMA',
@level2type = N'Column', @level2name = 'es_original';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PELICULA_FORMATO',
@level2type = N'Column', @level2name = 'id_pelicula_formato';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PELICULA_PRODUCTORA',
@level2type = N'Column', @level2name = 'id_pelicula_productora';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PELICULA_DISTRIBUIDORA',
@level2type = N'Column', @level2name = 'id_pelicula_distribuidora';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'FUNCION',
@level2type = N'Column', @level2name = 'id_funcion';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Idioma en que se proyecta esta función',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'FUNCION',
@level2type = N'Column', @level2name = 'id_idioma';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Formato de esta función',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'FUNCION',
@level2type = N'Column', @level2name = 'id_formato';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Empleado que programó la función',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'FUNCION',
@level2type = N'Column', @level2name = 'registrado_por';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'CLIENTE',
@level2type = N'Column', @level2name = 'id_cliente';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'DIRECCION_CLIENTE',
@level2type = N'Column', @level2name = 'id_direccion_cli';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'El profesor lo pidió explícitamente',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'DIRECCION_CLIENTE',
@level2type = N'Column', @level2name = 'activo';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TELEFONO_CLIENTE',
@level2type = N'Column', @level2name = 'id_telefono';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TARJETA_FIDELIZACION',
@level2type = N'Column', @level2name = 'id_tarjeta';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'UK — un cliente, una tarjeta',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TARJETA_FIDELIZACION',
@level2type = N'Column', @level2name = 'id_cliente';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = '% de descuento aplicado en BOLETICA',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TARJETA_FIDELIZACION',
@level2type = N'Column', @level2name = 'descuento_porcentaje';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Empleado que emitió la tarjeta',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TARJETA_FIDELIZACION',
@level2type = N'Column', @level2name = 'registrado_por';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'EMPLEADO',
@level2type = N'Column', @level2name = 'id_empleado';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Código interno asignado por la empresa',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'EMPLEADO',
@level2type = N'Column', @level2name = 'codigo_empleado';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'NULL solo para el primer admin del sistema',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'EMPLEADO',
@level2type = N'Column', @level2name = 'registrado_por';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TELEFONO_EMPLEADO',
@level2type = N'Column', @level2name = 'id_telefono_emp';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TIPO_TELEFONO',
@level2type = N'Column', @level2name = 'id_tipo_telefono';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Celular, Fijo, WhatsApp, Satelital',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TIPO_TELEFONO',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'DIRECCION_EMPLEADO',
@level2type = N'Column', @level2name = 'id_direccion_emp';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'EMPLEADO_PROFESION',
@level2type = N'Column', @level2name = 'id_emp_profesion';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'USUARIO_SISTEMA',
@level2type = N'Column', @level2name = 'id_usuario';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Empleado que creó el usuario',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'USUARIO_SISTEMA',
@level2type = N'Column', @level2name = 'registrado_por';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'VENTA',
@level2type = N'Column', @level2name = 'id_venta';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'NULL permitido — cliente casual',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'VENTA',
@level2type = N'Column', @level2name = 'id_cliente';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'BOLETICA',
@level2type = N'Column', @level2name = 'id_boletica';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PRODUCTORA',
@level2type = N'Column', @level2name = 'id_productora';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'DISTRIBUIDORA',
@level2type = N'Column', @level2name = 'id_distribuidora';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TIPO_SALA',
@level2type = N'Column', @level2name = 'id_tipo_sala';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'General, VIP, IMAX, 4DX, Premiere',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TIPO_SALA',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Precio base de este tipo de silla',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'TIPO_SILLA',
@level2type = N'Column', @level2name = 'precio_base';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'CLASIFICACION',
@level2type = N'Column', @level2name = 'id_clasificacion';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'G, PG, PG-13, R, +18',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'CLASIFICACION',
@level2type = N'Column', @level2name = 'codigo';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Todo público, Mayores de 15, etc.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'CLASIFICACION',
@level2type = N'Column', @level2name = 'descripcion';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'METODO_PAGO',
@level2type = N'Column', @level2name = 'id_metodo_pago';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Efectivo, Tarjeta Débito, Tarjeta Crédito, PSE, Nequi',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'METODO_PAGO',
@level2type = N'Column', @level2name = 'nombre';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'SALA',
@level2type = N'Column', @level2name = 'id_sala';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PAIS',
@level2type = N'Column', @level2name = 'id_pais';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'CO, US, MX...',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'PAIS',
@level2type = N'Column', @level2name = 'codigo';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'DEPARTAMENTO',
@level2type = N'Column', @level2name = 'id_departamento';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'PK',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'BOLETICA_SILLA',
@level2type = N'Column', @level2name = 'id_boletica_silla';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Precio base según función, día y tipo de silla',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'BOLETICA_SILLA',
@level2type = N'Column', @level2name = 'precio_unitario';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Descuento por tarjeta de fidelización',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'BOLETICA_SILLA',
@level2type = N'Column', @level2name = 'descuento';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'precio_unitario - descuento',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'BOLETICA_SILLA',
@level2type = N'Column', @level2name = 'precio_final';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = '1=activa, 0=anulada',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'BOLETICA_SILLA',
@level2type = N'Column', @level2name = 'estado';
GO

ALTER TABLE [CIUDAD] ADD FOREIGN KEY ([id_departamento]) REFERENCES [DEPARTAMENTO] ([id_departamento])
GO

ALTER TABLE [TEATRO] ADD FOREIGN KEY ([id_ciudad]) REFERENCES [CIUDAD] ([id_ciudad])
GO

ALTER TABLE [TEATRO] ADD FOREIGN KEY ([registrado_por]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [SILLA] ADD FOREIGN KEY ([id_sala]) REFERENCES [SALA] ([id_sala])
GO

ALTER TABLE [SILLA] ADD FOREIGN KEY ([id_tipo_silla]) REFERENCES [TIPO_SILLA] ([id_tipo_silla])
GO

ALTER TABLE [SILLA] ADD FOREIGN KEY ([registrado_por]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [PELICULA] ADD FOREIGN KEY ([id_genero]) REFERENCES [GENERO] ([id_genero])
GO

ALTER TABLE [PELICULA] ADD FOREIGN KEY ([id_clasificacion]) REFERENCES [CLASIFICACION] ([id_clasificacion])
GO

ALTER TABLE [PELICULA] ADD FOREIGN KEY ([registrado_por]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [PELICULA_IDIOMA] ADD FOREIGN KEY ([id_pelicula]) REFERENCES [PELICULA] ([id_pelicula])
GO

ALTER TABLE [PELICULA_IDIOMA] ADD FOREIGN KEY ([id_idioma]) REFERENCES [IDIOMA] ([id_idioma])
GO

ALTER TABLE [PELICULA_FORMATO] ADD FOREIGN KEY ([id_pelicula]) REFERENCES [PELICULA] ([id_pelicula])
GO

ALTER TABLE [PELICULA_FORMATO] ADD FOREIGN KEY ([id_formato]) REFERENCES [FORMATO] ([id_formato])
GO

ALTER TABLE [PELICULA_PRODUCTORA] ADD FOREIGN KEY ([id_pelicula]) REFERENCES [PELICULA] ([id_pelicula])
GO

ALTER TABLE [PELICULA_PRODUCTORA] ADD FOREIGN KEY ([id_productora]) REFERENCES [PRODUCTORA] ([id_productora])
GO

ALTER TABLE [PELICULA_DISTRIBUIDORA] ADD FOREIGN KEY ([id_pelicula]) REFERENCES [PELICULA] ([id_pelicula])
GO

ALTER TABLE [PELICULA_DISTRIBUIDORA] ADD FOREIGN KEY ([id_distribuidora]) REFERENCES [DISTRIBUIDORA] ([id_distribuidora])
GO

ALTER TABLE [FUNCION] ADD FOREIGN KEY ([id_sala]) REFERENCES [SALA] ([id_sala])
GO

ALTER TABLE [FUNCION] ADD FOREIGN KEY ([id_pelicula]) REFERENCES [PELICULA] ([id_pelicula])
GO

ALTER TABLE [FUNCION] ADD FOREIGN KEY ([id_idioma]) REFERENCES [IDIOMA] ([id_idioma])
GO

ALTER TABLE [FUNCION] ADD FOREIGN KEY ([id_formato]) REFERENCES [FORMATO] ([id_formato])
GO

ALTER TABLE [FUNCION] ADD FOREIGN KEY ([registrado_por]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [CLIENTE] ADD FOREIGN KEY ([id_tipo_cliente]) REFERENCES [TIPO_CLIENTE] ([id_tipo_cliente])
GO

ALTER TABLE [CLIENTE] ADD FOREIGN KEY ([id_tipo_doc]) REFERENCES [TIPO_DOCUMENTO] ([id_tipo_doc])
GO

ALTER TABLE [CLIENTE] ADD FOREIGN KEY ([registrado_por]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [DIRECCION_CLIENTE] ADD FOREIGN KEY ([id_cliente]) REFERENCES [CLIENTE] ([id_cliente])
GO

ALTER TABLE [DIRECCION_CLIENTE] ADD FOREIGN KEY ([id_ciudad]) REFERENCES [CIUDAD] ([id_ciudad])
GO

ALTER TABLE [TELEFONO_CLIENTE] ADD FOREIGN KEY ([id_cliente]) REFERENCES [CLIENTE] ([id_cliente])
GO

ALTER TABLE [TELEFONO_CLIENTE] ADD FOREIGN KEY ([id_tipo_telefono]) REFERENCES [TIPO_TELEFONO] ([id_tipo_telefono])
GO

ALTER TABLE [TARJETA_FIDELIZACION] ADD FOREIGN KEY ([id_cliente]) REFERENCES [CLIENTE] ([id_cliente])
GO

ALTER TABLE [TARJETA_FIDELIZACION] ADD FOREIGN KEY ([registrado_por]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [EMPLEADO] ADD FOREIGN KEY ([id_teatro]) REFERENCES [TEATRO] ([id_teatro])
GO

ALTER TABLE [EMPLEADO] ADD FOREIGN KEY ([id_tipo_doc]) REFERENCES [TIPO_DOCUMENTO] ([id_tipo_doc])
GO

ALTER TABLE [EMPLEADO] ADD FOREIGN KEY ([registrado_por]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [TELEFONO_EMPLEADO] ADD FOREIGN KEY ([id_empleado]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [TELEFONO_EMPLEADO] ADD FOREIGN KEY ([id_tipo_telefono]) REFERENCES [TIPO_TELEFONO] ([id_tipo_telefono])
GO

ALTER TABLE [DIRECCION_EMPLEADO] ADD FOREIGN KEY ([id_empleado]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [DIRECCION_EMPLEADO] ADD FOREIGN KEY ([id_ciudad]) REFERENCES [CIUDAD] ([id_ciudad])
GO

ALTER TABLE [EMPLEADO_PROFESION] ADD FOREIGN KEY ([id_empleado]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [EMPLEADO_PROFESION] ADD FOREIGN KEY ([id_profesion]) REFERENCES [PROFESION] ([id_profesion])
GO

ALTER TABLE [USUARIO_SISTEMA] ADD FOREIGN KEY ([id_empleado]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [USUARIO_SISTEMA] ADD FOREIGN KEY ([id_rol]) REFERENCES [ROL] ([id_rol])
GO

ALTER TABLE [USUARIO_SISTEMA] ADD FOREIGN KEY ([registrado_por]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [VENTA] ADD FOREIGN KEY ([id_cliente]) REFERENCES [CLIENTE] ([id_cliente])
GO

ALTER TABLE [VENTA] ADD FOREIGN KEY ([id_empleado]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [VENTA] ADD FOREIGN KEY ([id_metodo_pago]) REFERENCES [METODO_PAGO] ([id_metodo_pago])
GO

ALTER TABLE [BOLETICA] ADD FOREIGN KEY ([id_venta]) REFERENCES [VENTA] ([id_venta])
GO

ALTER TABLE [BOLETICA] ADD FOREIGN KEY ([id_funcion]) REFERENCES [FUNCION] ([id_funcion])
GO

ALTER TABLE [PRODUCTORA] ADD FOREIGN KEY ([id_pais]) REFERENCES [PAIS] ([id_pais])
GO

ALTER TABLE [DISTRIBUIDORA] ADD FOREIGN KEY ([id_pais]) REFERENCES [PAIS] ([id_pais])
GO

ALTER TABLE [SALA] ADD FOREIGN KEY ([id_teatro]) REFERENCES [TEATRO] ([id_teatro])
GO

ALTER TABLE [SALA] ADD FOREIGN KEY ([id_tipo_sala]) REFERENCES [TIPO_SALA] ([id_tipo_sala])
GO

ALTER TABLE [SALA] ADD FOREIGN KEY ([registrado_por]) REFERENCES [EMPLEADO] ([id_empleado])
GO

ALTER TABLE [DEPARTAMENTO] ADD FOREIGN KEY ([id_pais]) REFERENCES [PAIS] ([id_pais])
GO

ALTER TABLE [BOLETICA_SILLA] ADD FOREIGN KEY ([id_boletica]) REFERENCES [BOLETICA] ([id_boletica])
GO

ALTER TABLE [BOLETICA_SILLA] ADD FOREIGN KEY ([id_silla]) REFERENCES [SILLA] ([id_silla])
GO


-- Datos ---

-- INSERTS BASE CINECOLOMBIA
SET NOCOUNT ON;
GO

INSERT INTO PAIS (id_pais, nombre, codigo) VALUES
(1, N'Colombia', N'CO'),
(2, N'Estados Unidos', N'US'),
(3, N'México', N'MX');
GO

INSERT INTO DEPARTAMENTO (id_departamento, id_pais, nombre) VALUES
(1, 1, N'Antioquia'),
(2, 1, N'Cundinamarca'),
(3, 1, N'Valle del Cauca');
GO

INSERT INTO CIUDAD (id_ciudad, id_departamento, nombre) VALUES
(1, 1, N'Medellín'),
(2, 2, N'Bogotá'),
(3, 3, N'Cali');
GO

INSERT INTO GENERO (id_genero, nombre) VALUES
(1, N'Acción'),
(2, N'Drama'),
(3, N'Comedia'),
(4, N'Terror'),
(5, N'Animación');
GO

INSERT INTO CLASIFICACION (id_clasificacion, codigo, descripcion) VALUES
(1, N'G', N'Todo público'),
(2, N'PG', N'Orientación parental'),
(3, N'PG-13', N'Mayores de 13'),
(4, N'R', N'Mayores de 17');
GO

INSERT INTO TIPO_CLIENTE (id_tipo_cliente, nombre, descripcion) VALUES
(1, N'VIP', N'Cliente VIP'),
(2, N'Registrado', N'Cliente registrado'),
(3, N'Casual', N'Cliente ocasional');
GO

INSERT INTO TIPO_DOCUMENTO (id_tipo_doc, codigo, descripcion) VALUES
(1, N'CC', N'Cédula de Ciudadanía'),
(2, N'CE', N'Cédula de Extranjería'),
(3, N'TI', N'Tarjeta de Identidad'),
(4, N'PA', N'Pasaporte');
GO

INSERT INTO IDIOMA (id_idioma, nombre, codigo) VALUES
(1, N'Español', N'ES'),
(2, N'Inglés', N'EN');
GO

INSERT INTO FORMATO (id_formato, nombre) VALUES
(1, N'2D'),
(2, N'3D'),
(3, N'IMAX'),
(4, N'4DX');
GO

INSERT INTO ROL (id_rol, nombre, descripcion) VALUES
(1, N'Administrador', N'Acceso total'),
(2, N'Cajero', N'Ventas'),
(3, N'Supervisor', N'Consulta y reportes');
GO

INSERT INTO PROFESION (id_profesion, nombre) VALUES
(1, N'Administrador'),
(2, N'Cajero'),
(3, N'Supervisor');
GO

INSERT INTO TIPO_TELEFONO (id_tipo_telefono, nombre) VALUES
(1, N'Celular'),
(2, N'Fijo'),
(3, N'WhatsApp');
GO

INSERT INTO TIPO_SALA (id_tipo_sala, nombre) VALUES
(1, N'General'),
(2, N'VIP'),
(3, N'IMAX');
GO

INSERT INTO TIPO_SILLA (id_tipo_silla, nombre, precio_base) VALUES
(1, N'General', 12000),
(2, N'Preferencial', 18000),
(3, N'VIP', 25000);
GO

INSERT INTO METODO_PAGO (id_metodo_pago, nombre) VALUES
(1, N'Efectivo'),
(2, N'Tarjeta Débito'),
(3, N'Tarjeta Crédito'),
(4, N'PSE'),
(5, N'Nequi');
GO

INSERT INTO PRODUCTORA (id_productora, nombre, id_pais) VALUES
(1, N'Warner Bros.', 2),
(2, N'Marvel Studios', 2),
(3, N'Pixar', 2);
GO

INSERT INTO DISTRIBUIDORA (id_distribuidora, nombre, id_pais) VALUES
(1, N'Warner Bros. Pictures', 2),
(2, N'Walt Disney Studios', 2),
(3, N'Universal Pictures', 2);
GO

IF NOT EXISTS (SELECT 1 FROM TEATRO WHERE id_teatro = 1)
BEGIN
  EXEC('ALTER TABLE TEATRO ALTER COLUMN registrado_por int NULL');
  INSERT INTO TEATRO (id_teatro, id_ciudad, nombre, direccion, telefono, email, activo, registrado_por, fecha_registro) VALUES
  (1, 1, N'Cine Colombia Santafé', N'Calle 7A # 43A-239', N'3001111111', N'santafe@cinecolombia.com', 1, NULL, GETDATE()),
  (2, 1, N'Cine Colombia Oviedo', N'Cra. 43A # 6 Sur-15', N'3002222222', N'oviedo@cinecolombia.com', 1, NULL, GETDATE());
END
GO

IF NOT EXISTS (SELECT 1 FROM EMPLEADO WHERE id_empleado = 0)
BEGIN
  -- Empleado semilla de sistema: sirve como origen único para registrar a los demás.
  INSERT INTO EMPLEADO (id_empleado, codigo_empleado, id_teatro, id_tipo_doc, num_documento, nombres, apellidos, fecha_ingreso, activo, registrado_por, fecha_registro) VALUES
  (0, N'EMP000', 1, 1, N'1000000000', N'Sistema', N'CineColombia', '2025-01-01', 1, NULL, GETDATE()),
  (1, N'EMP001', 1, 1, N'1001291016', N'Santiago', N'Suárez', '2025-01-15', 1, 0, GETDATE()),
  (2, N'EMP002', 1, 1, N'1000000001', N'Carlos', N'Pérez', '2024-03-01', 1, 1, GETDATE());
END
GO

UPDATE TEATRO
SET registrado_por = 0
WHERE id_teatro IN (1, 2);
GO

EXEC('ALTER TABLE TEATRO ALTER COLUMN registrado_por int NOT NULL');
GO

IF NOT EXISTS (SELECT 1 FROM EMPLEADO WHERE id_empleado = 3)
BEGIN
  INSERT INTO EMPLEADO (id_empleado, codigo_empleado, id_teatro, id_tipo_doc, num_documento, nombres, apellidos, fecha_ingreso, activo, registrado_por, fecha_registro) VALUES
  (3, N'EMP003', 1, 1, N'1000000002', N'Laura', N'Martínez', '2024-08-01', 1, 1, GETDATE());
END
GO

INSERT INTO SALA (id_sala, id_teatro, id_tipo_sala, nombre_sala, capacidad_total, activo, registrado_por, fecha_registro) VALUES
(1, 1, 1, N'Sala 1', 10, 1, 1, GETDATE()),
(2, 1, 3, N'Sala 2 IMAX', 10, 1, 1, GETDATE());
GO

INSERT INTO SILLA (id_silla, id_sala, id_tipo_silla, fila, numero, estado, registrado_por, fecha_registro) VALUES
(1, 1, 1, N'A', 1, 1, 1, GETDATE()),
(2, 1, 1, N'A', 2, 1, 1, GETDATE()),
(3, 1, 1, N'A', 3, 1, 1, GETDATE()),
(4, 1, 1, N'A', 4, 1, 1, GETDATE()),
(5, 1, 1, N'A', 5, 1, 1, GETDATE()),
(6, 2, 3, N'B', 1, 1, 1, GETDATE()),
(7, 2, 3, N'B', 2, 1, 1, GETDATE()),
(8, 2, 3, N'B', 3, 1, 1, GETDATE()),
(9, 2, 3, N'B', 4, 1, 1, GETDATE()),
(10, 2, 3, N'B', 5, 1, 1, GETDATE());
GO

INSERT INTO PELICULA (id_pelicula, id_genero, id_clasificacion, titulo_original, nombre_oferta, resumen, anio_estreno, trailer_link, duracion_min, registrado_por, fecha_registro) VALUES
(1, 1, 3, N'Deadpool & Wolverine', N'Deadpool 3', N'Película de acción y humor.', '2024-07-26', N'https://youtu.be/73_1biulkYk', 127, 1, GETDATE()),
(2, 1, 3, N'Furiosa: A Mad Max Saga', N'Furiosa', N'Historia de acción postapocalíptica.', '2024-05-24', N'https://youtu.be/XJMuhwVlca4', 148, 1, GETDATE()),
(3, 5, 1, N'Inside Out 2', N'Intensamente 2', N'Riley enfrenta nuevas emociones.', '2024-06-14', N'https://youtu.be/LEjhY15eCx0', 100, 1, GETDATE());
GO

INSERT INTO PELICULA_IDIOMA (id_pelicula_idioma, id_pelicula, id_idioma, es_original) VALUES
(1, 1, 2, 1),
(2, 2, 2, 1),
(3, 3, 1, 1);
GO

INSERT INTO PELICULA_FORMATO (id_pelicula_formato, id_pelicula, id_formato) VALUES
(1, 1, 2),
(2, 2, 3),
(3, 3, 1);
GO

INSERT INTO PELICULA_PRODUCTORA (id_pelicula_productora, id_pelicula, id_productora) VALUES
(1, 1, 2),
(2, 2, 1),
(3, 3, 3);
GO

INSERT INTO PELICULA_DISTRIBUIDORA (id_pelicula_distribuidora, id_pelicula, id_distribuidora) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 3, 2);
GO

INSERT INTO CLIENTE (id_cliente, id_tipo_cliente, id_tipo_doc, num_documento, nombres, apellidos, email, activo, registrado_por, fecha_registro) VALUES
(1, 2, 1, N'1017001001', N'Juan', N'Pérez', N'juan.perez@email.com', 1, 1, GETDATE()),
(2, 3, 1, N'1152002002', N'María', N'Gómez', N'maria.gomez@email.com', 1, 1, GETDATE());
GO

INSERT INTO DIRECCION_CLIENTE (id_direccion_cli, id_cliente, id_ciudad, direccion, activo) VALUES
(1, 1, 1, N'Cra 80 # 45-20', 1),
(2, 2, 1, N'Cl 10 # 55-30', 1);
GO

INSERT INTO TELEFONO_CLIENTE (id_telefono, id_cliente, id_tipo_telefono, numero) VALUES
(1, 1, 1, N'3001234567'),
(2, 2, 1, N'3017654321');
GO

INSERT INTO TARJETA_FIDELIZACION (id_tarjeta, id_cliente, numero_tarjeta, fecha_emision, fecha_vencimiento, puntos_acumulados, descuento_porcentaje, estado, registrado_por, fecha_registro) VALUES
(1, 1, N'CC-VIP-001', '2025-01-01', '2027-01-01', 150.00, 10.00, 1, 1, GETDATE());
GO

INSERT INTO TELEFONO_EMPLEADO (id_telefono_emp, id_empleado, id_tipo_telefono, numero) VALUES
(1, 1, 1, N'3105551111'),
(2, 2, 1, N'3105552222');
GO

INSERT INTO DIRECCION_EMPLEADO (id_direccion_emp, id_empleado, id_ciudad, direccion, activo) VALUES
(1, 1, 1, N'Cra 50 # 10-20', 1),
(2, 2, 1, N'Cl 30 # 40-50', 1);
GO

INSERT INTO EMPLEADO_PROFESION (id_emp_profesion, id_empleado, id_profesion) VALUES
(1, 1, 1),
(2, 2, 2);
GO

INSERT INTO USUARIO_SISTEMA (id_usuario, id_empleado, id_rol, username, password_hash, activo, ultimo_login, registrado_por, fecha_registro) VALUES
(1, 1, 1, N'santiago.suarez', N'0x240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 1, NULL, 1, GETDATE()),
(2, 2, 2, N'carlos.perez', N'0x240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 1, NULL, 1, GETDATE());
GO

INSERT INTO FUNCION (id_funcion, id_sala, id_pelicula, id_idioma, id_formato, fecha_funcion, hora_inicio, hora_fin, precio_base, estado, registrado_por, fecha_registro) VALUES
(1, 1, 1, 1, 2, '2026-05-28', '2026-05-28T12:00:00', '2026-05-28T14:07:00', 15000, 1, 1, GETDATE()),
(2, 2, 2, 1, 3, '2026-05-28', '2026-05-28T14:30:00', '2026-05-28T17:00:00', 26000, 1, 1, GETDATE()),
(3, 1, 3, 1, 1, '2026-05-28', '2026-05-28T17:30:00', '2026-05-28T19:10:00', 13000, 1, 1, GETDATE());
GO

INSERT INTO VENTA (id_venta, id_cliente, id_empleado, id_metodo_pago, fecha_hora, subtotal, total_descuento, total_venta, estado) VALUES
(1, 1, 1, 1, GETDATE(), 15000, 1500, 13500, 1);
GO

INSERT INTO BOLETICA (id_boletica, id_venta, id_funcion, estado) VALUES
(1, 1, 1, 1);
GO

INSERT INTO BOLETICA_SILLA (id_boletica_silla, id_boletica, id_silla, precio_unitario, descuento, precio_final, estado) VALUES
(1, 1, 1, 15000, 1500, 13500, 1);