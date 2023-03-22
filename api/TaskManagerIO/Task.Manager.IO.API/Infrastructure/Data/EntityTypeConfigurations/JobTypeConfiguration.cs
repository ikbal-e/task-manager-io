using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagerIO.API.Entities;

namespace TaskManagerIO.API.Infrastructure.Data.EntityTypeConfigurations;

public class JobTypeConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey(x => x.DepartmnetId);

        builder
            .HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById);

        builder
            .HasOne(x => x.AssignedTo)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.AssignedToId);

        builder
            .HasOne(x => x.ClosedBy)
            .WithMany()
            .HasForeignKey(x => x.ClosedById);

        builder
            .HasOne(x => x.ApprovedBy)
            .WithMany()
            .HasForeignKey(x => x.ApprovedById);
    }
}