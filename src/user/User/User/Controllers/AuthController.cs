using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using User.Domain.Configurations;

namespace User.Api.Controllers
{
    /// <summary>
    /// Controller de autenticação
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("sys/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SigningCredentials _signingCredentials;
        private const string GUID_STRING_FORMAT = "N";
        private const string IDENTITY_TYPE = "Login";
        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="secretSettings"></param>
        public AuthController(IOptions<SecretSettings> secretSettings)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretSettings.Value.Secret));
            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        /// <summary>
        /// retorna um bearer token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetToken()
        {
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity("gabriel@invalid.com", IDENTITY_TYPE),
                                      new[] { new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(GUID_STRING_FORMAT)),
                                      new Claim(JwtRegisteredClaimNames.UniqueName, "gabriel@invalid.com") });

            var tokenHandler = new JwtSecurityTokenHandler();
            DateTime createdAt = DateTime.Now;
            DateTime expiresAt = createdAt.AddHours(60);

            var secToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "_Issuer",
                Audience = "_Audience",
                Subject = identity,
                NotBefore = createdAt.AddMinutes(-2),
                Expires = expiresAt,
                SigningCredentials = _signingCredentials
            });

            tokenHandler.WriteToken(secToken);

            return Ok(tokenHandler.WriteToken(secToken));
        }
    }
}
