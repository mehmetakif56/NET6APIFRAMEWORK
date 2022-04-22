using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TTBS.Core.Interfaces;
using TTBS.Models;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ISessionHelper _sessionHelper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<DonemController> _logger;
        public readonly IMapper _mapper;

        public LoginController(IConfiguration config, 
                               ISessionHelper sessionHelper,
                               IHttpContextAccessor contextAccessor,
                               ILogger<DonemController> logger, IMapper mapper)
        {
            _sessionHelper = sessionHelper;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _mapper = mapper;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost]
        public IActionResult Login()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var roleList = userClaims.Where(x => x.Type == ClaimTypes.Role);


                return Ok(new UserModel
                {
                    Token = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    FullName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Roles = userClaims.Where(x => x.Type == ClaimTypes.Role)?
                            .Select(c => new RoleModel { RoleName = c.Value }).ToArray()

               
            });
            }

            return NotFound("User not found");
        }
    }
}
