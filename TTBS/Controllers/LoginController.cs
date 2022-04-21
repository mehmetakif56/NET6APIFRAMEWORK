using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
            if (_sessionHelper.User != null)
            {
                var claims = _contextAccessor.HttpContext.User.Claims;
                var user = _sessionHelper.User;
                var model = _mapper.Map<UserModel>(user);
                model.Token = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
                return Ok(model);
            }

            return NotFound("User not found");
        }

      

        
    }
}
