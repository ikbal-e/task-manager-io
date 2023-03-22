using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagerIO.API.Entities;

namespace TaskManagerIO.API.Infrastructure.Data.EntityTypeConfigurations;

public class DepartmentTypeConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
