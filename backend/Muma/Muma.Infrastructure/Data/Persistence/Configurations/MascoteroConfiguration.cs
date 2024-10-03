using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muma.Domain.Entities;

namespace Muma.Infrastructure.Data.Persistence.Configurations;

public class MascoteroConfiguration : IEntityTypeConfiguration<Mascotero>
{
    public void Configure(EntityTypeBuilder<Mascotero> builder)
    {
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UsuarioAsociadoId).HasColumnName("usuarioasociado");
        builder.Property(x => x.Nombre).HasColumnName("nombre");
        builder.Property(x => x.Email).HasColumnName("email");
    }
}
