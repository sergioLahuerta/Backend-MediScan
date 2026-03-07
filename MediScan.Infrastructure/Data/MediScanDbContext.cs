using Microsoft.EntityFrameworkCore;
using MediScan.Core.Entities;
using MediScan.Core.Enums;

namespace MediScan.Infrastructure.Data;

public class MediScanDbContext : DbContext
{
    public MediScanDbContext(DbContextOptions<MediScanDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UsageCounter> UsageCounters { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Professional> Professionals { get; set; } = null!;
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<DoctorPatient> DoctorPatients { get; set; } = null!;
    public DbSet<ProfessionalOrganization> ProfessionalOrganizations { get; set; } = null!;
    public DbSet<ProfessionalSchedule> ProfessionalSchedules { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<ChatSession> ChatSessions { get; set; } = null!;
    public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
    public DbSet<MessageAttachment> MessageAttachments { get; set; } = null!;
    public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
    public DbSet<Treatment> Treatments { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Plan> Plans { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<DetailInvoice> DetailInvoices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Composite Keys
        modelBuilder.Entity<DoctorPatient>()
            .HasKey(dp => new { dp.PatientId, dp.ProfessionalId });

        modelBuilder.Entity<ProfessionalOrganization>()
            .ToTable("Professional_Org")
            .HasKey(po => new { po.ProfessionalId, po.OrganizationId });

        // Primary Keys for User relative entities
        modelBuilder.Entity<Patient>().HasKey(p => p.UserId);
        modelBuilder.Entity<Patient>().HasOne(p => p.User).WithOne(u => u.Patient).HasForeignKey<Patient>(p => p.UserId);

        modelBuilder.Entity<Professional>().HasKey(p => p.UserId);
        modelBuilder.Entity<Professional>().HasOne(p => p.User).WithOne(u => u.Professional).HasForeignKey<Professional>(p => p.UserId);

        modelBuilder.Entity<ProfessionalSchedule>().ToTable("Professional_Schedules");

        // Relationships configurations to prevent cascading loops
        modelBuilder.Entity<Appointment>().HasOne(a => a.Patient).WithMany(p => p.Appointments).HasForeignKey(a => a.PatientId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Appointment>().HasOne(a => a.Professional).WithMany(p => p.Appointments).HasForeignKey(a => a.ProfessionalId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Diagnosis>().ToTable("Diagnosis");
        modelBuilder.Entity<Diagnosis>().HasOne(d => d.Patient).WithMany(p => p.Diagnoses).HasForeignKey(d => d.PatientId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Diagnosis>().HasOne(d => d.Professional).WithMany(p => p.Diagnoses).HasForeignKey(d => d.ProfessionalId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Review>().HasOne(r => r.Patient).WithMany(p => p.Reviews).HasForeignKey(r => r.PatientId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Review>().HasOne(r => r.Professional).WithMany(p => p.Reviews).HasForeignKey(r => r.ProfessionalId).OnDelete(DeleteBehavior.Restrict);

        // Enum conversions
        modelBuilder.Entity<Appointment>().Property(e => e.Status).HasConversion<string>();
        modelBuilder.Entity<ChatSession>().Property(e => e.SessionType).HasConversion<string>();
        modelBuilder.Entity<ChatMessage>().Property(e => e.SenderType).HasConversion<string>();
        modelBuilder.Entity<Subscription>().Property(e => e.Status).HasConversion<string>();
        modelBuilder.Entity<Payment>().Property(e => e.Status).HasConversion<string>();
    }
}
