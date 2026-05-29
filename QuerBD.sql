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

CREATE TABLE TIPO_DOCUMENTO (
    id_tipo_doc  INT          NOT NULL PRIMARY KEY,
    codigo       VARCHAR(10)  NOT NULL,
    descripcion  VARCHAR(50)  NOT NULL
);

CREATE TABLE TIPO_CLIENTE (
    id_tipo_cliente  INT          NOT NULL PRIMARY KEY,
    nombre           VARCHAR(30)  NOT NULL,
    descripcion      VARCHAR(100) NOT NULL
);

CREATE TABLE METODO_PAGO (
    id_metodo_pago  INT          NOT NULL PRIMARY KEY,
    nombre          VARCHAR(50)  NOT NULL
);

CREATE TABLE TIPO_SILLA (
    id_tipo_silla  INT             NOT NULL PRIMARY KEY,
    nombre         VARCHAR(30)     NOT NULL,
    precio_base    DECIMAL(10,2)   NOT NULL
);

CREATE TABLE FORMATO (
    id_formato  INT          NOT NULL PRIMARY KEY,
    nombre      VARCHAR(20)  NOT NULL
);

CREATE TABLE IDIOMA (
    id_idioma  INT          NOT NULL PRIMARY KEY,
    nombre     VARCHAR(50)  NOT NULL,
    codigo     VARCHAR(5)   NOT NULL
);

CREATE TABLE ROL (
    id_rol      INT          NOT NULL PRIMARY KEY,
    nombre      VARCHAR(30)  NOT NULL,
    descripcion VARCHAR(100)
);

-- ─── INFRAESTRUCTURA ─────────────────────────────────────────

CREATE TABLE TEATRO (
    id_teatro   INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
    nombre      VARCHAR(100)  NOT NULL,
    direccion   VARCHAR(200)  NOT NULL,
    activo      BIT           NOT NULL DEFAULT 1
);

CREATE TABLE TIPO_SALA (
    id_tipo_sala  INT          NOT NULL PRIMARY KEY,
    nombre        VARCHAR(30)  NOT NULL
);

CREATE TABLE SALA (
    id_sala          INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_teatro        INT           NOT NULL REFERENCES TEATRO(id_teatro),
    id_tipo_sala     INT           NOT NULL REFERENCES TIPO_SALA(id_tipo_sala),
    nombre_sala      VARCHAR(50)   NOT NULL,
    capacidad_total  INT           NOT NULL,
    activo           BIT           NOT NULL DEFAULT 1
);

CREATE TABLE SILLA (
    id_silla       INT     NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_sala        INT     NOT NULL REFERENCES SALA(id_sala),
    id_tipo_silla  INT     NOT NULL REFERENCES TIPO_SILLA(id_tipo_silla),
    fila           NCHAR(2) NOT NULL,
    numero         INT     NOT NULL,
    estado         INT     NOT NULL DEFAULT 1,
    CONSTRAINT UQ_SILLA UNIQUE (id_sala, fila, numero)
);

-- ─── PELÍCULAS Y FUNCIONES ───────────────────────────────────

CREATE TABLE GENERO (
    id_genero  INT          NOT NULL PRIMARY KEY,
    nombre     VARCHAR(50)  NOT NULL
);

CREATE TABLE CLASIFICACION (
    id_clasificacion  INT          NOT NULL PRIMARY KEY,
    codigo            VARCHAR(10)  NOT NULL,
    descripcion       VARCHAR(100) NOT NULL
);

CREATE TABLE PELICULA (
    id_pelicula       INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_genero         INT           NOT NULL REFERENCES GENERO(id_genero),
    id_clasificacion  INT           NOT NULL REFERENCES CLASIFICACION(id_clasificacion),
    titulo_original   VARCHAR(150)  NOT NULL,
    nombre_oferta     VARCHAR(150)  NOT NULL,
    resumen           VARCHAR(500)  NOT NULL,
    anio_estreno      DATE          NOT NULL,
    duracion_min      INT           NOT NULL,
    trailer_link      VARCHAR(300)
);

CREATE TABLE FUNCION (
    id_funcion      INT             NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_sala         INT             NOT NULL REFERENCES SALA(id_sala),
    id_pelicula     INT             NOT NULL REFERENCES PELICULA(id_pelicula),
    id_idioma       INT             NOT NULL REFERENCES IDIOMA(id_idioma),
    id_formato      INT             NOT NULL REFERENCES FORMATO(id_formato),
    fecha_funcion   DATE            NOT NULL,
    hora_inicio     DATETIME        NOT NULL,
    hora_fin        DATETIME        NOT NULL,
    precio_base     DECIMAL(10,2)   NOT NULL,
    estado          BIT             NOT NULL DEFAULT 1,
    CONSTRAINT UQ_FUNCION UNIQUE (id_sala, fecha_funcion, hora_inicio)
);

-- ─── CLIENTES ────────────────────────────────────────────────

CREATE TABLE CLIENTE (
    id_cliente       INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_tipo_cliente  INT           NOT NULL REFERENCES TIPO_CLIENTE(id_tipo_cliente),
    id_tipo_doc      INT           NOT NULL REFERENCES TIPO_DOCUMENTO(id_tipo_doc),
    num_documento    VARCHAR(20)   NOT NULL,
    nombres          VARCHAR(100)  NOT NULL,
    apellidos        VARCHAR(100)  NOT NULL,
    email            VARCHAR(150),
    activo           BIT           NOT NULL DEFAULT 1,
    CONSTRAINT UQ_CLIENTE UNIQUE (id_tipo_doc, num_documento)
);

CREATE TABLE TARJETA_FIDELIZACION (
    id_tarjeta            INT             NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_cliente            INT             NOT NULL REFERENCES CLIENTE(id_cliente),
    numero_tarjeta        VARCHAR(20)     NOT NULL,
    fecha_emision         DATE            NOT NULL,
    fecha_vencimiento     DATE            NOT NULL,
    puntos_acumulados     DECIMAL(10,2)   NOT NULL DEFAULT 0,
    descuento_porcentaje  DECIMAL(5,2)    NOT NULL DEFAULT 0,
    estado                BIT             NOT NULL DEFAULT 1,
    CONSTRAINT UQ_TARJETA UNIQUE (id_cliente)
);

-- ─── EMPLEADOS Y USUARIOS ────────────────────────────────────

CREATE TABLE EMPLEADO (
    id_empleado      INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_teatro        INT           NOT NULL REFERENCES TEATRO(id_teatro),
    id_tipo_doc      INT           NOT NULL REFERENCES TIPO_DOCUMENTO(id_tipo_doc),
    num_documento    VARCHAR(20)   NOT NULL,
    nombres          VARCHAR(100)  NOT NULL,
    apellidos        VARCHAR(100)  NOT NULL,
    fecha_ingreso    DATE          NOT NULL,
    activo           BIT           NOT NULL DEFAULT 1,
    CONSTRAINT UQ_EMPLEADO UNIQUE (id_tipo_doc, num_documento)
);

CREATE TABLE USUARIO_SISTEMA (
    id_usuario      INT           NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_empleado     INT           NOT NULL REFERENCES EMPLEADO(id_empleado),
    id_rol          INT           NOT NULL REFERENCES ROL(id_rol),
    username        VARCHAR(50)   NOT NULL,
    password_hash   VARCHAR(255)  NOT NULL,
    activo          BIT           NOT NULL DEFAULT 1,
    ultimo_login    DATETIME,
    CONSTRAINT UQ_USUARIO    UNIQUE (id_empleado),
    CONSTRAINT UQ_USERNAME   UNIQUE (username)
);

-- ─── VENTAS Y BOLETERÍA ──────────────────────────────────────

CREATE TABLE VENTA (
    id_venta         INT             NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_cliente       INT             REFERENCES CLIENTE(id_cliente),  -- NULL = casual
    id_empleado      INT             NOT NULL REFERENCES EMPLEADO(id_empleado),
    id_metodo_pago   INT             NOT NULL REFERENCES METODO_PAGO(id_metodo_pago),
    fecha_hora       DATETIME        NOT NULL DEFAULT GETDATE(),
    subtotal         DECIMAL(10,2)   NOT NULL,
    total_descuento  DECIMAL(10,2)   NOT NULL DEFAULT 0,
    total_venta      DECIMAL(10,2)   NOT NULL,
    estado           BIT             NOT NULL DEFAULT 1
);

CREATE TABLE BOLETICA (
    id_boletica  INT   NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_venta     INT   NOT NULL REFERENCES VENTA(id_venta),
    id_funcion   INT   NOT NULL REFERENCES FUNCION(id_funcion),
    estado       INT   NOT NULL DEFAULT 1
);

CREATE TABLE BOLETICA_SILLA (
    id_boletica_silla  INT             NOT NULL PRIMARY KEY IDENTITY(1,1),
    id_boletica        INT             NOT NULL REFERENCES BOLETICA(id_boletica),
    id_silla           INT             NOT NULL REFERENCES SILLA(id_silla),
    precio_unitario    DECIMAL(10,2)   NOT NULL,
    descuento          DECIMAL(10,2)   NOT NULL DEFAULT 0,
    precio_final       DECIMAL(10,2)   NOT NULL,
    estado             INT             NOT NULL DEFAULT 1,
    CONSTRAINT UQ_BOLETICA_SILLA UNIQUE (id_boletica, id_silla)
);

GO

-- ============================================================
-- INSERTS DE DATOS DE PRUEBA
-- ============================================================

-- Catálogos
INSERT INTO TIPO_DOCUMENTO VALUES (1,'CC','Cédula de Ciudadanía'),(2,'CE','Cédula de Extranjería'),(3,'TI','Tarjeta de Identidad'),(4,'PA','Pasaporte');
INSERT INTO TIPO_CLIENTE   VALUES (1,'VIP','Cliente VIP con tarjeta de fidelización premium'),(2,'Registrado','Cliente registrado con tarjeta regular'),(3,'Casual','Cliente ocasional sin registro');
INSERT INTO METODO_PAGO    VALUES (1,'Efectivo'),(2,'Tarjeta Débito'),(3,'Tarjeta Crédito'),(4,'PSE'),(5,'Nequi');
INSERT INTO TIPO_SILLA     VALUES (1,'General',12000),(2,'Preferencial',18000),(3,'VIP',25000);
INSERT INTO FORMATO        VALUES (1,'2D'),(2,'3D'),(3,'IMAX'),(4,'4DX');
INSERT INTO IDIOMA         VALUES (1,'Español','es'),(2,'Inglés','en'),(3,'Francés','fr');
INSERT INTO ROL            VALUES (1,'Administrador','Acceso total al sistema'),(2,'Cajero','Acceso al módulo de ventas'),(3,'Supervisor','Acceso de consulta y reportes');
INSERT INTO GENERO         VALUES (1,'Acción'),(2,'Drama'),(3,'Comedia'),(4,'Terror'),(5,'Animación');
INSERT INTO CLASIFICACION  VALUES (1,'G','Todo público'),(2,'PG','Orientación parental sugerida'),(3,'PG-13','Mayores de 13 años'),(4,'R','Mayores de 17 años');
INSERT INTO TIPO_SALA      VALUES (1,'General'),(2,'VIP'),(3,'IMAX'),(4,'4DX');

-- Teatro, Salas y Sillas
INSERT INTO TEATRO (nombre, direccion, activo) VALUES
('Cine Colombia Unicentro','Cl. 85 #53-50, Medellín',1),
('Cine Colombia Oviedo','Cra. 43A #6Sur-15, Medellín',1);

INSERT INTO SALA (id_teatro, id_tipo_sala, nombre_sala, capacidad_total, activo) VALUES
(1, 1, 'Sala 1', 10, 1),
(1, 3, 'Sala 2 IMAX', 10, 1),
(2, 1, 'Sala 1', 10, 1);

-- Sillas Sala 1 (filas A y B, 5 sillas c/u)
INSERT INTO SILLA (id_sala, id_tipo_silla, fila, numero, estado) VALUES
(1,1,'A',1,1),(1,1,'A',2,1),(1,1,'A',3,1),(1,1,'A',4,1),(1,1,'A',5,1),
(1,2,'B',1,1),(1,2,'B',2,1),(1,2,'B',3,1),(1,2,'B',4,1),(1,2,'B',5,1);

-- Sillas Sala 2 IMAX
INSERT INTO SILLA (id_sala, id_tipo_silla, fila, numero, estado) VALUES
(2,3,'A',1,1),(2,3,'A',2,1),(2,3,'A',3,1),(2,3,'A',4,1),(2,3,'A',5,1),
(2,3,'B',1,1),(2,3,'B',2,1),(2,3,'B',3,1),(2,3,'B',4,1),(2,3,'B',5,1);

-- Películas
INSERT INTO PELICULA (id_genero, id_clasificacion, titulo_original, nombre_oferta, resumen, anio_estreno, duracion_min, trailer_link) VALUES
(1, 3, 'Deadpool & Wolverine', 'Deadpool 3', 'Wade Wilson regresa junto a Wolverine en una aventura multiversal.', '2024-07-26', 127, 'https://youtu.be/73_1biulkYk'),
(1, 3, 'Furiosa: A Mad Max Saga', 'Furiosa: La Saga de Mad Max', 'El origen de la guerrera Furiosa en el yermo post-apocalíptico.', '2024-05-24', 148, 'https://youtu.be/XJMuhwVlca4'),
(5, 1, 'Inside Out 2', 'Intensamente 2', 'Riley enfrenta la adolescencia con nuevas emociones.', '2024-06-14', 100, 'https://youtu.be/LEjhY15eCx0');

-- Funciones
INSERT INTO FUNCION (id_sala, id_pelicula, id_idioma, id_formato, fecha_funcion, hora_inicio, hora_fin, precio_base, estado) VALUES
(1, 1, 1, 2, '2026-05-28', '2026-05-28T12:00:00', '2026-05-28T14:07:00', 15000, 1),
(2, 2, 1, 3, '2026-05-28', '2026-05-28T14:30:00', '2026-05-28T17:00:00', 26000, 1),
(1, 3, 1, 1, '2026-05-28', '2026-05-28T17:30:00', '2026-05-28T19:10:00', 13000, 1);

-- Empleados y Usuarios
INSERT INTO EMPLEADO (id_teatro, id_tipo_doc, num_documento, nombres, apellidos, fecha_ingreso, activo) VALUES
(1, 1, '1001291016', 'Santiago', 'Suárez', '2025-01-15', 1),
(1, 1, '1000000001', 'Carlos', 'Pérez', '2024-03-01', 1);

-- password_hash = 'admin123' en SHA256 (puedes cambiarlo luego)
INSERT INTO USUARIO_SISTEMA (id_empleado, id_rol, username, password_hash, activo) VALUES
(1, 1, 'santiago.suarez', '0x240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 1),
(2, 2, 'carlos.perez',    '0x240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9', 1);

-- Clientes de prueba
INSERT INTO CLIENTE (id_tipo_cliente, id_tipo_doc, num_documento, nombres, apellidos, email, activo) VALUES
(1, 1, '1017001001', 'Juan',  'Pérez',  'juan.perez@email.com',  1),
(2, 1, '1152002002', 'María', 'Gómez',  'maria.gomez@email.com', 1),
(3, 1, '1000000099', 'Pedro', 'Ramírez', NULL, 1);

-- Tarjeta fidelización para Juan (VIP)
INSERT INTO TARJETA_FIDELIZACION (id_cliente, numero_tarjeta, fecha_emision, fecha_vencimiento, puntos_acumulados, descuento_porcentaje, estado) VALUES
(1, 'CC-VIP-001', '2025-01-01', '2027-01-01', 150.00, 10.00, 1);

GO
PRINT 'Base de datos CineColombia creada exitosamente.';


select * from TIPO_DOCUMENTO;