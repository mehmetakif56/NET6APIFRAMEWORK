using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalController : BaseController<GlobalController>
    {
        private readonly IGlobalService _globalService;
        private readonly IStenografService _stenoService;
        private readonly ILogger<GlobalController> _logger;
        public readonly IMapper _mapper;

        public GlobalController(IGlobalService globalService, IStenografService stenoService,ILogger<GlobalController> logger, IMapper mapper)
        {
            _globalService = globalService;
            _stenoService = stenoService;
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
        public IActionResult CreateDonem(DonemModel model)
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

        [HttpGet("GetYasamaByDonemId")]
        public IEnumerable<YasamaModel> GetYasamaByDonemId(Guid id)
        {
            var entity = _globalService.GetYasamaByDonemId(id);
            var model = _mapper.Map<IEnumerable<YasamaModel>>(entity);
            return model;
        }

        [HttpGet("GetAllYasama")]
        public IEnumerable<YasamaModel> GetAllYasama()
        {
            var entity = _globalService.GetAllYasama();
            var model = _mapper.Map<IEnumerable<YasamaModel>>(entity);
            return model;
        }

        [HttpPost("CreateYasama")]
        public IActionResult CreateYasama(YasamaModel model)
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
            var model = entity != null ? _mapper.Map<BirlesimModel>(entity.FirstOrDefault()):new BirlesimModel { };
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
            try
            {
                if (model.ToplanmaTuru == ToplanmaTuru.GenelKurul)
                {
                    var result = _globalService.CreateBirlesimGorevAtama(SetBirlesimWithMap(model));
                    if (result.HasError)
                        return BadRequest(result.Message);
                }
                else
                {
                    _globalService.CreateBirlesim(SetBirlesimWithMap(model));
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
            
            return Ok();
        }

        [HttpDelete("DeleteBirlesim")]
        public IActionResult DeleteBirlesim(Guid id)
        {
            try
            {
               _globalService.DeleteBirlesim(id);                
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }


        private Birlesim SetBirlesimWithMap(BirlesimModel model)
        {
            var birlesimEntity = Mapper.Map<Birlesim>(model);
            birlesimEntity.TurAdedi = 3;
            birlesimEntity.ToplanmaDurumu = ToplanmaStatu.Oluşturuldu;
           return birlesimEntity;
        }
        #endregion

        #region Oturum

        [HttpPost("CreateOturum")]
        public IActionResult CreateOturum(OturumModel model)
        {
            var entity = Mapper.Map<Oturum>(model);
            _globalService.CreateOturum(entity);
            return Ok(entity);
            
        }

        [HttpPost("UpdateOturum")]
        public IActionResult UpdateOturum(OturumModel model)
        {
            var entity = Mapper.Map<Oturum>(model);
            _globalService.UpdateOturum(entity);
            return Ok(entity);

        }

        [HttpGet("GetOturumByBirlesimId")]
        public IEnumerable<OturumModel> GetOturumByBirlesimId(Guid id)
        {
            var entity = _globalService.GetOturumByBirlesimId(id);
            var model = _mapper.Map<IEnumerable<OturumModel>>(entity);
            return model;
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

        [HttpDelete("DeleteAltKomisyon")]
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

        [HttpPut("UpdateAltKomisyon")]
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
        public IEnumerable<GrupModel> GetAllGrup(int grupTuru)
        {
            var entity = _globalService.GetAllGrup(grupTuru);
            var model = _mapper.Map<IEnumerable<GrupModel>>(entity);
            return model;
        }

        [HttpDelete("DeleteGroup")]
        public IActionResult DeleteStenoGroup(GrupModel model)
        {

            var entity = Mapper.Map<Grup>(model);
            _globalService.DeleteGroup(entity);
            return Ok();
        }

        [HttpPost("CreateStenografBeklemeSure")]
        public IActionResult CreateStenografBeklemeSure(List<StenoBeklemeSureModel> model)
        {
            var entity = Mapper.Map<List<StenografBeklemeSure>>(model);
            _globalService.CreateStenografBeklemeSure(entity);
            return Ok(entity);
        }
        [HttpPut("UpdateStenografBeklemeSure")]
        public IActionResult UpdateStenografBeklemeSure(List<StenoBeklemeSureModel> model)
        {
            var entity = Mapper.Map<List<StenografBeklemeSure>>(model);
            _globalService.UpdateStenografBeklemeSure(entity);
            return Ok(entity);
        }
        [HttpGet("GetAllStenografBeklemeSure")]
        public IEnumerable<StenoBeklemeSureModel> GetAllStenografBeklemeSure()
        {
            var entity = _globalService.GetAllStenografBeklemeSure();
            var model = _mapper.Map<IEnumerable<StenoBeklemeSureModel>>(entity);
            return model;
        }

        [HttpPost("CreateOzelGorevTur")]
        public IActionResult CreateOzelGorevTur(OzelGorevTurModel model)
        {
            var entity = Mapper.Map<OzelGorevTur>(model);
            _globalService.CreateOzelGorevTur(entity);
            return Ok(entity);
        }
        [HttpPut("UpdateOzelGorevTur")]
        public IActionResult UpdateOzelGorevTur(OzelGorevTurModel model)
        {
            var entity = Mapper.Map<OzelGorevTur>(model);
            _globalService.UpdateOzelGorevTur(entity);
            return Ok(entity);
        }
        [HttpGet("GetAllOzelGorevTur")]
        public IEnumerable<OzelGorevTurModel> GetAllOzelGorevTur()
        {
            var entity = _globalService.GetAllOzelGorevTur();
            var model = _mapper.Map<IEnumerable<OzelGorevTurModel>>(entity);
            return model;
        }
        [HttpGet("GetOzelGorevTurById")]
        public OzelGorevTurModel GetOzelGorevTurById(Guid id)
        {
            var entity = _globalService.GetOzelGorevTurById(id);
            var model = _mapper.Map<OzelGorevTurModel>(entity);
            return model;
        }
        [HttpDelete("DeleteOzelGorevTur")]
        public IActionResult DeleteOzelGorevTur(Guid id)
        {
           _globalService.DeleteOzelGorevTur(id);
            return Ok();
        }
        #endregion

        [HttpPost("CreateStenoToplamSure")]
        public IActionResult CreateStenoToplamSure(StenoToplamGenelSureModel model)
        {
            var entity = Mapper.Map<StenoToplamGenelSure>(model);
            _globalService.InsertStenoToplamSure(entity);
            return Ok(entity);
        }

        [HttpDelete("DeleteStenoToplamSure")]
        public IActionResult DeleteStenoToplamSure(Guid id)
        {
            _globalService.DeleteStenoToplamSure(id);
            return Ok();
        }

        [HttpGet("GetStenoToplamSureByGroupAndDate")]
        public List<StenoToplamGenelSureModel> GetStenoToplamSureByGroupAndDate(Guid groupID, DateTime baslangic, DateTime bitis)
        {
            var entity = _globalService.GetGrupToplamSureByDate(groupID, baslangic, bitis, null);
            return _mapper.Map<List<StenoToplamGenelSureModel>>(entity);
        }

        #region GidenGrup
        [HttpPost("CreateGidenGrup")]
        public IActionResult CreateGidenGrup(GidenGrupOlusturModel model)
        {
            var entity = Mapper.Map<GidenGrup>(model);
            _globalService.CreateGidenGrup(entity);
            return Ok(entity);

        }
        [HttpGet("GetGidenGrup")]
        public List<GidenGrupOlusturModel> GetGidenGrup()
        {
           var gidenGrup= _globalService.GetGidenGrup();
            var model = _mapper.Map<List<GidenGrupOlusturModel>>(gidenGrup);
            return model;
        }
        [HttpPost("UpdateGidenGrup")]
        public IActionResult UpdateGidenGrup(GidenGrupOlusturModel model)
        {
            var entity = Mapper.Map<GidenGrup>(model);
            _globalService.UpdateGidenGrup(entity);
            return Ok(entity);

        }
        #endregion

    }
}
