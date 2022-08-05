using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController<ReportController>
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;
        public readonly IMapper _mapper;

        public ReportController(IReportService reportService, ILogger<ReportController> logger, IMapper mapper)
        {
            _reportService = reportService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetReportStenoPlanBetweenDateGorevTur")]
        public IEnumerable<ReportPlanDetayModel> GetReportStenoPlanBetweenDateGorevTur(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int? gorevTuru)
        {
            var stenoGrpEntity = _reportService.GetReportStenoPlanBetweenDateGorevTur(gorevBasTarihi, gorevBitTarihi, gorevTuru);
            var model = _mapper.Map<IEnumerable<ReportPlanDetayModel>>(stenoGrpEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByStenografAndDate")]
        public IEnumerable<ReportPlanModel> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            var stenoGrpEntity = _reportService.GetStenoGorevByStenografAndDate(stenografId, gorevBasTarihi, gorevBitTarihi).GroupBy(x => new { x.BirlesimId, x.StenografId });
            var model = _mapper.Map<IEnumerable<ReportPlanModel>>(stenoGrpEntity);
            return model;
        }
    }
}
