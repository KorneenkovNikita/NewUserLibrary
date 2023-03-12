using Microsoft.EntityFrameworkCore;
using UserLibrary;

namespace DBLibrary
{
	public class ApplicantDBContext : DbContext
	{
		public ApplicantDBContext(DbContextOptions dbContext) : base(dbContext)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Role>(rb =>
			{
				rb.HasKey(r => r.ID);
				rb.HasIndex(r => r.Name).IsUnique();
			});

			modelBuilder.Entity<User>(ur =>
			{
				ur.HasKey(u => u.ID);
				ur.HasIndex(u => u.Name).IsUnique();
				ur.HasOne(u => u.Role)
				  .WithMany()
				  .HasForeignKey("RoleID");
				ur.Navigation(u => u.Role)
				  .AutoInclude();
			});

			modelBuilder.Entity<Applicant>(app =>
			{
				app.HasKey(a => a.ID);
				app.HasIndex(a => a.ID);

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