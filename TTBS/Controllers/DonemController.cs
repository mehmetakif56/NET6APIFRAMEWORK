using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonemController : BaseController<DonemController>
    {
        private readonly IDonemService _donemService;
        private readonly ILogger<DonemController> _logger;
        public readonly IMapper _mapper;

        public DonemController(IDonemService donemService, ILogger<DonemController> logger, IMapper mapper)
        {
            _donemService = donemService;
            _logger = logger;
            _mapper = mapper;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet]
        public DonemModel Index()
        {
            var donemEntity = _donemService.GetDonem();
            var model = _mapper.Map<DonemModel>(donemEntity);
            return model;
        }
    }
}
