using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Muma.Domain.Entities;
namespace Muma.Infrastructure.Data.Persistence.Configurations;

public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
{
    public void Configure(EntityTypeBuilder<Mascota> builder)
    {
        builder.Property(m => m.Id).HasColumnName("id");
        builder.Property(m => m.Nombre).HasColumnName("nombre").IsRequired();
        builder.Property(m => m.TipoAnimal).HasColumnName("tipoanimal").IsRequired();
        builder.Property(m => m.Raza).HasColumnName("raza");
        builder.Property(m => m.Descripcion).HasColumnName("descripcion");
        builder.Property(m => m.Sexo).HasColumnName("sexo").IsRequired();
        builder.Property(m => m.Tamano).HasColumnName("tamano").IsRequired();
        builder.Property(m => m.Ciudad).HasColumnName("ciudad").IsRequired();
        builder.Property(m => m.MesAnioNacimiento).HasColumnName("mesanionacimiento").IsRequired();
        builder.Property(m => m.Edad).HasColumnName("edad");
        builder.Property(m => m.Baja).HasColumnName("baja").IsRequired();
        builder.Property(m => m.MotivoBaja).HasColumnName("motivobaja");
        builder.Property(m => m.IdMascotero).HasColumnName("idmascotero");
        builder.Property(m => m.FechaAdopcion).HasColumnName("fechaadopcion");
        builder.Property(m => m.DescripcionBaja).HasColumnName("descripcionbaja");
        builder.Property(m => m.TemperamentoConAnimales).HasColumnName("temperamentoconanimales");
        builder.Property(m => m.TemperamentoConPersonas).HasColumnName("temperamentoconpersonas");

        builder.Property(m => m.Fotos).HasColumnName("fotos").IsRequired();
        builder.Property(m => m.ProtectoraId).HasColumnName("protectoraid");

        builder.HasOne(m => m.Protectora)
               .WithMany(p => p.Mascotas)
               .HasForeignKey(m => m.ProtectoraId)
               .IsRequired();
    }
}