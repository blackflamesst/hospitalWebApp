using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Context
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DoctorProcedure> Doctor_Procedures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorProcedure>()
                .HasKey(dp => new { dp.Doctor_Id, dp.Procedure_Id });

            modelBuilder.Entity<Doctor>().Property(d => d.Category_Id).HasColumnName("category_id");
            modelBuilder.Entity<DoctorProcedure>().ToTable("doctor_procedures");
        }
    }
}