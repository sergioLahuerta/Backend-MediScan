-- Proyecto MediScan DB Schema (MySQL)

CREATE DATABASE IF NOT EXISTS MediScanDB;
USE MediScanDB;

-- Roles de sistema (Admin, User, etc.)
CREATE TABLE IF NOT EXISTS Roles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE,
    Description VARCHAR(255)
);

-- Usuarios principales (Guids para integración con entidades externas)
CREATE TABLE IF NOT EXISTS Users (
    Id CHAR(36) PRIMARY KEY, -- Usaremos GUIDs como solicitaste
    Email VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255),
    RoleId INT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    IsActive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

-- Contadores de uso para limitar la IA en la versión Free
CREATE TABLE IF NOT EXISTS UsageCounters (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId CHAR(36), -- NULL si es usuario invitado
    ClientIdentifier VARCHAR(255), -- IP o Token de cookie para trackear invitados
    TokensUsed INT DEFAULT 0,
    MessagesSent INT DEFAULT 0,
    PeriodStart DATETIME,
    PeriodEnd DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Extendemos el usuario para los Pacientes
CREATE TABLE IF NOT EXISTS Patients (
    UserId CHAR(36) PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    DateOfBirth DATE,
    Gender VARCHAR(20),
    Phone VARCHAR(20),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Extendemos el usuario para los Profesionales Médicos
CREATE TABLE IF NOT EXISTS Professionals (
    UserId CHAR(36) PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    LicenseNumber VARCHAR(100) UNIQUE NOT NULL,
    Specialty VARCHAR(100) NOT NULL,
    Phone VARCHAR(20),
    Bio TEXT,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Organizaciones o Clínicas
CREATE TABLE IF NOT EXISTS Organizations (
    Id CHAR(36) PRIMARY KEY, -- GUID
    Name VARCHAR(200) NOT NULL,
    Address VARCHAR(255),
    ContactPhone VARCHAR(20),
    ContactEmail VARCHAR(255)
);

-- Relación N:M entre Paciente y Médico (Si tienen historial juntos)
CREATE TABLE IF NOT EXISTS DoctorPatient (
    PatientId CHAR(36),
    ProfessionalId CHAR(36),
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (PatientId, ProfessionalId),
    FOREIGN KEY (PatientId) REFERENCES Patients(UserId),
    FOREIGN KEY (ProfessionalId) REFERENCES Professionals(UserId)
);

-- Relación N:M entre Profesionales y Organizaciones
CREATE TABLE IF NOT EXISTS Professional_Org (
    ProfessionalId CHAR(36),
    OrganizationId CHAR(36),
    JoinedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (ProfessionalId, OrganizationId),
    FOREIGN KEY (ProfessionalId) REFERENCES Professionals(UserId),
    FOREIGN KEY (OrganizationId) REFERENCES Organizations(Id)
);

-- Horarios para agenda automática de IA
CREATE TABLE IF NOT EXISTS Professional_Schedules (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProfessionalId CHAR(36) NOT NULL,
    DayOfWeek INT NOT NULL, -- 0=Domingo, 1=Lunes, etc.
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    IsAvailable BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (ProfessionalId) REFERENCES Professionals(UserId)
);

-- Citas agendadas por el paciente o la IA
CREATE TABLE IF NOT EXISTS Appointments (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PatientId CHAR(36) NOT NULL,
    ProfessionalId CHAR(36) NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    DurationMinutes INT DEFAULT 30,
    Status VARCHAR(50) DEFAULT 'Scheduled', -- Scheduled, Completed, Cancelled
    Notes TEXT,
    FOREIGN KEY (PatientId) REFERENCES Patients(UserId),
    FOREIGN KEY (ProfessionalId) REFERENCES Professionals(UserId)
);

-- Sesiones de Chat con la IA
CREATE TABLE IF NOT EXISTS ChatSessions (
    Id CHAR(36) PRIMARY KEY, -- GUID para URL única (Token temporal para invitados)
    UserId CHAR(36), -- Permitimos NULL para usuarios anónimos/invitados
    SessionType VARCHAR(50) DEFAULT 'Diagnosis', -- 'Diagnosis' o 'ProfessionalConsultation'
    StartedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    EndedAt DATETIME, -- Registra cuándo el usuario cerró el chat o expiró por inactividad
    Title VARCHAR(255),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Mensajes dentro de una sesión de chat
CREATE TABLE IF NOT EXISTS ChatMessages (
    Id BIGINT AUTO_INCREMENT PRIMARY KEY,
    ChatSessionId CHAR(36) NOT NULL,
    SenderType VARCHAR(50) NOT NULL, -- Ej: 'User', 'Guest' o 'AI'
    MessageText TEXT NOT NULL,
    SentAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ChatSessionId) REFERENCES ChatSessions(Id)
);

-- Adjuntos de los mensajes (Fotos de síntomas, documentos, etc.)
CREATE TABLE IF NOT EXISTS MessageAttachments (
    Id BIGINT AUTO_INCREMENT PRIMARY KEY,
    ChatMessageId BIGINT NOT NULL,
    FileUrl VARCHAR(500) NOT NULL,
    FileType VARCHAR(50) NOT NULL, -- Ej: 'image/jpeg', 'image/png'
    UploadedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ChatMessageId) REFERENCES ChatMessages(Id)
);

-- Historial médico: Diagnósticos
CREATE TABLE IF NOT EXISTS Diagnosis (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PatientId CHAR(36) NOT NULL,
    ProfessionalId CHAR(36) NOT NULL,
    DiagnosisDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Description TEXT NOT NULL,
    ICD10Code VARCHAR(20), -- Código internacional de enfermedades (opcional)
    FOREIGN KEY (PatientId) REFERENCES Patients(UserId),
    FOREIGN KEY (ProfessionalId) REFERENCES Professionals(UserId)
);

-- Historial médico: Tratamientos adheridos a diagnósticos
CREATE TABLE IF NOT EXISTS Treatments (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DiagnosisId INT NOT NULL,
    Description TEXT NOT NULL,
    StartDate DATE,
    EndDate DATE,
    FOREIGN KEY (DiagnosisId) REFERENCES Diagnosis(Id)
);

-- Reseñas de pacientes a los médicos
CREATE TABLE IF NOT EXISTS Reviews (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PatientId CHAR(36) NOT NULL,
    ProfessionalId CHAR(36) NOT NULL,
    Rating INT CHECK (Rating >= 1 AND Rating <= 5),
    Comment TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (PatientId) REFERENCES Patients(UserId),
    FOREIGN KEY (ProfessionalId) REFERENCES Professionals(UserId)
);

-- Planes de Suscripción (Free, Premium)
CREATE TABLE IF NOT EXISTS Plans (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL, 
    Price DECIMAL(10, 2) NOT NULL,
    MaxMessages INT, -- Límite de mensajes para la IA
    Features TEXT
);

-- Suscripciones de los usuarios
CREATE TABLE IF NOT EXISTS Subscriptions (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId CHAR(36) NOT NULL,
    PlanId INT NOT NULL,
    StartDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    EndDate DATETIME,
    Status VARCHAR(50) DEFAULT 'Active', -- Active, Expired o Cancelled (cualqiera)
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (PlanId) REFERENCES Plans(Id)
);

-- Pagos recurrentes
CREATE TABLE IF NOT EXISTS Payments (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    SubscriptionId INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    PaymentMethod VARCHAR(50),
    Status VARCHAR(50) DEFAULT 'Completed',
    FOREIGN KEY (SubscriptionId) REFERENCES Subscriptions(Id)
);

-- Facturas generadas por pago
CREATE TABLE IF NOT EXISTS Invoices (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PaymentId INT NOT NULL,
    InvoiceNumber VARCHAR(100) UNIQUE NOT NULL,
    InvoiceDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (PaymentId) REFERENCES Payments(Id)
);

-- Detalles de cada factura
CREATE TABLE IF NOT EXISTS DetailInvoices (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    InvoiceId INT NOT NULL,
    Description VARCHAR(255) NOT NULL,
    Quantity INT DEFAULT 1,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(Id)
);