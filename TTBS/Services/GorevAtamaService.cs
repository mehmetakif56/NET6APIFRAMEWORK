using AutoMapper;
using System;
using System.Globalization;
using System.Transactions;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;
using TTBS.Core.Interfaces;
using TTBS.Models;
using TTBS.MongoDB;
using GorevStatu = TTBS.Core.Enums.GorevStatu;

namespace TTBS.Services
{
    public interface IGorevAtamaService
    {
        Birlesim CreateBirlesim(Birlesim birlesim);
        Guid CreateOturum(Oturum Oturum);
        void CreateStenoAtamaGK(List<GorevAtamaGenelKurul> gorevAtamaList);
        void UpdateStenoAtamaGK(List<GorevAtamaGenelKurul> gorevAtamaList);
        void CreateStenoAtamaKom(List<GorevAtamaKomisyon> gorevAtamaList);
        Birlesim UpdateBirlesimGorevAtama(Guid birlesimId, int turAdedi);
        List<GorevAtamaModel> AddStenoGorevAtamaKomisyon(IEnumerable<StenoKomisyonGrupModel> stenografIds, Guid birlesimId, Guid oturumId);
        void ApproveStenografKomisyon();
        void CancelStenografKomisyon();
        List<GorevAtamaModel> CreateStenoGorevDonguKomisyon(Guid birlesimId, Guid oturumId);
        List<GorevAtamaModel> CreateStenoGorevDonguGenelKurul(Guid birlesimId, Guid oturumId, StenoGorevTuru gorevTuru);
        List<GorevAtamaModel> GetGorevAtamaByBirlesimId(Guid birlesimId, ToplanmaTuru toplanmaTuru);
        void ChangeOrderStenografKomisyon(Guid kaynakBirlesimId, Guid kaynakStenoId, Guid hedefBirlesimId, Guid hedefStenoId);
        Task<List<GorevAtamaKomisyon>> ChangeSureStenografKomisyon(Guid birlesimId, int satırNo, double sure, bool digerAtamalarDahil = false);
        IEnumerable<Stenograf> GetStenografIdList();
        void DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, Guid stenografId, ToplanmaTuru toplanmaTuru);
        void UpdateBirlesimStenoGorevBaslama(Guid birlesimId, DateTime basTarih, ToplanmaTuru toplanmaTuru);
        void UpdateBirlesimStenoGorevDevamEtme(Guid birlesimId, DateTime basTarih, DateTime oturumKapanmaTarihi, Guid oturumId, ToplanmaTuru toplanmaTuru);
        List<GorevAtamaModel> UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId, ToplanmaTuru toplanmaTuru, StenoGorevTuru gorevTuru);
        List<GorevAtamaModel> UpdateGorevDurumById(Guid id, Guid birlesimId, ToplanmaTuru toplanmaTuru,StenoGorevTuru gorevTuru);
        void UpdateStenoGorevTamamla(Guid birlesimId, ToplanmaTuru toplanmaTuru, int satırNo);
        IEnumerable<GorevAtamaKomisyon> GetAssignedStenoByBirlesimId(Guid birlesimId);
        IzınTuru GetStenoIzinByGorevBasTarih(Guid stenoId, DateTime? gorevBasTarih);
        GrupDetay GetGidenGrup(Guid grupId);
        DateTime GidenGrupHesaplama(ToplanmaTuru toplanmaTuru, double sure,Guid grupId);
        string GetKomisyonMinMaxDate(Guid stenoId, DateTime? gorevBasTarih, DateTime? gorevBitisTarih, double sure);
        void UpdateGorevAtama(IEnumerable<GorevAtamaModel> model, ToplanmaTuru toplanmaTuru);
        bool ActivateGidenGrupByGorevAtama(DurumStatu gidenGrupPasif, DateTime? gidenGrupSaat, DurumStatu uygula);
        void UpdateBirlesim(Birlesim birlesim);
    }
    public class GorevAtamaService : BaseService, IGorevAtamaService
    {
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<GorevAtamaGenelKurul> _gorevAtamaGKRepo;
        private IRepository<GorevAtamaKomisyon> _gorevAtamaKomRepo;
        private IRepository<GorevAtamaOzelToplanma> _gorevAtamaOzelRepo;
        private IRepository<Stenograf> _stenografRepo;
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<Oturum> _oturumRepo;
        private IRepository<GrupDetay> _grupDetayRepo;
        private IRepository<GorevAtamaKomisyonOnay> _komisyonOnayRepo;
        private IRepository<StenoToplamGenelSure> _stenoToplamGenelSureRepo;
        public readonly IMapper _mapper;

        public GorevAtamaService(IRepository<Birlesim> birlesimRepo,
                                 IRepository<GorevAtamaGenelKurul> gorevAtamaGKRepo,
                                 IRepository<GorevAtamaKomisyon> gorevAtamaKomRepo,
                                 IRepository<GorevAtamaOzelToplanma> gorevAtamaOzelRepo,
                                 IRepository<Stenograf> stenografRepo,
                                 IRepository<StenoIzin> stenoIzinRepo,
                                 IRepository<GrupDetay> grupDetayRepo,
                                 IRepository<Oturum> oturumRepo,
                                 IRepository<GorevAtamaKomisyonOnay> komisyonOnayRepo,
                                 IRepository<StenoToplamGenelSure> stenoToplamGenelSureRepo,
                                 IMapper mapper,
                                 IServiceProvider provider) : base(provider)
        {
            _birlesimRepo = birlesimRepo;
            _gorevAtamaGKRepo = gorevAtamaGKRepo;
            _gorevAtamaKomRepo = gorevAtamaKomRepo;
            _stenografRepo = stenografRepo;
            _oturumRepo = oturumRepo;
            _stenoIzinRepo = stenoIzinRepo;
            _gorevAtamaOzelRepo = gorevAtamaOzelRepo;
            _grupDetayRepo = grupDetayRepo;
            _komisyonOnayRepo = komisyonOnayRepo;
            _stenoToplamGenelSureRepo = stenoToplamGenelSureRepo;
            _mapper = mapper;
        }
        public Birlesim CreateBirlesim(Birlesim birlesim)
        {
            _birlesimRepo.Create(birlesim, CurrentUser.Id);
            _birlesimRepo.Save();

            return birlesim;
        }
        public void UpdateBirlesim(Birlesim birlesim)
        {
            _birlesimRepo.Update(birlesim, CurrentUser.Id);
            _birlesimRepo.Save();
        }

        public Guid CreateOturum(Oturum oturum)
        {
            var otr = _oturumRepo.Get(x => x.BirlesimId == oturum.BirlesimId);
            if (otr != null && otr.Count() > 0)
                oturum.OturumNo = otr.Max(x => x.OturumNo) + 1;
            _oturumRepo.Create(oturum, CurrentUser.Id);
            _oturumRepo.Save();
            return oturum.Id;
        }
        public void CreateStenoAtamaGK(List<GorevAtamaGenelKurul> gorevAtamaList)
        {
            _gorevAtamaGKRepo.Create(gorevAtamaList);
            _gorevAtamaGKRepo.Save();
        }
        public void UpdateStenoAtamaGK(List<GorevAtamaGenelKurul> gorevAtamaList)
        {
            _gorevAtamaGKRepo.Update(gorevAtamaList);
        }
        public void CreateStenoAtamaKom(List<GorevAtamaKomisyon> gorevAtamaList)
        {
            _gorevAtamaKomRepo.Create(gorevAtamaList);
            _gorevAtamaKomRepo.Save();
            AddKomisyonOnay(_mapper.Map<List<GorevAtamaModel>>(gorevAtamaList));
        }
        public Birlesim UpdateBirlesimGorevAtama(Guid birlesimId, int turAdedi)
        {
            var birlesim = _birlesimRepo.GetById(birlesimId);
            birlesim.ToplanmaDurumu = ToplanmaStatu.Planlandı;
            birlesim.TurAdedi = turAdedi;
            _birlesimRepo.Update(birlesim);
            _birlesimRepo.Save();
            return birlesim;
        }
        public IEnumerable<Stenograf> GetStenografIdList()
        {
            return _stenografRepo.Get().OrderBy(x => x.SiraNo).Select(x => new Stenograf { Id = x.Id, StenoGorevTuru = x.StenoGorevTuru, AdSoyad = x.AdSoyad, GrupId = x.GrupId });
        }
        public List<GorevAtamaModel> CreateStenoGorevDonguKomisyon(Guid birlesimId, Guid oturumId)
        {
           var result =_gorevAtamaKomRepo.Get(x => x.BirlesimId == birlesimId,includeProperties:"Stenograf").OrderBy(x => x.SatırNo).ToList();
            var stenoList = new List<GorevAtamaModel>();
            if (result != null && result.Count > 0)
            {
                var grpList = result.GroupBy(c => new { c.StenografId, c.Stenograf.GrupId }).Select(x => new { StenografId = x.Key.StenografId,GrupId =x.Key.GrupId });
                var gorevBitis = result.Where(x => x.GorevStatu != GorevStatu.Iptal).LastOrDefault().GorevBitisTarihi;
                var refSatırNo = result.LastOrDefault().SatırNo;
                int firsRec = 1;               
                foreach (var item in grpList)
                {
                    var newEntity = new GorevAtamaModel();
                    newEntity.BirlesimId = birlesimId;
                    newEntity.OturumId = oturumId;
                    newEntity.StenografId = item.StenografId;
                    newEntity.StenoSure = result.Where(x => x.StenografId == item.StenografId).LastOrDefault().StenoSure;
                    newEntity.GorevBasTarihi = gorevBitis;
                    newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.Value.AddMinutes(newEntity.StenoSure);
                    newEntity.SatırNo = refSatırNo + firsRec;
                    gorevBitis = newEntity.GorevBitisTarihi;
                    newEntity.StenoIzinTuru = GetStenoIzinByGorevBasTarih(item.StenografId, newEntity.GorevBasTarihi);
                    newEntity.GidenGrupMu = GidenGrupHesaplama(ToplanmaTuru.Komisyon, newEntity.StenoSure,(Guid)item.GrupId) != DateTime.MinValue &&
                    newEntity.GorevBasTarihi >= GidenGrupHesaplama(ToplanmaTuru.Komisyon, newEntity.StenoSure,(Guid)item.GrupId) ? true : false;
                    newEntity.GidenGrup = newEntity.GidenGrupMu ? "GidenGrup" : string.Empty;
                    stenoList.Add(newEntity);
                    firsRec++;
                }
            }
            return stenoList;
        }

        public List<GorevAtamaModel> CreateStenoGorevDonguGenelKurul(Guid birlesimId, Guid oturumId,StenoGorevTuru gorevTuru)
        {
            var result = _gorevAtamaGKRepo.Get(x => x.BirlesimId == birlesimId, includeProperties: "Stenograf").OrderBy(x => x.SatırNo).ToList();
            var stenoList = new List<GorevAtamaModel>();
            if (result != null && result.Count > 0)
            {
                var grpList = result.GroupBy(c => new { c.StenografId, c.Stenograf.GrupId }).Select(x => new { StenografId = x.Key.StenografId, GrupId = x.Key.GrupId });
                var gorevBitis = result.Where(x => x.GorevStatu != GorevStatu.Iptal).LastOrDefault().GorevBitisTarihi;
                var refSatırNo = result.LastOrDefault().SatırNo;
                int firsRec = 1;
                foreach (var item in grpList)
                {
                    var newEntity = new GorevAtamaModel();
                    newEntity.BirlesimId = birlesimId;
                    newEntity.OturumId = oturumId;
                    newEntity.StenografId = item.StenografId;
                    newEntity.StenoSure = result.Where(x => x.StenografId == item.StenografId).LastOrDefault().StenoSure;
                    newEntity.GorevBasTarihi = gorevBitis;
                    newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.Value.AddMinutes(newEntity.StenoSure);
                    newEntity.SatırNo = refSatırNo + firsRec;
                    gorevBitis = newEntity.GorevBitisTarihi;
                    newEntity.StenoIzinTuru = GetStenoIzinByGorevBasTarih(item.StenografId, newEntity.GorevBasTarihi);
                    newEntity.KomisyonAd = gorevTuru == StenoGorevTuru.Stenograf ? GetKomisyonMinMaxDate(item.StenografId, newEntity.GorevBasTarihi, newEntity.GorevBitisTarihi, newEntity.StenoSure) : null;
                    newEntity.GidenGrupMu = GidenGrupHesaplama(ToplanmaTuru.GenelKurul, newEntity.StenoSure,(Guid)item.GrupId) != DateTime.MinValue &&
                    newEntity.GorevBasTarihi >= GidenGrupHesaplama(ToplanmaTuru.GenelKurul, newEntity.StenoSure,(Guid)item.GrupId) ? true : false;
                    newEntity.GidenGrup = newEntity.GidenGrupMu ? "GidenGrup" : string.Empty;
                    stenoList.Add(newEntity);
                    firsRec++;
                }
            }
            return stenoList;
        }
        public List<GorevAtamaModel> AddStenoGorevAtamaKomisyon(IEnumerable<StenoKomisyonGrupModel> stenografIds, Guid birlesimId, Guid oturumId)
        {
            var result = _gorevAtamaKomRepo.Get(x => x.BirlesimId == birlesimId).OrderBy(x => x.SatırNo);
            var stenoList = new List<GorevAtamaModel>();
            if (result != null && result.Count() > 0)
            {
                _mapper.Map(result, stenoList);
                foreach (var steno in stenografIds)
                {
                    var grpListCnt = stenoList.GroupBy(c => new
                    {
                        c.StenografId,
                    }).Count();
                    int k = 0;
                    for (int i = 1; i <= stenoList.Count() / grpListCnt; i++)
                    {
                        var deger = (grpListCnt * i) + k;
                        var refSteno = stenoList.Where(x => x.SatırNo == deger).OrderBy(x => x.SatırNo).FirstOrDefault();
                        if (refSteno != null)
                        {
                            foreach (var item in stenoList.Where(x => x.SatırNo > refSteno.SatırNo).OrderBy(x => x.SatırNo))
                            {
                                item.GorevBasTarihi = item.GorevBasTarihi.Value.AddMinutes(refSteno.StenoSure);
                                item.GorevBitisTarihi = item.GorevBitisTarihi.Value.AddMinutes(refSteno.StenoSure);
                                item.SatırNo = item.SatırNo + 1;
                            }
                            var nwGrv = new GorevAtamaModel
                            {
                                SatırNo = deger + 1,
                                StenografId = steno.Id,
                                BirlesimId = birlesimId,
                                OturumId = oturumId,
                                StenoSure = refSteno.StenoSure,
                                GorevBasTarihi = refSteno.GorevBitisTarihi,
                                GorevBitisTarihi = refSteno.GorevBitisTarihi.Value.AddMinutes(refSteno.StenoSure)
                            };
                            stenoList.Add(nwGrv);
                            k++;
                        }
                    }
                }
                _gorevAtamaKomRepo.Delete(result);
                _gorevAtamaKomRepo.Save();
            }
            return stenoList ;
        }
        public List<GorevAtamaModel> GetGorevAtamaByBirlesimId(Guid birlesimId, ToplanmaTuru toplanmaTuru)
        {
            var model = new List<GorevAtamaModel>();
            if (toplanmaTuru == ToplanmaTuru.GenelKurul)
            {
                var result = _gorevAtamaGKRepo.Get(x => x.BirlesimId == birlesimId, includeProperties: "Stenograf").OrderBy(x => x.SatırNo).ToList();
                _mapper.Map(result, model);
                //model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaGKRepo.Get(x => x.BirlesimId == birlesimId ,includeProperties:"Stenograf").OrderBy(x=>x.SatırNo).ToList());
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaKomRepo.Get(x => x.BirlesimId == birlesimId, includeProperties: "Stenograf").OrderBy(x => x.SatırNo).ToList());
            }
            else if (toplanmaTuru == ToplanmaTuru.OzelToplanti)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaOzelRepo.Get(x => x.BirlesimId == birlesimId, includeProperties: "Stenograf").OrderBy(x => x.SatırNo).ToList());
            }

            return model;
        }
        public bool ActivateGidenGrupByGorevAtama(DurumStatu gidenGrupPasif, DateTime? gidenGrupSaat, DurumStatu uygula)
        {
            var conventionTypes = Enum.GetValues(typeof(ToplanmaTuru)).Cast<ToplanmaTuru>();
            foreach (var type in conventionTypes)
            {
                if (type.Equals(ToplanmaTuru.GenelKurul))
                {
                    var gkModel = _gorevAtamaGKRepo.Get(x => x.GorevStatu != GorevStatu.Iptal && x.GorevStatu != GorevStatu.Tamamlandı).OrderBy(x => x.SatırNo).ToList();
                    if (gkModel != null && gkModel.Any())
                    {
                        var extractedGK = new List<GorevAtamaGenelKurul>();
                        var gidenGrupTarih = uygula == DurumStatu.Hayır ? gidenGrupSaat.Value.AddMinutes(-60) : gidenGrupSaat;
                        extractedGK = gkModel.Where(p => p.GorevBasTarihi >= gidenGrupTarih).ToList();
                        if(gidenGrupPasif == DurumStatu.Evet)
                        {
                            extractedGK.AsParallel().ForAll((data) =>
                            {
                                data.GidenGrupMu = false;
                                data.GidenGrup = string.Empty;
                            });
                        }
                        else
                        {
                            extractedGK.AsParallel().ForAll((data) =>
                            {
                                data.GidenGrupMu = true;
                                data.GidenGrup = "GidenGrup";
                            });
                        }                    
                        _gorevAtamaGKRepo.Update(extractedGK);
                    }
                }
                else if (type.Equals(ToplanmaTuru.Komisyon))
                {
                    var commisionModel = _gorevAtamaKomRepo.Get(x => x.GorevStatu != GorevStatu.Iptal && x.GorevStatu != GorevStatu.Tamamlandı).OrderBy(x => x.SatırNo).ToList();
                    if (commisionModel != null && commisionModel.Any())
                    {
                        var extractedCommision = new List<GorevAtamaKomisyon>();
                        if (gidenGrupPasif == DurumStatu.Evet)
                        {
                            commisionModel.AsParallel().ForAll((data) =>
                            {
                                if (data.GorevBasTarihi >= (uygula == DurumStatu.Hayır ? gidenGrupSaat.Value.AddMinutes(-9 * data.StenoSure) : gidenGrupSaat))
                                {
                                    data.GidenGrupMu = false;
                                    data.GidenGrup = string.Empty;
                                    extractedCommision.Add(data);
                                }
                            });
                        }
                        else
                        {
                            commisionModel.AsParallel().ForAll((data) =>
                            {
                                if (data.GorevBasTarihi >= (uygula == DurumStatu.Hayır ? gidenGrupSaat.Value.AddMinutes(-9 * data.StenoSure) : gidenGrupSaat))
                                {
                                    data.GidenGrupMu = true;
                                    data.GidenGrup = "GidenGrup";
                                    extractedCommision.Add(data);
                                }
                            });
                        }                       
                        _gorevAtamaKomRepo.Update(extractedCommision);
                    }
                }
            }
            return true;
        }
        public async Task<List<GorevAtamaKomisyon>> ChangeSureStenografKomisyon(Guid birlesimId, int satırNo, double sure, bool digerAtamalarDahil = false)
        {
            var list = new List<GorevAtamaKomisyon>();
            try
            {
                var stenoGorev = _gorevAtamaKomRepo.Get(x => x.BirlesimId == birlesimId).OrderBy(x => x.SatırNo).ToList();
                //var modelList = _mapper.Map<List<GorevAtamaModel>>(stenoGorev);
                var stenoGorevLine = stenoGorev.Where(x => x.SatırNo >= satırNo);
                if (stenoGorevLine != null && stenoGorevLine.Count() > 0)
                {
                    var gorevBas = stenoGorevLine.FirstOrDefault().GorevBasTarihi;
                    var stenoSure = stenoGorevLine.FirstOrDefault().StenoSure;

                    foreach (var item in stenoGorevLine)
                    {
                        item.StenoSure = sure;
                        item.GorevBasTarihi = gorevBas;
                        item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(sure);
                        gorevBas = item.GorevBitisTarihi;
                        sure = digerAtamalarDahil ? item.StenoSure : stenoSure;
                        list.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return list;
        }
        public void ChangeOrderStenografKomisyon(Guid kaynakBirlesimId, Guid kaynakStenoId, Guid hedefBirlesimId, Guid hedefStenoId)
        {

            if (kaynakBirlesimId != hedefBirlesimId)
            {
                var stenoGrevHedef = _gorevAtamaKomRepo.Get(x => x.BirlesimId == hedefBirlesimId).OrderBy(x => x.SatırNo).ToList();
                
                if (stenoGrevHedef != null && stenoGrevHedef.Count() > 0)
                {
                    var minStenoGorev = stenoGrevHedef.Where(x => x.StenografId == hedefStenoId);

                    foreach (var item in minStenoGorev)
                    {
                        var newEntity = new GorevAtamaKomisyon();
                        newEntity.BirlesimId = hedefBirlesimId;
                        newEntity.OturumId = item.OturumId;
                        newEntity.StenografId = kaynakStenoId;
                        newEntity.GorevBasTarihi = item.GorevBasTarihi;
                        newEntity.GorevBitisTarihi = item.GorevBitisTarihi;
                        newEntity.StenoSure = item.StenoSure;
                        newEntity.SatırNo = item.SatırNo;
                        _gorevAtamaKomRepo.Create(newEntity);
                        _gorevAtamaKomRepo.Save();

                        var hedefStenoGorev = stenoGrevHedef.Where(x => x.GorevBasTarihi >= item.GorevBasTarihi);
                        foreach (var hedef in hedefStenoGorev)
                        {
                            hedef.GorevBasTarihi = hedef.GorevBasTarihi.Value.AddMinutes(hedef.StenoSure);
                            hedef.GorevBitisTarihi = hedef.GorevBasTarihi.Value.AddMinutes(hedef.StenoSure);
                            _gorevAtamaKomRepo.Update(hedef);
                            _gorevAtamaKomRepo.Save();
                        }

                        var stenoGrevHedefReOrder = _gorevAtamaKomRepo.Get(x => x.BirlesimId == hedefBirlesimId &&
                        ( x.SatırNo>= item.SatırNo && x.StenografId != kaynakStenoId)).OrderBy(x => x.SatırNo).ToList();
                        stenoGrevHedefReOrder.ForEach(p =>
                        {
                            p.SatırNo += 1;
                            _gorevAtamaKomRepo.Update(p);
                            _gorevAtamaKomRepo.Save();
                        }
                        );

                    }                                      

                    var kaynak = _gorevAtamaKomRepo.Get(x => x.BirlesimId == kaynakBirlesimId && x.StenografId == kaynakStenoId);
                    if (kaynak != null)
                    {
                        _gorevAtamaKomRepo.Delete(kaynak);
                        _gorevAtamaKomRepo.Save();
                    }
                }
            }
            else
            {
                var stenoGrevHedef = _gorevAtamaKomRepo.Get(x => x.BirlesimId == hedefBirlesimId).OrderBy(x => x.SatırNo);
                if (stenoGrevHedef != null && stenoGrevHedef.Count() > 0)
                {
                    var stenoList = new List<GorevAtamaKomisyon>();
                    var hedefSteno = stenoGrevHedef.Where(x => x.StenografId == hedefStenoId).OrderBy(x => x.SatırNo).ToArray();
                    var kaynakSteno = stenoGrevHedef.Where(x => x.StenografId == kaynakStenoId).OrderBy(x => x.SatırNo).ToArray();

                    for (int i = 0; i < hedefSteno.Count(); i++)
                    {
                        var hedefGorevBasTarihi = hedefSteno[i].GorevBasTarihi;
                        var hedefGorevBitisTarihi = hedefSteno[i].GorevBitisTarihi;
                        var hedefSure = hedefSteno[i].StenoSure;
                        var hedefSatırNo = hedefSteno[i].SatırNo;

                        hedefSteno[i].GorevBasTarihi = kaynakSteno[i].GorevBasTarihi;
                        hedefSteno[i].GorevBitisTarihi = kaynakSteno[i].GorevBitisTarihi;
                        hedefSteno[i].StenoSure = kaynakSteno[i].StenoSure;
                        hedefSteno[i].SatırNo = kaynakSteno[i].SatırNo;
                        stenoList.Add(hedefSteno[i]);

                        kaynakSteno[i].GorevBasTarihi = hedefGorevBasTarihi;
                        kaynakSteno[i].GorevBitisTarihi = hedefGorevBitisTarihi;
                        kaynakSteno[i].StenoSure = hedefSure;
                        kaynakSteno[i].SatırNo = hedefSatırNo;
                        stenoList.Add(kaynakSteno[i]);
                    }
                    _gorevAtamaKomRepo.Update(stenoList);
                }
            }
        }
        public void DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, Guid stenografId, ToplanmaTuru toplanmaTuru)
        {
            var birlesim = _birlesimRepo.GetById(birlesimId);
            if (birlesim != null && (birlesim.ToplanmaDurumu == ToplanmaStatu.Oluşturuldu || birlesim.ToplanmaDurumu == ToplanmaStatu.Planlandı))
            {
                var gorevler = GetGorevAtamaByBirlesimId(birlesimId, toplanmaTuru);
                var modelList = _mapper.Map<List<GorevAtamaModel>>(gorevler);
                var gorevSteno = gorevler.Where(x => x.StenografId == stenografId);
                if (gorevSteno != null && gorevSteno.Count() > 0)
                {
                    DeleteGorevAtama(gorevSteno, toplanmaTuru);

                    gorevler = gorevler.Where(x => x.StenografId != stenografId).OrderBy(x => x.GorevBasTarihi).ToList();
                    if (gorevler != null && gorevler.Count() > 0)
                    {
                        var minDate = birlesim.BaslangicTarihi;
                        var updateList = new List<GorevAtamaModel>();
                        foreach (var item in gorevler)
                        {
                            item.GorevBasTarihi = minDate.Value;
                            item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(item.StenoSure);
                            minDate = item.GorevBitisTarihi;
                            updateList.Add(item);
                        }
                        UpdateGorevAtama(updateList, toplanmaTuru);
                    }
                    else
                    {
                        var oturum = _oturumRepo.Get(x => x.BirlesimId == birlesimId && x.BitisTarihi == null).FirstOrDefault();
                        if (oturum != null)
                        {
                            _oturumRepo.Delete(oturum);
                            _oturumRepo.Save();
                        }

                        if (birlesim != null)
                        {
                            birlesim.ToplanmaDurumu = ToplanmaStatu.Oluşturuldu;
                            _birlesimRepo.Update(birlesim);
                            _birlesimRepo.Save();
                        }
                    }
                }
            }
        }
        public void UpdateGorevAtama(IEnumerable<GorevAtamaModel> model, ToplanmaTuru toplanmaTuru)
        {            
            if (toplanmaTuru == ToplanmaTuru.GenelKurul)
            {
                var entity = new List<GorevAtamaGenelKurul>();
                _mapper.Map(model, entity);
                _gorevAtamaGKRepo.Update(entity);
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                var entity = new List<GorevAtamaKomisyon>();
                _mapper.Map(model, entity);
                _gorevAtamaKomRepo.Update(entity);
            }
            else if (toplanmaTuru == ToplanmaTuru.OzelToplanti)
            {
                var entity = new List<GorevAtamaOzelToplanma>();
                _mapper.Map(model, entity);
                _gorevAtamaOzelRepo.Update(entity);
            }
        }
        private void DeleteGorevAtama(IEnumerable<GorevAtamaModel> model, ToplanmaTuru toplanmaTuru)
        {
            if (toplanmaTuru == ToplanmaTuru.GenelKurul)
            {
                var entity = Mapper.Map<GorevAtamaGenelKurul>(model);
                _gorevAtamaGKRepo.Delete(entity);
                _gorevAtamaGKRepo.Save();
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                var entity = Mapper.Map<GorevAtamaKomisyon>(model);
                _gorevAtamaKomRepo.Delete(entity);
                _gorevAtamaKomRepo.Save();
            }
            else if (toplanmaTuru == ToplanmaTuru.OzelToplanti)
            {
                var entity = Mapper.Map<GorevAtamaOzelToplanma>(model);
                _gorevAtamaOzelRepo.Delete(entity);
                _gorevAtamaOzelRepo.Save();
            }
        }
        public void UpdateBirlesimStenoGorevDevamEtme(Guid birlesimId, DateTime basTarih, DateTime oturumKapanmaTarihi, Guid oturumId, ToplanmaTuru toplanmaTuru)
        {
            var model = GetGorevAtamaByBirlesimId(birlesimId, toplanmaTuru).Where(x => oturumKapanmaTarihi <= x.GorevBasTarihi);
            if (model != null && model.Count() > 0)
            {
                if (toplanmaTuru == ToplanmaTuru.GenelKurul)
                {
                    SetToplanmaDevamEtme(model.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf).ToList(), basTarih, toplanmaTuru, oturumId);
                    SetToplanmaDevamEtme(model.Where(x => x.StenoGorevTuru == StenoGorevTuru.Uzman).ToList(), basTarih, toplanmaTuru, oturumId);
                }
                else
                {
                    SetToplanmaDevamEtme(model.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf).ToList(), basTarih, toplanmaTuru, oturumId);
                }
            }
        }
        public void UpdateStenoGorevTamamla(Guid birlesimId, ToplanmaTuru toplanmaTuru, int satırNo)
        {

            var result = GetGorevAtamaByBirlesimId(birlesimId, toplanmaTuru);
            if (result != null && result.Count() > 0)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {

                        result.Where(x => x.SatırNo <= satırNo && x.GorevStatu != GorevStatu.Iptal).ToList().ForEach(x => x.GorevStatu = GorevStatu.Tamamlandı);

                        UpdateGorevAtama(result, toplanmaTuru);
                        //
                        var birlesimNotCompletedAtamas = result.Where(x => x.SatırNo > satırNo).OrderBy(x => x.GorevBasTarihi).ToList();
                        ControlAndUpdateNotcompletedBirlesimAtama(toplanmaTuru, birlesimNotCompletedAtamas);
                        //
                        SaveStenoStatistics(result, satırNo, toplanmaTuru, birlesimId);

                        scope.Complete();
                    }
                    catch (Exception exception)
                    {
                        scope.Dispose();

                        throw exception;
                    }
                }

            }

        }

        private void ControlAndUpdateNotcompletedBirlesimAtama(ToplanmaTuru toplanmaTuru, List<GorevAtamaModel> birlesimNotCompletedAtamas)
        {
            if (toplanmaTuru.Equals(ToplanmaTuru.GenelKurul))
            {
                if (birlesimNotCompletedAtamas.Any())
                {
                    var stenograflar = _stenografRepo.Get();
                    var exractedStenos = stenograflar.Where(stenoRepoPredict => !birlesimNotCompletedAtamas.Any(kalanPredict => kalanPredict.StenografId == stenoRepoPredict.Id))
                        .OrderBy(x => x.SiraNo).ToList();

                    var notCompletedBirlesimStenos = stenograflar.Where(stenoRepoPredict => birlesimNotCompletedAtamas.Any(kalanPredict => kalanPredict.StenografId == stenoRepoPredict.Id)).OrderBy(x => x.SiraNo).ToList();

                    UpdateStenographOrders(exractedStenos, notCompletedBirlesimStenos);
                }
                else
                {
                    var stenograflar = _stenografRepo.Get().ToList();
                    stenograflar.ForEach(x =>
                    {
                        x.BirlesimSıraNo = x.SiraNo;
                    });
                }

            }
        }

        private bool UpdateStenographOrders(List<Stenograf> exractedStenos, List<Stenograf> notCompletedBirlesimStenos)
        {
            List<Stenograf> reOrderedStenographs = new List<Stenograf>();
            int restartedIndex = 0;

            //birlesimde görevini tamamlamayan stenografların sıraları güncelleniyor
            notCompletedBirlesimStenos.ForEach(x =>
            {
                x.BirlesimSıraNo = restartedIndex += 1;
            });

            reOrderedStenographs.AddRange(notCompletedBirlesimStenos);

            //birlesimde görev almayan stenografların sıraları güncelleniyor
            exractedStenos.ForEach(x =>
            {
                x.BirlesimSıraNo = restartedIndex += 1;
            });
            reOrderedStenographs.AddRange(exractedStenos);

            _stenografRepo.Update(reOrderedStenographs, CurrentUser.Id);
            return _stenografRepo.Save();
        }

        public void SaveStenoStatistics(List<GorevAtamaModel> gorevList, int satırNo, ToplanmaTuru toplanmaTuru, Guid birlesimId)
        {

            var statistics = gorevList.Where(x => x.SatırNo <= satırNo && x.GorevStatu != GorevStatu.Iptal).GroupBy(x => x.StenografId);
            statistics.ToList().ForEach(x =>
            {
                var steno = _stenografRepo.GetById(x.FirstOrDefault().StenografId);
                StenoToplamGenelSureModel stenoToplamGenelSure = new StenoToplamGenelSureModel();
                stenoToplamGenelSure.BirlesimId = birlesimId;

                //Toplantı türüne göre birlesim adı belirleme
                if (toplanmaTuru == ToplanmaTuru.GenelKurul)
                {
                    var birlesim = _birlesimRepo.GetById(birlesimId);
                    stenoToplamGenelSure.BirlesimAd = birlesim.BirlesimNo;
                    stenoToplamGenelSure.YasamaId = birlesim.YasamaId;
                }
                else if (toplanmaTuru == ToplanmaTuru.Komisyon)
                {
                    var komisyon = _birlesimRepo.Get(x => x.Id == birlesimId, includeProperties: "Komisyon");
                    stenoToplamGenelSure.BirlesimAd = komisyon.FirstOrDefault().Komisyon.Ad;
                    stenoToplamGenelSure.YasamaId = komisyon.FirstOrDefault().YasamaId;
                }
                else
                {
                    var ozelToplanma = _birlesimRepo.Get(x => x.Id == birlesimId, includeProperties: "OzelToplanma");
                    stenoToplamGenelSure.BirlesimAd = ozelToplanma.FirstOrDefault().OzelToplanma.Ad;
                    stenoToplamGenelSure.YasamaId = ozelToplanma.FirstOrDefault().YasamaId;
                }

                stenoToplamGenelSure.ToplantiTur = toplanmaTuru;
                stenoToplamGenelSure.GroupId = (Guid)steno.GrupId;
                stenoToplamGenelSure.StenografId = steno.Id;
                stenoToplamGenelSure.StenografAd = steno.AdSoyad;
                stenoToplamGenelSure.Tarih = x.FirstOrDefault().GorevBasTarihi.Value;
                stenoToplamGenelSure.Sure = x.Select(t => t.StenoSure).Sum();

                _stenoToplamGenelSureRepo.Create(_mapper.Map<StenoToplamGenelSure>(stenoToplamGenelSure));
                _stenoToplamGenelSureRepo.Save();
            });

        }
        public void UpdateBirlesimStenoGorevBaslama(Guid birlesimId, DateTime basTarih, ToplanmaTuru toplanmaTuru)
        {
            var model = GetGorevAtamaByBirlesimId(birlesimId, toplanmaTuru);
            if (model != null && model.Count() > 0)
            {
                if (toplanmaTuru == ToplanmaTuru.GenelKurul)
                {
                    SetToplanmaBaslama(model.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf).ToList(), birlesimId, basTarih, toplanmaTuru);
                    SetToplanmaBaslamaUzman(model.Where(x => x.StenoGorevTuru == StenoGorevTuru.Uzman).ToList(), birlesimId, basTarih, toplanmaTuru);
                }
                else
                {
                    SetToplanmaBaslama(model.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf).ToList(), birlesimId, basTarih, toplanmaTuru);
                }
            }
        }
        private void SetToplanmaBaslamaUzman(List<GorevAtamaModel> result, Guid birlesimId, DateTime basTarih, ToplanmaTuru toplanmaTuru)
        {
            var updateList = new List<GorevAtamaModel>();
            var resultFirst = result.Where(x => x.GorevStatu != GorevStatu.Iptal).FirstOrDefault();
            var mindate = resultFirst.GorevBasTarihi.Value;
            var gorevId = resultFirst.Id;
            var mindateDiff = basTarih.Subtract(result.Min(x => x.GorevBasTarihi).Value).TotalMinutes;

            var modResult = result.Select(x => x.StenoSure);
            if (modResult != null && modResult.Count() > 0)
            {
                var sonuc = modResult.FirstOrDefault() - (mindateDiff % modResult.FirstOrDefault());
                var minStenoResult = result.Where(x => x.GorevBasTarihi == mindate).FirstOrDefault();
                if (minStenoResult.GorevStatu == GorevStatu.Iptal)
                {
                    updateList.Add(minStenoResult);
                    minStenoResult = result.Where(x => x.GorevBasTarihi == mindate && x.GorevStatu != GorevStatu.Iptal).FirstOrDefault();
                }

                minStenoResult.GorevBasTarihi = mindate.AddMinutes(mindateDiff);
                minStenoResult.GorevBitisTarihi = minStenoResult.GorevBasTarihi.Value.AddMinutes(sonuc);
                var gorevBasPlan = minStenoResult.GorevBasTarihi.Value.AddMinutes(-(mindateDiff % modResult.FirstOrDefault()));
                var remain = gorevBasPlan.Subtract(mindate).TotalMinutes;
                updateList.Add(minStenoResult);

                var remainResult = result.Where(x => x.Id != gorevId);
                foreach (var item in remainResult)
                {
                    item.GorevBasTarihi = item.GorevBasTarihi.Value.AddMinutes(remain);
                    item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(modResult.FirstOrDefault());
                    updateList.Add(item);
                }
                UpdateGorevAtama(updateList, toplanmaTuru);
            }
        }
        private void SetToplanmaBaslama(List<GorevAtamaModel> result, Guid birlesimId, DateTime basTarih, ToplanmaTuru toplanmaTuru)
        {
            var updateList = new List<GorevAtamaModel>();
            var resultFirst = result.Where(x => x.GorevStatu != GorevStatu.Iptal).FirstOrDefault();
            var mindate = resultFirst.GorevBasTarihi.Value;
            var gorevId = resultFirst.Id;
            var mindateDiff = basTarih.Subtract(result.Min(x => x.GorevBasTarihi).Value).TotalMinutes;

            UpdateBirlesimBaslamaSaat(birlesimId, mindateDiff);

            var modResult = result.Select(x => x.StenoSure);
            if (modResult != null && modResult.Count() > 0)
            {
                var sonuc = modResult.FirstOrDefault() - (mindateDiff % modResult.FirstOrDefault());
                var minStenoResult = result.Where(x => x.GorevBasTarihi == mindate).FirstOrDefault();
                if (minStenoResult.GorevStatu == GorevStatu.Iptal)
                {
                    minStenoResult.GorevBasTarihi = mindate.AddMinutes(mindateDiff);
                    minStenoResult.GorevBitisTarihi = minStenoResult.GorevBasTarihi.Value.AddMinutes(sonuc);
                    updateList.Add(minStenoResult);
                    minStenoResult = result.Where(x => x.GorevBasTarihi == mindate && x.GorevStatu != GorevStatu.Iptal).FirstOrDefault();
                }

                minStenoResult.GorevBasTarihi = mindate.AddMinutes(mindateDiff);
                minStenoResult.GorevBitisTarihi = minStenoResult.GorevBasTarihi.Value.AddMinutes(sonuc);
                var gorevBasPlan = minStenoResult.GorevBasTarihi.Value.AddMinutes(-(mindateDiff % modResult.FirstOrDefault()));
                var remain = gorevBasPlan.Subtract(mindate).TotalMinutes;
                updateList.Add(minStenoResult);

                var remainResult = result.Where(x => !updateList.Select(x => x.Id).Contains(x.Id));
                foreach (var item in remainResult)
                {
                    item.GorevBasTarihi = item.GorevBasTarihi.Value.AddMinutes(remain);
                    item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(modResult.FirstOrDefault());
                    updateList.Add(item);
                }
                UpdateGorevAtama(updateList, toplanmaTuru);
            }
        }
        private void SetToplanmaDevamEtme(List<GorevAtamaModel> resultList, DateTime basTarih, ToplanmaTuru toplanmaTuru, Guid oturumId)
        {
            var updateList = new List<GorevAtamaModel>();
            if (resultList != null && resultList.Count() > 0)
            {
                var result = resultList.FirstOrDefault();
                var ilkGorevBitisTarihi = result.GorevBasTarihi.Value;
                var mindateDiff = basTarih.Subtract(result.GorevBasTarihi.Value).TotalMinutes;
                var modResult = result.StenoSure;
                if (modResult != null && modResult > 0)
                {
                    var sonuc = modResult - (mindateDiff % modResult);
                    result.GorevBasTarihi = basTarih;
                    result.GorevBitisTarihi = result.GorevBasTarihi.Value.AddMinutes(sonuc);
                    result.OturumId = oturumId;
                    updateList.Add(result);

                    var remain = result.GorevBitisTarihi.Value.Subtract(ilkGorevBitisTarihi).TotalMinutes;
                    var remainResult = resultList.Where(x => x.Id != result.Id);
                    foreach (var item in remainResult)
                    {
                        item.GorevBasTarihi = item.GorevBasTarihi.Value.AddMinutes(remain);
                        item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(modResult);
                        item.OturumId = oturumId;
                        updateList.Add(result);
                    }
                    UpdateGorevAtama(updateList, toplanmaTuru);
                }
            }
        }
        private void UpdateBirlesimBaslamaSaat(Guid birlesimId, double mindateDiff)
        {
            var birlesim = _birlesimRepo.GetById(birlesimId);
            if (birlesim != null)
            {
                birlesim.BaslangicTarihi = birlesim.BaslangicTarihi.Value.AddMinutes(mindateDiff);
                birlesim.ToplanmaDurumu = ToplanmaStatu.DevamEdiyor;
                _birlesimRepo.Update(birlesim);
                _birlesimRepo.Save();
            }
        }
        public List<GorevAtamaModel> UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId, ToplanmaTuru toplanmaTuru,StenoGorevTuru gorevTuru)
        {
            var allRresult = UpdateGorevDurumModelList(birlesimId, toplanmaTuru, gorevTuru);
            if (allRresult != null && allRresult.Count() > 0)
            {
                var resultPlan = allRresult.Where(x => x.StenografId == stenoId && x.GorevStatu == GorevStatu.Planlandı);
                if (resultPlan != null && resultPlan.Count() > 0)
                {
                    allRresult.Where(x => x.StenografId == stenoId && x.GorevStatu == GorevStatu.Planlandı).ToList().ForEach(x => x.GorevStatu = GorevStatu.Iptal);
                }
                else
                {
                    var resultIptal = allRresult.Where(x => x.StenografId == stenoId && x.GorevStatu == GorevStatu.Iptal);
                    if (resultPlan != null && resultPlan.Count() > 0)
                        allRresult.Where(x => x.StenografId == stenoId && x.GorevStatu == GorevStatu.Iptal).ToList().ForEach(x => x.GorevStatu = GorevStatu.Planlandı);
                }
            }
            return allRresult;
        }
        private void AddKomisyonOnay(List<GorevAtamaModel> modelList)
        {
            if (modelList != null && modelList.Count > 0)
            {
                var komisyonOnay = _komisyonOnayRepo.Get(x => x.BirlesimId == modelList.FirstOrDefault().BirlesimId);
                if (komisyonOnay != null && komisyonOnay.Count() > 0)
                {
                    return;
                }
                modelList.ForEach(x => x.Id = Guid.NewGuid());
                var komisyonOnayList = _mapper.Map<List<GorevAtamaKomisyonOnay>>(modelList);
                _komisyonOnayRepo.Create(komisyonOnayList);
                _komisyonOnayRepo.Save();
            }
        }
        private List<GorevAtamaModel> UpdateGorevDurumModelList(Guid birlesimId, ToplanmaTuru toplanmaTuru, StenoGorevTuru gorevTuru)
        {
            var result = new List<GorevAtamaModel>();
            if (toplanmaTuru == ToplanmaTuru.GenelKurul)
            {
                result = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaGKRepo.Get(x => x.BirlesimId == birlesimId && x.StenoGorevTuru == StenoGorevTuru.Stenograf && x.GorevBasTarihi >= DateTime.Now)); 
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                result = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaKomRepo.Get(x => x.BirlesimId == birlesimId &&  x.StenoGorevTuru == StenoGorevTuru.Stenograf && x.GorevBasTarihi >= DateTime.Now));
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                result = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaOzelRepo.Get(x => x.BirlesimId == birlesimId && x.StenoGorevTuru == StenoGorevTuru.Stenograf && x.GorevBasTarihi >= DateTime.Now));
            }
            return result;
        }
        public List<GorevAtamaModel> UpdateGorevDurumById(Guid id, Guid birlesimId, ToplanmaTuru toplanmaTuru,StenoGorevTuru gorevTuru)
        {
            var result = UpdateGorevDurumModelList(birlesimId, toplanmaTuru, gorevTuru);
            if (result != null && result.Count > 0)
            {
                var detay = result.Where(x => x.Id == id);
                if (detay != null && detay.Count() > 0)
                {
                    if (detay.FirstOrDefault().GorevStatu == GorevStatu.Iptal)
                        result.Where(x => x.Id == id).FirstOrDefault().GorevStatu = GorevStatu.Planlandı;
                    else if (detay.FirstOrDefault().GorevStatu == GorevStatu.Planlandı)
                        result.Where(x => x.Id == id).FirstOrDefault().GorevStatu = GorevStatu.Iptal;
                }
            }
            return result;
        }
        public IEnumerable<GorevAtamaKomisyon> GetAssignedStenoByBirlesimId(Guid birlesimId)
        {
            return _gorevAtamaKomRepo.Get(x => x.BirlesimId == birlesimId, includeProperties: "Stenograf,Birlesim");
        }
        public IzınTuru GetStenoIzinByGorevBasTarih(Guid stenoId, DateTime? gorevBasTarih)
        {
            var izinTur = IzınTuru.Bulunmuyor;
            var result = _stenoIzinRepo.Get(x => x.StenografId == stenoId && x.BaslangicTarihi <= gorevBasTarih && x.BitisTarihi >= gorevBasTarih).Select(x => x.IzinTuru);
            if (result.Any())
                izinTur = result.FirstOrDefault();
            return izinTur;
        }
        public GrupDetay GetGidenGrup(Guid grupId)
        {
            return _grupDetayRepo.Get(x => x.GrupId == grupId && x.GidenGrupPasif == DurumStatu.Hayır && x.GidenGrupSaat.HasValue && x.GidenGrupSaat.Value.Date == DateTime.Now.Date).FirstOrDefault();
        }
        public string GetKomisyonMinMaxDate(Guid stenoId, DateTime? gorevBasTarih, DateTime? gorevBitisTarih, double sure)
        {
            var result = _gorevAtamaKomRepo.Get(x => x.StenografId == stenoId && x.GorevStatu != GorevStatu.Iptal && x.GorevStatu != GorevStatu.Tamamlandı &&
                                                x.Birlesim.ToplanmaDurumu != ToplanmaStatu.Iptal &&
                                                x.Birlesim.ToplanmaDurumu != ToplanmaStatu.Tamamlandı, includeProperties: "Birlesim.Komisyon")
                                                .GroupBy(x => new { StenoId = x.StenografId, BirlesimId = x.BirlesimId })
                                                .Select(x => new
                                                {
                                                    MinDate = x.Min(z => z.GorevBasTarihi).Value.AddMinutes(-60),
                                                    MaxDate = x.Max(z => z.GorevBitisTarihi).Value.AddMinutes(9 * sure),
                                                    KomisyonAd = x.Max(z => z.Birlesim.Komisyon.Ad)
                                                });
            var str = result.Where(x => gorevBasTarih >= x.MinDate && gorevBitisTarih <= x.MaxDate).ToList();
            return str != null && str.Count() > 0 ? str.FirstOrDefault().KomisyonAd : null;
        }
        public void ApproveStenografKomisyon()
        {
            var result = _gorevAtamaKomRepo.Get(x => !x.OnayDurumu && x.Birlesim.ToplanmaDurumu != ToplanmaStatu.Iptal && x.Birlesim.ToplanmaDurumu != ToplanmaStatu.Tamamlandı).OrderBy(x => x.SatırNo).ToList();
            if (result != null && result.Count() > 0)
            {
                result.ForEach(x => x.OnayDurumu = true);
                _gorevAtamaKomRepo.Update(result);

                var onaylar = _komisyonOnayRepo.GetAll();
                if (onaylar != null)
                {
                    _komisyonOnayRepo.Delete(onaylar);
                    _komisyonOnayRepo.Save();

                    var modelList = new List<GorevAtamaModel>();
                    _mapper.Map(result, modelList);
                    _komisyonOnayRepo.Create(_mapper.Map<List<GorevAtamaKomisyonOnay>>(modelList));
                    _komisyonOnayRepo.Save();
                }
            }
        }
        public void CancelStenografKomisyon()
        {
            var result = _gorevAtamaKomRepo.Get(x => !x.OnayDurumu && x.Birlesim.ToplanmaDurumu != ToplanmaStatu.Iptal && x.Birlesim.ToplanmaDurumu != ToplanmaStatu.Tamamlandı).OrderBy(x => x.SatırNo).ToList();
            if (result != null && result.Count() > 0)
            {
                result.ForEach(x => x.IsDeleted = true);
                _gorevAtamaKomRepo.Update(result);

                var onaylar = _komisyonOnayRepo.GetAll().OrderBy(x => x.SatırNo).ToList();
                if (onaylar != null)
                {
                    var onayResult = _mapper.Map<List<GorevAtamaModel>>(onaylar);
                    onaylar.ForEach(x => x.OnayDurumu = true);
                    var entityList = _mapper.Map<List<GorevAtamaKomisyon>>(onayResult);
                    _gorevAtamaKomRepo.Create(entityList);
                    _gorevAtamaKomRepo.Save();
                }
            }
        }
        public DateTime GidenGrupHesaplama(ToplanmaTuru toplanmaTuru, double sure,Guid grupId)
        {
            var gidenGrupSaat = DateTime.MinValue;
            var gidenTarihResult = GetGidenGrup(grupId);
            if (gidenTarihResult != null)
            {
                if (gidenTarihResult.GidenGrupSaatUygula == DurumStatu.Evet)
                {
                    gidenGrupSaat = gidenTarihResult.GidenGrupSaat.Value;
                }
                else
                {
                    gidenGrupSaat = toplanmaTuru == ToplanmaTuru.GenelKurul ? gidenTarihResult.GidenGrupSaat.Value.AddMinutes(-60) : gidenTarihResult.GidenGrupSaat.Value.AddMinutes(-9 * sure);
                }
            }
            return gidenGrupSaat;
        }
        #region kapatıldı, şimdilik,açılabilir
        #endregion
    }
}