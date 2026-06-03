# CineColombia API

## Descripción
API backend para el sistema de CineColombia, diseñada para gestionar:
- Autenticación de usuarios (login, registro, roles).
- Gestión de películas (cartelera, detalles, horarios).
- Reservas y ventas de boletas.
- Salas y funciones.
- Pagos y transacciones.

Esta documentación está optimizada para ser interpretada por una IA o un desarrollador front-end, con ejemplos claros y estructurados.

---

## Tecnologías usadas
- **Framework**: .NET Core (versión 6.0 o superior).
- **Base de datos**: SQL Server.
- **Autenticación**: JWT (JSON Web Tokens).
- **Documentación**: Swagger (disponible en `/swagger`).
- **ORM**: Entity Framework Core.

---

## Requisitos previos
1. **.NET SDK 6.0+**: [Descargar aquí](https://dotnet.microsoft.com/download).
2. **SQL Server**: Versión 2019 o superior (o SQL Server Express).
3. **Node.js**: Para el front-end (si aplica).
4. **Herramientas opcionales**:
   - Postman (para probar endpoints).
   - Docker (para despliegue).

---

## Instalación y configuración
### 1. Clonar el repositorio
```bash
 git clone https://github.com/tu-usuario/CineColombiaApi.git
 cd CineColombiaApi
```

### 2. Configurar la base de datos
- Restaurar la base de datos desde el archivo `CineColombiaDb.bak` (si está disponible) o ejecutar las migraciones:
  ```bash
  dotnet ef database update
  ```

### 3. Configurar variables de entorno
- Renombrar el archivo `appsettings.example.json` a `appsettings.json`.
- Actualizar las siguientes claves:
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=tu-servidor;Database=CineColombiaDb;User Id=tu-usuario;Password=tu-contraseña;TrustServerCertificate=True;"
    },
    "Jwt": {
      "Key": "tu-clave-secreta-para-jwt",
      "Issuer": "tu-issuer",
      "Audience": "tu-audience"
    }
  }
  ```

### 4. Instalar dependencias y ejecutar
```bash
 dotnet restore
 dotnet run
```

La API estará disponible en `http://localhost:5000` o `https://localhost:5001`.

---

## Estructura del proyecto
```
CineColombiaApi/
├── Controllers/       # Endpoints de la API (ej: MoviesController, AuthController).
├── Models/            # Entidades y DTOs (ej: Movie, User, Booking).
├── Services/          # Lógica de negocio (ej: MovieService, AuthService).
├── Data/              # Contexto de Entity Framework y migraciones.
├── appsettings.json   # Configuración de la aplicación.
└── Program.cs         # Punto de entrada de la API.
```

---

## Endpoints principales
### Autenticación
| Método | Endpoint               | Descripción                     | Ejemplo de solicitud (JSON)                     |
|--------|------------------------|---------------------------------|-----------------------------------------------|
| POST   | `/api/auth/login`      | Iniciar sesión.                | `{ "email": "user@example.com", "password": "123456" }` |
| POST   | `/api/auth/register`   | Registrar usuario.             | `{ "name": "User", "email": "user@example.com", "password": "123456", "role": "Client" }` |

### Películas
| Método | Endpoint               | Descripción                     | Parámetros (query)               |
|--------|------------------------|---------------------------------|-----------------------------------|
| GET    | `/api/movies`          | Listar películas en cartelera.  | `?page=1&pageSize=10`             |
| GET    | `/api/movies/{id}`     | Obtener detalles de una película. | -                                 |

### Reservas
| Método | Endpoint               | Descripción                     | Ejemplo de solicitud (JSON)                     |
|--------|------------------------|---------------------------------|-----------------------------------------------|
| POST   | `/api/bookings`        | Crear una reserva.              | `{ "movieId": 1, "showtimeId": 1, "seats": ["A1", "A2"] }` |
| GET    | `/api/bookings/{id}`   | Obtener detalles de una reserva. | -                                 |

### Funciones (horarios)
| Método | Endpoint               | Descripción                     | Parámetros (query)               |
|--------|------------------------|---------------------------------|-----------------------------------|
| GET    | `/api/showtimes`       | Listar funciones disponibles.   | `?movieId=1&date=2026-06-03`     |

---

## Autenticación en el front-end
### 1. Iniciar sesión
- Enviar una solicitud `POST` a `/api/auth/login` con el email y contraseña.
- La respuesta incluirá un `token` JWT:
  ```json
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiration": "2026-06-04T00:00:00Z"
  }
  ```

### 2. Almacenar el token
- Guardar el token en el `localStorage` o `sessionStorage` del navegador:
  ```javascript
  localStorage.setItem('token', response.token);
  ```

### 3. Usar el token en solicitudes
- Incluir el token en el encabezado `Authorization` de las solicitudes:
  ```javascript
  fetch('/api/movies', {
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    }
  });
  ```

---

## Ejemplo de integración con React
```javascript
// Ejemplo: Obtener películas en cartelera
import React, { useEffect, useState } from 'react;

function MovieList() {
  const [movies, setMovies] = useState([]);

  useEffect(() => {
    const fetchMovies = async () => {
      const response = await fetch('http://localhost:5000/api/movies', {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      });
      const data = await response.json();
      setMovies(data);
    };

    fetchMovies();
  }, []);

  return (
    <div>
      {movies.map(movie => (
        <div key={movie.id}>
          <h2>{movie.title}</h2>
          <p>{movie.description}</p>
        </div>
      ))}
    </div>
  );
}
```

---

## Manejo de errores
### Códigos de respuesta comunes
| Código | Descripción                     | Acción recomendada                          |
|--------|---------------------------------|---------------------------------------------|
| 200    | Éxito.                          | Procesar la respuesta.                      |
| 400    | Solicitud incorrecta.           | Revisar los datos enviados.                 |
| 401    | No autorizado.                  | Redirigir al login.                         |
| 403    | Prohibido.                      | Verificar permisos del usuario.             |
| 404    | Recurso no encontrado.          | Mostrar mensaje de error al usuario.        |
| 500    | Error interno del servidor.     | Mostrar mensaje genérico de error.          |

### Ejemplo de manejo de errores en React
```javascript
fetch('/api/bookings', {
  method: 'POST',
  headers: {
    'Authorization': `Bearer ${localStorage.getItem('token')}`,
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({ movieId: 1, showtimeId: 1, seats: ['A1'] })
})
.then(response => {
  if (!response.ok) {
    throw new Error(`Error: ${response.status}`);
  }
  return response.json();
})
.then(data => console.log(data))
.catch(error => {
  console.error('Error:', error);
  alert('Ocurrió un error al procesar tu reserva.');
});
```

---

## Despliegue
### Opciones recomendadas
1. **Azure App Service**:
   - Subir la API a Azure usando el CLI de .NET o GitHub Actions.
   - Configurar la base de datos en Azure SQL Database.

2. **Docker**:
   - Usar el `Dockerfile` incluido para crear una imagen:
     ```bash
     docker build -t cinecolombia-api .
     docker run -p 5000:80 cinecolombia-api
     ```

3. **AWS**:
   - Desplegar en AWS Elastic Beanstalk o ECS.

---

## Contribución
1. **Fork** el repositorio.
2. Crea una **rama** para tu feature (`git checkout -b feature/nueva-feature`).
3. Haz **commit** de tus cambios (`git commit -m 'Añadir nueva feature'`).
4. Haz **push** a la rama (`git push origin feature/nueva-feature`).
5. Abre un **Pull Request**.

---

## Licencia
Este proyecto está bajo la licencia **MIT**.