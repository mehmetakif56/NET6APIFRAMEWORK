using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController<GlobalController>
    {
        private readonly IGlobalService _globalService;
        private readonly ILogger<GlobalController> _logger;
        public readonly IMapper _mapper;

        public ReportController(IGlobalService globalService, ILogger<GlobalController> logger, IMapper mapper)
        {
            _globalService = globalService;
            _logger = logger;
            _mapper = mapper;
        }
      
    }
}
