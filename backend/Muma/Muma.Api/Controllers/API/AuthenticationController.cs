using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muma.Api.JWT;
using Muma.Application.Usuarios;


namespace Muma.Api.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTHelper _jwtHelper;
        private readonly IConfiguration _configuration;
        private readonly UsuarioService _usuariosService;
        private readonly int tokenTTL;
        private readonly int refreshTokenTTL;

        public AuthenticationController(IJWTHelper jwtHelper, IConfiguration configuration, UsuarioService usuariosService)
        {
            this._jwtHelper = jwtHelper;
            this._configuration = configuration;
            this._usuariosService = usuariosService;
            tokenTTL = int.Parse(_configuration["JwtSettings:LifetimeMinutes"]);
            refreshTokenTTL = int.Parse(_configuration["JwtSettings:refreshLifetimeMinutes"]);
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] TokenGenerationRequest request)
        {
            var loginResult = await _usuariosService.Login(request.User, request.Password);

            if (loginResult.Success)
            {
                var token = _jwtHelper.GetJWT(request);
                var refreshToken = _jwtHelper.GetRefreshJWT(request);

                return await Task.FromResult(Ok(new
                {
                    token,
                    tokenTTL,
                    refreshToken,
                    refreshTokenTTL,
                    usuario = loginResult.GetResult()
                }));
            }
            else
            {
                return BadRequest(new { errors = loginResult.GetErrorsList() });
            }

        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            //Valida si es un token válido para refresh
            var tokenValidation = _jwtHelper.ValidateRefreshJWT(request.Token);

            if (!tokenValidation.isValid) return BadRequest();

            var tokenRequest = new TokenGenerationRequest() { User = tokenValidation.user };

            var token = _jwtHelper.GetJWT(tokenRequest);
            var refreshToken = _jwtHelper.GetRefreshJWT(tokenRequest);

            return await Task.FromResult(Ok(new
            {
                token,
                tokenTTL,
                refreshToken,
                refreshTokenTTL
            }));
        }

    }
}
