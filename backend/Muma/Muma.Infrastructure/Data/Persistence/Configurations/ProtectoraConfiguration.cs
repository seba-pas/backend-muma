using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Infrastructure.Data.Persistence.Configurations
{
    public class ProtectoraConfiguration : IEntityTypeConfiguration<Protectora>
    {
        public void Configure(EntityTypeBuilder<Protectora> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id");
			builder.Property(x => x.UsuarioAsociadoId).HasColumnName("usuarioasociado");
			builder.Property(x => x.Nombre).HasColumnName("nombre");
            builder.Property(x => x.Descripcion).HasColumnName("descripcion");
            builder.Property(x => x.PaginaWeb).HasColumnName("paginaweb");
            builder.Property(x => x.Instagram).HasColumnName("instagram");
            builder.Property(x => x.Facebook).HasColumnName("facebook");
            builder.Property(x => x.IdCiudad).HasColumnName("idciudad");
            builder.Property(x => x.Calle).HasColumnName("calle");
            builder.Property(x => x.Numero).HasColumnName("numero");
            builder.Property(x => x.Piso).HasColumnName("piso");
            builder.Property(x => x.Departamento).HasColumnName("departamento");
            builder.Property(x => x.CantidadDeMascotas).HasColumnName("cantidaddemascotas");

            builder
                .HasOne(p => p.Ciudad)
                .WithMany()
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(p => p.IdCiudad);
        }
    }
}
