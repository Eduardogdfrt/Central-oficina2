using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ellp.Api.Infra.SqlServer.Configurations
{
    public class WorkshopProfessorConfiguration : IEntityTypeConfiguration<WorkshopProfessor>
    {
        public void Configure(EntityTypeBuilder<WorkshopProfessor> builder)
        {
            builder.ToTable("ProfessorWorkshop");

            builder.HasKey(pw => new { pw.ProfessorId, pw.WorkshopId });

            builder.HasOne(pw => pw.Professor);

        }
    }
}
