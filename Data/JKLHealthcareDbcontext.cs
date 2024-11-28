using Microsoft.EntityFrameworkCore;
using JKLHealthcare.Models;

namespace JKLHealthcare.Data
{
    public class JKLHealthcareDbContext : DbContext
    {
        public JKLHealthcareDbContext(DbContextOptions<JKLHealthcareDbContext> options)
            : base(options)
        {
        }

        // Define DbSets for each models
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Caregiver> Caregivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Patient entity with a primary key
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.PatientId);

            
        }
    }
}
