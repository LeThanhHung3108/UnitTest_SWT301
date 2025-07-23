using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Context
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext()
        {
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HealthCheckupResult> HealthCheckupResults { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<MedicalConsultation> MedicalConsultations { get; set; }
        public DbSet<MedicalIncident> MedicalIncidents { get; set; }
        public DbSet<MedicalSupply> MedicalSupplies { get; set; }
        public DbSet<MedicalSupplyUsage> MedicalSupplyUsages { get; set; }
        public DbSet<MedicalRequest> MedicationRequests { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<ConsentForm> ConsentForms { get; set; }
        public DbSet<VaccinationResult> VaccinationResults { get; set; }
        public DbSet<MedicalDiary> MedicalDiaries { get; set; }
        public DbSet<ScheduleDetail> ScheduleDetails { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(options =>
            {
                options.HasMany(u => u.Blogs)
                    .WithOne(b => b.Author)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasMany(u => u.MedicalConsultations)
                    .WithOne(m => m.MedicalStaff)
                    .HasForeignKey(m => m.MedicalStaffId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasMany(u => u.MedicationRequests)
                    .WithOne(m => m.MedicalStaff)
                    .HasForeignKey(m => m.MedicalStaffId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasMany(u => u.Students)
                    .WithOne(s => s.Parent)
                    .HasForeignKey(s => s.ParentId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasMany(u => u.MedicalIncidents)
                    .WithOne(c => c.MedicalStaff)
                    .HasForeignKey(c => c.MedicalStaffId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Student>(options =>
            {
                options.HasMany(s => s.ScheduleDetails)
                    .WithOne(v => v.Student)
                    .HasForeignKey(v => v.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasMany(s => s.MedicationRequests)
                    .WithOne(m => m.Student)
                    .HasForeignKey(m => m.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasMany(s => s.ConsentForms)
                    .WithOne(c => c.Student)
                    .HasForeignKey(c => c.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasMany(s => s.MedicalIncidents)
                    .WithOne(m => m.Student)
                    .HasForeignKey(m => m.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasMany(s => s.MedicalConsultations)
                    .WithOne(m => m.Student)
                    .HasForeignKey(m => m.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MedicalSupplyUsage>(options =>
            {
                options.HasKey(msu => new { msu.SupplyId, msu.IncidentId });

                options.HasOne(msu => msu.MedicalSupply)
                    .WithMany(ms => ms.MedicalSupplyUsages)
                    .HasForeignKey(msu => msu.SupplyId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasOne(msu => msu.Incident)
                    .WithMany(mi => mi.MedicalSupplyUsages)
                    .HasForeignKey(msu => msu.IncidentId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MedicalRequest>(options =>
            {
                options.HasMany(m => m.MedicalDiaries)
                    .WithOne(md => md.MedicationReq)
                    .HasForeignKey(md => md.MedicationReqId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Campaign>(options =>
            {
                options.HasMany(c => c.Schedules)
                    .WithOne(s => s.Campaign)
                    .HasForeignKey(s => s.CampaignId)
                    .OnDelete(DeleteBehavior.NoAction);

                options.HasMany(c => c.ConsentForms)
                    .WithOne(cf => cf.Campaign)
                    .HasForeignKey(cf => cf.CampaignId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Schedule>(options =>
            {
                options.HasMany(s => s.ScheduleDetails)
                    .WithOne(sd => sd.Schedule)
                    .HasForeignKey(sd => sd.ScheduleId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}