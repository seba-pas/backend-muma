using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;

namespace Muma.Infrastructure.Data.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Nombre).HasColumnName("nombre");
            builder.Property(x => x.Apellido).HasColumnName("apellido");
            builder.Property(x => x.Password).HasColumnName("password");
            builder.Property(x => x.IdTipoRegistro).HasColumnName("idtiporegistro");

            builder.HasOne(u => u.TipoRegistro)
                .WithMany()
                .HasPrincipalKey(tr => tr.Id)
                .HasForeignKey(u => u.IdTipoRegistro);
        }
    }
}
