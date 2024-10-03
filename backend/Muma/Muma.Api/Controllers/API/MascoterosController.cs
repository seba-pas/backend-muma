using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muma.Application.Mascoteros;
using Muma.Application.Mascoteros.Dtos;

namespace Muma.Api.Controllers.API;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MascoterosController(MascoteroService mascoteroService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProtectora(int id)
    {
        var mascotero = await mascoteroService.GetMascotero(id);

        if (mascotero == null) { return NotFound(); }

        return Ok(mascotero);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var mascoteros = await mascoteroService.GetAll();

        return Ok(mascoteros);
    }

    [AllowAnonymous]
    [HttpPost("registro")]
    public async Task<IActionResult> RegistrarMascotero([FromBody] RegistrarMascoteroDto input)
    {
        var result = await mascoteroService.RegistrarMascotero(input);
        if (result.Success)
        {
            return Ok(new { mascotero = result.GetResult() });
        }
        else
        {
            return BadRequest(new { errors = result.GetErrorsList() });
        }


    }
}
