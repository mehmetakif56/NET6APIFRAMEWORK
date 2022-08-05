using Microsoft.AspNetCore.Mvc;
using TTBS.MongoDB;
using TTBS.Services;

namespace TTBS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGorevAtamaMongoRepository _gorevAtamaRepo;
        private readonly IGlobalService _globalService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGorevAtamaMongoRepository weatherForecastDal, IGlobalService globalService)
        {
            _logger = logger;
            this._gorevAtamaRepo = weatherForecastDal;
            _globalService = globalService;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<T> Get()
        //{
        //    _logger.LogInformation("Get");
        //    //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    //{
        //    //    Date = DateTime.Now.AddDays(index),
        //    //    TemperatureC = Random.Shared.Next(-20, 55),
        //    //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    //})
        //    //.ToArray();
        //}

        [HttpPost(Name = "CreateWetaher")]
        public IActionResult CreateWetaher(Guid birlesimId)
        {
            //var birlesim = _globalService.GetBirlesimById(birlesimId).FirstOrDefault();
            //var newEntity = new GorevAtamaMongoDB();
            //newEntity.BirlesimId = birlesim.Id;
            //Guid oturmId = Guid.Empty;
            //Guid.TryParse("798D2C68 - 6177 - 4322 - 2661 - 08DA75443FB6", out oturmId) ;
            //Guid stenoId = Guid.Empty;
            //Guid.TryParse("3899453F-9129-4F91-AFB5-C48E05C64645", out stenoId);
            //newEntity.OturumId = oturmId;
            //newEntity.StenografId = stenoId;
            //newEntity.GorevBasTarihi = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value.AddMinutes(1 * birlesim.UzmanStenoSure) : null;
            //newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.HasValue ? newEntity.GorevBasTarihi.Value.AddMinutes(birlesim.UzmanStenoSure) : null;
            //newEntity.StenoSure = birlesim.UzmanStenoSure;
            //var result = weatherForecastDal.AddAsync(newEntity).Result;

            _gorevAtamaRepo.CloneCollection();
            //foreach (var item in entity)
            //{
            //    var result = weatherForecastDal.AddAsync(item).Result;
            //}
            //var result = weatherForecastDal.AddAsync(entity.FirstOrDefault()).Status;


            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _gorevAtamaRepo.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }
    }
}