using Muma.Domain.Entities;

namespace Muma.Application.Combos.Dtos;

public sealed class TipoRegistroDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}

public static class TipoRegistroMaps
{
    public static Func<TipoRegistro, TipoRegistroDto> MapToDto()
    {
        return t => new TipoRegistroDto()
        {
            Id = t.Id,
            Descripcion = t.Descripcion,
        };
    }
}
