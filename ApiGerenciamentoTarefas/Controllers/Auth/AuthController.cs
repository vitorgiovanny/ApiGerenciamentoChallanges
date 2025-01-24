using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiGerenciamentoTarefas.Controllers.Auth
{
    [Route("api/authenticator")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _key;

        public AuthController(IConfiguration config)
        {
            _key = config["Jwt:Secret"];
        }

        [HttpGet("Auth")]
        public IActionResult Authenticar()
        {
            return Ok(GenerateJwtToken("AnythingNames"));
        }

        private string GenerateJwtToken(string anything)
        {
          var claims = new[]
          {
                new Claim(ClaimTypes.Name,anything),
                new Claim("meuValor", "oque voce quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // tempo de expiração do token: 1 hora
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
