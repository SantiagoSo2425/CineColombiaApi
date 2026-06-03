# Documentación de API - CineColombia

## Información General

- **Base URL:** `http://<host>:<port>`
- **Formato:** JSON
- **Autenticación:** Ninguna (sin JWT ni tokens)
- **Swagger:** `http://<host>:<port>/swagger`
- **Route Pattern:** `api/[controller]`
- **Base de datos:** SQL Server con Entity Framework Core
- **CORS:** Abierto (configurable vía `Cors:Origins`)

## Patrón General de Respuestas

La mayoría de los endpoints devuelven directamente los objetos del modelo. Algunos (Departamento, Ciudad, Clasificacion) incluyen paginación con headers HTTP apropiados.

### Códigos de retorno en métodos POST/PUT

| Valor | Significado |
|-------|-------------|
| `1` | Éxito |
| `-1` | Conflicto (duplicado) |
| `-2` | No encontrado |

En controladores que retornan `IActionResult` (Departamento, Ciudad, Clasificacion):
| HTTP Code | Significado |
|-----------|-------------|
| `201 Created` | POST exitoso |
| `200 OK` | PUT exitoso |
| `409 Conflict` | Duplicado |
| `404 Not Found` | No encontrado |
| `400 Bad Request` | Error general |

### Errores globales (500)
```json
{
  "mensaje": "Error interno del servidor",
  "detalle": "mensaje de la excepción"
}
```

## Endpoints por Controlador

### Catálogos (GET, GET/{id}, POST, PUT) — Sin `inactivar`

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/Genero` | Lista todos los géneros |
| GET | `/api/Genero/{idGenero}` | Consulta un género por ID |
| POST | `/api/Genero` | Crea un nuevo género |
| PUT | `/api/Genero` | Modifica un género existente |
| GET | `/api/Clasificacion?page=1&pageSize=50` | Lista clasificaciones (paginado) |
| GET | `/api/Clasificacion/{idClasificacion}` | Consulta una clasificación |
| POST | `/api/Clasificacion` | Crea una clasificación |
| PUT | `/api/Clasificacion` | Modifica una clasificación |
| GET | `/api/Formato` | Lista todos los formatos |
| GET | `/api/Formato/{idFormato}` | Consulta un formato |
| POST | `/api/Formato` | Crea un formato |
| PUT | `/api/Formato` | Modifica un formato |
| GET | `/api/Idioma` | Lista todos los idiomas |
| GET | `/api/Idioma/{idIdioma}` | Consulta un idioma |
| POST | `/api/Idioma` | Crea un idioma |
| PUT | `/api/Idioma` | Modifica un idioma |
| GET | `/api/Pais` | Lista todos los países |
| GET | `/api/Pais/{idPais}` | Consulta un país |
| POST | `/api/Pais` | Crea un país |
| PUT | `/api/Pais` | Modifica un país |
| GET | `/api/Departamento?page=1&pageSize=50` | Lista departamentos (paginado) |
| GET | `/api/Departamento/{idDepartamento}` | Consulta un departamento |
| POST | `/api/Departamento` | Crea un departamento |
| PUT | `/api/Departamento` | Modifica un departamento |
| GET | `/api/Ciudad?page=1&pageSize=50` | Lista ciudades (paginado) |
| GET | `/api/Ciudad/{idCiudad}` | Consulta una ciudad |
| POST | `/api/Ciudad` | Crea una ciudad |
| PUT | `/api/Ciudad` | Modifica una ciudad |
| GET | `/api/Distribuidora` | Lista distribuidoras |
| GET | `/api/Distribuidora/{idDistribuidora}` | Consulta una distribuidora |
| POST | `/api/Distribuidora` | Crea una distribuidora |
| PUT | `/api/Distribuidora` | Modifica una distribuidora |
| GET | `/api/Productora` | Lista productoras |
| GET | `/api/Productora/{idProductora}` | Consulta una productora |
| POST | `/api/Productora` | Crea una productora |
| PUT | `/api/Productora` | Modifica una productora |
| GET | `/api/Profesion` | Lista profesiones |
| GET | `/api/Profesion/{idProfesion}` | Consulta una profesión |
| POST | `/api/Profesion` | Crea una profesión |
| PUT | `/api/Profesion` | Modifica una profesión |
| GET | `/api/Rol` | Lista roles |
| GET | `/api/Rol/{idRol}` | Consulta un rol |
| POST | `/api/Rol` | Crea un rol |
| PUT | `/api/Rol` | Modifica un rol |
| GET | `/api/TipoCliente` | Lista tipos de cliente |
| GET | `/api/TipoCliente/{idTipoCliente}` | Consulta un tipo de cliente |
| POST | `/api/TipoCliente` | Crea un tipo de cliente |
| PUT | `/api/TipoCliente` | Modifica un tipo de cliente |
| GET | `/api/TipoDocumento` | Lista tipos de documento |
| GET | `/api/TipoDocumento/{idTipoDocumento}` | Consulta un tipo de documento |
| POST | `/api/TipoDocumento` | Crea un tipo de documento |
| PUT | `/api/TipoDocumento` | Modifica un tipo de documento |
| GET | `/api/TipoSala` | Lista tipos de sala |
| GET | `/api/TipoSala/{idTipoSala}` | Consulta un tipo de sala |
| POST | `/api/TipoSala` | Crea un tipo de sala |
| PUT | `/api/TipoSala` | Modifica un tipo de sala |
| GET | `/api/TipoSilla` | Lista tipos de silla |
| GET | `/api/TipoSilla/{idTipoSilla}` | Consulta un tipo de silla |
| POST | `/api/TipoSilla` | Crea un tipo de silla |
| PUT | `/api/TipoSilla` | Modifica un tipo de silla |
| GET | `/api/TipoTelefono` | Lista tipos de teléfono |
| GET | `/api/TipoTelefono/{idTipoTelefono}` | Consulta un tipo de teléfono |
| POST | `/api/TipoTelefono` | Crea un tipo de teléfono |
| PUT | `/api/TipoTelefono` | Modifica un tipo de teléfono |
| GET | `/api/MetodoPago` | Lista métodos de pago |
| GET | `/api/MetodoPago/{idMetodoPago}` | Consulta un método de pago |
| POST | `/api/MetodoPago` | Crea un método de pago |
| PUT | `/api/MetodoPago` | Modifica un método de pago |

### Tablas Relacionales (GET, GET/{id}, POST, PUT) — Sin `inactivar`

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/PeliculaIdioma` | Lista relaciones película-idioma |
| GET | `/api/PeliculaIdioma/{idPeliculaIdioma}` | Consulta una relación |
| POST | `/api/PeliculaIdioma` | Crea una relación |
| PUT | `/api/PeliculaIdioma` | Modifica una relación |
| GET | `/api/PeliculaFormato` | Lista relaciones película-formato |
| GET | `/api/PeliculaFormato/{idPeliculaFormato}` | Consulta una relación |
| POST | `/api/PeliculaFormato` | Crea una relación |
| PUT | `/api/PeliculaFormato` | Modifica una relación |
| GET | `/api/PeliculaProductora` | Lista relaciones película-productora |
| GET | `/api/PeliculaProductora/{idPeliculaProductora}` | Consulta una relación |
| POST | `/api/PeliculaProductora` | Crea una relación |
| PUT | `/api/PeliculaProductora` | Modifica una relación |
| GET | `/api/PeliculaDistribuidora` | Lista relaciones película-distribuidora |
| GET | `/api/PeliculaDistribuidora/{idPeliculaDistribuidora}` | Consulta una relación |
| POST | `/api/PeliculaDistribuidora` | Crea una relación |
| PUT | `/api/PeliculaDistribuidora` | Modifica una relación |
| GET | `/api/EmpleadoProfesion` | Lista relaciones empleado-profesión |
| GET | `/api/EmpleadoProfesion/{idEmpProfesion}` | Consulta una relación |
| POST | `/api/EmpleadoProfesion` | Crea una relación |
| PUT | `/api/EmpleadoProfesion` | Modifica una relación |
| GET | `/api/TelefonoCliente` | Lista teléfonos de cliente |
| GET | `/api/TelefonoCliente/{idTelefono}` | Consulta un teléfono |
| POST | `/api/TelefonoCliente` | Crea un teléfono |
| PUT | `/api/TelefonoCliente` | Modifica un teléfono |
| GET | `/api/TelefonoEmpleado` | Lista teléfonos de empleado |
| GET | `/api/TelefonoEmpleado/{idTelefonoEmp}` | Consulta un teléfono |
| POST | `/api/TelefonoEmpleado` | Crea un teléfono |
| PUT | `/api/TelefonoEmpleado` | Modifica un teléfono |

### Entidades Principales (GET, GET/{id}, POST, PUT, PUT inactivar/{id})

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/Pelicula` | Lista películas |
| GET | `/api/Pelicula/{idPelicula}` | Consulta una película |
| POST | `/api/Pelicula` | Crea una película |
| PUT | `/api/Pelicula` | Modifica una película |
| PUT | `/api/Pelicula/inactivar/{idPelicula}` | Inactiva una película |
| GET | `/api/Funcion` | Lista funciones |
| GET | `/api/Funcion/{idFuncion}` | Consulta una función |
| POST | `/api/Funcion` | Crea una función |
| PUT | `/api/Funcion` | Modifica una función |
| PUT | `/api/Funcion/inactivar/{idFuncion}` | Inactiva una función |
| GET | `/api/Cliente` | Lista clientes |
| GET | `/api/Cliente/{idCliente}` | Consulta un cliente |
| POST | `/api/Cliente` | Crea un cliente |
| PUT | `/api/Cliente` | Modifica un cliente |
| PUT | `/api/Cliente/inactivar/{idCliente}` | Inactiva un cliente |
| GET | `/api/Empleado` | Lista empleados |
| GET | `/api/Empleado/{idEmpleado}` | Consulta un empleado |
| POST | `/api/Empleado` | Crea un empleado |
| PUT | `/api/Empleado` | Modifica un empleado |
| PUT | `/api/Empleado/inactivar/{idEmpleado}` | Inactiva un empleado |
| GET | `/api/Teatro` | Lista teatros |
| GET | `/api/Teatro/{idTeatro}` | Consulta un teatro |
| POST | `/api/Teatro` | Crea un teatro |
| PUT | `/api/Teatro` | Modifica un teatro |
| PUT | `/api/Teatro/inactivar/{idTeatro}` | Inactiva un teatro |
| GET | `/api/Sala` | Lista salas |
| GET | `/api/Sala/{idSala}` | Consulta una sala |
| POST | `/api/Sala` | Crea una sala |
| PUT | `/api/Sala` | Modifica una sala |
| PUT | `/api/Sala/inactivar/{idSala}` | Inactiva una sala |
| GET | `/api/Silla` | Lista sillas |
| GET | `/api/Silla/{idSilla}` | Consulta una silla |
| POST | `/api/Silla` | Crea una silla |
| PUT | `/api/Silla` | Modifica una silla |
| PUT | `/api/Silla/inactivar/{idSilla}` | Inactiva una silla |
| GET | `/api/TarjetaFidelizacion` | Lista tarjetas de fidelización |
| GET | `/api/TarjetaFidelizacion/{idTarjeta}` | Consulta una tarjeta |
| POST | `/api/TarjetaFidelizacion` | Crea una tarjeta |
| PUT | `/api/TarjetaFidelizacion` | Modifica una tarjeta |
| PUT | `/api/TarjetaFidelizacion/inactivar/{idTarjeta}` | Inactiva una tarjeta |
| GET | `/api/Venta` | Lista ventas |
| GET | `/api/Venta/{idVenta}` | Consulta una venta |
| GET | `/api/Venta/{idVenta}/detalle` | Consulta detalle de venta |
| POST | `/api/Venta` | Crea una venta |
| PUT | `/api/Venta` | Modifica una venta |
| PUT | `/api/Venta/inactivar/{idVenta}` | Inactiva una venta |
| GET | `/api/Boletica` | Lista boletos |
| GET | `/api/Boletica/{idBoletica}` | Consulta un boleto |
| POST | `/api/Boletica` | Crea un boleto |
| PUT | `/api/Boletica` | Modifica un boleto |
| PUT | `/api/Boletica/inactivar/{idBoletica}` | Inactiva un boleto |
| GET | `/api/BoleticaSilla` | Lista asignaciones boleto-silla |
| GET | `/api/BoleticaSilla/{idBoleticaSilla}` | Consulta una asignación |
| POST | `/api/BoleticaSilla` | Crea una asignación |
| PUT | `/api/BoleticaSilla` | Modifica una asignación |
| PUT | `/api/BoleticaSilla/inactivar/{idBoleticaSilla}` | Inactiva una asignación |
| GET | `/api/DireccionCliente` | Lista direcciones de cliente |
| GET | `/api/DireccionCliente/{idDireccionCli}` | Consulta una dirección |
| POST | `/api/DireccionCliente` | Crea una dirección |
| PUT | `/api/DireccionCliente` | Modifica una dirección |
| PUT | `/api/DireccionCliente/inactivar/{idDireccionCli}` | Inactiva una dirección |
| GET | `/api/DireccionEmpleado` | Lista direcciones de empleado |
| GET | `/api/DireccionEmpleado/{idDireccionEmp}` | Consulta una dirección |
| POST | `/api/DireccionEmpleado` | Crea una dirección |
| PUT | `/api/DireccionEmpleado` | Modifica una dirección |
| PUT | `/api/DireccionEmpleado/inactivar/{idDireccionEmp}` | Inactiva una dirección |
| GET | `/api/UsuarioSistema` | Lista usuarios del sistema |
| GET | `/api/UsuarioSistema/{idUsuario}` | Consulta un usuario |
| GET | `/api/UsuarioSistema/login?username=&passwordHash=` | Login de usuario |
| POST | `/api/UsuarioSistema` | Crea un usuario |
| PUT | `/api/UsuarioSistema` | Modifica un usuario |
| PUT | `/api/UsuarioSistema/inactivar/{idUsuario}` | Inactiva un usuario |

---

## Modelos de Datos

### Catálogos

```
Genero {
  idGenero: int,
  nombre: string (255)
}

Clasificacion {
  idClasificacion: int,
  codigo: string (255),
  descripcion: string (255)
}

Formato {
  idFormato: int,
  nombre: string (255)
}

Idioma {
  idIdioma: int,
  nombre: string (255),
  codigo: string (255)
}

Pais {
  idPais: int,
  nombre: string (255),
  codigo: string (255)
}

Departamento {
  idDepartamento: int,
  idPais: int (FK -> Pais),
  nombre: string (255)
}

Ciudad {
  idCiudad: int,
  idDepartamento: int (FK -> Departamento),
  nombre: string (255)
}

Distribuidora {
  idDistribuidora: int,
  nombre: string (255),
  idPais: int | null (FK -> Pais)
}

Productora {
  idProductora: int,
  nombre: string (255),
  idPais: int | null (FK -> Pais)
}

Profesion {
  idProfesion: int,
  nombre: string (255)
}

Rol {
  idRol: int,
  nombre: string (255),
  descripcion: string | null (255)
}

TipoCliente {
  idTipoCliente: int,
  nombre: string (255),
  descripcion: string (255)
}

TipoDocumento {
  idTipoDoc: int,
  codigo: string (255),
  descripcion: string (255)
}

TipoSala {
  idTipoSala: int,
  nombre: string (255)
}

TipoSilla {
  idTipoSilla: int,
  nombre: string (255),
  precioBase: decimal (18,0)
}

TipoTelefono {
  idTipoTelefono: int,
  nombre: string (255)
}

MetodoPago {
  idMetodoPago: int,
  nombre: string (255)
}
```

### Tablas Relacionales

```
PeliculaIdioma {
  idPeliculaIdioma: int,
  idPelicula: int (FK -> Pelicula),
  idIdioma: int (FK -> Idioma),
  esOriginal: bool
}

PeliculaFormato {
  idPeliculaFormato: int,
  idPelicula: int (FK -> Pelicula),
  idFormato: int (FK -> Formato)
}

PeliculaProductora {
  idPeliculaProductora: int,
  idPelicula: int (FK -> Pelicula),
  idProductora: int (FK -> Productora)
}

PeliculaDistribuidora {
  idPeliculaDistribuidora: int,
  idPelicula: int (FK -> Pelicula),
  idDistribuidora: int (FK -> Distribuidora)
}

EmpleadoProfesion {
  idEmpProfesion: int,
  idEmpleado: int (FK -> Empleado),
  idProfesion: int (FK -> Profesion)
}

TelefonoCliente {
  idTelefono: int,
  idCliente: int (FK -> Cliente),
  idTipoTelefono: int (FK -> TipoTelefono),
  numero: string (255)
}

TelefonoEmpleado {
  idTelefonoEmp: int,
  idEmpleado: int (FK -> Empleado),
  idTipoTelefono: int (FK -> TipoTelefono),
  numero: string (255)
}
```

### Entidades Principales

```
Pelicula {
  idPelicula: int,
  idGenero: int (FK -> Genero),
  idClasificacion: int (FK -> Clasificacion),
  tituloOriginal: string (255),
  nombreOferta: string (255),
  resumen: string (255),
  anioEstreno: date (YYYY-MM-DD),
  trailerLink: string | null (255),
  duracionMin: int,
  registradoPor: int (FK -> UsuarioSistema),
  fechaRegistro: datetime
}

Funcion {
  idFuncion: int,
  idSala: int (FK -> Sala),
  idPelicula: int (FK -> Pelicula),
  idIdioma: int (FK -> Idioma),
  idFormato: int (FK -> Formato),
  fechaFuncion: date (YYYY-MM-DD),
  horaInicio: datetime,
  horaFin: datetime,
  precioBase: decimal (18,0),
  estado: bool,
  registradoPor: int (FK -> UsuarioSistema),
  fechaRegistro: datetime
}

Cliente {
  idCliente: int,
  idTipoCliente: int (FK -> TipoCliente),
  idTipoDoc: int (FK -> TipoDocumento),
  numDocumento: string (255),
  nombres: string (255),
  apellidos: string (255),
  email: string | null (255),
  activo: bool,
  registradoPor: int (FK -> UsuarioSistema),
  fechaRegistro: datetime
}

Empleado {
  idEmpleado: int,
  codigoEmpleado: string (255) [unique],
  idTeatro: int (FK -> Teatro),
  idTipoDoc: int (FK -> TipoDocumento),
  numDocumento: string (255),
  nombres: string (255),
  apellidos: string (255),
  fechaIngreso: date (YYYY-MM-DD),
  activo: bool,
  registradoPor: int | null,
  fechaRegistro: datetime
}

Teatro {
  idTeatro: int,
  idCiudad: int (FK -> Ciudad),
  nombre: string (255),
  direccion: string (255),
  telefono: string | null (255),
  email: string | null (255),
  activo: bool,
  registradoPor: int | null,
  fechaRegistro: datetime
}

Sala {
  idSala: int,
  idTeatro: int (FK -> Teatro),
  idTipoSala: int (FK -> TipoSala),
  nombreSala: string (255),
  capacidadTotal: int,
  activo: bool,
  registradoPor: int (FK -> UsuarioSistema),
  fechaRegistro: datetime
}

Silla {
  idSilla: int,
  idSala: int (FK -> Sala),
  idTipoSilla: int (FK -> TipoSilla),
  fila: string (1 char) [unique per sala+fila+numero],
  numero: int [unique per sala+fila+numero],
  estado: int,
  registradoPor: int (FK -> UsuarioSistema),
  fechaRegistro: datetime
}

UsuarioSistema {
  idUsuario: int,
  idEmpleado: int (FK -> Empleado) [unique],
  idRol: int (FK -> Rol),
  username: string (255) [unique],
  passwordHash: string (255),
  activo: bool,
  ultimoLogin: datetime | null,
  registradoPor: int (FK -> UsuarioSistema),
  fechaRegistro: datetime
}

TarjetaFidelizacion {
  idTarjeta: int,
  idCliente: int (FK -> Cliente) [unique],
  numeroTarjeta: string (255),
  fechaEmision: date (YYYY-MM-DD),
  fechaVencimiento: date (YYYY-MM-DD),
  puntosAcumulados: decimal (18,0),
  descuentoPorcentaje: decimal (18,0),
  estado: bool,
  registradoPor: int,
  fechaRegistro: datetime
}

Ventum (Venta) {
  idVenta: int,
  idCliente: int | null (FK -> Cliente),
  idEmpleado: int (FK -> Empleado),
  idMetodoPago: int (FK -> MetodoPago),
  fechaHora: datetime,
  subtotal: decimal (18,0),
  totalDescuento: decimal (18,0),
  totalVenta: decimal (18,0),
  estado: bool
}

Boletica {
  idBoletica: int,
  idVenta: int (FK -> Venta),
  idFuncion: int (FK -> Funcion),
  estado: int
}

BoleticaSilla {
  idBoleticaSilla: int,
  idBoletica: int (FK -> Boletica),
  idSilla: int (FK -> Silla),
  precioUnitario: decimal (18,0),
  descuento: decimal (18,0),
  precioFinal: decimal (18,0),
  estado: int
}

DireccionCliente {
  idDireccionCli: int,
  idCliente: int (FK -> Cliente),
  idCiudad: int (FK -> Ciudad),
  direccion: string (255),
  activo: bool
}

DireccionEmpleado {
  idDireccionEmp: int,
  idEmpleado: int (FK -> Empleado),
  idCiudad: int (FK -> Ciudad),
  direccion: string (255),
  activo: bool
}
```

---

## Notas importantes para el frontend

1. **IDs auto-generados:** El frontend **debe enviar el ID** en POST. El backend NO usa auto-increment (`ValueGeneratedNever()`).

2. **Tipos de fecha:**
   - `date` (DateOnly) → formato JSON: `"YYYY-MM-DD"` — usado en: `anioEstreno`, `fechaFuncion`, `fechaEmision`, `fechaVencimiento`, `fechaIngreso`
   - `datetime` (DateTime) → formato JSON: `"YYYY-MM-DDThh:mm:ss"` — usado en: `fechaRegistro`, `fechaHora`, `horaInicio`, `horaFin`, `ultimoLogin`

3. **Booleans:** `true`/`false` en JSON para propiedades `bool`. Algunas entidades usan `int` para `estado` (Boletica, BoleticaSilla, Silla).

4. **Paginación:** Solo `Departamento`, `Ciudad` y `Clasificacion` aceptan `?page=1&pageSize=50`.

5. **`registradoPor`:** Siempre es el `idUsuario` del `UsuarioSistema` que creó/registró el registro.

6. **Login de UsuarioSistema:**
   ```
   GET /api/UsuarioSistema/login?username=admin&passwordHash=hash123
   ```
   Retorna un `IQueryable` con resultados (vacío si no hay match). No hay generación de tokens JWT.

7. **Para `PUT /inactivar/{id}`:** Se envía la petición sin body, solo el ID en la URL.

8. **Endpoints reciben y devuelven los mismos objetos** que los modelos. No hay DTOs ni mappers.

9. **Unicidad compuesta** (relevante para validaciones frontend):
   - `Silla`: unique por (idSala, fila, numero)
   - `Funcion`: unique por (idSala, fechaFuncion, horaInicio)
   - `Cliente`: unique por (idTipoDoc, numDocumento)
   - `Empleado`: unique por (idTipoDoc, numDocumento), unique `codigoEmpleado`
   - `UsuarioSistema`: unique `username`, unique `idEmpleado`
   - `TarjetaFidelizacion`: unique `idCliente`
   - `PeliculaIdioma`: unique por (idPelicula, idIdioma)
   - `PeliculaFormato`: unique por (idPelicula, idFormato)
   - `PeliculaProductora`: unique por (idPelicula, idProductora)
   - `PeliculaDistribuidora`: unique por (idPelicula, idDistribuidora)
   - `EmpleadoProfesion`: unique por (idEmpleado, idProfesion)
   - `BoleticaSilla`: unique por (idBoletica, idSilla)

---

## Diagrama de Relaciones (FKs)

```
Pais ──< Departamento ──< Ciudad ──< Teatro
                                    └──< Sala ──< Silla
                                                └─< Funcion

Genero ─┐
         ├─< Pelicula ──< PeliculaIdioma >── Idioma
Clasif ─┘              ├─< PeliculaFormato >── Formato
                       ├─< PeliculaProductora >── Productora >── Pais
                       └─< PeliculaDistribuidora >── Distribuidora >── Pais

Cliente ──< DireccionCliente >── Ciudad
├──< TelefonoCliente >── TipoTelefono
├──< TarjetaFidelizacion
└──< Venta >── MetodoPago

Empleado ──< DireccionEmpleado >── Ciudad
├──< TelefonoEmpleado >── TipoTelefono
├──< EmpleadoProfesion >── Profesion
├──< UsuarioSistema >── Rol
├──< Venta
└──< Teatro (FK: registrado_por)

Funcion ──< Boletica ──< BoleticaSilla >── Silla
```

---

## Ejemplo de Flujo Completo (Compra de Ticket)

1. `GET /api/Pelicula` → listar películas disponibles
2. `GET /api/Genero` + `GET /api/Clasificacion` → obtener catálogos para filtros
3. `GET /api/Teatro` → ver teatros disponibles
4. `GET /api/Funcion` → obtener funciones con filtros por película/fecha
5. `GET /api/Funcion/{idFuncion}` → consultar función específica
6. `GET /api/Sala/{idSala}` → ver detalle de la sala
7. `GET /api/Silla` → obtener todas las sillas (o filtrar por idSala)
8. `GET /api/TipoSilla` → obtener tipos de silla con precio base
9. `POST /api/Venta` → crear venta
10. `POST /api/Boletica` → crear boleto asociado a venta y función
11. `POST /api/BoleticaSilla` → asignar sillas al boleto
12. `GET /api/Venta/{idVenta}/detalle` → obtener detalle completo de la venta
