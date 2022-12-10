using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProjectName).IsRequired();
            builder.Property(x => x.CompanyCustomer).IsRequired();

            builder.HasMany(x => x.Workers)
                .WithMany(x => x.Projects)
                .UsingEntity(x=> x.ToTable("ProjectWorker"));

            builder.HasOne(x => x.Manager)
                .WithMany()
                .HasForeignKey("ManagerForeignKey")
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
