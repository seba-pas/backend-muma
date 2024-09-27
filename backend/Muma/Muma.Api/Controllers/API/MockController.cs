using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Muma.Api.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MockController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            return await Task.FromResult(Ok("Mocked endpoint - GET result"));
        }
    }
}
