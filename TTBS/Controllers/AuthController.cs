using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController<AuthController>
    {
        private readonly ILogger<DashboardController> _logger;
        public readonly IMapper _mapper;
        public readonly IAuthService _authService;

        public AuthController(IAuthService authService,
            IMapper mapper,
            ILogger<DashboardController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _authService=authService;
        }


        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            User userToLogin;
            try
            {
                userToLogin = _authService.Login(userForLoginDto);
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
            

            var result = _authService.CreateAccessToken(userToLogin);
           
                return Ok(result);
           
        }
    }
}
