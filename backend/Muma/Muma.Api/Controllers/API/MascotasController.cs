using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muma.Application.Mascotas;
using Muma.Application.Mascotas.Dtos;
using Muma.Application.Usuarios;
using System.Security.Claims;

namespace Muma.Api.Controllers.API;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MascotasController(MascotaService mascotaService, UsuarioService usuarioService) : ControllerBase
{
    private readonly MascotaService _mascotaService = mascotaService;
    private readonly UsuarioService _usuarioService = usuarioService;

    [HttpPost("registro")]
    public async Task<IActionResult> RegistrarMascota([FromBody] MascotaDto input)
    {
        List<string> validationResult = MascotaService.ValidarCreacionModificacionMascota(input);
        if (validationResult != null)
            return BadRequest(validationResult);

        try
        {
            var result = await _mascotaService.CrearMascota(input);
            if (result.Success)
            {
                return Ok(new { id = result.GetResult().Id, nombre = result.GetResult().Nombre });
            }
            return BadRequest(result.GetErrorsList());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message, innerException = ex.InnerException?.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerMascota(int id)
    {
        var result = await _mascotaService.ObtenerMascota(id);
        if (result == null)
        {
            return NotFound(new { message = $"No se encontró una mascota con el id {id}" });
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodasMascotas()
    {
        var result = await _mascotaService.ObtenerTodasMascotas();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ModificarMascota(int id, [FromBody] MascotaDto input)
    {
        List<string> validationResult = MascotaService.ValidarCreacionModificacionMascota(input);
        if (validationResult != null)
            return BadRequest(validationResult);

        var result = await _mascotaService.ModificarMascota(id, input);
        if (result.Success)
        {
            return Ok(new { id = result.GetResult().Id, nombre = result.GetResult().Nombre });
        }
        return BadRequest(result.GetErrorsList());
    }

    [HttpPost("{id}/baja")]
    public async Task<IActionResult> DarDeBajaMascota(int id, [FromBody] MascotaBajaDto input)
    {
        var emailUsuario = User.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrWhiteSpace(emailUsuario))
        {
            return Unauthorized(new { message = "Usuario no autenticado" });
        }

        var usuario = usuarioService.GetUsuarioByEmail(emailUsuario);

        if (usuario == null)
        {
            return Unauthorized("Usuario no encontrado o no autorizado.");
        }

        var result = await _mascotaService.DarDeBajaMascota(id, input, usuario.Id);

        if (result.Success)
        {
            return Ok(result.GetResult());
        }

        return BadRequest(result.GetErrorsList());
    }
}