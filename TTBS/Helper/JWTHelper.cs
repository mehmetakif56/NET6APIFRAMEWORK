using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TTBS.Helper
{
    public class JWTHelper
    {
        private readonly JwtSecurityTokenHandler JwtTokenHandler;
        internal static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());

        public JWTHelper()
        {
            JwtTokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateJwtToken(string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new InvalidOperationException("Name is not specified.");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, paramName) };
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("ExampleServer", "ExampleClients", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
            return JwtTokenHandler.WriteToken(token);
        }
    }
}
