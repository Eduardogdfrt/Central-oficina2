using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ellp.Api.Infra.SqlServer.Configurations
{
    public class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.ToTable("Workshop");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName("Nome")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.ProfessorIdW)
                .HasColumnName("ProfessorId")
                .IsRequired();

            builder.Property(x => x.HelperIDW)
                .HasColumnName("AlunoId")
                .IsRequired(false);

            builder.Property(x => x.Data)
                .HasColumnName("Data")
                .IsRequired();

            builder.HasOne(x => x.Professor)
                .WithMany()
                .HasForeignKey(x => x.ProfessorIdW)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Helper)
                .WithMany()
                .HasForeignKey(x => x.HelperIDW)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}

