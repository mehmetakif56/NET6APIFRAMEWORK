using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonemController : ControllerBase
    {
        private readonly IDonemService _donemService;

        public DonemController(IDonemService donemService)
        {
            _donemService = donemService;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet]
        public DonemModel Index()
        {
            var donemEntity = _donemService.GetDonem();
            //var model = Mapper.Map<DonemModel>(donemEntity);
            return null;
        }
    }
}
