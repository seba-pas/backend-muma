using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muma.Domain.Entities;

namespace Muma.Infrastructure.Data.Persistence.Configurations;

public class ProvinciaConfiguration : IEntityTypeConfiguration<Provincia>
{
    public void Configure(EntityTypeBuilder<Provincia> builder)
    {
        builder.Property(tr => tr.Id)
            .HasColumnName("id");

        builder.Property(tr => tr.Nombre)
            .HasColumnName("nombre");
    }
}
