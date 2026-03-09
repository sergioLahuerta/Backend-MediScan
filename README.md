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

Clonar los repositorios:

git clone https://github.com/sergioLahuerta/Backend-MediScan.git  
git clone https://github.com/sergioLahuerta/Frontend-MediScan.git  

Tras clonar los dos repositorios, en la raíz del repositorio del Backend-MediScan sacar el docker-compose.yaml para alojarlo al mismo nivel que sendos repositorios.  

Con esa disposición, en una terminal en la ruta del docker-compose.yaml hacer:
docker-compose build  
Y después:   
docker-compose up -d  


Tras el mensaje de que están Started los tres contenedores, estará disponible:  
Web: http://localhost:8080  
Api Swagger: http://localhost:5073/swagger  



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


## 🤝 Contribuir

Estamos abiertos a posibles mejoras y avances de la aplicación.

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 📞 Contacto

Para preguntas o soporte, contacta al equipo de desarrollo.

---

*MediScan - Tecnología para una mejor atención médica*

## REVISAR
# Backend-MediScan
[![Ask DeepWiki](https://devin.ai/assets/askdeepwiki.png)](https://deepwiki.com/sergioLahuerta/Backend-MediScan.git)

This repository contains the backend service for MediScan, a medical application designed to connect patients with healthcare professionals. The service is built with ASP.NET Core and handles user management, AI-powered chat sessions, appointment scheduling, and more.

## Features

*   **User Management:** Role-based system for Patients, Professionals, and Admins.
*   **AI Chat:** Functionality for preliminary diagnosis and consultation via chat sessions.
*   **Appointment Scheduling:** Manage appointments between patients and professionals, with support for automated scheduling based on availability.
*   **Medical History:** Store and manage patient diagnoses, treatments, and reviews.
*   **Subscription System:** Manage user subscriptions for different service tiers (e.g., Free, Premium), including payment and invoicing.
*   **Usage Tracking:** Monitor AI service usage (tokens used, messages sent) to enforce limits on free-tier users.
*   **Organizations & Professionals:** Link medical professionals to clinics or organizations.

## Technology Stack

*   **Backend:** ASP.NET Core 8 Web API
*   **Database:** MySQL
*   **API Documentation:** Swagger (OpenAPI)

## Database Schema

The core logic and data structure are defined in the MySQL database schema. Key tables include:

*   `Users`, `Patients`, `Professionals`: For user data and roles.
*   `Appointments`, `Professional_Schedules`: For managing schedules and bookings.
*   `ChatSessions`, `ChatMessages`: To handle interactions with the AI assistant.
*   `Diagnosis`, `Treatments`: To store patient medical history.
*   `Subscriptions`, `Plans`, `Payments`: For the subscription and billing system.

The complete database structure can be found in `api/db/schema.sql`, and sample data for testing is available in `api/db/seed.sql`.

## Getting Started

To get a local copy up and running, follow these steps.

### Prerequisites

*   [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [MySQL Server](https://dev.mysql.com/downloads/mysql/)

### Database Setup

1.  In your MySQL instance, create the database for the project:
    ```sql
    CREATE DATABASE IF NOT EXISTS MediScanDB;
    ```
2.  Use the `MediScanDB` database.
    ```sql
    USE MediScanDB;
    ```
3.  Execute the schema script `api/db/schema.sql` to create the tables.
4.  Optionally, execute the seed script `api/db/seed.sql` to populate the database with initial test data.

### Running the Application

1.  Clone the repository:
    ```sh
    git clone https://github.com/sergiolahuerta/backend-mediscan.git
    ```
2.  Navigate to the API project directory:
    ```sh
    cd backend-mediscan/api
    ```
3.  Install the required .NET packages:
    ```sh
    dotnet restore
    ```
4.  Run the application:
    ```sh
    dotnet run
    ```
5.  The API will be available at `http://localhost:5027`.

## API Documentation

This project uses Swagger for interactive API documentation. Once the application is running, you can explore and test the available endpoints.

Navigate to **`http://localhost:5027/swagger`** in your web browser.
