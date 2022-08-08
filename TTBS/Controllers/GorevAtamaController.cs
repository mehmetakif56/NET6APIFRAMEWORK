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
                    _gorevAtamaService.CreateStenoAtama(birlesim, oturumId,null);
                    _gorevAtamaService.CreateUzmanStenoAtama(birlesim, oturumId,null);
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
                var entity = Mapper.Map<GorevAtama>(model);
                var birlesim = _gorevAtamaService.UpdateBirlesimGorevAtama(entity.BirlesimId);
                _gorevAtamaService.CreateStenoAtama(birlesim, entity.OturumId,entity.StenografIds);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }
    }
}
