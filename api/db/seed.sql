-- Insertar Datos Reales de Prueba (Semilla / Seed) para la DB MediScan
USE MediScanDB;

-- 1. Roles
INSERT INTO Roles (Name, Description) VALUES
('Admin', 'Administrador del sistema'),
('Patient', 'Usuario paciente registrado'),
('Professional', 'Profesional médico');

-- 2. Variables GUID (Para poder relacionar fácilmente en script sin memorizarlos)
SET @AdminId = 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb';
SET @PatientId = 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa';
SET @DoctorId = 'cccccccc-cccc-cccc-cccc-cccccccccccc';
SET @OrgGuid = 'dddddddd-dddd-dddd-dddd-dddddddddddd';

-- 3. Usuarios
INSERT INTO Users (Id, Email, PasswordHash, RoleId) VALUES
(@AdminId, 'admin@mediscan.com', 'hashed_pass_123', (SELECT Id FROM Roles WHERE Name = 'Admin')),
(@PatientId, 'juan.paciente@email.com', 'hashed_pass_456', (SELECT Id FROM Roles WHERE Name = 'Patient')),
(@DoctorId, 'dra.martinez@mediscan.com', 'hashed_pass_789', (SELECT Id FROM Roles WHERE Name = 'Professional'));

-- 4. Pacientes
INSERT INTO Patients (UserId, FirstName, LastName, DateOfBirth, Gender, Phone) VALUES
(@PatientId, 'Juan', 'Pérez', '1985-05-15', 'Masculino', '+34600123456');

-- 5. Profesionales Médicos
INSERT INTO Professionals (UserId, FirstName, LastName, LicenseNumber, Specialty, Phone, Bio) VALUES
(@DoctorId, 'Laura', 'Martínez', 'MED-12345-MAD', 'Dermatología', '+34611987654', 'Dermatóloga con 10 años de experiencia.');

-- 6. Organizaciones o Clínicas
INSERT INTO Organizations (Id, Name, Address, ContactPhone, ContactEmail) VALUES
(@OrgGuid, 'Clínica Sanitas Centro', 'Calle Mayor 12, Madrid', '+34910000000', 'contacto@sanitascentro.com');

-- 7. Contadores de Uso (Para el paciente)
INSERT INTO UsageCounters (UserId, TokensUsed, MessagesSent, PeriodStart, PeriodEnd) VALUES
(@PatientId, 500, 5, '2026-03-01 00:00:00', '2026-04-01 00:00:00');

-- 8. Relaciones
-- Paciente con Médico
INSERT INTO DoctorPatient (PatientId, ProfessionalId) VALUES
(@PatientId, @DoctorId);
-- Médico con Organización
INSERT INTO Professional_Org (ProfessionalId, OrganizationId) VALUES
(@DoctorId, @OrgGuid);

-- 9. Horarios Automáticos (Dra. Martínez: Lunes 09:00 a 14:00)
INSERT INTO Professional_Schedules (ProfessionalId, DayOfWeek, StartTime, EndTime, IsAvailable) VALUES
(@DoctorId, 1, '09:00:00', '14:00:00', TRUE);

-- 10. Citas
INSERT INTO Appointments (PatientId, ProfessionalId, AppointmentDate, DurationMinutes, Status, Notes) VALUES
(@PatientId, @DoctorId, '2026-03-10 10:00:00', 30, 'Scheduled', 'Revisión de lunar sospechoso en la espalda.');

-- 11. Chat Sessions (Simulación del Paciente consultando a la IA sobre Dermatología)
SET @ChatSessionId = 'eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee';
INSERT INTO ChatSessions (Id, UserId, SessionType, Title) VALUES
(@ChatSessionId, @PatientId, 'Diagnosis', 'Consulta sobre enrojecimiento en la piel');

-- 12. Mensajes del Chat
SET @Msg1Id = 1, @Msg2Id = 2; -- Asumiremos IDs para los adjuntos
INSERT INTO ChatMessages (ChatSessionId, SenderType, MessageText) VALUES
(@ChatSessionId, 'User', 'Hola IA, me ha salido una mancha roja en el brazo y pica mucho.'),
(@ChatSessionId, 'AI', 'Hola Juan. Según los síntomas y la foto enviada, podría ser una dermatitis de contacto. Te recomiendo pedir cita con dermatología. ¿Quieres que busque hueco con la Dra. Martínez?');

-- 13. Adjuntos de Mensajes (La foto del brazo)
-- Usamos LAST_INSERT_ID() para recoger el ID autonumérico del primer mensaje insertado
INSERT INTO MessageAttachments (ChatMessageId, FileUrl, FileType) VALUES
(LAST_INSERT_ID() - 1, 'https://s3.aws.com/mediscan-bucket/uploads/mancha_brazo.jpg', 'image/jpeg');

-- 14. Diagnósticos e Historial de la Dra. a Juan
INSERT INTO Diagnosis (PatientId, ProfessionalId, Description, ICD10Code) VALUES
(@PatientId, @DoctorId, 'Dermatitis de contacto alérgica', 'L23.9');

-- 15. Tratamiento
INSERT INTO Treatments (DiagnosisId, Description, StartDate, EndDate) VALUES
(LAST_INSERT_ID(), 'Aplicar crema con hidrocortisona al 1% 2 veces al día y paños fríos.', '2026-03-10', '2026-03-17');

-- 16. Reseñas
INSERT INTO Reviews (PatientId, ProfessionalId, Rating, Comment) VALUES
(@PatientId, @DoctorId, 5, 'Excelente trato y muy rápida la IA al agendar.');

-- 17. Planes de pago
INSERT INTO Plans (Name, Price, MaxMessages, Features) VALUES
('Free', 0.00, 20, 'Acceso básico a la IA, agendamiento de citas.'),
('Premium', 9.99, NULL, 'Mensajes IA ilimitados, Historial fotográfico, Prioridad de citas.');

-- 18. Suscripción
INSERT INTO Subscriptions (UserId, PlanId, EndDate) VALUES
(@PatientId, (SELECT Id FROM Plans WHERE Name = 'Premium'), '2026-12-31 23:59:59');

-- 19. Pagos
INSERT INTO Payments (SubscriptionId, Amount, PaymentMethod) VALUES
(LAST_INSERT_ID(), 9.99, 'Tarjeta Crédito terminada en 4444');

-- 20. Facturas y Detalle Factura
INSERT INTO Invoices (PaymentId, InvoiceNumber, TotalAmount) VALUES
(LAST_INSERT_ID(), 'INV-2026-0001', 9.99);

INSERT INTO DetailInvoices (InvoiceId, Description, Quantity, UnitPrice, Subtotal) VALUES
(LAST_INSERT_ID(), 'Suscripción Premium Mensual (Marzo)', 1, 9.99, 9.99);
