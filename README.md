# TaskFlow API

REST API para la gestión de proyectos y tareas, desarrollada con ASP.NET Core y Entity Framework Core.

El proyecto implementa autenticación mediante JWT, autorización basada en roles, gestión de usuarios, proyectos y tareas, además de persistencia de datos utilizando SQL Server.

## Características

* Gestión de usuarios
* Gestión de proyectos
* Gestión de tareas
* CRUD completo
* Autenticación mediante JWT
* Autorización basada en roles
* Hashing seguro de contraseñas
* DTOs para controlar los datos de entrada y salida
* Validaciones de datos
* Entity Framework Core
* Migraciones de base de datos
* Swagger/OpenAPI para documentación y pruebas de la API
* Relaciones entre usuarios, proyectos y tareas

## Tecnologías

* C#
* ASP.NET Core 8
* Entity Framework Core 8
* SQL Server
* JWT
* Swagger / OpenAPI
* Visual Studio 2022

## Arquitectura

El proyecto utiliza una separación por responsabilidades:

```text
Controllers
    ↓
Services
    ↓
Entity Framework Core
    ↓
SQL Server
```

Los DTOs se utilizan para controlar la información que recibe y devuelve la API.

## Autenticación y autorización

TaskFlow utiliza JSON Web Tokens (JWT) para autenticar a los usuarios.

El flujo de autenticación es:

```text
Login
  ↓
Validación de credenciales
  ↓
Generación del JWT
  ↓
Authorization: Bearer <token>
  ↓
Acceso a endpoints protegidos
```

El sistema cuenta con dos roles:

* `Administrador`
* `Usuario`

Los endpoints pueden estar protegidos mediante:

```csharp
[Authorize]
```

o restringidos a un rol específico:

```csharp
[Authorize(Roles = "Administrador")]
```

Las contraseñas no se almacenan directamente en la base de datos. Se almacenan utilizando hashing mediante `PasswordHasher`.

## Base de datos

El proyecto utiliza SQL Server y Entity Framework Core.

Principales entidades:

* `Usuario`
* `Proyecto`
* `Tarea`

Relaciones principales:

```text
Usuario
   │
   ├── Proyecto
   │      │
   │      └── Tarea
   │
   └── Tarea
```

Entity Framework Core se utiliza para:

* Consultas
* Inserciones
* Actualizaciones
* Eliminaciones
* Relaciones entre entidades
* Migraciones
* Configuración mediante Fluent API

## Documentación de la API

La API incluye Swagger/OpenAPI para visualizar y probar los endpoints.

Al ejecutar el proyecto en entorno de desarrollo, Swagger está disponible en:

```text
/swagger
```

Desde Swagger se pueden realizar operaciones sobre la API y probar la autenticación mediante JWT.

## Configuración

La conexión a SQL Server se configura mediante `appsettings.json`.

La clave utilizada para firmar los JWT no se almacena directamente en el código fuente. El proyecto utiliza ASP.NET Core User Secrets para mantenerla fuera del repositorio.

Se requiere configurar:

```text
Jwt:Key
```

antes de ejecutar la aplicación.

## Ejecución

### Requisitos

* .NET 8 SDK
* SQL Server
* Visual Studio 2022

### Pasos

1. Clonar el repositorio.
2. Configurar la cadena de conexión de SQL Server.
3. Configurar la clave JWT mediante User Secrets.
4. Ejecutar las migraciones de Entity Framework Core.
5. Ejecutar la aplicación.
6. Abrir Swagger para probar la API.

## Objetivo del proyecto

TaskFlow fue desarrollado como proyecto de aprendizaje y portafolio para practicar el desarrollo de APIs REST utilizando tecnologías del ecosistema .NET.

El proyecto permitió aplicar conceptos de:

* Desarrollo backend
* Programación orientada a objetos
* Entity Framework Core
* SQL Server
* Arquitectura por servicios
* Inyección de dependencias
* Autenticación y autorización
* Seguridad de contraseñas
* JWT
* Diseño y consumo de APIs REST

## Autor

**José Daniel Esteban Cea**

Estudiante de Ingeniería en Sistemas.

**GitHub:** `josecea-dev`
