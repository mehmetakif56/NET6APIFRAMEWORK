using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StenografController : BaseController<DonemController>
    {
        private readonly IStenografService _stenoService;
        private readonly ILogger<DonemController> _logger;
        public readonly IMapper _mapper;

        public StenografController(IStenografService stenoService, ILogger<DonemController> logger, IMapper mapper)
        {
            _stenoService = stenoService;
            _logger = logger;
            _mapper = mapper;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet]
        public IEnumerable<StenoPlanModel> GetStenoGorevPlan()
        {
            var stenoEntity = _stenoService.GetStenoPlan;
            var model = _mapper.Map<IEnumerable<StenoPlanModel>>(stenoEntity);
            return model;
        }

        [HttpPost]
        public IActionResult CreateStenoPlan(StenoPlanModel model)
        {
            StenoPlan entity = Mapper.Map<StenoPlan>(model);
            _stenoService.CreateStenoPlan(entity);
            return View(model);

        }
    }
}
