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
                var birlesimEntity = Mapper.Map<Birlesim>(model);
                var result = _gorevAtamaService.CreateBirlesim(birlesimEntity);
                if (result.HasError)
                    return BadRequest(result.Message);

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("CreateOturum")]
        public IActionResult CreateOturum(OturumModel model)
        {
            var entity = Mapper.Map<Oturum>(model);
            _gorevAtamaService.CreateOturum(entity);
            return Ok(entity);

        }
    }
}
