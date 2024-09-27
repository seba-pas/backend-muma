using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Muma.Application.Protectoras;
using Muma.Application.Protectoras.Dtos;

namespace Muma.Api.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Protectoras(ProtectoraService protectorasService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProtectora(int id)
        {
            var protectora = await protectorasService.GetProtectora(id);

            if (protectora == null) { return NotFound(); }

            return Ok(protectora);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var protectoras = await protectorasService.GetAll();

            return Ok(protectoras);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarProtectora([FromBody]RegistroProtectoraDto input)
        { 
            var result = await protectorasService.RegistrarProtectora(input);
            if (result.Success)
            {
                return Ok(new { protectora = result.GetResult() });
            }
            else
            {
                return BadRequest(new { errors = result.GetErrorsList() });
            }

            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProtectora(int id)
        { 
            var deleteResult = await protectorasService.DeleteProtectora(id);

            if (deleteResult.Success)
            {
                return Ok();
            }
            else
            { 
                return BadRequest(new { errors = deleteResult.GetErrorsList() });
            }
        }

    }
}
