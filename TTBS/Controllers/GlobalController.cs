using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalController : BaseController<GlobalController>
    {
        private readonly IGlobalService _globalService;
        private readonly ILogger<GlobalController> _logger;
        public readonly IMapper _mapper;

        public GlobalController(IGlobalService globalService, ILogger<GlobalController> logger, IMapper mapper)
        {
            _globalService = globalService;
            _logger = logger;
            _mapper = mapper;
        }
        #region Donem
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetDonemById")]
        public DonemModel GetDonemById(Guid id)
        {
            var entity = _globalService.GetDonemById(id);
            var model = _mapper.Map<DonemModel>(entity);
            return model;
        }

        [HttpGet("GetAllDonem")]
        public IEnumerable<DonemModel> GetAllDonem()
        {
            var entity = _globalService.GetAllDonem();
            var model = _mapper.Map<IEnumerable<DonemModel>>(entity);
            return model;
        }

        [HttpPost("CreateDonem")]
        public IActionResult CreateDonem(Donem model)
        {
            var entity = Mapper.Map<Donem>(model);
            _globalService.CreateDonem(entity);
            return Ok(entity);

        }
        #endregion

        #region Yasama
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetYasamaById")]
        public YasamaModel GetYasamaById(Guid id)
        {
            var entity = _globalService.GetYasamaById(id);
            var model = _mapper.Map<YasamaModel>(entity);
            return model;
        }

        [HttpGet("GetAllYasama")]
        public IEnumerable<DonemModel> GetAllYasama()
        {
            var entity = _globalService.GetAllYasama();
            var model = _mapper.Map<IEnumerable<DonemModel>>(entity);
            return model;
        }

        [HttpPost("CreateYasama")]
        public IActionResult CreateYasama(Yasama model)
        {
            var entity = Mapper.Map<Yasama>(model);
            _globalService.CreateYasama(entity);
            return Ok(entity);

        }
        #endregion

        #region Birlesim

        [HttpGet("GetBirlesimById")]
        public BirlesimModel GetBirlesimById(Guid id)
        {
            var entity = _globalService.GetBirlesimById(id);
            var model = _mapper.Map<BirlesimModel>(entity);
            return model;
        }
     

        [HttpGet("GetAllBirlesim")]
        public IEnumerable<BirlesimModel> GetAllBirlesim()
        {
            var entity = _globalService.GetAllBirlesim();
            var model = _mapper.Map<IEnumerable<BirlesimModel>>(entity);
            return model;
        }

        [HttpPost("CreateBirlesim")]
        public IActionResult CreateBirlesim(BirlesimModel model)
        {
            var entity = Mapper.Map<Birlesim>(model);
            _globalService.CreateBirlesim(entity);
            return Ok(entity);

        }
        #endregion

        #region Komisyon
        [HttpGet("GetKomisyonById")]
        public KomisyonModel GetKomisyonById(Guid id)
        {
            var entity = _globalService.GetKomisyonById(id);
            var model = _mapper.Map<KomisyonModel>(entity);
            return model;
        }

        [HttpGet("GetAllKomisyon")]
        public IEnumerable<KomisyonModel> GetAllKomisyon()
        {
            var entity = _globalService.GetAllKomisyon();
            var model = _mapper.Map<IEnumerable<KomisyonModel>>(entity);
            return model;
        }

        [HttpPost("CreateKomisyon")]
        public IActionResult CreateKomisyon(KomisyonModel model)
        {
            var entity = Mapper.Map<Komisyon>(model);
            _globalService.CreateKomisyon(entity);
            return Ok(entity);

        }
        [HttpPost("CreateAltKomisyon")]
        public IActionResult CreateAltKomisyon(AltKomisyonModel model)
        {
            var entity = Mapper.Map<AltKomisyon>(model);
            _globalService.CreateAltKomisyon(entity);
            return Ok(entity);

        }

        [HttpPost("DeleteAltKomisyon")]
        public IActionResult DeleteAltKomisyon(Guid id)
        {
            try
            {
                _globalService.DeleteAltKomisyon(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
            return Ok();
        }

        [HttpPost("UpdateAltKomisyon")]
        public IActionResult UpdateAltKomisyon(AltKomisyonModel model)
        {
            try
            {
                var entity = Mapper.Map<AltKomisyon>(model);
                _globalService.UpdateAltKomisyon(entity);
                            
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("GetAllKomisyonAltDetay")]
        public IEnumerable<KomisyonAltModel> GetAllKomisyonAltDetay()
        {
            var entity = _globalService.GetAllAltKomisyon();
            var model = _mapper.Map<IEnumerable<KomisyonAltModel>>(entity);
            return model;
        }

        [HttpGet("GetAltKomisyon")]
        public IEnumerable<AltKomisyonModel> GetAltKomisyon()
        {
            var entity = _globalService.GetAltKomisyon();
            var model = _mapper.Map<IEnumerable<AltKomisyonModel>>(entity);
            return model;
        }

        #endregion

        #region Grup
        [HttpPost("CreateGrup")]
        public IActionResult CreateGrup(GrupModel model)
        {
            var entity = Mapper.Map<Grup>(model);
            _globalService.CreateGrup(entity);
            return Ok(entity);

        }
        [HttpGet("GetGrupById")]
        public GrupModel GetGrupById(Guid id)
        {
            var entity = _globalService.GetGrupById(id);
            var model = _mapper.Map<GrupModel>(entity);
            return model;
        }

        [HttpGet("GetAllGrup")]
        public IEnumerable<GrupModel> GetAllGrup()
        {
            var entity = _globalService.GetAllGrup();
            var model = _mapper.Map<IEnumerable<GrupModel>>(entity);
            return model;
        }

        [HttpPost("DeleteGroup")]
        public IActionResult DeleteStenoGroup(GrupModel model)
        {

            var entity = Mapper.Map<Grup>(model);
            _globalService.DeleteGroup(entity);
            return Ok();
        }

        [HttpPost("CreateStenografBeklemeSure")]
        public IActionResult CreateStenografBeklemeSure(StenoBeklemeSureModel model)
        {
            var entity = Mapper.Map<StenografBeklemeSure>(model);
            _globalService.CreateStenografBeklemeSure(entity);
            return Ok(entity);
        }
        [HttpPost("UpdateStenografBeklemeSure")]
        public IActionResult UpdateStenografBeklemeSure(StenoBeklemeSureModel model)
        {
            var entity = Mapper.Map<StenografBeklemeSure>(model);
            _globalService.UpdateStenografBeklemeSure(entity);
            return Ok(entity);
        }
        [HttpGet("GetAllStenografBeklemeSure")]
        public IEnumerable<StenoBeklemeSureModel> GetAllStenografBeklemeSure()
        {
            var entity = _globalService.GetAllStenografBeklemeSure;
            var model = _mapper.Map<IEnumerable<StenoBeklemeSureModel>>(entity);
            return model;
        }
        #endregion
    }
}
