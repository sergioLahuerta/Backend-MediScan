# MediScan Backend

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-purple.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## 📋 Descripción

MediScan Backend es la API RESTful desarrollada en .NET 8 con C# que soporta la aplicación MediScan, un sistema de gestión médica integral. Implementa arquitectura limpia (Clean Architecture) con capas separadas para una mejor mantenibilidad y escalabilidad.

## ✨ Características

- **Gestión de Usuarios**: Autenticación y autorización con roles (Admin, Profesional, Paciente)
- **Citas Médicas**: Programación y gestión de appointments
- **Chat IA en Tiempo Real**: Comunicación con chat de IA para brindarte un diagnostico temprano aunque no definitivo
- **Diagnósticos y Tratamientos**: Registro de diagnósticos y tratamientos
- **Contadores de Uso**: Monitoreo de uso de la plataforma

## 🛠️ Tecnologías Utilizadas

- **Framework**: .NET 8 (ASP.NET Core Web API)
- **Lenguaje**: C# 12
- **Base de Datos**: MySQL (con scripts en `db/`)
- **Arquitectura**: Clean Architecture (Core, Application, Infrastructure, Api)
- **ORM**: Entity Framework Core
- **Autenticación**: JWT Tokens
- **Documentación API**: Swagger

## 📋 Prerrequisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) o compatible
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/) con extensiones C#

## 🚀 Instalación y Configuración



### Desarrollo
```bash
dotnet run --project MediScan.Api
```

### Build y Publish
```bash
dotnet build MediScan.sln
dotnet publish MediScan.Api -c Release -o ./publish
```

La API estará disponible en `https://localhost:5001` (HTTPS) y `http://localhost:5000` (HTTP).

## 📚 Documentación API

Accede a Swagger UI en: `https://localhost:5001/swagger`

### Endpoints Principales

- **Auth**: `/api/auth/login`, `/api/auth/register`
- **Servicios (IA)**: `/api/services`
- **Perfil**: `/api/profile`
- **Contacto**: `/api/contact`
- **Política de proivacidad**: `/api/privacy`
- **Terminos y Condiciones**: `/api/terms`

## 👥 Usuarios de Prueba

### Admin
- **Email**: admin@mediscan.com
- **Contraseña**: hashed_pass_123

### Paciente
- **Email**: juan.paciente@email.com
- **Contraseña**: hashed_pass_456

### Profesional Médico
- **Email**: dra.martinez@mediscan.com
- **Contraseña**: hashed_pass_789

## 🧪 Pruebas

```bash
dotnet test
```

## 🤝 Contribuir

Estamos abiertos a posibles mejoras y avances de la aplicación.

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 📞 Contacto

Para preguntas o soporte, contacta al equipo de desarrollo.

---

*MediScan - Tecnología para una mejor atención médica*