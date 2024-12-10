using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ellp.Api.Infra.SqlServer.Configurations;

public class WorkshopAlunoConfiguration : IEntityTypeConfiguration<WorkshopAluno>
{
    public void Configure(EntityTypeBuilder<WorkshopAluno> builder)
    {
        builder.ToTable("WorkshopAluno");

        builder.HasKey(wa => new { wa.WorkshopId, wa.StudentId });

    }
}
