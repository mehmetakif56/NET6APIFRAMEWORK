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
        [HttpGet("GetStenoPlan")]
        public IEnumerable<StenoPlanModel> GetStenoPlan()
        {
            var stenoEntity = _stenoService.GetStenoPlan();
            var model = _mapper.Map<IEnumerable<StenoPlanModel>>(stenoEntity);
            return model;
        }

        [HttpPost("CreateStenoPlan")]
        public IActionResult CreateStenoPlan(StenoPlanOlusturModel model)
        {
            try
            {
                var entity = Mapper.Map<StenoPlan>(model);
                _stenoService.CreateStenoPlan(entity);
            }
            catch (Exception ex)
            {

            }
           
            return Ok();

        }

        [HttpGet("GetStenoPlanByStatus")]
        public List<StenoPlanModel> GetStenoPlanByStatus(int status=0)
        {
            var stenoEntity = _stenoService.GetStenoPlanByStatus(status);
            var model = _mapper.Map<List<StenoPlanModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoPlanByDateAndStatus")]
        public List<StenoPlanModel> GetStenoPlanByDateAndStatus(DateTime gorevTarihi,int gorevTuru)
        {
            var stenoEntity = _stenoService.GetStenoPlanByDateAndStatus(gorevTarihi, gorevTuru);

            var model = _mapper.Map<List<StenoPlanModel>>(stenoEntity);
            return model;
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

        [HttpGet("GetStenoIzinByStenografId")]
        public IEnumerable<StenoIzinModel> GetStenoIzinByStenografId(Guid id)
        {
            var stenoEntity = _stenoService.GetStenoIzinByStenografId(id);
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

        [HttpGet("GetStenoIzinBetweenDate")]
        public IEnumerable<StenoIzinModel> GetStenoIzinBetweenDate(DateTime basTarihi,DateTime bitTarihi)
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

        [HttpGet("GetStenoGorevByPlanId")]
        public List<StenoGorevModel> GetStenoGorevByPlanId(Guid planId)
        {
            var stenoEntity = _stenoService.GetStenoGorevByPlanId(planId);
            var model = _mapper.Map<List<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByName")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByName(string adSoyad)
        {
            var stenoEntity = _stenoService.GetStenoGorevByName(adSoyad);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByDateAndStatus")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByDateAndStatus(DateTime gorevAtamaTarihi, int status,int gorevSaati)
        {
            var stenoEntity = _stenoService.GetStenoGorevByDateAndStatus(gorevAtamaTarihi, status, gorevSaati);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        //[HttpPost("CreateStenoGorev")]
        //public IActionResult CreateStenoGorev(List<StenoGorevModel> model)
        //{
        //    try
        //    {
        //        foreach (var item in model)
        //        {
        //            var entity = Mapper.Map<StenoGorev>(item);
        //            _stenoService.CreateStenoGorev(entity);                   
        //        }
        //    }
        //    catch(Exception ex)
        //    { return BadRequest(ex.Message); }

        //    return Ok();


        //}

        [HttpPost("CreateStenoGorevAtama")]
        public IActionResult CreateStenoGorevAtama(StenoGorevAtamaModel model)
        {
            try
            {
                    var entity = Mapper.Map<StenoGorev>(model);
                    _stenoService.CreateStenoGorev(entity);
             
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();


        }

        [HttpGet("GetStenoGorevByStatus")]
        public List<StenoGorevModel> GetStenoGorevByStatus(int status=0)
        {
            var stenoEntity = _stenoService.GetStenoGorevBySatatus(status);
            var model = _mapper.Map<List<StenoGorevModel>>(stenoEntity);
            return model;
        }
        #endregion

        # region Stenograf
        [HttpGet("GetAllStenograf")]
        public IEnumerable<StenoModel> GetAllStenograf()
        {
            var stenoEntity = _stenoService.GetAllStenograf();
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetAllStenografByGorevTuru")]
        public IEnumerable<StenoModel> GetAllStenografByGorevTuru(int gorevTuru)
        {
            var stenoEntity = _stenoService.GetAllStenografByGorevTuru(gorevTuru);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            return model;
        }
        [HttpPost("CreateStenograf")]
        public IActionResult CreateStenograf(StenoModel model)
        {
            try
            {
                var entity = Mapper.Map<Stenograf>(model);
                _stenoService.CreateStenograf(entity);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("CreateStenoGrup")]
        public IActionResult CreateStenoGrup(StenoGrupModel model)
        {
            try
            {
                var entity = Mapper.Map<StenoGrup>(model);
                _stenoService.CreateStenoGrup(entity);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }
        [HttpGet("GetAllStenoGrup")]
        public IEnumerable<StenoGrupModel> GetAllStenoGrup()
        {
            var stenoGrpEntity = _stenoService.GetAllStenoGrup();
            var model = _mapper.Map<IEnumerable<StenoGrupModel>>(stenoGrpEntity);
            return model;
        }
        #endregion

    }
}
