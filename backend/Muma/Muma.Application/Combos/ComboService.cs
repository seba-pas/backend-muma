using Microsoft.EntityFrameworkCore;
using Muma.Application.Combos.Dtos;
using Muma.Application.Common;
using Muma.Domain.Entities;

namespace Muma.Application.Combos;

public class ComboService(IApplicationDbContext context)
{
    public async Task<List<TipoRegistroDto>> GetAllTipoRegistros()
    {
        var tipoRegistros = await context.TipoRegistros.ToListAsync();

        return tipoRegistros
            .Select(TipoRegistroMaps.MapToDto())
            .ToList();
    }

    public async Task<List<ProvinciaDto>> GetAllProvincias()
    {
        List<Provincia> provincias = await context.Provincias.ToListAsync();

        return provincias
            .Select(ProvinciaMaps.MapToDto())
            .ToList();
    }

    public async Task<List<CiudadDto>> GetAllCiudades(int idProvincia)
    { 
        List<Ciudad> ciudades = await context.Ciudades.Where(x => x.IdProvincia == idProvincia).ToListAsync();

        return ciudades
            .Select(CiudadMaps.MapToDto())
            .ToList();
    }
}
