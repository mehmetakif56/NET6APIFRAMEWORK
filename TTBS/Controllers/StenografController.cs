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

        public StenografController(IStenografService stenoService, IGlobalService globalService, ILogger<StenografController> logger, IMapper mapper)
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
        public IEnumerable<StenoIzinModel> GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi,DateTime bitTarihi,string? field,string? sortOrder, int? izinTur,Guid? stenograf, int pageIndex, int pagesize)
        {
            var stenoEntity = _stenoService.GetStenoIzinBetweenDateAndStenograf(basTarihi, bitTarihi, field, sortOrder, izinTur, stenograf, pageIndex, pagesize);
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

        [HttpPut("UpdateGorevDurumByBirlesimAndSteno")]
        public IActionResult UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId)
        {
            try
            {
                _stenoService.UpdateGorevDurumByBirlesimAndSteno(birlesimId, stenoId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut("UpdateGorevDurumById")]
        public IActionResult UpdateGorevDurumById(Guid id)
        {
            try
            {
                _stenoService.UpdateGorevDurumById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("CreateStenoGorevDonguEkle")]
        public IActionResult CreateStenoGorevDonguEkle(Guid birlesimId,Guid oturumId, int gorevturu)
        {
            try
            {
                var stenoEntity = _stenoService.GetStenoGorevByBirlesimIdAndGorevTuru(birlesimId,gorevturu);
                if (stenoEntity != null && stenoEntity.Count() > 0)
                {

                    _stenoService.CreateStenoGorevDonguEkle(birlesimId, oturumId, stenoEntity, gorevturu);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("GetStenoUzmanGorevByBirlesimId")]
        public List<StenoGorevModel> GetStenoUzmanGorevByBirlesimId(Guid birlesimId, int gorevturu)
        {
            var lst = new List<StenoGorevModel>();
            var stenoEntity = _stenoService.GetStenoGorevByGorevTuru(gorevturu);
            if (stenoEntity != null && stenoEntity.Count() > 0)
            {
                var birlesimList = stenoEntity.Where(x => x.BirlesimId == birlesimId).OrderBy(x => x.GorevBasTarihi).ToList();
                if (birlesimList != null && birlesimList.Count() > 0)
                {
                    var model = _mapper.Map<List<StenoGorevModel>>(birlesimList);
                    var gorevBasTarihi = model.FirstOrDefault().GorevBasTarihi.Value;
                    var gorevBitTarihi = model.FirstOrDefault().GorevBitisTarihi.Value;

                    var birlesim = birlesimList.FirstOrDefault().Birlesim;
                    double sure = 0;
                    var ste = model.Where(x => x.StenografId == model.FirstOrDefault().StenografId);
                    var stenoToplamSureAsım = ste.Max(x => x.GorevBitisTarihi.Value).Subtract(ste.Min(x => x.GorevBasTarihi.Value)).TotalMinutes <= 50;

                    foreach (var item in model)
                    {

                        var iz = birlesimList.Where(x => x.StenografId == item.StenografId).SelectMany(x => x.Stenograf.StenoIzins)
                                            .Where(x => x.BaslangicTarihi.Value <= gorevBitTarihi &&
                                                        x.BitisTarihi.Value >= gorevBitTarihi);
                        item.StenoIzinTuru = iz != null && iz.Count() > 0 ? iz.Select(x => x.IzinTuru).FirstOrDefault() : 0;

                        if (birlesim.ToplanmaTuru == ToplanmaTuru.GenelKurul)
                        {
                            var maxBitis = stenoEntity.Where(x => x.BirlesimId != item.BirlesimId && x.StenografId == item.StenografId && x.GorevStatu != GorevStatu.Iptal).Max(x => x.GorevBitisTarihi);

                            var query = stenoEntity.Where(x => x.BirlesimId != item.BirlesimId &&
                                                               x.StenografId == item.StenografId &&
                                                               x.GorevStatu != GorevStatu.Iptal &&
                                                               x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes > 0 &&
                                                               x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes <= 60);

                            item.StenoToplantiVar = (query != null && query.Count() > 0) || (maxBitis.HasValue && maxBitis.Value.AddMinutes(sure * 9) >= gorevBitTarihi) ? true : false;
                            sure = gorevturu == (int)StenoGorevTuru.Stenograf ? birlesim.StenoSure : birlesim.UzmanStenoSure;
                        }
                        else
                        {
                            item.StenoToplantiVar = false;
                            sure = item.StenoSure;
                        }


                        if (item.StenoToplantiVar || item.GorevStatu == GorevStatu.Iptal || item.GorevStatu == GorevStatu.GidenGrup || (iz != null && iz.Count() > 0))
                        {
                            item.GorevStatu = item.GorevStatu  == GorevStatu.GidenGrup ? GorevStatu.GidenGrup : GorevStatu.Iptal;
                        }
                        else
                        {

                            if (item.GorevBasTarihi != gorevBasTarihi)
                            {
                                item.GorevBasTarihi = gorevBitTarihi;
                            }
                            if (item.GorevBitisTarihi != gorevBitTarihi)
                            {
                                item.GorevBitisTarihi = gorevBitTarihi.AddMinutes(sure);

                            }

                            gorevBasTarihi = item.GorevBasTarihi.Value;
                            gorevBitTarihi = item.GorevBitisTarihi.HasValue ? item.GorevBitisTarihi.Value : DateTime.MinValue;
                        }

                        item.StenoToplamSureAsım = stenoToplamSureAsım;
                        lst.Add(item);
                    }
                }
            }
            //var entity = Mapper.Map<List<GorevAtama>>(model);
            //_stenoService.UpdateStenoGorev(entity);
            return lst;
        }

        [HttpGet("GetStenoGorevByBirlesimId")]
        public List<StenoGorevModel> GetStenoGorevByBirlesimId(Guid birlesimId, int gorevturu)
        {
            var lst = new List<StenoGorevModel>();
            var stenoEntity = _stenoService.GetStenoGorevByGorevTuru(gorevturu);
            //var model = _mapper.Map<List<StenoGorevModel>>(stenoEntity);

            if (stenoEntity != null && stenoEntity.Count() > 0)
            {
                var birlesimList = stenoEntity.Where(x => x.BirlesimId == birlesimId).OrderBy(x => x.GorevBasTarihi).ToList();
                if (birlesimList != null && birlesimList.Count() > 0)
                {
                    var model = _mapper.Map<List<StenoGorevModel>>(birlesimList);
                    var gorevBasTarihi = model.FirstOrDefault().GorevBasTarihi.Value;
                    var gorevBitTarihi = model.FirstOrDefault().GorevBitisTarihi.Value;

                    var birlesim = birlesimList.FirstOrDefault().Birlesim;
                    double sure = 0;
                    var ste = model.Where(x => x.StenografId == model.FirstOrDefault().StenografId);
                    var stenoToplamSureAsım = ste.Max(x => x.GorevBitisTarihi.Value).Subtract(ste.Min(x => x.GorevBasTarihi.Value)).TotalMinutes <= 50;

                    foreach (var item in model)
                    {

                        var iz = birlesimList.Where(x => x.StenografId == item.StenografId).SelectMany(x => x.Stenograf.StenoIzins)
                                           .Where(x => x.BaslangicTarihi.Value <= gorevBitTarihi &&
                                                       x.BitisTarihi.Value >= gorevBitTarihi);
                       item.StenoIzinTuru = iz != null && iz.Count() > 0 ? iz.Select(x => x.IzinTuru).FirstOrDefault() : 0;

                       if (birlesim.ToplanmaTuru == ToplanmaTuru.GenelKurul)
                       {
                            var maxBitis =  stenoEntity.Where(x => x.BirlesimId != item.BirlesimId && x.StenografId == item.StenografId && x.GorevStatu != GorevStatu.Iptal).Max(x => x.GorevBitisTarihi);
     
                            var query = stenoEntity.Where(x => x.BirlesimId != item.BirlesimId &&
                                                               x.StenografId == item.StenografId &&
                                                               x.GorevStatu != GorevStatu.Iptal &&
                                                               x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes > 0 &&
                                                               x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes <= 60);

                            item.StenoToplantiVar = (query != null && query.Count() > 0) || (maxBitis.HasValue && maxBitis.Value.AddMinutes(sure * 9) >= gorevBitTarihi) ? true : false;
                           sure = gorevturu == (int)StenoGorevTuru.Stenograf ? birlesim.StenoSure : birlesim.UzmanStenoSure;
                       }
                       else
                       {
                           item.StenoToplantiVar = false;
                           sure = item.StenoSure;
                       }


                       if (item.StenoToplantiVar || item.GorevStatu == GorevStatu.Iptal || item.GorevStatu == GorevStatu.GidenGrup || (iz != null && iz.Count() > 0))
                       {
                           item.GorevStatu = item.GorevStatu == GorevStatu.GidenGrup ? GorevStatu.GidenGrup : GorevStatu.Iptal;
                       }
                       else
                       {

                           if (item.GorevBasTarihi != gorevBasTarihi)
                           {
                               item.GorevBasTarihi = gorevBitTarihi;
                           }
                           if (item.GorevBitisTarihi != gorevBitTarihi)
                           {
                               item.GorevBitisTarihi = gorevBitTarihi.AddMinutes(sure);

                           }

                           gorevBasTarihi = item.GorevBasTarihi.Value;
                           gorevBitTarihi = item.GorevBitisTarihi.HasValue ? item.GorevBitisTarihi.Value : DateTime.MinValue;
                       }

                       item.StenoToplamSureAsım = stenoToplamSureAsım;
                       lst.Add(item);
                   };
                }
            }
            //var entity = Mapper.Map<List<GorevAtama>>(model);
            //_stenoService.UpdateStenoGorev(entity);
            return lst;
        }

        [HttpGet("GetStenoGorevByName")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByName(string adSoyad)
        {
            var stenoEntity = _stenoService.GetStenoGorevByName(adSoyad);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByDateAndStatus")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByDateAndStatus(DateTime gorevBasTarihi, DateTime gorevBitTarihi,int status)
        {
            var stenoEntity = _stenoService.GetStenoGorevByDateAndStatus(gorevBasTarihi, gorevBitTarihi, status);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByStenografAndDate")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            var stenoEntity = _stenoService.GetStenoGorevByStenografAndDate(stenografId, gorevBasTarihi, gorevBitTarihi);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpPut("UpdateBirlesimStenoGorev")]
        public IActionResult UpdateBirlesimStenoGorev(BirlesimStenoGorevModel model)
        {
            try
            {
                if(ToplanmaBaslatmaStatu.Baslama == model.ToplanmaBaslatmaStatu)
                {
                    _stenoService.UpdateBirlesimStenoGorevBaslama(model.BirlesimId, model.BasTarihi,model.StenoGorevTuru);
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BaslangicTarihi = model.BasTarihi;
                        _globalService.UpdateOturum(oturum);
                    }
                }
                else if(ToplanmaBaslatmaStatu.AraVerme == model.ToplanmaBaslatmaStatu)
                {
                    var birlesim =_globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if(birlesim != null)
                    {
                        birlesim.ToplanmaDurumu = ToplanmaStatu.AraVerme;
                        _globalService.UpdateBirlesim(birlesim);
                    }
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if(oturum!= null)
                    {
                        oturum.BitisTarihi = model.BasTarihi;
                        _globalService.UpdateOturum(oturum);
                    }
                }
                else if(ToplanmaBaslatmaStatu.DevamEtme == model.ToplanmaBaslatmaStatu)
                {
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.ToplanmaDurumu = ToplanmaStatu.DevamEdiyor;
                        _globalService.UpdateBirlesim(birlesim);
                    }
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi != null).LastOrDefault();
                   var oturumId= _globalService.CreateOturum(new Oturum { BirlesimId = model.BirlesimId, BaslangicTarihi = model.BasTarihi });
                   _stenoService.UpdateBirlesimStenoGorevDevamEtme(model.BirlesimId, model.BasTarihi,model.StenoGorevTuru, oturum.BitisTarihi.Value, oturumId);
                }
                else if(ToplanmaBaslatmaStatu.Sonladırma == model.ToplanmaBaslatmaStatu)
                {
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BitisTarihi = model.BasTarihi;
                        _globalService.UpdateOturum(oturum);

                    }
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if(birlesim !=null)
                    {
                        birlesim.BitisTarihi = model.BasTarihi;
                        birlesim.ToplanmaDurumu = ToplanmaStatu.Tamamlandı;
                        _globalService.UpdateBirlesim(birlesim);
                    }
                    _stenoService.UpdateStenoGorevTamamla(model.BirlesimId, model.StenoGorevTuru);
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }


        [HttpPost("CreateStenoGorevAtama")]
        public IActionResult CreateStenoGorevAtama(StenoGorevAtamaModel model)
        {
            if(model.StenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                var entity = Mapper.Map<GorevAtama>(model);
               _stenoService.CreateStenoGorevAtama(entity);             
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("AddStenoGorevAtamaKomisyon")]
        public IActionResult AddStenoGorevAtamaKomisyon(StenoGorevAtamaModel model)
        {
            if (model.StenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                if (oturum != null)
                    model.OturumId = oturum.Id;

                var entity = Mapper.Map<GorevAtama>(model);          
                _stenoService.AddStenoGorevAtamaKomisyon(entity);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("ChangeOrderStenografKomisyon")]
        public IActionResult ChangeOrderStenografKomisyon(Guid kaynakBirlesimId, Guid kaynakStenografId, Guid hedefBirlesimId, Guid hedefStenografId)
        {
            try
            {
                _stenoService.ChangeOrderStenografKomisyon(kaynakBirlesimId, kaynakStenografId, hedefBirlesimId, hedefStenografId);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("ChangeSureStenografKomisyon")]
        public IActionResult ChangeSureStenografKomisyon(Guid gorevAtamaId,  double sure,bool digerAtamalarDahil =false)
        {
            try
            {
                _stenoService.ChangeSureStenografKomisyon(gorevAtamaId, sure, digerAtamalarDahil);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPut("UpdateStenoGorevAtama")]
        public IActionResult UpdateStenoGorevAtama(List<StenoGorevGüncelleModel> model)
        {
            try
            {
                var entity = Mapper.Map<List<GorevAtama>>(model);
                _stenoService.UpdateStenoGorev(entity);
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
            var stenoEntity = _stenoService.GetAllStenografByGroupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            model.ToList().ForEach(x => { x.GorevStatu = -1; x.GunlukGorevSuresi = _globalService.GetStenoSureDailyById(x.Id); x.HaftalikGorevSuresi = (int)_globalService.GetStenoSureWeeklyById(x.Id); x.YillikGorevSuresi = (int)_globalService.GetStenoSureYearlyById(x.Id, yasamaId); });
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
                var steno = stenoEntity.SelectMany(x => x.StenoGrups).Where(x => x.GrupId == item.GrupId).Select(cl => new StenoViewModel
                {
                    AdSoyad = cl.Stenograf.AdSoyad,
                    Id = cl.Stenograf.Id,
                    SonGorevSuresi = cl.Stenograf.GorevAtamas.Where(x => x.GorevBasTarihi >= DateTime.Now.AddDays(-7)).Sum(c => c.GorevDakika),
                    StenoGorevTuru = cl.Stenograf.StenoGorevTuru
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

        [HttpGet("GetStenoGorevByGrupId")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByGrupId(Guid groupId)
        {
            var stenoGrpEntity = _stenoService.GetStenoGorevByGrupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoGrpEntity);
            return model;
        }
        [HttpDelete("DeleteStenoGorev")]
        public IActionResult DeleteStenoGorev(Guid stenoGorevId)
        {
            try
            {
                _stenoService.DeleteStenoGorev(stenoGorevId);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpDelete("DeleteGorevByBirlesimIdAndStenoId")]
        public IActionResult DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId,Guid stenografId)
        {
            try
            {
                _stenoService.DeleteGorevByBirlesimIdAndStenoId(birlesimId, stenografId);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("CreateStenoGroup")]
        public IActionResult CreateStenoGroup(StenoGrupModel model)
        {

            var entity = Mapper.Map<StenoGrup>(model);
            _stenoService.CreateStenoGroup(entity);
            return Ok();
        }

        [HttpDelete("DeleteStenoGroup")]
        public IActionResult DeleteStenoGroup(StenoGrupModel model)
        {

            var entity = Mapper.Map<StenoGrup>(model);
            _stenoService.DeleteStenoGroup(entity);
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

        [HttpGet("GetAssignedStenoByBirlesimId")]
        public IEnumerable<StenoModel> GetAssignedStenoByBirlesimId(Guid birlesimId)
        {
            var stenoEntity = _stenoService.GetAssignedStenoByBirlesimId(birlesimId);
            var stenoGroup = stenoEntity.GroupBy(x => new
            {
                x.StenografId,
                x.Stenograf.AdSoyad,
                x.Stenograf.StenoGorevTuru,
                x.Stenograf.SiraNo,
                x.Stenograf.SonGorevSuresi,
                x.Stenograf.StenoGorevDurum
                //x.GorevStatu
            }).Select(z => new Stenograf
            {
                Id = z.Key.StenografId,
                AdSoyad = z.Key.AdSoyad,
                StenoGorevTuru = z.Key.StenoGorevTuru,
                SiraNo = z.Key.SiraNo,
                SonGorevSuresi = z.Key.SonGorevSuresi,
                StenoGorevDurum = z.Key.StenoGorevDurum
                //GorevStatu=(int)z.Key.GorevStatu
            });
            var yasamaId = stenoEntity.Count() > 0? stenoEntity.First().Birlesim.YasamaId : new Guid("00000000-0000-0000-0000-000000000000");

            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGroup);
            model.ToList().ForEach(x => { x.GunlukGorevSuresi = _globalService.GetStenoSureDailyById(x.Id); x.HaftalikGorevSuresi = _globalService.GetStenoSureWeeklyById(x.Id); x.YillikGorevSuresi = _globalService.GetStenoSureYearlyById(x.Id, yasamaId); });
            return model;
        }

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

    }
}
