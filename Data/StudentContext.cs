using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManager.Areas.Identity.Data;
using StudentManager.Models.Entities;

namespace StudentManager.Data
{
    public class StudentContext : IdentityDbContext<User>
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }

        public DbSet<Student> Students { get; set; }
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(150);
        builder.Property(x => x.LastName).HasMaxLength(150);
    }
}
