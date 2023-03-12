using Microsoft.EntityFrameworkCore;
using UserLibrary;

namespace DBLibrary
{
    public class ApplicantDbContext : DbContext
    {
        public ApplicantDbContext(DbContextOptions dbContext) : base(dbContext)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(rb =>
            {
                rb.HasKey(r => r.Id);
                rb.HasIndex(r => r.Name).IsUnique();
            });

            modelBuilder.Entity<User>(ur =>
            {
                ur.HasKey(u => u.Id);
                ur.HasIndex(u => u.FullName).IsUnique();
                ur.HasOne(u => u.Role)
                    .WithMany()
                    .HasForeignKey("RoleId");
                ur.Navigation(u => u.Role)
                    .AutoInclude();
            });

            modelBuilder.Entity<Applicant>(app =>
            {
                app.HasKey(a => a.Id);
                app.HasIndex(a => a.Id);

                app.OwnsOne(ap => ap.Document, appDocBuilder =>
                {
                    appDocBuilder.Property<Guid>("ApplicantId");

                    appDocBuilder.WithOwner()
                        .HasForeignKey("ApplicantId");

                    appDocBuilder.ToTable("ApplicantDocument");
                });
                app.ToTable("ApplicantDocument");
            });
        }
    }
}