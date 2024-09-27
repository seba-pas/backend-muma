using Muma.Domain.Entities;

namespace Muma.Application.Combos.Dtos;

public class ProvinciaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}


public static class ProvinciaMaps
{
    public static Func<Provincia, ProvinciaDto> MapToDto()
    {
        return t => new ProvinciaDto()
        {
            Id = t.Id,
            Nombre = t.Nombre,
        };
    }
}