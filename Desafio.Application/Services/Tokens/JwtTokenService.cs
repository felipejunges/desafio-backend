using Desafio.Application.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Desafio.Application.Services.Tokens
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly AuthConfiguration _configuration;

        public JwtTokenService(AuthConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(string identifier, string usuarioId, string nomeUsuario, string role)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.KeySecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, identifier),
                    new Claim(ClaimTypes.Upn, usuarioId),
                    new Claim(ClaimTypes.Name, usuarioId),
                    new Claim(ClaimTypes.GivenName, nomeUsuario),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.Add(_configuration.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string? ObterIdentifierDoToken(string token)
        {
            var principal = ObterPrincipalDoTokenExpirado(token);

            if (principal == null)
                return null;

            return principal.FindFirst(ClaimTypes.Sid)?.Value;
        }

        private ClaimsPrincipal? ObterPrincipalDoTokenExpirado(string token)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.KeySecret);
            var signingKey = new SymmetricSecurityKey(key);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateAudience = false,
                ValidateIssuer = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }

        public string GerarRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}