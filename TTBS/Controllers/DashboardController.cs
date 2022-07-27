using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController<DashboardController>
    {
        private readonly IStenografService _stenoService;
        private readonly IDashboardService _dashboardService;
        private readonly IGlobalService _globalService;
        private readonly ILogger<DashboardController> _logger;
        public readonly IMapper _mapper;

        public DashboardController(IStenografService stenoService, 
            IGlobalService globalService, 
            IDashboardService dashboardService,
            IMapper mapper,
            ILogger<DashboardController> logger)
        {
            _stenoService = stenoService;
            _globalService = globalService;
            _logger = logger;
            _mapper = mapper;
            _dashboardService = dashboardService;
        }


        [HttpGet("GetActiveGorevler")]
        public IEnumerable<GorevAtama> GetActiveGorevler()
        {
            var entity = _dashboardService.GetActiveGorevler();
            var model = _mapper.Map<IEnumerable<GorevAtama>>(entity);
            return model;
        }
    }
}
