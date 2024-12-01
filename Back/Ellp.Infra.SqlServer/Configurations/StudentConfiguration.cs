using Ellp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ellp.Api.Infra.SqlServer.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Aluno");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Password)
             .HasColumnName("Senha")
             .HasMaxLength(100)
             .IsRequired(false);

            builder.Property(x => x.BirthDate)
                .HasColumnName("DataNascimento")
                .IsRequired();

            builder.Property(x => x.Name)
             .HasColumnName("Nome")
             .IsRequired()
             .HasMaxLength(100);

            builder.Property(x => x.IsAuthenticated)
                .HasColumnName("Autenticado") 
                .IsRequired();
        }
    }
}






