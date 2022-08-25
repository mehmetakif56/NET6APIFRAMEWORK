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
        private readonly IGlobalService _globalService;
        public GorevAtamaController(IGorevAtamaService gorevAtamaService, ILogger<GorevAtamaController> logger, IMapper mapper,IGlobalService globalService)
        {
            _gorevAtamaService = gorevAtamaService;
            _logger = logger;
            _mapper = mapper;
            _globalService = globalService;
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
                    var stenoList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf)
                                                .Select(x => x.Id);
                                                
                    var modelList = SetGorevAtama(birlesim, oturumId, stenoList,birlesim.StenoSure);
                    var stenoUzmanList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Uzman)
                                                     .Select(x => x.Id);
                                                    
                    var modelUzmanList = SetGorevAtama(birlesim, oturumId, stenoUzmanList, birlesim.UzmanStenoSure);
                    modelList.AddRange(modelUzmanList);
                    var entityList = Mapper.Map<List<GorevAtamaGenelKurul>>(modelList);
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

        [HttpPost("CreateStenoGorevAtama")]
        public IActionResult CreateStenoGorevAtama(StenoGorevAtamaModel model)
        {
            if (model.StenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                var birlesim = _gorevAtamaService.UpdateBirlesimGorevAtama(model.BirlesimId,model.TurAdedi);
                var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                var modelList = SetGorevAtama(birlesim, oturum.Id, model.StenografIds, birlesim.StenoSure);
                var entityList = Mapper.Map<List<GorevAtamaKomisyon>>(modelList);
                _gorevAtamaService.CreateStenoAtamaKom(entityList);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        private List<GorevAtamaModel> SetGorevAtama(Birlesim birlesim, Guid oturumId, IEnumerable<Guid> stenoList,double sure)
        {
            var atamaList = new List<GorevAtamaModel>();
            var basDate = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value:DateTime.Now;
            int firstRec = 0;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtamaModel();
                    newEntity.BirlesimId = birlesim.Id;
                    newEntity.OturumId = oturumId;
                    newEntity.StenografId = item;
                    newEntity.GorevBasTarihi = basDate.AddMinutes(firstRec * sure);
                    newEntity.GorevBitisTarihi = basDate.AddMinutes((firstRec * sure) + sure);
                    newEntity.StenoSure = sure;
                    newEntity.StenoIzinTuru = _gorevAtamaService.GetStenoIzinByGorevBasTarih(item, newEntity.GorevBasTarihi) ;
                    //newEntity.GorevStatu = item.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                    firstRec++;
                    newEntity.SatırNo = firstRec ;
                    atamaList.Add(newEntity);
                    
                }
            }
            //var ste = atamaList.Where(x => x.StenografId == stenoList.FirstOrDefault());
            //var stenoToplamSureAsım = ste.Max(x => x.GorevBitisTarihi.Value).Subtract(ste.Min(x => x.GorevBasTarihi.Value)).TotalMinutes <= 50;
            //atamaList.ForEach(x => x.SureAsmaVar = stenoToplamSureAsım);

            return atamaList;
        }


        [HttpPost("AddStenoGorevAtamaKomisyon")]
        public IActionResult AddStenoGorevAtamaKomisyon(StenoGorevAtamaModel model)
        {
            if (model.StenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                _gorevAtamaService.AddStenoGorevAtamaKomisyon(model.StenografIds, model.BirlesimId, model.OturumId);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPut("CreateStenoGorevDonguEkle")]
        public IActionResult CreateStenoGorevDonguEkle(Guid birlesimId, Guid oturumId)
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
        public List<GorevAtamaModel> GetStenoGorevByBirlesimId(Guid birlesimId,ToplanmaTuru toplanmaTuru)
        {
            var entity = _gorevAtamaService.GetGorevAtamalarByBirlesimId(birlesimId, toplanmaTuru);
            var model = _mapper.Map<List<GorevAtamaModel>>(entity);

            return model;
        }

        //[HttpPost("ChangeOrderStenografKomisyon")]
        //public IActionResult ChangeOrderStenografKomisyon(string kaynakBirlesimId, Dictionary<string, string> kaynakStenoList, string hedefBirlesimId, Dictionary<string, string> hedefStenografList)
        //{
        //    try
        //    {
        //        _gorevAtamaService.ChangeOrderStenografKomisyon(kaynakBirlesimId, kaynakStenoList, hedefBirlesimId, hedefStenografList);
        //    }
        //    catch (Exception ex)
        //    { return BadRequest(ex.Message); }

        //    return Ok();
        //}

        [HttpPost("ChangeSureStenografKomisyon")]
        public IActionResult ChangeSureStenografKomisyon(string birlesimId, int satırNo, double sure, bool digerAtamalarDahil = false)
        {
            try
            {
                _gorevAtamaService.ChangeSureStenografKomisyon(birlesimId, satırNo, sure, digerAtamalarDahil);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpGet("GetStenografIdList")]
        public List<GorevAtamaModel> GetStenografIdList()
        {
            var entity = _gorevAtamaService.GetStenografIdListLast();
            var model = _mapper.Map<List<GorevAtamaModel>>(entity);
            return model;
        }
        [HttpPut("UpdateGorevDurumById")]
        public IActionResult UpdateGorevDurumById(Guid id, ToplanmaTuru toplanmaTuru)
        {
            try
            {
                _gorevAtamaService.UpdateGorevDurumById(id, toplanmaTuru);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut("UpdateGorevDurumByBirlesimAndSteno")]
        public IActionResult UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId, ToplanmaTuru toplanmaTuru)
        {
            try
            {
                _gorevAtamaService.UpdateGorevDurumByBirlesimAndSteno(birlesimId, stenoId, toplanmaTuru);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("UpdateBirlesimStenoGorev")]
        public IActionResult UpdateBirlesimStenoGorev(BirlesimStenoGorevModel model)
        {
            try
            {
                if (ToplanmaBaslatmaStatu.Baslama == model.ToplanmaBaslatmaStatu)
                {
                    _gorevAtamaService.UpdateBirlesimStenoGorevBaslama(model.BirlesimId, model.BasTarihi, model.ToplanmaTuru);
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BaslangicTarihi = model.BasTarihi;
                        _globalService.UpdateOturum(oturum);
                    }
                }
                else if (ToplanmaBaslatmaStatu.AraVerme == model.ToplanmaBaslatmaStatu)
                {
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.ToplanmaDurumu = ToplanmaStatu.AraVerme;
                        _globalService.UpdateBirlesim(birlesim);
                    }
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BitisTarihi = model.BasTarihi;
                        _globalService.UpdateOturum(oturum);
                    }
                }
                else if (ToplanmaBaslatmaStatu.DevamEtme == model.ToplanmaBaslatmaStatu)
                {
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.ToplanmaDurumu = ToplanmaStatu.DevamEdiyor;
                        _globalService.UpdateBirlesim(birlesim);
                    }
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi != null).LastOrDefault();
                    //var oturumId= _globalService.CreateOturum(new Oturum { BirlesimId = model.BirlesimId, BaslangicTarihi = model.BasTarihi });
                    var oturumId = Guid.Empty;
                    _gorevAtamaService.UpdateBirlesimStenoGorevDevamEtme(model.BirlesimId, model.BasTarihi, oturum.BitisTarihi.Value, oturumId,model.ToplanmaTuru);
                }
                else if (ToplanmaBaslatmaStatu.Sonladırma == model.ToplanmaBaslatmaStatu)
                {
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BitisTarihi = model.BasTarihi;
                        _globalService.UpdateOturum(oturum);

                    }
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.BitisTarihi = model.BasTarihi;
                        birlesim.ToplanmaDurumu = ToplanmaStatu.Tamamlandı;
                        _globalService.UpdateBirlesim(birlesim);
                    }
                    _gorevAtamaService.UpdateStenoGorevTamamla(model.BirlesimId,  model.ToplanmaTuru);
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpDelete("DeleteGorevByBirlesimIdAndStenoId")]
        public IActionResult DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, Guid stenografId, ToplanmaTuru toplanmaTuru)
        {
            try
            {
                _gorevAtamaService.DeleteGorevByBirlesimIdAndStenoId(birlesimId, stenografId, toplanmaTuru);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpGet("GetAssignedStenoByBirlesimId")]
        public IEnumerable<StenoModel> GetAssignedStenoByBirlesimId(Guid birlesimId)
        {
            var stenoEntity = _gorevAtamaService.GetAssignedStenoByBirlesimId(birlesimId);
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
            var yasamaId = stenoEntity.Count() > 0 ? stenoEntity.First().Birlesim.YasamaId : new Guid("00000000-0000-0000-0000-000000000000");

            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGroup);
            //şimdilik kaldırıldı, tablodan direkt getirelecek, perfomanstan dolayı
            //model.ToList().ForEach(x => { x.GunlukGorevSuresi = _globalService.GetStenoSureDailyById(x.Id); x.HaftalikGorevSuresi = _globalService.GetStenoSureWeeklyById(x.Id); x.YillikGorevSuresi = _globalService.GetStenoSureYearlyById(x.Id, yasamaId); });
            return model;
        }
    }
}
