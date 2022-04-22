using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TTBS.Core.Entities;
using TTBS.Core.Interfaces;
using TTBS.Services;

namespace TTBS.Middlewares
{
    public class UserSessionMiddleware
    {
        private RequestDelegate _next;
        private IConfiguration _config;

        /// <summary>
        /// Gets user data from storage and creates session data
        /// </summary>
        /// <param name="next"></param>
        public UserSessionMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context,
            IUserService userService,
            ISessionHelper sessionHelper,
            ILogger<UserSessionMiddleware> logger)
        {

            var user = userService.GetUserByUserName("mehmetakif.ayd");
            if(user !=null && user.UserRoles != null)
            {
                var token = Generate(user);
                  
                List<ClaimEntity> roleClaims = userService.GetUserRoleClaims(user.UserRoles.Select(ur => ur.Role.RoleStatusId).ToArray()).ToList();

                #region Create user claims

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(CreateUserClaims(user, roleClaims, token),
                    CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                    });
                sessionHelper.User = user;
                context.User = claimsPrincipal;
            }
   
            #endregion
            await _next.Invoke(context);
        }

        private IEnumerable<Claim> CreateUserClaims(UserEntity user, IEnumerable<ClaimEntity> roleClaims, string token)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email,user.Email??""),
                new Claim(ClaimTypes.NameIdentifier, token)
            };
            claims.AddRange(user.UserRoles.ToList().Select(o => new Claim(ClaimTypes.Role, o.Role.Name.ToString())));
            claims.AddRange(roleClaims.Select(o => new Claim(o.ClaimType, o.ClaimValue)));

            return claims;
        }

        private string Generate(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.UserName) };
             var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

