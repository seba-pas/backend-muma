using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muma.Domain.Entities;

namespace Muma.Infrastructure.Data.Persistence.Configurations;

public class TipoRegistroConfiguration : IEntityTypeConfiguration<TipoRegistro>
{
    public void Configure(EntityTypeBuilder<TipoRegistro> builder)
    {
        builder.Property(tr => tr.Id)
            .HasColumnName("id");

        builder.Property(tr => tr.Descripcion)
            .HasColumnName("descripcion");
    }
}
