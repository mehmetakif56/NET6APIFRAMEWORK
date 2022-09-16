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
    public class StenografController : BaseController<StenografController>
    {
        private readonly IStenografService _stenoService;
        private readonly IGlobalService _globalService;   
        //private readonly IMongoDBService _mongoDBService;
        private readonly ILogger<StenografController> _logger;
        public readonly IMapper _mapper;

        public StenografController(IStenografService stenoService, IGlobalService globalService,ILogger<StenografController> logger, IMapper mapper)
        {
            _stenoService = stenoService;
            _globalService = globalService;
            //_mongoDBService = mongoDBService;
            _logger = logger;
            _mapper = mapper;
        }
        #region KomisyonToplanma

        //[HttpGet("GetStenoPlanByStatus")]
        //public List<StenoPlanModel> GetStenoPlanByStatus(int status=0)
        //{
        //    var stenoEntity = _stenoService.GetStenoPlanByStatus(status);
        //    var model = _mapper.Map<List<StenoPlanModel>>(stenoEntity);
        //    return model;
        //}

        [HttpGet("GetBirlesimByDateAndTur")]
        public List<BirlesimViewModel> GetBirlesimByDateAndTur(DateTime gorevTarihi, DateTime gorevBitTarihi, int toplanmaTuru)
        {
            var stenoEntity = _stenoService.GetBirlesimByDateAndTur(gorevTarihi, gorevBitTarihi, toplanmaTuru);
            var model = _mapper.Map<List<BirlesimViewModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetBirlesimByDate")]
        public List<BirlesimViewModel> GetBirlesimByDate(DateTime gorevTarihi, int gorevTuru)
        {
            var stenoEntity = _stenoService.GetBirlesimByDate(gorevTarihi, gorevTuru);
            var model = _mapper.Map<List<BirlesimViewModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoWeeklyStatisticstKomisyonAndBirlesimByDateAndGroup")]
        public StenoGroupStatisticsModel GetStenoWeeklyStatisticstKomisyonAndBirlesimByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grupId)
        {
            StenoGroupStatisticsModel model = new StenoGroupStatisticsModel();
            var istatistikEntity = _globalService.GetGrupToplamSureByDate(grupId, baslangic, bitis, yasamaId);
            model.stenoToplamGenelSureModels = _mapper.Map<List<StenoToplamGenelSureModel>>(istatistikEntity);
            return model;
        }

        [HttpGet("GetUzmanWeeklyStatisticstKomisyonAndBirlesimByDateAndGroup")]
        public StenoGroupStatisticsModel GetUzmanWeeklyStatisticstKomisyonAndBirlesimByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grupId)
        {
            StenoGroupStatisticsModel model = new StenoGroupStatisticsModel();
            var istatistikEntity = _globalService.GetGrupToplamSureByDate(grupId, baslangic, bitis, yasamaId);
            // TODO : Burada uzman stenografların okuduğu sayfa sayısı de eklenecek editör yapıldıktan sonra kararlaştırılıcak.
            model.stenoToplamGenelSureModels = _mapper.Map<List<StenoToplamGenelSureModel>>(istatistikEntity);
            return model;
        }


        //[HttpGet("GetIntersectStenoPlan")]
        //public List<StenoGorevPlanModel> GetIntersectStenoPlan(Guid stenoPlanId, Guid stenoId)
        //{
        //    var stenoEntity = _stenoService.GetIntersectStenoPlan(stenoPlanId, stenoId);
        //    var model = _mapper.Map<List<StenoGorevPlanModel>>(stenoEntity);
        //    return model;
        //}

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

        [HttpGet("GetStenoIzinBetweenDateAndStenograf")]
        public StenoIzınCountModel GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi,DateTime bitTarihi,string? field,string? sortOrder, int? izinTur,Guid? stenograf, int pageIndex, int pagesize)
        {
            var stenoEntity = _stenoService.GetStenoIzinBetweenDateAndStenograf(basTarihi, bitTarihi, field, sortOrder, izinTur, stenograf, pageIndex, pagesize);
            //var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity.StenoIzinModels);
            return stenoEntity;
        }

        [HttpPost("CreateStenoIzin")]
        public IActionResult CreateStenoIzin(StenoIzinModel model)
        {
            try
            {
                var entity = Mapper.Map<StenoIzin>(model);
                _stenoService.CreateStenoIzin(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        #endregion

        #region StenoGorev

        [HttpGet("GetStenoGorevByStenografAndDate")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevAtamaTarihi, DateTime gorevBitTarihi)
        {
            var stenoEntity = _stenoService.GetStenoGorevByStenografAndDate(stenografId, gorevAtamaTarihi, gorevBitTarihi);
            //var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return stenoEntity;
        }


        #endregion

        #region Stenograf
        [HttpGet("GetAllStenografByGroupId")]
        public IEnumerable<StenoModel> GetAllStenografByGroupId(Guid? groupId)
        {
            var stenoEntity = _stenoService.GetAllStenografByGroupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            model.ToList().ForEach(x => { x.GorevStatu = -1; });
            return model;
        }

        [HttpGet("GetAllStenografWithStatisticsByGroupId")]
        public IEnumerable<StenoModel> GetAllStenografWithStatisticsByGroupId(Guid? groupId, Guid yasamaId)
        {
            var stenoEntity = _stenoService.GetAllStenografWithStatisticsByGroupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            //şimdilik kaldırıldı, tablodan direkt getirelecek, perfomanstan dolayı
            model.ToList().ForEach(x =>
            {
                var statistic = _globalService.GetStenoSureModelById(x.Id, yasamaId);
                x.GunlukGorevSuresi = statistic.GunlukGorevSuresi;
                x.HaftalikGorevSuresi = statistic.HaftalikGorevSuresi;
                x.YillikGorevSuresi = statistic.YillikGorevSuresi;
                x.StenoIzinTuru = _stenoService.GetStenoIzinTodayByStenoId(x.Id);
            });
            return model;
        }

        [HttpGet("GetAllStenografGroup")]
        public List<StenoGrupViewModel> GetAllStenografGroup(int gorevTur)
        {
            var stenoEntity = _stenoService.GetAllStenografGroup(gorevTur);
            var lst =new List<StenoGrupViewModel>();

            foreach (var item in stenoEntity.GroupBy(c => new {
                c.Id,
                c.Ad,
                c.StenoGrupTuru
            }).Select(gcs => new StenoGrupViewModel()
            {
                GrupId = gcs.Key.Id,
                GrupName = gcs.Key.Ad,
                StenoGrupTuru = gcs.Key.StenoGrupTuru
            }))
            {
                lst.Add(new StenoGrupViewModel { GrupId =item.GrupId,GrupName =item.GrupName,StenoGrupTuru =item.StenoGrupTuru} );
            }
            foreach (var item in lst) 
            {
                var steno = stenoEntity.SelectMany(x => x.Stenografs).Where(x => x.GrupId == item.GrupId).Select(cl => new StenoViewModel
                {
                    AdSoyad = cl.AdSoyad,
                    Id = cl.Id,
                    SonGorevSuresi = 0, //kaldırıldı başka bir yere ekleencek//cl.GorevAtamas.Where(x => x.GorevBasTarihi >= DateTime.Now.AddDays(-7)).Sum(c => c.GorevDakika),
                    StenoGorevTuru = cl.StenoGorevTuru
                }).ToList();
                item.StenoViews = new List<StenoViewModel>();
                foreach (var item2 in steno.Where(x=>(int)x.StenoGorevTuru == gorevTur))
                {
                    item.StenoViews.Add(item2);
                }
            }
            return lst;
        }

        [HttpGet("GetAllStenografByGorevTuru")]
        public IEnumerable<StenoModel> GetAllStenografByGorevTuru(int? gorevTuru)
        {
            var stenoEntity = _stenoService.GetAllStenografByGorevTuru(gorevTuru);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            return model;
        }
        [HttpPost("CreateStenograf")]
        public IActionResult CreateStenograf(List<StenoCreateModel> model)
        {
            try
            {
                var entity = Mapper.Map<List<Stenograf>>(model);
                _stenoService.CreateStenograf(entity);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }
       
        [HttpGet("GetAllStenoGrupNotInclueded")]
        public IEnumerable<StenoModel> GetAllStenoGrupNotInclueded()
        {
            var stenoGrpEntity = _stenoService.GetAllStenoGrupNotInclueded();
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGrpEntity);
            return model;
        }

        //[HttpGet("GetAvaliableStenoBetweenDateBySteno")]
        //public IEnumerable<StenoModel> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi,int gorevTuru, int toplantiTur)
        //{
        //    var stenoGrpEntity = _stenoService.GetAvaliableStenoBetweenDateBySteno(basTarihi, bitTarihi, gorevTuru, toplantiTur);
        //    var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGrpEntity);
        //    return model;
        //}

        //[HttpGet("GetAvaliableStenoBetweenDateByGroup")]
        //public IEnumerable<StenoModel> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId, int toplantiTur)
        //{
        //    var stenoGrpEntity = _stenoService.GetAvaliableStenoBetweenDateByGroup(basTarihi, bitTarihi, groupId, toplantiTur);
        //    var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGrpEntity);
        //    return model;
        //}

        [HttpPut("UpdateStenoSiraNo")]
        public IActionResult UpdateStenoSiraNo(List<StenoModel> model)
        {
            try
            {
                var entityList = Mapper.Map<List<Stenograf>>(model);
                _stenoService.UpdateStenoSiraNo(entityList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //[HttpGet("GetStenoGorevByIdFromMongo")]
        //public GorevAtamaMongo GetStenoGorevByIdFromMongo(Guid id)
        //{
        //   var result = _stenoService.GetStenoGorevByIdFromMongo(id);
        //    return result;
        //}
        #endregion
        [HttpPost("CreateStenoGroup")]
        public IActionResult CreateStenoGroup(StenoGrupModel model)
        {
            try
            {
                _stenoService.CreateStenoGroup(model.StenoId, model.GrupId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
