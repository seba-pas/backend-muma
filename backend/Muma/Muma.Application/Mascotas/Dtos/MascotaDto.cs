using Muma.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Muma.Application.Mascotas.Dtos;

public class MascotaDto
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required]
    public string TipoAnimal { get; set; }

    public string Raza { get; set; }

    public string Descripcion { get; set; }

    [Required]
    public string Sexo { get; set; }

    [Required]
    public string Tamano { get; set; }

    public string? TemperamentoConAnimales { get; set; }

    public string? TemperamentoConPersonas { get; set; }
    public int? Edad { get; set; }
    public string? Estado { get; set; }

    [Required]
    public string Ciudad { get; set; }

    [Required]
    public string MesAnioNacimiento { get; set; }

    [Required]
    public int ProtectoraId { get; set; }

    [Required]
    public List<string> Fotos { get; set; }
}

public static class MascotaMaps
{
    public static MascotaDto MapToDto(Mascota mascota)
    {
        return new MascotaDto
        {
            Id = mascota.Id,
            Nombre = mascota.Nombre,
            TipoAnimal = mascota.TipoAnimal,
            Raza = mascota.Raza,
            Descripcion = mascota.Descripcion,
            MesAnioNacimiento = mascota.MesAnioNacimiento,
            Sexo = mascota.Sexo,
            Tamano = mascota.Tamano,
            Ciudad = mascota.Ciudad,
            ProtectoraId = mascota.ProtectoraId,
            Fotos = mascota.Fotos
        };
    }
}