# CineColombia API

API REST para la gestión de una cadena de cines (Cine Colombia). Desarrollada en .NET 10 con Entity Framework Core y SQL Server.

---

## Tabla de Contenidos

1. [Descripción General](#1-descripción-general)
2. [Stack Tecnológico](#2-stack-tecnológico)
3. [Requisitos y Configuración](#3-requisitos-y-configuración)
4. [Arquitectura del Proyecto](#4-arquitectura-del-proyecto)
5. [Program.cs — Punto de Entrada](#5-programcs--punto-de-entrada)
6. [CineColombiaContext.cs — DbContext](#6-cinecolombiacontextcs--dbcontext)
7. [Modelos de Datos (Models/)](#7-modelos-de-datos-models)
8. [DTO — RegistroVentaDto](#8-dto--registroventadto)
9. [Capa de Negocio (Clases/)](#9-capa-de-negocio-clases)
10. [Capa de Controladores (Controllers/)](#10-capa-de-controladores-controllers)
11. [Base de Datos (QuerBD.sql)](#11-base-de-datos-querbd-sql)
12. [Endpoints de la API](#12-endpoints-de-la-api)
13. [Flujo RegistrarVentaCompleta](#13-flujo-registrarventacompleta)
14. [Decisiones Técnicas y Limitaciones](#14-decisiones-técnicas-y-limitaciones)
15. [Posibles Preguntas de Sustentación](#15-posibles-preguntas-de-sustentación)

---

## 1. Descripción General

**CineColombiaApi** es un backend completo para la administración de una cadena de cines. Permite:

- **Catálogo de películas** con géneros, clasificaciones, formatos, idiomas, distribuidoras y productoras
- **Gestión de cines (teatros)**, salas y sillas con mapa de asientos
- **Programación de funciones** vinculando películas a salas específicas
- **Administración de clientes** con direcciones, teléfonos y tarjetas de fidelización
- **Administración de empleados** con profesiones, direcciones, teléfonos y cuentas de usuario
- **Venta de boletas** con asignación de sillas y detección de duplicados
- **Login de usuarios** del sistema (sin JWT, consulta directa a BD)

---

## 2. Stack Tecnológico

| Componente | Versión |
|---|---|
| .NET | 10.0 (preview) |
| Entity Framework Core | 10.0.8 |
| SQL Server (LocalDB) | — |
| Swashbuckle (Swagger) | 10.1.7 |
| ASP.NET Core OpenAPI | 10.0.8 |

---

## 3. Requisitos y Configuración

### Requisitos

- .NET SDK 10.0+
- SQL Server LocalDB (o cualquier instancia SQL Server)
- Visual Studio 2022+ / VS Code / JetBrains Rider

### Configuración

**Cadena de conexión** (`appsettings.json`):

```json
{
  "ConnectionStrings": {
    "cnx": "Server=(localdb)\\MSSQLLocalDB;Database=CineColombia;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Database**: Ejecutar `QuerBD.sql` en SQL Server para crear la BD, tablas, índices, FK y datos de prueba.

**Ejecución**:

```bash
dotnet run
# Servidor en http://localhost:5140
# Swagger UI en http://localhost:5140/swagger
```

**Perfil de lanzamiento** (`Properties/launchSettings.json`):

```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "applicationUrl": "http://localhost:5140",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## 4. Arquitectura del Proyecto

### Estructura de Directorios

```
CineColombiaApi/
├── Program.cs                        # Punto de entrada y middleware pipeline
├── appsettings.json                  # Configuración (connection string, logging)
├── appsettings.Development.json      # Override para desarrollo
├── CineColombiaApi.csproj            # Archivo de proyecto .NET
├── CineColombiaApi.slnx              # Solución
├── QuerBD.sql                        # Script de creación de BD + datos semilla
├── Properties/
│   └── launchSettings.json           # Configuración de ejecución
├── Models/
│   ├── CineColombiaContext.cs        # DbContext (Fluent API)
│   ├── RegistroVentaDto.cs           # DTO para venta completa
│   └── *.cs                          # 28 entidades (POCOs)
├── Clases/
│   └── clsOpe*.cs                    # 33 clases de repositorio/lógica
├── Controllers/
│   └── *Controller.cs                # 36 controladores
├── WeatherForecast.cs                # Archivo residual (no usado)
├── API_DOC.md                        # Documentación de API
├── README_Errors.md                  # Notas de errores
└── .github/workflows/                # (vacío)
```

### Patrón Arquitectónico

```
HTTP Request
    │
    ▼
Controllers/ (Manejo HTTP)
    │  Reciben CineColombiaContext por inyección de dependencia
    │  Crean instancias de clsOpe* manualmente (sin DI)
    ▼
Clases/ (Repositorio + Lógica de Negocio)
    │  Cada clase envuelve una entidad
    │  Contienen LINQ queries, validaciones, auto-generación de IDs
    ▼
Models/ (EF Core + DbContext)
    │  Mapeo ORM con Fluent API
    ▼
SQL Server (Base de datos)
```

**Flujo típico de una petición**:

1. Request HTTP llega a `Program.cs` → pasa por middleware pipeline
2. Se enruta al Controller correspondiente
3. Controller crea un objeto `clsOpe*` (pasándole el `DbContext`)
4. El método del `clsOpe*` ejecuta LINQ contra `DbSet<T>`
5. EF Core traduce LINQ a SQL y ejecuta contra SQL Server
6. Resultado se devuelve al Controller → se serializa a JSON → HTTP Response

---

## 5. Program.cs — Punto de Entrada

Archivo completo (60 líneas):

```csharp
// Program.cs
```

### Línea 1-3: Usings

```csharp
using CineColombiaApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
```

- **`CineColombiaApi.Models`**: Namespace donde está el `DbContext` y todas las entidades
- **`Microsoft.AspNetCore.Diagnostics`**: Para el middleware de manejo global de errores (`UseExceptionHandler`)
- **`Microsoft.EntityFrameworkCore`**: Para usar `UseSqlServer()` y el `AddDbContext`

### Línea 5: WebApplication Builder

```csharp
var builder = WebApplication.CreateBuilder(args);
```

- Crea el builder con configuración por defecto
- Carga `appsettings.json`, `appsettings.{Environment}.json`, variables de entorno
- Registra servicios por defecto (logging, configuración, etc.)

### Líneas 7-11: Validación de Connection String

```csharp
var connectionString = builder.Configuration.GetConnectionString("cnx");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión 'cnx' no está configurada. Usa la variable de entorno ConnectionStrings__cnx.");
}
```

- Lee la connection string llamada `"cnx"` desde `appsettings.json`
- Si no está configurada, lanza una excepción con un mensaje claro
- Esto evita que la aplicación arranque sin una BD configurada

### Líneas 13-14: DbContext Registration

```csharp
builder.Services.AddDbContext<CineColombiaContext>(options =>
    options.UseSqlServer(connectionString));
```

- **AddDbContext**: Registra `CineColombiaContext` en el contenedor DI con **scoped lifetime** (una instancia por request)
- **UseSqlServer**: Configura EF Core para usar SQL Server con la cadena de conexión
- El DbContext se resuelve automáticamente en los constructores de los Controllers

### Línea 16: Controllers

```csharp
builder.Services.AddControllers();
```

- Registra todos los controladores del proyecto
- Habilita el binding de parámetros, validación de modelos, formateo JSON

### Línea 17: API Explorer

```csharp
builder.Services.AddEndpointsApiExplorer();
```

- Expone metadatos de endpoints para que Swagger pueda generar la documentación

### Línea 18: Swagger

```csharp
builder.Services.AddSwaggerGen();
```

- Configura Swashbuckle para generar el documento OpenAPI/Swagger
- Sin configuración adicional (usa valores por defecto)

### Líneas 20-28: CORS

```csharp
var corsOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CineCors", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});
```

- Lee orígenes CORS desde configuración (si existe la sección `Cors:Origins`)
- Crea la política `"CineCors"` que permite **cualquier origen, header y método**
- **Sin restricciones**: la API es completamente abierta en CORS

### Línea 30: Build

```csharp
var app = builder.Build();
```

- Construye el pipeline de middleware con todos los servicios registrados

### Líneas 32-45: Exception Handler (Global Error Handler)

```csharp
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            mensaje = "Error interno del servidor",
            detalle = exception?.Message
        });
    });
});
```

- **UseExceptionHandler**: Captura cualquier excepción no manejada en el pipeline
- Extrae la excepción desde `IExceptionHandlerFeature`
- Retorna **500 Internal Server Error** con JSON:
  - `mensaje`: Texto fijo "Error interno del servidor"
  - `detalle`: `exception.Message` (expone detalles del error al cliente — **esto no es seguro para producción**)

### Líneas 47-52: Swagger UI

```csharp
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CineColombia API v1");
});
```

- **UseSwagger()**: Genera el archivo JSON `/swagger/v1/swagger.json`
- **UseSwaggerUI()**: Sirve la interfaz web de Swagger en `/swagger`
- `RoutePrefix = "swagger"`: La UI está en `http://localhost:5140/swagger`

### Línea 54: CORS Middleware

```csharp
app.UseCors("CineCors");
```

- Aplica la política CORS configurada anteriormente a todas las respuestas

### Línea 56: Ruta Raíz

```csharp
app.MapGet("/", () => "Hey there! Welcome to CineColombia API.");
```

- Endpoint simple en la raíz que devuelve un mensaje de bienvenida (string plano)

### Línea 58: Mapa de Controllers

```csharp
app.MapControllers();
```

- Habilita el enrutamiento basado en atributos de los controladores (`[Route]`, `[HttpGet]`, etc.)

### Línea 60: Run

```csharp
app.Run();
```

- Inicia el servidor web y comienza a escuchar peticiones

---

## 6. CineColombiaContext.cs — DbContext

Archivo: `Models/CineColombiaContext.cs` (871 líneas)

### Declaración y Constructores

```csharp
namespace CineColombiaApi.Models;

public partial class CineColombiaContext : DbContext
{
    public CineColombiaContext() { }

    public CineColombiaContext(DbContextOptions<CineColombiaContext> options)
        : base(options) { }
```

- **`partial`**: Permite extender la clase en otro archivo si es necesario
- **Constructor sin parámetros**: Para uso en tiempo de diseño (scaffolding)
- **Constructor con opciones**: Recibe `DbContextOptions` desde la DI (inyectado en controllers)
- **`DbContextOptions<CineColombiaContext>`**: Configurado en Program.cs con `AddDbContext` + `UseSqlServer`

### DbSets (Propiedades)

Declara 28 `DbSet<T>` públicos virtuales. Cada uno representa una tabla en la BD.

```csharp
public virtual DbSet<Boletica> Boleticas { get; set; }
public virtual DbSet<BoleticaSilla> BoleticaSillas { get; set; }
public virtual DbSet<Ciudad> Ciudads { get; set; }
// ... (28 DbSets en total)
public virtual DbSet<Ventum> Venta { get; set; }
```

**Nota**: `Ventum` tiene DbSet llamado `Venta` (no `Ventums`) — es la única inconsistencia de nomenclatura en el proyecto.

### OnConfiguring (Comentado)

```csharp
/* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;...");
*/
```

- Código comentado que se usaba para conectar sin DI
- Ahora la cadena viene de `Program.cs`

### OnModelCreating — Fluent API

El método `OnModelCreating` mapea cada entidad a su tabla SQL y configura:

1. **Clave primaria** (`HasKey`)
2. **Nombre de tabla** (`ToTable`)
3. **Generación de ID** (`ValueGeneratedNever()`)
4. **Nombre de columna** (`HasColumnName`)
5. **Longitud/tipo de datos** (`HasMaxLength`, `HasColumnType`)
6. **Índices únicos** (`HasIndex().IsUnique()`)
7. **Relaciones/Claves foráneas** (`HasOne...WithMany...HasForeignKey`)

**Ejemplo de mapeo** (Boletica):

```csharp
modelBuilder.Entity<Boletica>(entity =>
{
    entity.HasKey(e => e.IdBoletica).HasName("PK__BOLETICA__DBAF29793A38A4A1");
    entity.ToTable("BOLETICA");
    entity.Property(e => e.IdBoletica)
        .ValueGeneratedNever()
        .HasColumnName("id_boletica");
    entity.Property(e => e.Estado).HasColumnName("estado");
    entity.Property(e => e.IdFuncion).HasColumnName("id_funcion");
    entity.Property(e => e.IdVenta).HasColumnName("id_venta");
    entity.HasOne<Ventum>().WithMany().HasForeignKey(e => e.IdVenta)
          .HasConstraintName("FK__BOLETICA__id_ven");
});
```

**Explicación**:
- `HasKey`: Define la PK con un nombre específico (copiado de la BD)
- `ToTable`: La clase C# `Boletica` mapea a la tabla `BOLETICA` en SQL
- `ValueGeneratedNever()`: El ID **no se auto-genera**, se asigna manualmente en código
- `HasColumnName`: Mapea propiedades C# (PascalCase) a columnas SQL (snake_case)
- `HasOne<Ventum>().WithMany().HasForeignKey(...)`: Define FK sin navegación de navegación inversa

**Patrón de `ValueGeneratedNever()`**: Es la decisión más importante del proyecto. En lugar de usar `IDENTITY` o `SEQUENCE` de SQL Server, los IDs se generan manualmente con `Max(id) + 1`. Esto es deliberado (evitó usar `SET IDENTITY_INSERT ON`) pero tiene implicaciones de concurrencia.

### OnModelCreatingPartial

```csharp
partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
```

- Método parcial para permitir configuración adicional desde otro archivo partial
- Actualmente no tiene implementación

### Resumen de Índices Únicos Configurados

| Índice | Columnas |
|--------|----------|
| SILLA_index_0 | IdSala, Fila, Numero |
| PELICULA_IDIOMA_index_1 | IdPelicula, IdIdioma |
| PELICULA_FORMATO_index_2 | IdPelicula, IdFormato |
| PELICULA_PRODUCTORA_index_3 | IdPelicula, IdProductora |
| PELICULA_DISTRIBUIDORA_index_4 | IdPelicula, IdDistribuidora |
| FUNCION_index_5 | IdSala, FechaFuncion, HoraInicio |
| CLIENTE_index_6 | IdTipoDoc, NumDocumento |
| TARJETA_FIDELIZACION_index_7 | IdCliente |
| EMPLEADO_index_8 | IdTipoDoc, NumDocumento |
| EMPLEADO_PROFESION_index_9 | IdEmpleado, IdProfesion |
| USUARIO_SISTEMA_index_10 | IdEmpleado |
| USUARIO_SISTEMA_index_11 | Username |
| BOLETICA_SILLA_index_12 | IdBoletica, IdSilla |

---

## 7. Modelos de Datos (Models/)

Cada archivo en `Models/` es una clase POCO (Plain Old CLR Object) que representa una tabla de la BD. Son clases `partial` sin anotaciones de datos (todo el mapeo está en Fluent API en el DbContext).

### 7.1 Catálogos

#### Pais (`Pai.cs`)

```csharp
public partial class Pai
{
    public int IdPais { get; set; }
    public string Nombre { get; set; }
    public string Codigo { get; set; }
}
```

- **Tabla**: PAIS
- **PK**: id_pais
- **Columnas**: id_pais, nombre, codigo
- **Nota**: La clase se llama `Pai` (sin `s`) pero la tabla es `PAIS`
- **Propósito**: Catálogo de países (Colombia, USA, México)

#### Departamento

```csharp
public partial class Departamento
{
    public int IdDepartamento { get; set; }
    public int IdPais { get; set; }
    public string Nombre { get; set; }
}
```

- **Tabla**: DEPARTAMENTO
- **FK**: IdPais → PAIS
- **Propósito**: Departamentos/estados dentro de un país (Antioquia, Cundinamarca)

#### Ciudad

```csharp
public partial class Ciudad
{
    public int IdCiudad { get; set; }
    public int IdDepartamento { get; set; }
    public string Nombre { get; set; }
}
```

- **Tabla**: CIUDAD
- **FK**: IdDepartamento → DEPARTAMENTO
- **Propósito**: Ciudades (Medellín, Bogotá, Cali)

#### Genero

```csharp
public partial class Genero
{
    public int IdGenero { get; set; }
    public string Nombre { get; set; }
}
```

- **Tabla**: GENERO
- **Propósito**: Géneros de películas (Acción, Drama, Comedia, Terror, Animación)

#### Clasificacion

```csharp
public partial class Clasificacion
{
    public int IdClasificacion { get; set; }
    public string Codigo { get; set; }
    public string Descripcion { get; set; }
}
```

- **Tabla**: CLASIFICACION
- **Propósito**: Clasificaciones/rating de películas (G, PG, PG-13, R)

#### Formato

```csharp
public partial class Formato
{
    public int IdFormato { get; set; }
    public string Nombre { get; set; }
}
```

- **Tabla**: FORMATO
- **Propósito**: Formatos de proyección (2D, 3D, IMAX, 4DX)

#### Idioma

```csharp
public partial class Idioma
{
    public int IdIdioma { get; set; }
    public string Nombre { get; set; }
    public string Codigo { get; set; }
}
```

- **Tabla**: IDIOMA
- **Propósito**: Idiomas (Español, Inglés) con código ISO

#### TipoCliente

```csharp
public partial class TipoCliente
{
    public int IdTipoCliente { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
}
```

- **Tabla**: TIPO_CLIENTE
- **Propósito**: Tipos de cliente (VIP, Registrado, Casual)

#### TipoDocumento

```csharp
public partial class TipoDocumento
{
    public int IdTipoDoc { get; set; }
    public string Codigo { get; set; }
    public string Descripcion { get; set; }
}
```

- **Tabla**: TIPO_DOCUMENTO
- **Propósito**: Tipos de documento de identidad (CC, CE, TI, PA)

#### TipoSala

```csharp
public partial class TipoSala
{
    public int IdTipoSala { get; set; }
    public string Nombre { get; set; }
}
```

- **Tabla**: TIPO_SALA
- **Propósito**: Tipos de sala (General, VIP, IMAX)

#### TipoSilla

```csharp
public partial class TipoSilla
{
    public int IdTipoSilla { get; set; }
    public string Nombre { get; set; }
    public decimal PrecioBase { get; set; }
}
```

- **Tabla**: TIPO_SILLA
- **Propósito**: Tipos de silla (General $12,000, Preferencial $18,000, VIP $25,000) con precio base

#### TipoTelefono

```csharp
public partial class TipoTelefono
{
    public int IdTipoTelefono { get; set; }
    public string Nombre { get; set; }
}
```

- **Tabla**: TIPO_TELEFONO
- **Propósito**: Tipos de teléfono (Celular, Fijo, WhatsApp)

#### MetodoPago

```csharp
public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }
    public string Nombre { get; set; }
}
```

- **Tabla**: METODO_PAGO
- **Propósito**: Métodos de pago (Efectivo, Tarjeta Débito, Tarjeta Crédito, PSE, Nequi)

#### Rol

```csharp
public partial class Rol
{
    public int IdRol { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
}
```

- **Tabla**: ROL
- **Propósito**: Roles del sistema (Administrador, Cajero, Supervisor)

#### Profesion

```csharp
public partial class Profesion
{
    public int IdProfesion { get; set; }
    public string Nombre { get; set; }
}
```

- **Tabla**: PROFESION
- **Propósito**: Profesiones de empleados (Administrador, Cajero, Supervisor)

#### Distribuidora

```csharp
public partial class Distribuidora
{
    public int IdDistribuidora { get; set; }
    public string Nombre { get; set; }
    public int? IdPais { get; set; }
}
```

- **Tabla**: DISTRIBUIDORA
- **FK**: IdPais → PAIS (nullable)
- **Propósito**: Distribuidoras de películas (Warner Bros., Walt Disney Studios, Universal)

#### Productora

```csharp
public partial class Productora
{
    public int IdProductora { get; set; }
    public string Nombre { get; set; }
    public int? IdPais { get; set; }
}
```

- **Tabla**: PRODUCTORA
- **FK**: IdPais → PAIS (nullable)
- **Propósito**: Productoras de películas (Warner Bros., Marvel Studios, Pixar)

### 7.2 Entidades Principales

#### Pelicula

```csharp
public partial class Pelicula
{
    public int IdPelicula { get; set; }
    public int IdGenero { get; set; }
    public int IdClasificacion { get; set; }
    public string TituloOriginal { get; set; }
    public string NombreOferta { get; set; }
    public string Resumen { get; set; }
    public DateOnly AnioEstreno { get; set; }
    public string? TrailerLink { get; set; }
    public int DuracionMin { get; set; }
    public int RegistradoPor { get; set; }
    public DateTime FechaRegistro { get; set; }
}
```

- **Tabla**: PELICULA
- **FKs**: IdGenero → GENERO, IdClasificacion → CLASIFICACION, RegistradoPor → EMPLEADO
- **Propósito**: Catálogo de películas con metadatos completos

#### Teatro

```csharp
public partial class Teatro
{
    public int IdTeatro { get; set; }
    public int IdCiudad { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public bool Activo { get; set; }
    public int? RegistradoPor { get; set; }
    public DateTime FechaRegistro { get; set; }
}
```

- **Tabla**: TEATRO
- **FKs**: IdCiudad → CIUDAD, RegistradoPor → EMPLEADO
- **Propósito**: Sedes físicas de la cadena de cines

#### Sala

```csharp
public partial class Sala
{
    public int IdSala { get; set; }
    public int IdTeatro { get; set; }
    public int IdTipoSala { get; set; }
    public string NombreSala { get; set; }
    public int CapacidadTotal { get; set; }
    public bool Activo { get; set; }
    public int RegistradoPor { get; set; }
    public DateTime FechaRegistro { get; set; }
}
```

- **Tabla**: SALA
- **FKs**: IdTeatro → TEATRO, IdTipoSala → TIPO_SALA, RegistradoPor → EMPLEADO
- **Propósito**: Salas de proyección dentro de cada teatro

#### Silla

```csharp
public partial class Silla
{
    public int IdSilla { get; set; }
    public int IdSala { get; set; }
    public int IdTipoSilla { get; set; }
    public string Fila { get; set; }  // nchar(1) — una letra (A, B, C...)
    public int Numero { get; set; }
    public int Estado { get; set; }
    public int RegistradoPor { get; set; }
    public DateTime FechaRegistro { get; set; }
}
```

- **Tabla**: SILLA
- **FKs**: IdSala → SALA, IdTipoSilla → TIPO_SILLA, RegistradoPor → EMPLEADO
- **Unique**: (IdSala, Fila, Numero)
- **Propósito**: Sillas individuales con ubicación exacta (fila + número)

#### Funcion

```csharp
public partial class Funcion
{
    public int IdFuncion { get; set; }
    public int IdSala { get; set; }
    public int IdPelicula { get; set; }
    public int IdIdioma { get; set; }
    public int IdFormato { get; set; }
    public DateOnly FechaFuncion { get; set; }
    public DateTime HoraInicio { get; set; }
    public DateTime HoraFin { get; set; }
    public decimal PrecioBase { get; set; }
    public bool Estado { get; set; }
    public int RegistradoPor { get; set; }
    public DateTime FechaRegistro { get; set; }
}
```

- **Tabla**: FUNCION
- **FKs**: IdSala → SALA, IdPelicula → PELICULA, IdIdioma → IDIOMA, IdFormato → FORMATO, RegistradoPor → EMPLEADO
- **Unique**: (IdSala, FechaFuncion, HoraInicio)
- **Propósito**: Programación de funciones (horarios de proyección)

#### Cliente

```csharp
public partial class Cliente
{
    public int IdCliente { get; set; }
    public int IdTipoCliente { get; set; }
    public int IdTipoDoc { get; set; }
    public string NumDocumento { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string? Email { get; set; }
    public bool Activo { get; set; }
    public int RegistradoPor { get; set; }
    public DateTime FechaRegistro { get; set; }
}
```

- **Tabla**: CLIENTE
- **FKs**: IdTipoCliente → TIPO_CLIENTE, IdTipoDoc → TIPO_DOCUMENTO, RegistradoPor → EMPLEADO
- **Unique**: (IdTipoDoc, NumDocumento) — un cliente no puede tener el mismo documento dos veces

#### Empleado

```csharp
public partial class Empleado
{
    public int IdEmpleado { get; set; }
    public string CodigoEmpleado { get; set; }
    public int IdTeatro { get; set; }
    public int IdTipoDoc { get; set; }
    public string NumDocumento { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public DateOnly FechaIngreso { get; set; }
    public bool Activo { get; set; }
    public int? RegistradoPor { get; set; }
    public DateTime FechaRegistro { get; set; }
}
```

- **Tabla**: EMPLEADO
- **FKs**: IdTeatro → TEATRO, IdTipoDoc → TIPO_DOCUMENTO, RegistradoPor → EMPLEADO
- **Unique**: (IdTipoDoc, NumDocumento) y CodigoEmpleado
- **Nota**: `RegistradoPor` es nullable para permitir el empleado semilla (id_empleado = 0)

### 7.3 Entidades Transaccionales

#### Ventum (Venta)

```csharp
public partial class Ventum
{
    public int IdVenta { get; set; }
    public int? IdCliente { get; set; }
    public int IdEmpleado { get; set; }
    public int IdMetodoPago { get; set; }
    public DateTime FechaHora { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TotalDescuento { get; set; }
    public decimal TotalVenta { get; set; }
    public bool Estado { get; set; }
}
```

- **Tabla**: VENTA
- **FKs**: IdCliente → CLIENTE (nullable — cliente casual), IdEmpleado → EMPLEADO, IdMetodoPago → METODO_PAGO
- **Propósito**: Transacciones de venta (cabecera de la compra)

#### Boletica

```csharp
public partial class Boletica
{
    public int IdBoletica { get; set; }
    public int IdVenta { get; set; }
    public int IdFuncion { get; set; }
    public int Estado { get; set; }
}
```

- **Tabla**: BOLETICA
- **FKs**: IdVenta → VENTA, IdFuncion → FUNCION
- **Propósito**: Cada boletica es un ticket dentro de una venta para una función específica

#### BoleticaSilla

```csharp
public partial class BoleticaSilla
{
    public int IdBoleticaSilla { get; set; }
    public int IdBoletica { get; set; }
    public int IdSilla { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Descuento { get; set; }
    public decimal PrecioFinal { get; set; }
    public int Estado { get; set; }
}
```

- **Tabla**: BOLETICA_SILLA
- **FKs**: IdBoletica → BOLETICA, IdSilla → SILLA
- **Unique**: (IdBoletica, IdSilla)
- **Propósito**: Asignación de sillas a cada boletica, con precios y descuentos

### 7.4 Entidades de Relación/Detalle

#### DireccionCliente

```csharp
public partial class DireccionCliente
{
    public int IdDireccionCli { get; set; }
    public int IdCliente { get; set; }
    public int IdCiudad { get; set; }
    public string Direccion { get; set; }
    public bool Activo { get; set; }
}
```

- **Tabla**: DIRECCION_CLIENTE
- **FKs**: IdCliente → CLIENTE, IdCiudad → CIUDAD

#### DireccionEmpleado

```csharp
public partial class DireccionEmpleado
{
    public int IdDireccionEmp { get; set; }
    public int IdEmpleado { get; set; }
    public int IdCiudad { get; set; }
    public string Direccion { get; set; }
    public bool Activo { get; set; }
}
```

- **Tabla**: DIRECCION_EMPLEADO
- **FKs**: IdEmpleado → EMPLEADO, IdCiudad → CIUDAD

#### TelefonoCliente

```csharp
public partial class TelefonoCliente
{
    public int IdTelefono { get; set; }
    public int IdCliente { get; set; }
    public int IdTipoTelefono { get; set; }
    public string Numero { get; set; }
}
```

- **Tabla**: TELEFONO_CLIENTE
- **FKs**: IdCliente → CLIENTE, IdTipoTelefono → TIPO_TELEFONO

#### TelefonoEmpleado

```csharp
public partial class TelefonoEmpleado
{
    public int IdTelefonoEmp { get; set; }
    public int IdEmpleado { get; set; }
    public int IdTipoTelefono { get; set; }
    public string Numero { get; set; }
}
```

- **Tabla**: TELEFONO_EMPLEADO
- **FKs**: IdEmpleado → EMPLEADO, IdTipoTelefono → TIPO_TELEFONO

#### EmpleadoProfesion

```csharp
public partial class EmpleadoProfesion
{
    public int IdEmpProfesion { get; set; }
    public int IdEmpleado { get; set; }
    public int IdProfesion { get; set; }
}
```

- **Tabla**: EMPLEADO_PROFESION
- **FKs**: IdEmpleado → EMPLEADO, IdProfesion → PROFESION
- **Unique**: (IdEmpleado, IdProfesion) — relación muchos a muchos

#### PeliculaIdioma

```csharp
public partial class PeliculaIdioma
{
    public int IdPeliculaIdioma { get; set; }
    public int IdPelicula { get; set; }
    public int IdIdioma { get; set; }
    public bool EsOriginal { get; set; }
}
```

- **Tabla**: PELICULA_IDIOMA
- **Unique**: (IdPelicula, IdIdioma)
- **Propósito**: Idiomas disponibles para cada película, con flag `EsOriginal`

#### PeliculaFormato

```csharp
public partial class PeliculaFormato
{
    public int IdPeliculaFormato { get; set; }
    public int IdPelicula { get; set; }
    public int IdFormato { get; set; }
}
```

- **Tabla**: PELICULA_FORMATO
- **Unique**: (IdPelicula, IdFormato)

#### PeliculaProductora

```csharp
public partial class PeliculaProductora
{
    public int IdPeliculaProductora { get; set; }
    public int IdPelicula { get; set; }
    public int IdProductora { get; set; }
}
```

- **Tabla**: PELICULA_PRODUCTORA
- **Unique**: (IdPelicula, IdProductora)

#### PeliculaDistribuidora

```csharp
public partial class PeliculaDistribuidora
{
    public int IdPeliculaDistribuidora { get; set; }
    public int IdPelicula { get; set; }
    public int IdDistribuidora { get; set; }
}
```

- **Tabla**: PELICULA_DISTRIBUIDORA
- **Unique**: (IdPelicula, IdDistribuidora)

#### UsuarioSistema

```csharp
public partial class UsuarioSistema
{
    public int IdUsuario { get; set; }
    public int IdEmpleado { get; set; }
    public int IdRol { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public bool Activo { get; set; }
    public DateTime? UltimoLogin { get; set; }
    public int RegistradoPor { get; set; }
    public DateTime FechaRegistro { get; set; }
}
```

- **Tabla**: USUARIO_SISTEMA
- **FKs**: IdEmpleado → EMPLEADO, IdRol → ROL, RegistradoPor → EMPLEADO
- **Unique**: Username, IdEmpleado
- **Propósito**: Credenciales de acceso al sistema para empleados

#### TarjetaFidelizacion

```csharp
public partial class TarjetaFidelizacion
{
    public int IdTarjeta { get; set; }
    public int IdCliente { get; set; }
    public string NumeroTarjeta { get; set; }
    public DateOnly FechaEmision { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public decimal PuntosAcumulados { get; set; }
    public decimal DescuentoPorcentaje { get; set; }
    public bool Estado { get; set; }
    public int RegistradoPor { get; set; }
    public DateTime FechaRegistro { get; set; }
}
```

- **Tabla**: TARJETA_FIDELIZACION
- **Unique**: IdCliente (un cliente, una tarjeta)
- **Propósito**: Programa de fidelización con puntos y descuentos

---

## 8. DTO — RegistroVentaDto

Archivo: `Models/RegistroVentaDto.cs` (27 líneas)

Este es el **único DTO** del proyecto. Se usa exclusivamente en el endpoint `POST /api/Venta/RegistrarVentaCompleta`.

### RegistroVentaDto

```csharp
public class RegistroVentaDto
{
    public int? IdCliente { get; set; }    // nullable — cliente casual
    public int IdEmpleado { get; set; }     // empleado que realiza la venta
    public int IdMetodoPago { get; set; }   // método de pago
    public DateTime FechaHora { get; set; }
    public decimal Subtotal { get; set; }
    public decimal TotalDescuento { get; set; }
    public decimal TotalVenta { get; set; }
    public List<BoleticaVentaDto> Boleticas { get; set; } = new();
}
```

### BoleticaVentaDto

```csharp
public class BoleticaVentaDto
{
    public int IdFuncion { get; set; }
    public List<SillaVentaDto> Sillas { get; set; } = new();
}
```

### SillaVentaDto

```csharp
public class SillaVentaDto
{
    public int IdSilla { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Descuento { get; set; }
    public decimal PrecioFinal { get; set; }
}
```

**JSON de ejemplo**:

```json
{
  "idCliente": 1,
  "idEmpleado": 1,
  "idMetodoPago": 1,
  "fechaHora": "2026-05-28T14:30:00",
  "subtotal": 30000,
  "totalDescuento": 3000,
  "totalVenta": 27000,
  "boleticas": [
    {
      "idFuncion": 1,
      "sillas": [
        {
          "idSilla": 1,
          "precioUnitario": 15000,
          "descuento": 1500,
          "precioFinal": 13500
        }
      ]
    }
  ]
}
```

**Propósito**: Permite crear en una sola llamada:
1. Una **Venta** (cabecera)
2. Una o más **Boleticas** (tickets)
3. Las **BoleticaSilla** (asignación de sillas con precios)

---

## 9. Capa de Negocio (Clases/)

### Patrón General

Cada archivo `clsOpe*.cs` sigue el mismo patrón:

```csharp
namespace apiCine.Clases;

public class clsOpe[Nombre]
{
    private readonly CineColombiaContext oCine;        // DbContext inyectado
    public [Entidad] tbl[Nombre] { get; set; }          // Propiedad pública para la entidad

    public clsOpe[Nombre](CineColombiaContext oCine)    // Constructor con DI
    {
        this.oCine = oCine;
        tbl[Nombre] = new [Entidad]();
    }

    public List<[Entidad]> Listar[Nombre]() { ... }     // SELECT *
    public IQueryable Consultar[Nombre](int id) { ... } // SELECT * WHERE id = ?
    public int Agregar() { ... }                        // INSERT
    public int Modificar() { ... }                      // UPDATE
    public int Inactivar(int id) { ... }                // Soft DELETE (no todas)
}
```

### Códigos de Retorno

| Valor | Significado |
|-------|-------------|
| `1` | Éxito (al menos 1 fila afectada en SaveChanges) |
| `0` | Sin cambios (SaveChanges devolvió 0) |
| `-1` | Conflicto / duplicado (unique constraint) |
| `-2` | No encontrado (registro a modificar/inactivar no existe) |
| `-3` | Error de validación (solo en RegistrarVentaCompleta) |

### Auto-generación de IDs

```csharp
if (tblEntidad.Id == 0)
{
    var maxId = oCine.Entidades.Max(e => (int?)e.Id) ?? 0;
    tblEntidad.Id = maxId + 1;
}
```

- **Por qué**: El DbContext usa `ValueGeneratedNever()`, así que el ID debe venir del cliente o generarse manualmente
- **Cuándo se genera**: Si el ID enviado es `0`, se auto-genera tomando el máximo actual + 1
- **Riesgo**: No es thread-safe. Dos peticiones simultáneas pueden obtener el mismo `maxId + 1`

### Explicación de Cada Clase

#### clsOpeVenta (`Clases/clsOpeVenta.cs`)

La clase más compleja del proyecto (168 líneas).

**Métodos**:

| Método | Líneas | Descripción |
|--------|--------|-------------|
| `ListarVentas()` | 18-21 | `oCine.Venta.ToList()` — devuelve todas las ventas |
| `ConsultarVenta(idVenta)` | 23-28 | `from x in oCine.Venta where x.IdVenta == idVenta select x` — LINQ Query sintax para filtrar por ID |
| `ConsultarDetalleVenta(idVenta)` | 30-35 | `from b in oCine.Boleticas where b.IdVenta == idVenta select b` — devuelve las boleticas de una venta |
| `Agregar()` | 37-46 | Auto-genera ID si es 0, añade y guarda |
| `RegistrarVentaCompleta(dto)` | 48-129 | Ver [sección 13](#13-flujo-registrarventacompleta) |
| `Modificar()` | 131-152 | Busca por ID, copia propiedades, guarda |
| `Inactivar(idVenta)` | 154-167 | Busca por ID, setea `Estado = false`, guarda |

**RegistrarVentaCompleta paso a paso** (el método más importante del proyecto):

```csharp
// Línea 50-51: Validación de entrada
if (dto.Boleticas == null || dto.Boleticas.Count == 0)
    return -3;

// Líneas 53-55: Obtener IDs máximos actuales para Venta, Boletica y BoleticaSilla
var maxIdVenta = oCine.Venta.Max(v => (int?)v.IdVenta) ?? 0;
var maxIdBoletica = oCine.Boleticas.Max(b => (int?)b.IdBoletica) ?? 0;
var maxIdBoleticaSilla = oCine.BoleticaSillas.Max(bs => (int?)bs.IdBoleticaSilla) ?? 0;

// Líneas 57-68: Crear objeto Venta con los datos del DTO
var venta = new Ventum { IdVenta = maxIdVenta + 1, ... Estado = true };

// Línea 70: Agregar la venta al contexto
oCine.Venta.Add(venta);

// Líneas 72-84: Detección de sillas ya ocupadas
// 1. Obtiene todos los IDs de función únicos del DTO
var funcionesIds = dto.Boleticas.Select(b => b.IdFuncion).Distinct().ToList();
// 2. Para cada función, consulta qué sillas ya están ocupadas
//    (JOIN entre BoleticaSilla y Boletica), guardándolas en un Dictionary<int, HashSet<int>>
// 3. Inicializa un segundo Dictionary para rastrear sillas solicitadas en ESTE request

// Líneas 86-126: Por cada boletica en el DTO:
//   - Incrementa contador de ID de boletica
//   - Crea Boletica y la agrega al contexto
//   - Por cada silla en esa boletica:
//       * Si la silla ya está ocupada (en BD) O ya fue solicitada (en este request)
//         → return -1 (silla duplicada)
//       * Si está disponible, la agrega al HashSet de sillas solicitadas
//       * Incrementa contador de ID de boleticaSilla
//       * Crea BoleticaSilla y la agrega al contexto

// Línea 128: Guarda todo en una sola transacción
return oCine.SaveChanges() > 0 ? venta.IdVenta : 0;
// Retorna el ID de la venta creada (en lugar de 1) para que el cliente sepa qué venta se creó
```

#### clsOpeBoletica (`Clases/clsOpeBoletica.cs`)

Métodos: `Listar`, `Consultar`, `Agregar`, `Modificar`, `Inactivar`.

**Agregar** (líneas 30-39): Auto-genera ID si es 0, agrega y guarda. **No** verifica duplicados en `Agregar()`.

**Modificar** (líneas 41-57): Busca por ID, copia `IdVenta`, `IdFuncion`, `Estado`.

**Inactivar** (líneas 59-72): Busca por ID, setea `Estado = 0` (soft delete lógico).

#### clsOpeBoleticaSilla (`Clases/clsOpeBoleticaSilla.cs`)

**Agregar** (líneas 30-60):
1. Auto-genera ID si es 0
2. **Verifica duplicados**: Busca si la silla ya está asignada a alguna boletica de la misma función
3. Si existe duplicado → `return -1`
4. Si no existe → agrega y guarda

**Modificar** (líneas 62-102):
1. Busca la boleticaSilla por ID
2. Obtiene la función asociada a la boletica
3. Verifica que la nueva silla no esté ocupada por otra boleticaSilla (excluyéndose a sí misma con `bs.IdBoleticaSilla != tblBoleticaSilla.IdBoleticaSilla`)
4. Copia todas las propiedades y guarda

#### clsOpeUsuarioSistema (`Clases/clsOpeUsuarioSistema.cs`)

**ConsultarLogin** (líneas 30-36): Método especial que filtra por `Username` y `PasswordHash`.

```csharp
return from x in oCine.UsuarioSistemas
       where x.Username == username
       && x.PasswordHash == passwordHash
       select x;
```

- **No usa hashing seguro**: Compara el passwordHash directamente (texto plano contra el hash almacenado)
- **No genera tokens**: Solo devuelve el registro del usuario si existe
- **No hay sesión**: El cliente debe enviar username+passwordHash en cada request

**Agregar** (líneas 38-65):
1. Auto-genera ID
2. Verifica que el `Username` no exista ya → si existe, `return -1`
3. Verifica que el `IdEmpleado` no tenga ya un usuario → si existe, `return -1`
4. Guarda

**Modificar** (líneas 67-108):
1. Busca por ID
2. Verifica que el nuevo username no esté en uso (excluyendo el registro actual)
3. Verifica que el nuevo IdEmpleado no tenga ya usuario (excluyendo el registro actual)
4. Copia propiedades y guarda

#### clsOpePelicula (`Clases/clsOpePelicula.cs`)

**Inactivar** (líneas 66-78): **No implementado** — solo retorna `1` sin hacer nada:

```csharp
public int Inactivar(int idPelicula)
{
    var pelicula = (from x in oCine.Peliculas ...).FirstOrDefault();
    if (pelicula == null) return -2;
    return 1;  // ← Siempre retorna éxito sin modificar nada
}
```

#### clsOpeFuncion (`Clases/clsOpeFuncion.cs`)

**Agregar** (líneas 30-50):
1. Auto-genera ID
2. Verifica duplicado de (IdSala, FechaFuncion, HoraInicio) → `return -1` si existe

**Modificar** (líneas 52-88):
1. Verifica que la función exista
2. Verifica que (IdSala, FechaFuncion, HoraInicio) nuevo no entre en conflicto (excluyéndose a sí misma)
3. Copia propiedades y guarda

#### clsOpeCliente (`Clases/clsOpeCliente.cs`)

**Agregar** (líneas 30-49):
1. Auto-genera ID
2. Verifica duplicado de (IdTipoDoc, NumDocumento) → `return -1` si existe

#### clsOpeEmpleado (`Clases/clsOpeEmpleado.cs`)

Mismo patrón que Cliente con verificación de (IdTipoDoc, NumDocumento).

#### clsOpeCiudad, clsOpeClasificacion, clsOpeDepartamento

Estas tres clases tienen **paginación** en `Listar()`:

```csharp
public List<Ciudad> ListarCiudades(int page = 1, int pageSize = 50)
{
    return oCine.Ciudads
        .OrderBy(c => c.IdCiudad)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();
}
```

- **page**: Número de página (default: 1)
- **pageSize**: Tamaño de página (default: 50)
- **OrderBy**: Ordena por ID antes de paginar (importante para consistencia)
- **Skip/Take**: Implementa paginación offset-based
- **Sin método Inactivar**: Estas clases no tienen eliminación lógica

#### clsOpeGenero, clsOpeFormato, clsOpeIdioma, clsOpePais, etc. (catálogos simples)

Siguen el patrón básico:
- `Listar()` → ToList()
- `Consultar()` → IQueryable filtrado por ID
- `Agregar()` → auto-ID + verificación de duplicado de ID
- `Modificar()` → buscar + copiar propiedades
- **Sin Inactivar**: La mayoría de catálogos no tienen eliminación

**Diferencia clave**: La verificación de duplicado en `Agregar()` de estas clases verifica solo por `Id` (no por campos únicos de negocio como nombre):

```csharp
var existe = (from x in oCine.Generos
              where x.IdGenero == tblGenero.IdGenero
              select x).Any();
```

Esto significa que si el ID se auto-genera correctamente (sigue la secuencia), nunca dará duplicado. La verificación es redundante.

#### clsOpePais, clsOpeDistribuidora, clsOpeProductora, clsOpeProfesion, clsOpeRol, clsOpeTipoCliente, clsOpeTipoDocumento, clsOpeTipoSala, clsOpeTipoSilla, clsOpeTipoTelefono, clsOpeMetodoPago

Todas siguen exactamente el mismo patrón del `clsOpeGenero`.

#### clsOpeSilla (`Clases/clsOpeSilla.cs`)

**Agregar** (líneas 30-50):
1. Auto-genera ID
2. Verifica duplicado de (IdSala, Fila, Numero) → `return -1` si existe

**Modificar** (líneas 52-84):
1. Verifica que la silla exista
2. Verifica que (IdSala, Fila, Numero) nuevo no entre en conflicto

#### clsOpeSala (`Clases/clsOpeSala.cs`)

Sin verificación de duplicados en `Agregar()`.

#### clsOpeTeatro (`Clases/clsOpeTeatro.cs`)

Sin verificación de duplicados en `Agregar()`.

#### clsOpeTarjetaFidelizacion (`Clases/clsOpeTarjetaFidelizacion.cs`)

**Agregar** (líneas 30-48):
1. Auto-genera ID
2. Verifica que el IdCliente no tenga ya tarjeta → `return -1` si existe

**Modificar** (líneas 50-81):
1. Verifica que exista
2. Verifica que el nuevo IdCliente no tenga conflicto

#### clsOpeDireccionCliente (`Clases/clsOpeDireccionCliente.cs`)

Sin verificación de duplicados.

#### clsOpeDireccionEmpleado (`Clases/clsOpeDireccionEmpleado.cs`)

Sin verificación de duplicados.

#### clsOpeTelefonoCliente (`Clases/clsOpeTelefonoCliente.cs`)

Sin verificación de duplicados.

#### clsOpeTelefonoEmpleado (`Clases/clsOpeTelefonoEmpleado.cs`)

Sin verificación de duplicados.

#### clsOpeEmpleadoProfesion (`Clases/clsOpeEmpleadoProfesion.cs`)

**Agregar**: Verifica duplicado de (IdEmpleado, IdProfesion).

#### clsOpePeliculaIdioma (`Clases/clsOpePeliculaIdioma.cs`)

**Agregar**: Verifica duplicado de (IdPelicula, IdIdioma).

#### clsOpePeliculaFormato, clsOpePeliculaProductora, clsOpePeliculaDistribuidora

Mismo patrón que PeliculaIdioma (verificación de par único).

---

## 10. Capa de Controladores (Controllers/)

### Patrón General

Todos los controladores siguen la misma estructura:

```csharp
namespace apiCine.Controllers;

[Route("api/[controller]")]
[ApiController]
public class [Entidad]Controller : ControllerBase
{
    private readonly CineColombiaContext oCine;

    public [Entidad]Controller(CineColombiaContext oCine)
    {
        this.oCine = oCine;  // DbContext inyectado
    }

    // GET api/[controller]
    [HttpGet]
    public List<[Entidad]> Listar() { ... }

    // GET api/[controller]/{id}
    [HttpGet("{id}")]
    public IQueryable Consultar(int id) { ... }

    // POST api/[controller]
    [HttpPost]
    public int Agregar([FromBody] [Entidad] entidad) { ... }

    // PUT api/[controller]
    [HttpPut]
    public int Modificar([FromBody] [Entidad] entidad) { ... }

    // PUT api/[controller]/inactivar/{id}
    [HttpPut("inactivar/{id}")]
    public int Inactivar(int id) { ... }  // Solo algunos controladores
}
```

### Notas sobre el código común

1. **`[Route("api/[controller]")]`**: Usa el nombre del controlador sin "Controller" (ej: `PeliculaController` → `api/Pelicula`)
2. **`[ApiController]`**: Habilita validación automática de modelos, binding de parámetros, etc.
3. **El DbContext se inyecta por constructor** (pero las clases clsOpe* se crean manualmente en cada método)
4. **Los métodos retornan tipos concretos** (`List<T>`, `IQueryable`, `int`) en lugar de `IActionResult` en la mayoría de casos
5. **No hay manejo de errores en los controllers** — las excepciones no atrapadas se manejan globalmente en `Program.cs`

### Variantes de Estilo de Respuesta

**Estilo 1 — Retorno directo** (mayoría de controllers):

```csharp
[HttpGet]
public List<Pelicula> ListarPeliculas()
{
    clsOpePelicula oPelicula = new clsOpePelicula(oCine);
    return oPelicula.ListarPeliculas();
}

[HttpPost]
public int Agregar([FromBody] Pelicula pelicula)
{
    clsOpePelicula oPelicula = new clsOpePelicula(oCine);
    oPelicula.tblPelicula = pelicula;
    return oPelicula.Agregar();
}
```

- `GET` devuelve `List<T>` → JSON array
- `GET {id}` devuelve `IQueryable` → JSON object (o array vacío si no existe)
- `POST/PUT` devuelve `int` → código de retorno (1, -1, -2, 0)
- **Problema**: Sin `IActionResult`, no se puede diferenciar entre 200 OK, 404 Not Found, 409 Conflict

**Estilo 2 — IActionResult** (CiudadController, ClasificacionController, DepartamentoController):

```csharp
[HttpPost]
public IActionResult Agregar([FromBody] Ciudad ciudad)
{
    clsOpeCiudad oCiudad = new clsOpeCiudad(oCine);
    oCiudad.tblCiudad = ciudad;
    var resultado = oCiudad.Agregar();

    if (resultado == 1) return CreatedAtAction(nameof(ConsultarCiudad), new { idCiudad = ciudad.IdCiudad }, ciudad);
    if (resultado == -1) return Conflict();
    return BadRequest();
}
```

- **1** → `CreatedAtAction` (201 Created) con URL del nuevo recurso
- **-1** → `Conflict` (409)
- **-2** → `NotFound` (404)
- Otro → `BadRequest` (400)

### Controllers Especiales

#### VentaController

- **`GET api/Venta/{idVenta}/detalle`**: Endpoint adicional que devuelve las boleticas de una venta
- **`POST api/Venta/RegistrarVentaCompleta`**: Endpoint compuesto que acepta `RegistroVentaDto`

#### UsuarioSistemaController

- **`GET api/UsuarioSistema/login?username=...&passwordHash=...`**: Endpoint de login que filtra por credenciales

---

## 11. Base de Datos (QuerBD.sql)

### Resumen del Script (1346 líneas)

El script `QuerBD.sql` crea la base de datos `CineColombia` completa con:

1. **Creación de BD**: `CREATE DATABASE CineColombia` (con drop si existe)
2. **Creación de 27 tablas** con sus columnas, tipos y restricciones
3. **13 índices únicos** para prevenir duplicados
4. **Foreign Keys** entre tablas relacionadas
5. **Extended Properties** (descripciones de columnas)
6. **Datos de prueba (seed data)**

### Orden de creación de tablas

El script sigue un orden específico para respetar las dependencias de FK:

1. Catálogos independientes: PAIS, DEPARTAMENTO, CIUDAD, GENERO, TIPO_CLIENTE, TIPO_DOCUMENTO, IDIOMA, FORMATO, ROL, PROFESION, TIPO_TELEFONO, TIPO_SALA, TIPO_SILLA, CLASIFICACION, METODO_PAGO
2. Entidades que referencian catálogos: PRODUCTORA, DISTRIBUIDORA, TEATRO, PELICULA, CLIENTE, EMPLEADO
3. Entidades que dependen de las anteriores: SALA, SILLA, PELICULA_IDIOMA, PELICULA_FORMATO, PELICULA_PRODUCTORA, PELICULA_DISTRIBUIDORA, FUNCION, DIRECCION_CLIENTE, TELEFONO_CLIENTE, TARJETA_FIDELIZACION, TELEFONO_EMPLEADO, DIRECCION_EMPLEADO, EMPLEADO_PROFESION, USUARIO_SISTEMA
4. Transaccionales: VENTA, BOLETICA, BOLETICA_SILLA

### Extended Properties

El script usa `sp_addextendedproperty` para documentar columnas. Ejemplos notables:

- **TIPO_DOCUMENTO.codigo**: "CC, CE, TI, PEP, PA"
- **FORMATO.nombre**: "2D, 3D, IMAX, 4DX"
- **ROL.nombre**: "Administrador, Cajero, Supervisor"
- **DIRECCION_CLIENTE.activo**: "El profesor lo pidió explícitamente" (comentario académico)
- **TARJETA_FIDELIZACION.id_cliente**: "UK — un cliente, una tarjeta"
- **VENTA.id_cliente**: "NULL permitido — cliente casual"
- **BOLETICA_SILLA.precio_final**: "precio_unitario - descuento"

### Seed Data

El script inserta datos de prueba para demostrar el funcionamiento:

- **3 países** (Colombia, Estados Unidos, México)
- **3 departamentos** (Antioquia, Cundinamarca, Valle del Cauca)
- **3 ciudades** (Medellín, Bogotá, Cali)
- **5 géneros**, **4 clasificaciones**, **2 idiomas**, **4 formatos**
- **3 roles**, **3 profesiones**, **3 tipos de teléfono**
- **3 tipos de sala**, **3 tipos de silla** con precios (12,000 / 18,000 / 25,000)
- **5 métodos de pago** (Efectivo, Débito, Crédito, PSE, Nequi)
- **3 productoras**, **3 distribuidoras**
- **2 teatros** (Cine Colombia Santafé y Oviedo)
- **4 empleados** incluyendo empleado semilla (id=0) para auto-referencia
- **2 salas** (Sala 1 con 5 sillas, Sala 2 IMAX con 5 sillas)
- **3 películas** (Deadpool & Wolverine, Furiosa, Inside Out 2)
- **2 clientes**, **1 tarjeta de fidelización**
- **1 venta** con **1 boletica** y **1 boletica_silla**
- **3 funciones** con horarios y precios

**Técnica del empleado semilla** (líneas 1219-1235):

```sql
-- Se inserta un empleado con id=0, registrado_por=NULL
INSERT INTO EMPLEADO (id_empleado, ...) VALUES (0, ...);
-- Luego se actualizan los TEATRO.registrado_por a 0
-- Finalmente se altera la columna a NOT NULL
ALTER TABLE TEATRO ALTER COLUMN registrado_por int NOT NULL;
```

**Explicación**: Para cumplir con la FK circular (TEATRO.registrado_por → EMPLEADO). Se inserta un empleado "sistema" que no requiere registrador, se usan sus datos para referenciar los demás registros, y luego se vuelve NOT NULL.

---

## 12. Endpoints de la API

### Convención de rutas

- Base URL: `http://localhost:5140`
- Formato: `api/[NombreEntidad]`
- Swagger UI: `http://localhost:5140/swagger`

### Catálogos (GET, GET por ID, POST, PUT — sin Inactivar)

| Controller | GET All | GET by ID | POST | PUT |
|---|---|---|---|---|
| Genero | `GET /api/Genero` | `GET /api/Genero/{idGenero}` | ✅ | ✅ |
| Clasificacion | `GET /api/Clasificacion?page=1&pageSize=50` | `GET /api/Clasificacion/{idClasificacion}` | ✅ (IActionResult) | ✅ |
| Formato | `GET /api/Formato` | `GET /api/Formato/{idFormato}` | ✅ | ✅ |
| Idioma | `GET /api/Idioma` | `GET /api/Idioma/{idIdioma}` | ✅ | ✅ |
| Pais | `GET /api/Pais` | `GET /api/Pais/{idPais}` | ✅ | ✅ |
| Departamento | `GET /api/Departamento?page=1&pageSize=50` | `GET /api/Departamento/{idDepartamento}` | ✅ (IActionResult) | ✅ |
| Ciudad | `GET /api/Ciudad?page=1&pageSize=50` | `GET /api/Ciudad/{idCiudad}` | ✅ (IActionResult) | ✅ |
| Distribuidora | `GET /api/Distribuidora` | `GET /api/Distribuidora/{idDistribuidora}` | ✅ | ✅ |
| Productora | `GET /api/Productora` | `GET /api/Productora/{idProductora}` | ✅ | ✅ |
| Profesion | `GET /api/Profesion` | `GET /api/Profesion/{idProfesion}` | ✅ | ✅ |
| Rol | `GET /api/Rol` | `GET /api/Rol/{idRol}` | ✅ | ✅ |
| TipoCliente | `GET /api/TipoCliente` | `GET /api/TipoCliente/{idTipoCliente}` | ✅ | ✅ |
| TipoDocumento | `GET /api/TipoDocumento` | `GET /api/TipoDocumento/{idTipoDoc}` | ✅ | ✅ |
| TipoSala | `GET /api/TipoSala` | `GET /api/TipoSala/{idTipoSala}` | ✅ | ✅ |
| TipoSilla | `GET /api/TipoSilla` | `GET /api/TipoSilla/{idTipoSilla}` | ✅ | ✅ |
| TipoTelefono | `GET /api/TipoTelefono` | `GET /api/TipoTelefono/{idTipoTelefono}` | ✅ | ✅ |
| MetodoPago | `GET /api/MetodoPago` | `GET /api/MetodoPago/{idMetodoPago}` | ✅ | ✅ |

### Relaciones/Junction Tables (GET, GET por ID, POST, PUT — sin Inactivar)

| Controller | GET All | GET by ID | POST | PUT |
|---|---|---|---|---|
| PeliculaIdioma | `GET /api/PeliculaIdioma` | `GET /api/PeliculaIdioma/{idPeliculaIdioma}` | ✅ | ✅ |
| PeliculaFormato | `GET /api/PeliculaFormato` | `GET /api/PeliculaFormato/{idPeliculaFormato}` | ✅ | ✅ |
| PeliculaProductora | `GET /api/PeliculaProductora` | `GET /api/PeliculaProductora/{idPeliculaProductora}` | ✅ | ✅ |
| PeliculaDistribuidora | `GET /api/PeliculaDistribuidora` | `GET /api/PeliculaDistribuidora/{idPeliculaDistribuidora}` | ✅ | ✅ |
| EmpleadoProfesion | `GET /api/EmpleadoProfesion` | `GET /api/EmpleadoProfesion/{idEmpProfesion}` | ✅ | ✅ |
| TelefonoCliente | `GET /api/TelefonoCliente` | `GET /api/TelefonoCliente/{idTelefono}` | ✅ | ✅ |
| TelefonoEmpleado | `GET /api/TelefonoEmpleado` | `GET /api/TelefonoEmpleado/{idTelefonoEmp}` | ✅ | ✅ |

### Entidades Principales (CRUD + Inactivar)

| Controller | GET All | GET by ID | POST | PUT | PUT Inactivar |
|---|---|---|---|---|---|
| Pelicula | `GET /api/Pelicula` | `GET /api/Pelicula/{idPelicula}` | ✅ | ✅ | `PUT /api/Pelicula/inactivar/{id}` (no-op) |
| Funcion | `GET /api/Funcion` | `GET /api/Funcion/{idFuncion}` | ✅ | ✅ | `PUT /api/Funcion/inactivar/{id}` |
| Cliente | `GET /api/Cliente` | `GET /api/Cliente/{idCliente}` | ✅ | ✅ | `PUT /api/Cliente/inactivar/{id}` |
| Empleado | `GET /api/Empleado` | `GET /api/Empleado/{idEmpleado}` | ✅ | ✅ | `PUT /api/Empleado/inactivar/{id}` |
| Teatro | `GET /api/Teatro` | `GET /api/Teatro/{idTeatro}` | ✅ | ✅ | `PUT /api/Teatro/inactivar/{id}` |
| Sala | `GET /api/Sala` | `GET /api/Sala/{idSala}` | ✅ | ✅ | `PUT /api/Sala/inactivar/{id}` |
| Silla | `GET /api/Silla` | `GET /api/Silla/{idSilla}` | ✅ | ✅ | `PUT /api/Silla/inactivar/{id}` |
| Venta | `GET /api/Venta` | `GET /api/Venta/{idVenta}` | ✅ | ✅ | `PUT /api/Venta/inactivar/{id}` |
| Boletica | `GET /api/Boletica` | `GET /api/Boletica/{idBoletica}` | ✅ | ✅ | `PUT /api/Boletica/inactivar/{id}` |
| BoleticaSilla | `GET /api/BoleticaSilla` | `GET /api/BoleticaSilla/{idBoleticaSilla}` | ✅ | ✅ | `PUT /api/BoleticaSilla/inactivar/{id}` |
| DireccionCliente | `GET /api/DireccionCliente` | `GET /api/DireccionCliente/{idDireccionCli}` | ✅ | ✅ | `PUT /api/DireccionCliente/inactivar/{id}` |
| DireccionEmpleado | `GET /api/DireccionEmpleado` | `GET /api/DireccionEmpleado/{idDireccionEmp}` | ✅ | ✅ | `PUT /api/DireccionEmpleado/inactivar/{id}` |
| TarjetaFidelizacion | `GET /api/TarjetaFidelizacion` | `GET /api/TarjetaFidelizacion/{idTarjeta}` | ✅ | ✅ | `PUT /api/TarjetaFidelizacion/inactivar/{id}` |
| UsuarioSistema | `GET /api/UsuarioSistema` | `GET /api/UsuarioSistema/{idUsuario}` | ✅ | ✅ | `PUT /api/UsuarioSistema/inactivar/{id}` |

### Endpoints Especiales

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/` | Mensaje de bienvenida |
| GET | `/swagger` | Swagger UI |
| GET | `/api/Venta/{id}/detalle` | Boleticas de una venta específica |
| POST | `/api/Venta/RegistrarVentaCompleta` | Crear venta completa con boleticas y sillas |
| GET | `/api/UsuarioSistema/login?username=&passwordHash=` | Login de usuario |

---

## 13. Flujo RegistrarVentaCompleta

El endpoint más complejo del proyecto. Acepta un `RegistroVentaDto` y crea en una sola transacción:

1. **Venta** (cabecera de la transacción)
2. **Boleticas** (tickets, uno por función)
3. **BoleticaSillas** (asientos asignados a cada ticket)

### Flujo Detallado

```
Paso 1: Validar que el DTO tenga al menos una boletica
  ↓
Paso 2: Obtener el máximo ID actual de Venta, Boletica y BoleticaSilla
  ↓
Paso 3: Crear objeto Venta con ID = maxIdVenta + 1
  ↓
Paso 4: Agregar la Venta al contexto (aún no guardada)
  ↓
Paso 5: Para cada función en el request:
    ├── Consultar sillas ya ocupadas para esa función (JOIN BoleticaSilla ↔ Boletica)
    └── Inicializar un HashSet para rastrear sillas solicitadas en este request
  ↓
Paso 6: Para cada boletica en el request:
    ├── Crear Boletica con ID = maxIdBoletica + 1
    ├── Agregar la Boletica al contexto
    ├── Para cada silla en esa boletica:
    │   ├── Si la silla ya está ocupada (en BD o en este request) → return -1
    │   └── Si está libre:
    │       ├── Registrar en HashSet de sillas solicitadas
    │       ├── Crear BoleticaSilla con ID = maxIdBoleticaSilla + 1
    │       └── Agregar al contexto
    └── (siguiente boletica)
  ↓
Paso 7: SaveChanges() → SQL Transaction implícita
  ├── Si éxito → return IdVenta (el ID de la venta creada)
  └── Si fallo → return 0 (todo se revierte)
```

### Detección de Sillas Duplicadas

```csharp
// Consulta sillas ocupadas en BD para cada función
sillasOcupadasPorFuncion[idFuncion] = (from bs in oCine.BoleticaSillas
                                       join b in oCine.Boleticas on bs.IdBoletica equals b.IdBoletica
                                       where b.IdFuncion == idFuncion
                                       select bs.IdSilla).ToHashSet();

// Verificación en el loop de sillas
if (sillasOcupadasPorFuncion[boleticaDto.IdFuncion].Contains(sillaDto.IdSilla)  // Ya ocupada en BD
    || sillasEnRequest[boleticaDto.IdFuncion].Contains(sillaDto.IdSilla))        // Ya solicitada en este request
    return -1;

sillasEnRequest[boleticaDto.IdFuncion].Add(sillaDto.IdSilla);
```

- **Dos niveles de protección**: contra datos existentes (BD) y contra el mismo request
- **`sillasEnRequest`**: HashSet que evita que el mismo request intente comprar la misma silla dos veces

### Transaccionalidad

Al llamar a `SaveChanges()` después de agregar todos los objetos al contexto, EF Core ejecuta todas las operaciones en una **transacción SQL implícita**. Si algo falla, todo se revierte (no hay medio-insertadas).

### ¿Por qué no usa transacción explícita?

Se podría haber usado `using var transaction = oCine.Database.BeginTransaction()` pero el comportamiento por defecto de `SaveChanges` ya es transaccional: agrupa todos los INSERT/UPDATE en una sola transacción.

---

## 14. Decisiones Técnicas y Limitaciones

### 14.1 Sin Autenticación ni Autorización

**Decisión**: La API no tiene JWT, tokens ni middleware de autenticación.

**Implicación**: Cualquier persona que acceda a la URL puede consumir todos los endpoints. El login (`GET /api/UsuarioSistema/login`) solo verifica credenciales contra la BD sin generar sesión ni token.

**Posible mejora**: Implementar JWT bearer token + middleware de autenticación.

### 14.2 Auto-generación Manual de IDs

**Decisión**: `ValueGeneratedNever()` en todas las entidades + `Max(id) + 1` en cada clase.

**Problema de concurrencia**: Si dos requests intentan crear un registro al mismo tiempo, ambos pueden obtener el mismo `maxId + 1`, causando violación de PK.

**Por qué se hizo así**: Simplifica las pruebas y evita usar `SET IDENTITY_INSERT ON` cuando se insertan IDs específicos (como el empleado id=0).

**Posible mejora**: Usar `IDENTITY` de SQL Server o `SEQUENCE` para IDs auto-incrementales.

### 14.3 Sin Capa de Servicios o Repositorios Abstractos

**Decisión**: Las clases `clsOpe*` actúan como repositorio y servicio a la vez, sin interfaces.

**Implicación**: No hay separación de preocupaciones entre acceso a datos y lógica de negocio. Difícil de testear con unit tests (no se puede mockear).

### 14.4 Sin AutoMapper ni DTOs (excepto 1)

**Decisión**: Las entidades se exponen directamente al cliente HTTP (excepto para `RegistrarVentaCompleta`).

**Implicación**: Se exponen todos los campos de la BD al cliente. Si una entidad cambia, el contrato de la API también cambia.

### 14.5 Instanciación Manual de clsOpe* en Controllers

**Decisión**: Cada método de controller crea `new clsOpe*(oCine)` en lugar de usar DI.

**Implicación**: No se puede usar lifetime management avanzado, y el patrón es más verbose.

### 14.6 Retorno de IQueryable desde Controllers

**Decisión**: `Consultar*(id)` retorna `IQueryable` directamente.

**Riesgo**: El `IQueryable` se ejecuta cuando se serializa a JSON, lo que significa que la consulta SQL se ejecuta en el momento de la serialización, no en el controller. Si hay un error de serialización, la excepción ocurre después del controller, en el middleware.

### 14.7 Sin Paginación Generalizada

**Decisión**: Solo Ciudad, Clasificación y Departamento tienen paginación.

**Implicación**: Entidades como Venta, Cliente o Película pueden devolver miles de registros en una sola llamada, afectando el rendimiento.

### 14.8 clsOpePelicula.Inactivar() es No-op

**Decisión**: El método `Inactivar` de Película no hace nada (retorna 1 sin modificar).

**Posible razón**: Era parte del requerimiento inicial pero la funcionalidad no se completó.

### 14.9 Namespace Inconsistente

Los controladores usan `namespace apiCine.Controllers`, las clases usan `namespace apiCine.Clases`, y los modelos usan `namespace CineColombiaApi.Models`. Esto sugiere que el proyecto fue renombrado de `apiCine` a `CineColombiaApi` pero los namespaces no se actualizaron.

### 14.10 Stale File: WeatherForecast.cs

El archivo `WeatherForecast.cs` es un residuo de la plantilla `dotnet new webapi` y no se usa en ningún controlador.

---

## 15. Posibles Preguntas de Sustentación

### Pregunta 1: ¿Por qué no hay autenticación?

> **Respuesta**: El proyecto se enfoca en la lógica de negocio y CRUD. El endpoint de login existe pero es básico (solo consulta a BD sin generar tokens). Para producción se debería implementar JWT con middleware de autenticación. En el contexto académico, decidimos priorizar la funcionalidad de gestión sobre la seguridad.

### Pregunta 2: ¿Cómo se generan los IDs y por qué no usamos IDENTITY de SQL Server?

> **Respuesta**: Usamos `ValueGeneratedNever()` en Fluent API y generamos IDs con `Max(id) + 1` en cada clase `clsOpe*`. Esto nos da control total sobre los IDs, permitiendo insertar registros con IDs específicos (como el empleado 0). La desventaja es que no es thread-safe: dos peticiones simultáneas podrían obtener el mismo ID. En producción usaríamos `IDENTITY` o `SEQUENCE`.

### Pregunta 3: ¿Por qué algunas clases retornan int y otras IActionResult?

> **Respuesta**: Hay dos estilos de respuesta. La mayoría de controladores retornan `int` (códigos 1, -1, -2, 0) por simplicidad. Ciudad, Clasificación y Departamento retornan `IActionResult` con códigos HTTP semánticos (201 Created, 409 Conflict, 404 NotFound). La inconsistencia es porque diferentes desarrolladores trabajaron en diferentes controladores.

### Pregunta 4: ¿Cómo funciona RegistarVentaCompleta?

> **Respuesta**: Es el endpoint más complejo. Recibe un DTO anidado con datos de venta, boleticas y sillas. Primero obtiene los IDs máximos actuales, luego verifica que ninguna silla esté ocupada (consultando BD + rastreando las solicitadas en el mismo request), y finalmente crea todos los registros en una sola transacción de EF Core. Si detecta una silla duplicada, retorna -1 y no guarda nada.

### Pregunta 5: ¿Qué pasa si dos usuarios compran la misma silla al mismo tiempo?

> **Respuesta**: Con la implementación actual, ambos requests leerían la BD antes de que cualquiera haya guardado, ambos verían la silla como libre, y ambos la insertarían. El segundo `SaveChanges()` lanzaría una excepción por violación del unique index `BOLETICA_SILLA_index_12` (id_boletica + id_silla). No manejamos esa excepción explícitamente, así que el segundo usuario recibiría un error 500. Se podría mejorar usando un `BEGIN TRANSACTION` con `SERIALIZABLE` isolation level o un lock optimista.

### Pregunta 6: ¿Por qué no hay AutoMapper?

> **Respuesta**: Para mantener el proyecto simple y evitar dependencias adicionales. Al ser un proyecto académico, decidimos exponer las entidades directamente como respuesta JSON. Solo tenemos un DTO (`RegistroVentaDto`) para el caso especial de venta completa. En un proyecto real, usaríamos AutoMapper para separar el modelo de datos del contrato de API.

### Pregunta 7: ¿Qué hace el middleware de excepciones?

> **Respuesta**: En `Program.cs` configuramos `UseExceptionHandler` que captura cualquier excepción no manejada durante el procesamiento de un request. Retorna un 500 con un JSON que contiene un mensaje genérico y el detalle de la excepción. Esto evita que los detalles técnicos se filtren al cliente.

### Pregunta 8: ¿Cómo se relacionan las entidades en la BD?

> **Respuesta**: La BD sigue un esquema relacional normalizado. Por ejemplo:
> - PAIS → DEPARTAMENTO → CIUDAD → TEATRO (jerarquía geográfica)
> - PELICULA → PELICULA_IDIOMA → IDIOMA (muchos a muchos)
> - CLIENTE ← VENTA → BOLETICA → BOLETICA_SILLA → SILLA (transacción de venta)
> - EMPLEADO → USUARIO_SISTEMA → ROL (acceso al sistema)

### Pregunta 9: ¿Por qué el método Inactivar de Película no hace nada?

> **Respuesta**: Es un bug o feature incompleta. El método `clsOpePelicula.Inactivar()` busca la película por ID pero retorna 1 sin modificar nada. Posiblemente el requerimiento original pedía inactivar películas pero no se implementó. Las demás entidades (Cliente, Empleado, etc.) sí implementan correctamente el soft delete seteando `Activo = false`.

### Pregunta 10: ¿Cómo se implementa la paginación?

> **Respuesta**: Solo en Ciudad, Clasificación y Departamento. Usamos `Skip()` y `Take()` de LINQ con parámetros `page` y `pageSize` (por defecto 1 y 50 respectivamente). Los resultados se ordenan por ID antes de paginar para asegurar consistencia entre páginas. Las demás entidades devuelven todos los registros sin paginar.

### Pregunta 11: ¿Qué políticas CORS tiene la API?

> **Respuesta**: Configuramos una política llamada "CineCors" que permite cualquier origen (`AllowAnyOrigin`), cualquier header (`AllowAnyHeader`) y cualquier método HTTP (`AllowAnyMethod`). Esto significa que cualquier aplicación web (Angular, React, etc.) puede consumir la API sin restricciones de CORS.

### Pregunta 12: ¿Cómo maneja las fechas la API?

> **Respuesta**: Las fechas se manejan como `DateTime` o `DateOnly` en C# y se serializan como ISO 8601 en JSON. El DbContext configura columnas específicas como `datetime` en SQL Server (ej: `fecha_registro`, `hora_inicio`) o `date` (ej: `fecha_funcion`, `anio_estreno`).

### Pregunta 13: ¿Por qué algunos controladores no tienen método Inactivar?

> **Respuesta**: Los catálogos (Genero, Formato, Idioma, etc.) no tienen Inactivar porque son datos de referencia que raramente se eliminan. Las entidades principales (Pelicula, Cliente, Empleado, etc.) sí tienen Inactivar para soft delete. Las tablas de relación (PeliculaIdioma, etc.) tampoco tienen Inactivar porque las relaciones se manejan por eliminación física o simplemente no se requiere.

### Pregunta 14: ¿Cómo se configura la conexión a BD?

> **Respuesta**: La cadena de conexión está en `appsettings.json` en la sección `ConnectionStrings.cnx`. En `Program.cs` se lee con `builder.Configuration.GetConnectionString("cnx")` y se valida que no esté vacía. Luego se pasa a `AddDbContext` con `UseSqlServer()`. El proyecto usa SQL Server LocalDB para desarrollo.

### Pregunta 15: ¿Qué pasa si la BD no existe?

> **Respuesta**: El script `QuerBD.sql` debe ejecutarse manualmente antes de iniciar la API. Si la BD no existe, EF Core no la crea automáticamente (no usamos `EnsureCreated()` ni migrations automáticas). La API fallará al intentar la primera consulta con un error de conexión a BD, que será capturado por el middleware de excepciones y retornado como 500.
