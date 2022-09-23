using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Transactions;
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
        private readonly IStenografService _stenografService;
        private readonly ILogger<GorevAtamaController> _logger;
        public readonly IMapper _mapper;
        private readonly IGlobalService _globalService;
        public GorevAtamaController(IGorevAtamaService gorevAtamaService, IStenografService stenografService,
            ILogger<GorevAtamaController> logger, IMapper mapper, IGlobalService globalService)
        {
            _stenografService = stenografService;
            _gorevAtamaService = gorevAtamaService;
            _logger = logger;
            _mapper = mapper;
            _globalService = globalService;
        }

        [HttpPost("CheckBirlesim")]
        public IActionResult CheckBirlesim()
        {
            try
            {
                var gkBirlesim = _globalService.GetAktifGKBirlesim();
                if (gkBirlesim != null && gkBirlesim.Count() > 0)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable, Json("Mevcut Genel Kurul toplantısı devam ettiğinde yeni bir Genel Kurul oluşturalamaz!"));
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
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
                    var stenoList = stenoAllList.Select(x => new StenoKomisyonGrupModel { Id = x.Id, GrupId = x.GrupId,BirlesimKapatan =x.BirlesimKapatanMi,StenoGorevTuru=x.StenoGorevTuru });
                    if (stenoList != null && stenoList.Count() > 0)
                    {
                        var modelList = SetGorevAtama(birlesim, oturumId, stenoList, birlesim.StenoSure, ToplanmaTuru.GenelKurul, StenoGorevTuru.Stenograf);
                        var entityList = Mapper.Map<List<GorevAtamaGenelKurul>>(modelList);
                        _gorevAtamaService.CreateStenoAtamaGK(entityList);
                    }
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("UpdateBirlesimGorevAtama")]
        public IActionResult UpdateBirlesimGorevAtama(BirlesimModel model)
        {
            try
            {
                var entity = Mapper.Map<Birlesim>(model);
                _gorevAtamaService.UpdateBirlesim(entity);
                if (model.ToplanmaTuru == ToplanmaTuru.GenelKurul)
                {
                    var atamaList = _gorevAtamaService.GetGorevAtamaByBirlesimId(entity.Id, ToplanmaTuru.GenelKurul);
                    if (atamaList != null)
                    {
                        var entityList = new List<GorevAtamaGenelKurul>();
                        _mapper.Map(atamaList, entityList);
                        entityList.ForEach(x => x.IsDeleted = true);
                        _gorevAtamaService.UpdateStenoAtamaGK(entityList);
                        var stenoAllList = _gorevAtamaService.GetStenografIdList();
                        var stenoList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf)
                                                    .Select(x => new StenoKomisyonGrupModel { Id = x.Id, GrupId = x.GrupId });
                        if (stenoList != null && stenoList.Count() > 0)
                        {
                            var modelList = SetGorevAtama(entity, atamaList.FirstOrDefault().OturumId, stenoList, model.StenoSure, ToplanmaTuru.GenelKurul, StenoGorevTuru.Stenograf);
                            var stenoUzmanList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Uzman)
                                                             .Select(x => new StenoKomisyonGrupModel { Id = x.Id, GrupId = x.GrupId });
                            if (stenoUzmanList != null && stenoUzmanList.Count() > 0)
                            {
                                var modelUzmanList = SetGorevAtama(entity, atamaList.FirstOrDefault().OturumId, stenoUzmanList, model.UzmanStenoSure, ToplanmaTuru.GenelKurul, StenoGorevTuru.Uzman);
                                modelList.AddRange(modelUzmanList);
                            }
                            var entityNewList = Mapper.Map<List<GorevAtamaGenelKurul>>(modelList);
                            _gorevAtamaService.CreateStenoAtamaGK(entityNewList);
                        }
                    }
                }
                else if (model.ToplanmaTuru == ToplanmaTuru.Komisyon)
                {
                    var atamaList = _gorevAtamaService.GetGorevAtamaByBirlesimId(entity.Id, ToplanmaTuru.Komisyon).OrderBy(x => x.SatırNo);
                    if (atamaList != null)
                    {
                        var entityList = new List<GorevAtamaKomisyon>();
                        _mapper.Map(atamaList, entityList);

                        var stenoAllList = _gorevAtamaService.GetAssignedStenoByBirlesimId((Guid)model.Id).OrderBy(x => x.SatırNo);
                        var stenoList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf)
                                                    .Select(x => new StenoKomisyonGrupModel { Id = x.StenografId, GrupId = x.Stenograf.GrupId });
                        if (stenoList != null && stenoList.Count() > 0)
                        {
                            var modelList = SetGorevAtama(entity, atamaList.FirstOrDefault().OturumId, stenoList, model.StenoSure, ToplanmaTuru.Komisyon, StenoGorevTuru.Stenograf);

                            entityList.ForEach(x => x.IsDeleted = true);
                            _gorevAtamaService.UpdateStenoAtamaKom(entityList);

                            modelList.AsParallel().ForAll(x => x.Id = Guid.Empty);
                            var entityNewList = Mapper.Map<List<GorevAtamaKomisyon>>(modelList);
                            _gorevAtamaService.CreateStenoAtamaKom(entityNewList);

                        }
                    }
                }
                //else if(model.ToplanmaTuru == ToplanmaTuru.OzelToplanti)
                //{
                //    var atamaList = _gorevAtamaService.GetGorevAtamaByBirlesimId(entity.Id, ToplanmaTuru.OzelToplanti).OrderBy(x => x.SatırNo);
                //    if (atamaList != null)
                //    {
                //        var entityList = new List<GorevAtamaOzelToplanma>();
                //        _mapper.Map(atamaList, entityList);

                //        var stenoAllList = _gorevAtamaService.GetAssignedStenoByBirlesimId((Guid)model.Id).OrderBy(x => x.SatırNo);
                //        var stenoList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf)
                //                                    .Select(x => new StenoKomisyonGrupModel { Id = x.StenografId, GrupId = x.Stenograf.GrupId });
                //        if (stenoList != null && stenoList.Count() > 0)
                //        {
                //            var modelList = SetGorevAtama(entity, atamaList.FirstOrDefault().OturumId, stenoList, model.StenoSure, ToplanmaTuru.OzelToplanti, StenoGorevTuru.Stenograf);

                //            entityList.ForEach(x => x.IsDeleted = true);
                //            _gorevAtamaService.UpdateStenoAtamaOzelToplanti(entityList);

                //            modelList.AsParallel().ForAll(x => x.Id = Guid.Empty);
                //            var entityNewList = Mapper.Map<List<GorevAtamaOzelToplanma>>(modelList);
                //            _gorevAtamaService.CreateStenoAtamaKom(entityNewList);

                //        }
                //    }
                //}
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateStenoGorevAtama")]
        public IActionResult CreateStenoGorevAtama(StenoGorevAtamaModel model)
        {
            if (model.StenografIds == null || model.TurAdedi == 0)
                return BadRequest("Stenograf Listesi veye Tur Sayısı Dolu Olmalıdır!");
            try
            {
                var birlesim = _gorevAtamaService.UpdateBirlesimGorevAtama(model.BirlesimId, model.TurAdedi);
                var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                var modelList = SetGorevAtama(birlesim, oturum.Id, model.StenografIds, birlesim.StenoSure, ToplanmaTuru.Komisyon, StenoGorevTuru.Stenograf);
                var entityList = Mapper.Map<List<GorevAtamaKomisyon>>(modelList);
                _gorevAtamaService.CreateStenoAtamaKom(entityList);
                //UpdateGenelKurulKomisyon();
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        private List<GorevAtamaModel> SetGorevAtama(Birlesim birlesim, Guid oturumId, IEnumerable<StenoKomisyonGrupModel> stenoList, double sure, ToplanmaTuru toplanmaTuru, StenoGorevTuru gorevTuru)
        {
            var atamaList = new List<GorevAtamaModel>();
            var basDate = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value : DateTime.Now;
            int firstRec = 0;
            bool firstRecInc = false;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtamaModel();
                    newEntity.BirlesimId = birlesim.Id;
                    newEntity.OturumId = oturumId;
                    newEntity.StenografId = item.Id;
                    newEntity.GorevBasTarihi = basDate.AddMinutes(firstRec * sure);
                    newEntity.GorevBitisTarihi = basDate.AddMinutes((firstRec * sure) + sure);
                    newEntity.StenoSure = sure;
                    newEntity.StenoGorevTuru = gorevTuru;
                    newEntity.StenoIzinTuru = _gorevAtamaService.GetStenoIzinByGorevBasTarih(item.Id, newEntity.GorevBasTarihi);
                    newEntity.KomisyonAd = gorevTuru == StenoGorevTuru.Stenograf && toplanmaTuru == ToplanmaTuru.GenelKurul ? _gorevAtamaService.GetKomisyonMinMaxDate(item.Id, newEntity.GorevBasTarihi, newEntity.GorevBitisTarihi, sure) : null;
                    newEntity.GidenGrupMu = _gorevAtamaService.GidenGrupHesaplama(ToplanmaTuru.Komisyon, newEntity.StenoSure, (Guid)item.GrupId) != DateTime.MinValue &&
                    newEntity.GorevBasTarihi >= _gorevAtamaService.GidenGrupHesaplama(ToplanmaTuru.Komisyon, newEntity.StenoSure, (Guid)item.GrupId) ? true : false;
                    newEntity.GidenGrup = newEntity.GidenGrupMu ? "GidenGrup" : string.Empty;
                    newEntity.BirlesimKapatanMı = item.BirlesimKapatan;
                    firstRec = gorevTuru == StenoGorevTuru.Uzman && !firstRecInc ? 1 : firstRec +1;
                    firstRecInc = gorevTuru == StenoGorevTuru.Uzman ? true : false;
                    newEntity.SatırNo = firstRec;
                    newEntity.OnayDurumu = true;
                    atamaList.Add(newEntity);
                }
            }
            return BirlesimSureHesaplama(atamaList);
        }


        private List<GorevAtamaModel> BirlesimSureHesaplama(List<GorevAtamaModel> atamaList)
        {
            var gorevBasTarihi = atamaList.OrderBy(x => x.SatırNo).Where(x => x.GorevBasTarihi != DateTime.MinValue).FirstOrDefault().GorevBasTarihi.Value;
            var gorevBitTarihi = atamaList.Where(x => x.GorevBitisTarihi != DateTime.MinValue).FirstOrDefault().GorevBitisTarihi.Value;
            var ste = atamaList.Where(x => x.StenografId == atamaList.FirstOrDefault().StenografId);
            var stenoToplamSureAsım = ste.Max(x => x.GorevBitisTarihi.Value).Subtract(ste.Min(x => x.GorevBasTarihi.Value)).TotalMinutes <= 50;
            var lst = new List<GorevAtamaModel>();
            foreach (var item in atamaList)
            {
                if (!string.IsNullOrEmpty(item.KomisyonAd) || item.GorevStatu == GorevStatu.Iptal || item.StenoIzinTuru != IzınTuru.Bulunmuyor || item.GidenGrupMu || item.BirlesimKapatanMı)
                {
                    item.GorevStatu = GorevStatu.Iptal;
                }
                else
                {
                    if(item.GorevBasTarihi != DateTime.MinValue) //Birleşimde açan kişiden önceki kişilerin görev tarihi min date
                    {
                        if (item.GorevBasTarihi != gorevBasTarihi)
                        {
                            item.GorevBasTarihi = gorevBasTarihi;
                        }
                        if (item.GorevBitisTarihi != gorevBitTarihi)
                        {
                            item.GorevBitisTarihi = gorevBitTarihi;
                        }
                        gorevBasTarihi = item.GorevBitisTarihi.Value;
                        gorevBitTarihi = item.GorevBitisTarihi.HasValue ? item.GorevBitisTarihi.Value.AddMinutes(item.StenoSure) : DateTime.MinValue;
                    }
                }
                item.SureAsmaVar = stenoToplamSureAsım;
                lst.Add(item);
            }
            return lst;
        }

        [HttpPost("AddStenoGorevAtamaKomisyon")]
        public IActionResult AddStenoGorevAtamaKomisyon(StenoGorevAtamaModel model)
        {
            if (model.StenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                var atamaList = _gorevAtamaService.AddStenoGorevAtamaKomisyon(model.StenografIds, model.BirlesimId, model.OturumId);
                if (atamaList != null && atamaList.Count() > 0)
                {
                    var modelList = BirlesimIptalHesaplama(atamaList.OrderBy(x => x.SatırNo).ToList());
                    var entityList = Mapper.Map<List<GorevAtamaKomisyon>>(modelList);
                    _gorevAtamaService.CreateStenoAtamaKom(entityList);
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPut("CreateStenoGorevDonguGenelKurul")]
        public IActionResult CreateStenoGorevDonguGenelKurul(Guid birlesimId, Guid oturumId, StenoGorevTuru gorevTuru)
        {
            try
            {
                var atamaList = _gorevAtamaService.CreateStenoGorevDonguGenelKurul(birlesimId, oturumId, gorevTuru);
                if (atamaList != null && atamaList.Count() > 0)
                {
                    var modelList = BirlesimSureHesaplama(atamaList.OrderBy(x => x.SatırNo).ToList());
                    var entityList = Mapper.Map<List<GorevAtamaGenelKurul>>(modelList);
                    _gorevAtamaService.CreateStenoAtamaGK(entityList);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("CreateStenoGorevDonguKomisyon")]
        public IActionResult CreateStenoGorevDonguKomisyon(Guid birlesimId, Guid oturumId)
        {
            try
            {
                var atamaList = _gorevAtamaService.CreateStenoGorevDonguKomisyon(birlesimId, oturumId);
                if (atamaList != null && atamaList.Count() > 0)
                {
                    var modelList = BirlesimSureHesaplama(atamaList.OrderBy(x => x.SatırNo).ToList());
                    var entityList = Mapper.Map<List<GorevAtamaKomisyon>>(modelList);
                    _gorevAtamaService.CreateStenoAtamaKom(entityList);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("GetStenoGorevByBirlesimId")]
        public IEnumerable<GorevAtamaModel> GetStenoGorevByBirlesimId(Guid birlesimId, ToplanmaTuru toplanmaTuru)
        {
            return _gorevAtamaService.GetGorevAtamaByBirlesimId(birlesimId, toplanmaTuru);
        }

        [HttpPost("ChangeOrderStenografKomisyon")]
        public IActionResult ChangeOrderStenografKomisyon(Guid kaynakBirlesimId, Guid kaynakStenografId, Guid hedefBirlesimId, Guid hedefStenografId)
        {
            try
            {
                _gorevAtamaService.ChangeOrderStenografKomisyon(kaynakBirlesimId, kaynakStenografId, hedefBirlesimId, hedefStenografId);
                var hedefResult = _gorevAtamaService.GetGorevAtamaByBirlesimId(hedefBirlesimId, ToplanmaTuru.Komisyon);
                if (hedefResult != null && hedefResult.Count() > 0)
                {
                    var modelList = new List<GorevAtamaModel>();
                    _mapper.Map(hedefResult, modelList);
                    _gorevAtamaService.UpdateGorevAtama(BirlesimIptalHesaplama(modelList), ToplanmaTuru.Komisyon);
                }

            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("ApproveStenografKomisyon")]
        public IActionResult ApproveStenografKomisyon()
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    _gorevAtamaService.ApproveStenografKomisyon();
                    UpdateGenelKurulKomisyon();
                    transactionScope.Complete();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

            }
        }

        [HttpPost("CancelStenografKomisyon")]
        public IActionResult CancelStenografKomisyon()
        {
            try
            {
                _gorevAtamaService.CancelStenografKomisyon();
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("ChangeSureStenografKomisyon")]
        public IActionResult ChangeSureStenografKomisyon(Guid birlesimId, int satırNo, double sure, bool digerAtamalarDahil = false)
        {
            try
            {
                var result = _gorevAtamaService.ChangeSureStenografKomisyon(birlesimId, satırNo, sure, digerAtamalarDahil);
                if (result != null && result.Result != null && result.Result.Count > 0)
                {
                    var modelList = new List<GorevAtamaModel>();
                    _mapper.Map(result.Result, modelList);
                    _gorevAtamaService.UpdateGorevAtama(BirlesimIptalHesaplama(modelList), ToplanmaTuru.Komisyon);
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPut("UpdateGorevDurumById")]
        public IActionResult UpdateGorevDurumById(Guid id, Guid birlesimId, ToplanmaTuru toplanmaTuru, StenoGorevTuru gorevTuru)
        {
            try
            {
                var result = _gorevAtamaService.UpdateGorevDurumById(id, birlesimId, toplanmaTuru, gorevTuru).OrderBy(x => x.SatırNo);
                if (result != null && result.Count() > 0)
                {
                    var hesaplama = BirlesimIptalHesaplama(result.ToList());
                    if (hesaplama != null && hesaplama.Count > 0)
                    {
                        _gorevAtamaService.UpdateGorevAtama(hesaplama, toplanmaTuru);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut("UpdateGorevDurumByBirlesimAndSteno")]
        public IActionResult UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId, ToplanmaTuru toplanmaTuru, StenoGorevTuru gorevTuru)
        {
            try
            {
                var result = _gorevAtamaService.UpdateGorevDurumByBirlesimAndSteno(birlesimId, stenoId, toplanmaTuru, gorevTuru).OrderBy(x => x.SatırNo);
                if (result != null && result.Count() > 0)
                {
                    var hesaplama = BirlesimIptalHesaplama(result.ToList());
                    if (hesaplama != null && hesaplama.Count > 0)
                    {
                        _gorevAtamaService.UpdateGorevAtama(hesaplama, toplanmaTuru);
                    }
                }
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
                        SetOturumModifiedStenoInfo(model.ToplanmaBaslatmaStatu, model, oturum);
                        _globalService.UpdateOturum(oturum);
                    }

                }
                else if (ToplanmaBaslatmaStatu.AraVerme == model.ToplanmaBaslatmaStatu)
                {
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.ToplanmaDurumu = ToplanmaStatu.AraVerme;
                        _gorevAtamaService.UpdateBirlesim(birlesim);
                    }
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BitisTarihi = model.BasTarihi;
                        SetOturumModifiedStenoInfo(model.ToplanmaBaslatmaStatu, model, oturum);
                        _globalService.UpdateOturum(oturum);
                    }
                }
                else if (ToplanmaBaslatmaStatu.DevamEtme == model.ToplanmaBaslatmaStatu)
                {
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.ToplanmaDurumu = ToplanmaStatu.DevamEdiyor;
                        _gorevAtamaService.UpdateBirlesim(birlesim);
                    }
                    var oturum = new Oturum { BirlesimId = model.BirlesimId, BaslangicTarihi = model.BasTarihi };
                    SetOturumModifiedStenoInfo(model.ToplanmaBaslatmaStatu, model, oturum);
                    _globalService.CreateOturum(oturum);
                    _gorevAtamaService.UpdateBirlesimStenoGorevDevamEtme(model.BirlesimId, model.BasTarihi, model.KaynakSatırNo, model.HedefSatırNo, oturum.Id, model.ToplanmaTuru);
                }
                else if (ToplanmaBaslatmaStatu.Sonladırma == model.ToplanmaBaslatmaStatu)
                {
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BitisTarihi = model.BasTarihi;
                        SetOturumModifiedStenoInfo(model.ToplanmaBaslatmaStatu, model, oturum);
                        _globalService.UpdateOturum(oturum);

                    }
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.BitisTarihi = model.BasTarihi;
                        birlesim.ToplanmaDurumu = ToplanmaStatu.Tamamlandı;
                        _gorevAtamaService.UpdateBirlesim(birlesim);
                    }
                    _gorevAtamaService.UpdateStenoGorevTamamla(model.BirlesimId, model.ToplanmaTuru, model.KaynakSatırNo, model.HedefSatırNo);
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        private void SetOturumModifiedStenoInfo(ToplanmaBaslatmaStatu toplanmaBaslatmaStatu, BirlesimStenoGorevModel model, Oturum? oturum)
        {
            var stenograf =_stenografService.GetStenoById(model.StenografId);
            if (toplanmaBaslatmaStatu.Equals(ToplanmaBaslatmaStatu.Baslama) || toplanmaBaslatmaStatu.Equals(ToplanmaBaslatmaStatu.DevamEtme))
            {
                switch (model.StenoGorevTuru)
                {
                    case StenoGorevTuru.Stenograf:
                        oturum.AcanSira = stenograf.SiraNo;
                        break;
                    case StenoGorevTuru.Uzman:
                        oturum.AcanSiraUzman = stenograf.SiraNo;
                        break;
                }
            }
            else if (toplanmaBaslatmaStatu.Equals(ToplanmaBaslatmaStatu.AraVerme) || toplanmaBaslatmaStatu.Equals(ToplanmaBaslatmaStatu.Sonladırma))
            {
                switch (model.StenoGorevTuru)
                {
                    case StenoGorevTuru.Stenograf:
                        oturum.KapatanSira = stenograf.SiraNo;
                        break;
                    case StenoGorevTuru.Uzman:
                        oturum.KapatanSiraUzman = stenograf.SiraNo;
                        break;
                }
            }
        }

        [HttpDelete("DeleteGorevByBirlesimIdAndStenoId")]
        public IActionResult DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, List<Guid> stenografId, ToplanmaTuru toplanmaTuru)
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
                StenoIzinTuru = _stenografService.GetStenoIzinTodayByStenoId(z.Key.StenografId),
                StenoGorevDurum = z.Key.StenoGorevDurum
                //GorevStatu=(int)z.Key.GorevStatu
            });
            var yasamaId = stenoEntity.Count() > 0 ? stenoEntity.First().Birlesim.YasamaId : new Guid("00000000-0000-0000-0000-000000000000");

            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGroup);
            //şimdilik kaldırıldı, tablodan direkt getirelecek, perfomanstan dolayı
            //model.ToList().ForEach(x => { x.GunlukGorevSuresi = _globalService.GetStenoSureDailyById(x.Id); x.HaftalikGorevSuresi = _globalService.GetStenoSureWeeklyById(x.Id); x.YillikGorevSuresi = _globalService.GetStenoSureYearlyById(x.Id, yasamaId); });
            return model;
        }
        private List<GorevAtamaModel> BirlesimIptalHesaplama(List<GorevAtamaModel> atamaList)
        {
            //atamaList.ForEach(x =>
            //{
            //    x.StenoIzinTuru = _gorevAtamaService.GetStenoIzinByGorevBasTarih(x.StenografId, x.GorevBasTarihi);
            //    x.GidenGrupMu = _gorevAtamaService.GidenGrupHesaplama(ToplanmaTuru.Komisyon, x.StenoSure) != DateTime.MinValue &&
            //    x.GorevBasTarihi >= _gorevAtamaService.GidenGrupHesaplama(ToplanmaTuru.Komisyon, x.StenoSure) ? true : false;
            //    x.GidenGrup = x.GidenGrupMu ? "GidenGrup" : string.Empty;
            //});
            var gorevBasTarihi = atamaList.FirstOrDefault().GorevBasTarihi.Value;
            var gorevBitTarihi = atamaList.FirstOrDefault().GorevBitisTarihi.Value;
            var lst = new List<GorevAtamaModel>();
            foreach (var item in atamaList)
            {
                if (item.GorevStatu == GorevStatu.Iptal || item.StenoIzinTuru != IzınTuru.Bulunmuyor || item.GidenGrupMu)
                {
                    item.GorevStatu = GorevStatu.Iptal;
                }
                else
                {
                    if (item.GorevBasTarihi != gorevBasTarihi)
                    {
                        item.GorevBasTarihi = gorevBasTarihi;
                    }
                    if (item.GorevBitisTarihi != gorevBitTarihi)
                    {
                        item.GorevBitisTarihi = gorevBitTarihi;
                    }
                    if (item.GorevBitisTarihi.Value.Subtract(item.GorevBasTarihi.Value).Minutes != item.StenoSure)
                    {
                        item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(item.StenoSure);
                    }
                    gorevBasTarihi = item.GorevBitisTarihi.Value;
                    gorevBitTarihi = item.GorevBitisTarihi.HasValue ? item.GorevBitisTarihi.Value.AddMinutes(item.StenoSure) : DateTime.MinValue;
                }
                item.OnayDurumu = false;
                lst.Add(item);
            }
            return lst;
        }
        private void UpdateGenelKurulKomisyon()
        {
            var gkBirlesim = _globalService.GetAktifGKBirlesim();
            if (gkBirlesim != null && gkBirlesim.Count() > 0)
            {
                var gkAtama = _gorevAtamaService.GetGorevAtamaByBirlesimId(gkBirlesim.FirstOrDefault().Id, ToplanmaTuru.GenelKurul);
                if (gkAtama != null && gkAtama.Count() > 0)
                {
                    var modelList = _mapper.Map<List<GorevAtamaModel>>(gkAtama);
                    modelList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf).ToList().ForEach(model => model.KomisyonAd = _gorevAtamaService.GetKomisyonMinMaxDate(model.StenografId, model.GorevBasTarihi, model.GorevBitisTarihi, model.StenoSure));
                    var result = BirlesimSureHesaplama(modelList);
                    var entityList = _mapper.Map<List<GorevAtamaGenelKurul>>(result);
                    _gorevAtamaService.UpdateStenoAtamaGK(entityList);
                }
            }
        }
    }
}
