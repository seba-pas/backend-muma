using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muma.Application.Combos;
using Muma.Application.Combos.Dtos;

namespace Muma.Api.Controllers.API;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CombosController(ComboService comboService) : ControllerBase
{
    [HttpGet("TipoRegistros")]
    public async Task<List<TipoRegistroDto>> GetTipoRegistros()
        => await comboService.GetAllTipoRegistros();

    [HttpGet("Provincias")]
    public async Task<List<ProvinciaDto>> GetProvincias() 
        => await comboService.GetAllProvincias();

    [HttpGet("Ciudades/{idProvincia}")]
    public async Task<List<CiudadDto>> GetCiudades(int idProvincia)
        => await comboService.GetAllCiudades(idProvincia);

}
