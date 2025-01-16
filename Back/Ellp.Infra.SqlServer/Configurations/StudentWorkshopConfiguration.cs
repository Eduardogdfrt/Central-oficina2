using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ellp.Api.Infra.SqlServer.Configurations
{
    public class StudentWorkshopConfiguration : IEntityTypeConfiguration<WorkshopAluno>
    {
        public void Configure(EntityTypeBuilder<WorkshopAluno> builder)
        {
            builder.HasKey(wa => new { wa.WorkshopId, wa.StudentId });

            builder.HasOne(wa => wa.Workshop)
                   .WithMany(w => w.WorkshopAlunos)
                   .HasForeignKey(wa => wa.WorkshopId);

            builder.HasOne(wa => wa.Student)
                   .WithMany(s => s.WorkshopAlunos)
                   .HasForeignKey(wa => wa.StudentId);
        }
    }
}

