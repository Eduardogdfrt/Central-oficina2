using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ellp.Api.Infra.SqlServer.Configurations
{
    public class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professor");
            builder.HasKey(x => x.ProfessorId);

            builder.Property(x => x.ProfessorId)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd(); 

            builder.Property(x => x.Name)
                .HasColumnName("Nome")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Specialty)
                .HasColumnName("Especialidade")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Password)
                .HasColumnName("Senha")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
