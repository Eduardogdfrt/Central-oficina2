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

            builder.Property(x => x.Nome)
                .HasColumnName("Nome")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Data)
                .HasColumnName("Data")
                .IsRequired();

        }
    }
}

