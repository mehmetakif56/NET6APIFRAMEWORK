using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController<GlobalController>
    {
        private readonly IReportService _reportService;
        private readonly ILogger<GlobalController> _logger;
        public readonly IMapper _mapper;

        public ReportController(IReportService reportService, ILogger<GlobalController> logger, IMapper mapper)
        {
            _reportService = reportService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetReportStenoPlanBetweenDateGorevTur")]
        public IEnumerable<ReportPlanDetayModel> GetReportStenoPlanBetweenDateGorevTur(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int gorevTuru)
        {
            var stenoGrpEntity = _reportService.GetReportStenoPlanBetweenDateGorevTur(gorevBasTarihi, gorevBitTarihi, gorevTuru);
            var model = _mapper.Map<IEnumerable<ReportPlanDetayModel>>(stenoGrpEntity);
            return model;
        }
        [HttpGet("GetStenoGorevByStenografAndDate")]
        public IEnumerable<ReportPlanModel> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            var stenoGrpEntity = _reportService.GetStenoGorevByStenografAndDate(stenografId, gorevBasTarihi, gorevBitTarihi);
            var model = _mapper.Map<IEnumerable<ReportPlanModel>>(stenoGrpEntity);
            return model;
        }
    }
}
