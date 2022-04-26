using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StenografController : BaseController<StenografController>
    {
        private readonly IStenografService _stenoService;
        private readonly ILogger<StenografController> _logger;
        public readonly IMapper _mapper;

        public StenografController(IStenografService stenoService, ILogger<StenografController> logger, IMapper mapper)
        {
            _stenoService = stenoService;
            _logger = logger;
            _mapper = mapper;
        }
        #region StenoPlan
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetStenoGorevPlan")]
        public IEnumerable<StenoPlanModel> GetStenoGorevPlan()
        {
            var stenoEntity = _stenoService.GetStenoPlan;
            var model = _mapper.Map<IEnumerable<StenoPlanModel>>(stenoEntity);
            return model;
        }

        [HttpPost("CreateStenoPlan")]
        public IActionResult CreateStenoPlan(StenoPlanModel model)
        {
            StenoPlan entity = Mapper.Map<StenoPlan>(model);
            _stenoService.CreateStenoPlan(entity);
            return Ok(entity);

        }
        #endregion 
        #region StenoIzin
        [HttpGet("GetAllStenoIzin")]
        public IEnumerable<StenoIzinModel> GetAllStenoIzin()
        {
            var stenoEntity = _stenoService.GetAllStenoIzin();
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoIzinById")]
        public IEnumerable<StenoIzinModel> GetStenoIzinById(Guid id)
        {
            var stenoEntity = _stenoService.GetStenoIzinById(id);
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoIzinByName")]
        public IEnumerable<StenoIzinModel> GetStenoIzinByName(string adSoyad)
        {
            var stenoEntity = _stenoService.GetStenoIzinByName(adSoyad);
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoIzinByName")]
        public IEnumerable<StenoIzinModel> GetStenoIzinByName(DateTime basTarihi,DateTime bitTarihi)
        {
            var stenoEntity = _stenoService.GetStenoIzinBetweenDate(basTarihi, bitTarihi);
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpPost("CreateStenoIzin")]
        public IActionResult CreateStenoIzin(StenoIzinModel model)
        {
            var entity = Mapper.Map<StenoIzin>(model);
            _stenoService.CreateStenoIzin(entity);
            return Ok(entity);

        }
        #endregion

        #region StenoGorev

        [HttpGet("GetStenoGorevById")]
        public IEnumerable<StenoGorevModel> GetStenoGorevById(Guid id)
        {
            var stenoEntity = _stenoService.GetStenoGorevById(id);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByName")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByName(string adSoyad)
        {
            var stenoEntity = _stenoService.GetStenoGorevByName(adSoyad);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByDateAndTime")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByDateAndTime(DateTime? basTarihi, DateTime? basSaati)
        {
            if(basTarihi.HasValue && basSaati.HasValue)
            {
                var stenoEntity = _stenoService.GetStenoGorevByDateAndTime(basTarihi, basSaati.Value.Hour * 60 + basSaati.Value.Minute);
                var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
                return model;
            }
           return new List<StenoGorevModel>();
        }

        [HttpPost("CreateStenoGorev")]
        public IActionResult CreateStenoGorev(StenoGorevModel model)
        {
            var entity = Mapper.Map<StenoGorev>(model);
            _stenoService.CreateStenoGorev(entity);
            return Ok(entity);

        }
        #endregion
    }
}
