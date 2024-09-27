using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Muma.Api.JWT
{
    public class JWTHelper : IJWTHelper
    {
        private readonly IConfiguration _configuration;

        private readonly string tokenSecret = "";
        private readonly string issuer = "";
        private readonly string audience = "";
        private readonly TimeSpan tokenLifetime;

        //Refresh token
        private readonly string refreshTokenSecret = "";
        private readonly TimeSpan refreshTokenLifetime;

        public JWTHelper(IConfiguration configuration)
        {
            this._configuration = configuration;

            this.tokenSecret = _configuration["JwtSettings:Key"];
            this.issuer = _configuration["JwtSettings:Issuer"];
            this.audience = _configuration["JwtSettings:Audience"];
            this.tokenLifetime = TimeSpan.FromMinutes(int.Parse(_configuration["JwtSettings:LifetimeMinutes"]));

            //Refresh token
            this.refreshTokenSecret = _configuration["JwtSettings:refreshKey"];
            this.refreshTokenLifetime = TimeSpan.FromMinutes(int.Parse(_configuration["JwtSettings:refreshLifetimeMinutes"]));
        }

        public string GetJWT(TokenGenerationRequest request)
        {
            return GenerateToken(request.User, this.tokenSecret, this.tokenLifetime);
        }

        public string GetRefreshJWT(TokenGenerationRequest request)
        {
            return GenerateToken(request.User, this.refreshTokenSecret, this.refreshTokenLifetime);
        }

        public (bool isValid, string user) ValidateRefreshJWT(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = this.issuer,
                    ValidAudience = this.audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.refreshTokenSecret))
                };

                SecurityToken validatedToken;

                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                var user = principal.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
                
                return (true, user);
            }
            catch (Exception)
            {
                return (false, "");
            }
        }

        private string GenerateToken(string user, string tokenSecret, TimeSpan lifeTime) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(tokenSecret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Name, user)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(lifeTime),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }




    }
}
