using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Muma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Infrastructure.Data.Persistence.Configurations
{
    public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
    {
        public void Configure(EntityTypeBuilder<Ciudad> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.IdProvincia)
                .HasColumnName("idprovincia");

            builder.Property(c => c.Nombre)
                .HasColumnName("nombre");

            builder.HasOne(c => c.Provincia)
                .WithMany()
                .HasPrincipalKey(p => p.Id)
                .HasForeignKey(c => c.IdProvincia);
        }
    }
}
