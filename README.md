# MediScan Backend

## 📋 Descripción

MediScan Backend es la API RESTful desarrollada en .NET 8 con C# que soporta la aplicación MediScan, un sistema de gestión médica integral. Implementa arquitectura limpia (Clean Architecture) con capas separadas para una mejor mantenibilidad y escalabilidad.

## ✨ Características

- **Gestión de Usuarios**: Autenticación y autorización con roles (Admin, Profesional, Paciente)
- **Chat IA en Tiempo Real**: Comunicación con chat de IA para brindarte un diagnostico temprano aunque no definitivo
- **Generación de informes automáticos**: Generación de informe preliminar de diagnóstico en referencia a tus consultas a la IA (Con posibilidad de descargarlo en formato pdf).
- **Diagnósticos y Tratamientos**: Registro de diagnósticos y tratamientos
- **Citas Médicas**: Programación y gestión de appointments
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

# Clonar los repositorios:

git clone https://github.com/sergioLahuerta/Backend-MediScan.git  
git clone https://github.com/sergioLahuerta/Frontend-MediScan.git  

# Tras clonar los dos repositorios, en la raíz del repositorio del Backend-MediScan sacar el docker-compose.yaml para alojarlo al mismo nivel que sendos repositorios.  
De esta forma:  
 ______________________  
| > Backend-Mediscan/  |  
| > Frontend-Mediscan/ |  
| docker-compose.yaml  |  
|______________________|  

# Con esa disposición, en una terminal en la ruta del docker-compose.yaml y los repositorios ->  
docker-compose up --build -d  


# Tras el mensaje de que están Started los tres contenedores, estará disponible:  
Web: http://localhost:8080  
Api Swagger: http://localhost:5073/swagger  



## 📚 Documentación API

Accede a Swagger UI en: `http://localhost:5073/swagger`

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


## 🤝 Contribuir

Estamos abiertos a posibles mejoras y avances de la aplicación por parte de la comunidad.  
Gracias.


## 📞 Contacto

Para preguntas o soporte, contacta al equipo de desarrollo.

---

*MediScan - Tecnología para una mejor atención médica*
