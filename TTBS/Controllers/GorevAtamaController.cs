using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Models;
using TTBS.MongoDB;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GorevAtamaController : BaseController<GorevAtamaController>
    {
        private readonly IGorevAtamaService _gorevAtamaService;
        private readonly ILogger<GorevAtamaController> _logger;
        public readonly IMapper _mapper;

        public GorevAtamaController(IGorevAtamaService gorevAtamaService, ILogger<GorevAtamaController> logger, IMapper mapper)
        {
            _gorevAtamaService = gorevAtamaService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("CreateBirlesim")]
        public IActionResult CreateBirlesim(BirlesimModel model)
        {
            try
            {
                model.ToplanmaDurumu = model.ToplanmaTuru == ToplanmaTuru.GenelKurul ? ToplanmaStatu.Planlandı : ToplanmaStatu.Oluşturuldu;
                var entity = Mapper.Map<Birlesim>(model);
                var birlesim = _gorevAtamaService.CreateBirlesim(entity);
                var oturumId = _gorevAtamaService.CreateOturum(new Oturum
                {
                    BirlesimId = birlesim.Id,
                    BaslangicTarihi = birlesim.BaslangicTarihi
                });

                if (model.ToplanmaTuru == ToplanmaTuru.GenelKurul)
                {
                    var stenoAllList = _gorevAtamaService.GetStenografIdList();
                    var stenoList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf).Select(x=>x.Id);
                    var modelList = SetGorevAtama(birlesim, oturumId, stenoList,birlesim.StenoSure);
                    var stenoUzmanList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Uzman).Select(x => x.Id);
                    var modelUzmanList = SetGorevAtama(birlesim, oturumId, stenoUzmanList, birlesim.UzmanStenoSure);
                    modelList.AddRange(modelUzmanList);
                    var entityList = Mapper.Map<List<GorevAtamaGKM>>(modelList);
                    _gorevAtamaService.CreateStenoAtamaGK(entityList);
                }
                else if (model.ToplanmaTuru == ToplanmaTuru.Komisyon)
                {
                    _gorevAtamaService.CreateBirlesimKomisyonRelation(birlesim.Id, birlesim.KomisyonId, birlesim.AltKomisyonId);
                }
                else if (model.ToplanmaTuru == ToplanmaTuru.OzelToplanti)
                {
                    _gorevAtamaService.CreateBirlesimOzelToplanmaRelation(birlesim.Id, birlesim.OzelToplanmaId);
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        //[HttpPost("CreateOturum")]
        //public IActionResult CreateOturum(OturumModel model)
        //{
        //    var entity = Mapper.Map<Oturum>(model);
        //    _gorevAtamaService.CreateOturum(entity);
        //    return Ok(entity);

        //}

        [HttpPost("CreateStenoGorevAtama")]
        public IActionResult CreateStenoGorevAtama(StenoGorevAtamaModel model)
        {
            if (model.StenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                var birlesim = _gorevAtamaService.UpdateBirlesimGorevAtama(model.BirlesimId,model.TurAdedi);
                var modelList = SetGorevAtama(birlesim, model.OturumId, model.StenografIds, birlesim.StenoSure);
                var entityList = Mapper.Map<List<GorevAtamaKomM>>(modelList);
                _gorevAtamaService.CreateStenoAtamaKom(entityList);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        private List<GorevAtamaMongoModel> SetGorevAtama(Birlesim birlesim, Guid oturumId, IEnumerable<Guid> stenoList,double sure)
        {
            var atamaList = new List<GorevAtamaMongoModel>();
            var basDate = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value:DateTime.Now;
            int firstRec = 0;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtamaMongoModel();
                    newEntity.BirlesimId = birlesim.Id.ToString();
                    newEntity.OturumId = oturumId.ToString();
                    newEntity.StenografId = item.ToString();
                    newEntity.GorevBasTarihi = basDate.AddMinutes(firstRec * sure).ToLongDateString();
                    newEntity.GorevBitisTarihi = basDate.AddMinutes((firstRec * sure) + sure).ToLongDateString();
                    newEntity.StenoSure = sure;
                    //newEntity.GorevStatu = item.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                    firstRec++;
                    newEntity.SatırNo = firstRec ;
                    atamaList.Add(newEntity);
                    
                }
            }
            return atamaList;
        }


        [HttpPost("AddStenoGorevAtamaKomisyon")]
        public IActionResult AddStenoGorevAtamaKomisyon(List<Guid> stenografIds,string birlesimId,string oturumId)
        {
            if (stenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                _gorevAtamaService.AddStenoGorevAtamaKomisyon(stenografIds, birlesimId, oturumId);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPut("CreateStenoGorevDonguEkle")]
        public IActionResult CreateStenoGorevDonguEkle(string birlesimId, string oturumId)
        {
            try
            {
                _gorevAtamaService.CreateStenoGorevDonguEkle(birlesimId, oturumId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("GetStenoGorevByBirlesimId")]
        public List<GorevAtamaMongoModel> GetStenoGorevByBirlesimId(string birlesimId)
        {
            var entity = _gorevAtamaService.GetGorevAtamaGKByBirlesimId(birlesimId);
            var model = _mapper.Map<List<GorevAtamaMongoModel>>(entity);
            return model;
        }

    }
}
