using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;
using TTBS.Core.Interfaces;
using TTBS.Models;
using TTBS.MongoDB;

namespace TTBS.Services
{
    public interface IGorevAtamaService
    {
        Birlesim CreateBirlesim(Birlesim birlesim);
        Guid CreateOturum(Oturum Oturum);
        void CreateStenoAtamaGK(List<GorevAtamaGenelKurul> gorevAtamaList);
        void CreateStenoAtamaKom(List<GorevAtamaKomisyon> gorevAtamaList);
        Birlesim UpdateBirlesimGorevAtama(Guid birlesimId,int turAdedi);
        void CreateBirlesimKomisyonRelation(Guid id, Guid komisyonId, Guid? altKomisyonId);
        void CreateBirlesimOzelToplanmaRelation(Guid id, Guid ozelToplanmaId);
        List<Stenograf> GetStenografIdList(DateTime gorevBasTarih);
        void AddStenoGorevAtamaKomisyon(List<Guid> stenografIds, string birlesimId, string oturumId);
        void CreateStenoGorevDonguEkle(string birlesimId, string oturumId);
        List<GorevAtamaModel> GetGorevAtamaByBirlesimId(Guid birlesimId, ToplanmaTuru toplanmaTuru);
        void ChangeOrderStenografKomisyon(string kaynakBirlesimId, Dictionary<string, string> kaynakStenoList, string hedefBirlesimId, Dictionary<string, string> hedefStenografId);
        void ChangeSureStenografKomisyon(string birlesimId, int satırNo, double sure, bool digerAtamalarDahil = false);
        IEnumerable<Stenograf> GetStenografIdList();
        void DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, Guid stenografId, ToplanmaTuru toplanmaTuru);
        void UpdateBirlesimStenoGorevBaslama(Guid birlesimId, DateTime basTarih, StenoGorevTuru stenoGorevTur, ToplanmaTuru toplanmaTuru);
        void UpdateBirlesimStenoGorevDevamEtme(Guid birlesimId, DateTime basTarih, StenoGorevTuru stenoGorevTuru, DateTime oturumKapanmaTarihi, Guid oturumId, ToplanmaTuru toplanmaTuru);
        void UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId, ToplanmaTuru toplanmaTuru);
        void UpdateGorevDurumById(Guid id, ToplanmaTuru toplanmaTuru);
        void UpdateStenoGorevTamamla(Guid birlesimId, StenoGorevTuru stenoGorevTur, ToplanmaTuru toplanmaTuru);
    }
    public class GorevAtamaService : BaseService, IGorevAtamaService
    {
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<GorevAtamaGenelKurul> _gorevAtamaGKRepo;
        private IRepository<GorevAtamaKomisyon> _gorevAtamaKomRepo;
        private IRepository<GorevAtamaOzelToplanma> _gorevAtamaOzelRepo;
        private IRepository<BirlesimKomisyon> _birlesimKomisyonRepo;
        private IRepository<BirlesimOzelToplanma> _birlesimOzeToplanmaRepo;
        private readonly IGorevAtamaGKMBusiness _gorevAtamaGKMRepo;
        private readonly IGorevAtamaKomMBusiness _gorevAtamaKomMRepo;
        private IRepository<Stenograf> _stenografRepo;
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<Oturum> _oturumRepo;
        public readonly IMapper _mapper;

        public GorevAtamaService(IRepository<Birlesim> birlesimRepo,
                                 IRepository<GorevAtamaGenelKurul> gorevAtamaGKRepo,
                                 IRepository<GorevAtamaKomisyon> gorevAtamaKomRepo,
                                 IRepository<BirlesimKomisyon> birlesimKomisyonRepo,
                                 IRepository<BirlesimOzelToplanma> birlesimOzeToplanmaRepo,
                                 IRepository<GorevAtamaOzelToplanma> gorevAtamaOzelRepo,
                                 IRepository<Stenograf> stenografRepo,
                                 IRepository<StenoIzin> stenoIzinRepo,
                                 IRepository<Oturum> oturumRepo,
                                 IMapper mapper,
                                 IServiceProvider provider) : base(provider)
        {
            _birlesimRepo=birlesimRepo;
            _gorevAtamaGKRepo = gorevAtamaGKRepo;
            _gorevAtamaKomRepo = gorevAtamaKomRepo;
            _birlesimKomisyonRepo = birlesimKomisyonRepo;
            _birlesimOzeToplanmaRepo = birlesimOzeToplanmaRepo;
            _birlesimOzeToplanmaRepo = birlesimOzeToplanmaRepo;
            _stenografRepo = stenografRepo;
            _oturumRepo = oturumRepo;
            _stenoIzinRepo = stenoIzinRepo;
            _gorevAtamaOzelRepo = gorevAtamaOzelRepo;
            _mapper = mapper;
        }
        public Birlesim CreateBirlesim(Birlesim birlesim)
        {
            _birlesimRepo.Create(birlesim, CurrentUser.Id);
            _birlesimRepo.Save();

            return birlesim;  
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
             //var result = _gorevAtamaGKMRepo.AddRangeAsync(gorevAtamaGKMongoList);
        }
        public void CreateStenoAtamaKom(List<GorevAtamaKomisyon> gorevAtamaList)
        {
            _gorevAtamaKomRepo.Create(gorevAtamaList);
            _gorevAtamaKomRepo.Save();
            //var result = _gorevAtamaKomMRepo.AddRangeAsync(gorevAtamaMongoList);
        }
        public void CreateBirlesimKomisyonRelation(Guid id, Guid komisyonId, Guid? altKomisyonId)
        {
            _birlesimKomisyonRepo.Create(new BirlesimKomisyon { BirlesimId = id, KomisyonId = komisyonId, AltKomisyonId = altKomisyonId });
            _birlesimKomisyonRepo.Save();
        }
        public void CreateBirlesimOzelToplanmaRelation(Guid id, Guid ozelToplanmaId)
        {
            _birlesimOzeToplanmaRepo.Create(new BirlesimOzelToplanma { BirlesimId = id, OzelToplanmaId = ozelToplanmaId });
            _birlesimOzeToplanmaRepo.Save();
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
        public List<Stenograf> GetStenografIdList(DateTime gorevBasTarih)
        {
            var result = from b in _stenografRepo.Query()
                         from p in _stenoIzinRepo.Query().
                             Where(p => b.Id == p.StenografId && p.BaslangicTarihi.Value.ToShortDateString() == DateTime.Now.ToShortDateString()).DefaultIfEmpty()
                         select new Stenograf { AdSoyad = b.AdSoyad };


            return result.ToList();//   _stenografRepo.Get().OrderBy(x => x.SiraNo).Select(x => new Stenograf { Id =x.Id,StenoGorevTuru =x.StenoGorevTuru,AdSoyad =x.AdSoyad});
        }
        public IEnumerable<Stenograf> GetStenografIdList()
        {
            return  _stenografRepo.Get().OrderBy(x => x.SiraNo).Select(x => new Stenograf { Id =x.Id,StenoGorevTuru =x.StenoGorevTuru,AdSoyad =x.AdSoyad});
        }
        public void CreateStenoGorevDonguEkle(string birlesimId, string oturumId)
        {
            var stenoList = _gorevAtamaKomMRepo.Get(x => x.BirlesimId == birlesimId).OrderBy(x => x.SatırNo).ToList();
            if(stenoList!=null && stenoList.Count> 0)
            {
                var grpList = stenoList.GroupBy(c => c.StenografId).Select(x => new { StenografId = x.Key });

                var gorevBitis = stenoList.LastOrDefault().GorevBitisTarihi;
                var refSatırNo = stenoList.LastOrDefault().SatırNo;
                var atamaList = new List<GorevAtamaKomM>();
                int firsRec = 1;
                foreach (var item in grpList)
                {
                    var newEntity = new GorevAtamaKomM();
                    newEntity.BirlesimId = birlesimId;
                    newEntity.OturumId = oturumId;
                    newEntity.StenografId = item.StenografId.ToString();
                    newEntity.StenoSure = stenoList.Where(x => x.StenografId == item.StenografId).LastOrDefault().StenoSure;
                    newEntity.GorevBasTarihi = gorevBitis;
                    newEntity.GorevBitisTarihi = DateTime.Parse(newEntity.GorevBasTarihi).AddMinutes(newEntity.StenoSure).ToLongDateString();
                    newEntity.SatırNo = refSatırNo + firsRec;
                    gorevBitis = newEntity.GorevBitisTarihi;
                    //newEntity.GorevStatu = item.Stenograf?.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet &&  newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                    atamaList.Add(newEntity);
                    firsRec++;
                }
                _gorevAtamaKomMRepo.AddRangeAsync(atamaList);
            }
        }
        public void AddStenoGorevAtamaKomisyon(List<Guid> stenografIds, string birlesimId, string oturumId)
        {
            foreach (var steno in stenografIds)
            {
                var stenoList = _gorevAtamaKomMRepo.Get(x => x.BirlesimId == birlesimId).OrderBy(x => x.SatırNo).ToList();
                if (stenoList != null && stenoList.Count() > 0)
                {
                    var atamaList = new List<GorevAtamaKomM>();
                    var grpListCnt = stenoList.GroupBy(c => new
                    {
                        c.StenografId,
                    }).Count();
                    int k = 0;
                    for (int i = 1; i <= stenoList.Count() / grpListCnt; i++)
                    {
                        var deger = (grpListCnt * i) + k;
                        var refSteno = stenoList.Where(x => x.SatırNo == deger).OrderBy(x => x.SatırNo).FirstOrDefault();
                        if(refSteno != null)
                        {
                            foreach (var item in stenoList.Where(x => x.SatırNo > refSteno.SatırNo).OrderBy(x => x.SatırNo))
                            {
                                item.GorevBasTarihi = DateTime.Parse(item.GorevBasTarihi).AddMinutes(refSteno.StenoSure).ToLongDateString();
                                item.GorevBitisTarihi = DateTime.Parse(item.GorevBitisTarihi).AddMinutes(refSteno.StenoSure).ToLongDateString();
                                item.SatırNo = item.SatırNo + 1;
                                _gorevAtamaKomMRepo.UpdateAsync(item.Id, item);
                            }
                            var nwGrv = new GorevAtamaKomM
                            {
                                SatırNo = deger + 1,
                                StenografId = steno.ToString(),
                                BirlesimId = birlesimId,
                                OturumId = oturumId,
                                StenoSure = refSteno.StenoSure,
                                GorevBasTarihi = refSteno.GorevBitisTarihi,
                                GorevBitisTarihi = DateTime.Parse(refSteno.GorevBitisTarihi).AddMinutes(refSteno.StenoSure).ToLongDateString()
                            };
                            stenoList.Add(nwGrv);
                            _gorevAtamaKomMRepo.AddAsync(nwGrv);
                            k++;
                        }
                    }
                }
            }
        }
        public List<GorevAtamaModel> GetGorevAtamaByBirlesimId(Guid birlesimId,ToplanmaTuru toplanmaTuru)
        {
            var model = new List<GorevAtamaModel>();
            if(toplanmaTuru == ToplanmaTuru.GenelKurul)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaGKRepo.Get(x => x.BirlesimId == birlesimId).ToList());
            }
            else if(toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaKomRepo.Get(x => x.BirlesimId == birlesimId).ToList());
            }
            else if(toplanmaTuru == ToplanmaTuru.OzelToplanti)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaOzelRepo.Get(x => x.BirlesimId == birlesimId).ToList());
            }

            return model;
        }
        public async void ChangeSureStenografKomisyon(string birlesimId,int satırNo, double sure, bool digerAtamalarDahil = false)
        {
            try
            {
                var stenoGorev = _gorevAtamaKomMRepo.Get(x=>x.BirlesimId == birlesimId && x.SatırNo >= satırNo);
                var gorevBas = stenoGorev.FirstOrDefault().GorevBasTarihi;
                var stenoSure = stenoGorev.FirstOrDefault().StenoSure;
                foreach (var item in stenoGorev)
                {
                    item.StenoSure = sure;
                    item.GorevBasTarihi = gorevBas;
                    item.GorevBitisTarihi = DateTime.Parse(item.GorevBasTarihi).AddMinutes(sure).ToLongDateString();
                    gorevBas = item.GorevBitisTarihi;
                    sure = digerAtamalarDahil ? item.StenoSure : stenoSure;
                    _gorevAtamaKomMRepo.UpdateAsync(item.Id, item);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void ChangeOrderStenografKomisyon(string kaynakBirlesimId, Dictionary<string, string> kaynakStenoList, string hedefBirlesimId, Dictionary<string, string> hedefStenografList)
        {
            if (kaynakBirlesimId != hedefBirlesimId) 
            {
                var stenoGrevHedef = _gorevAtamaKomMRepo.Get(x => x.BirlesimId == hedefBirlesimId).OrderBy(x => x.SatırNo).ToList();               
                if (stenoGrevHedef != null && stenoGrevHedef.Count() > 0)
                {
                    var minStenoGorev = stenoGrevHedef.Where(x => x.StenografId == hedefStenografList.FirstOrDefault().Key);
                    //var hedefStenoGorev = stenoGrevHedef.Where(x => x.GorevBasTarihi >= minStenoGorev.GorevBasTarihi);

                    foreach (var item in minStenoGorev)
                    {
                        var newEntity = new GorevAtamaKomM();
                        newEntity.BirlesimId = hedefBirlesimId;
                        newEntity.OturumId = item.OturumId;
                        newEntity.StenografId = kaynakStenoList.FirstOrDefault().Key;
                        newEntity.AdSoyad = kaynakStenoList.FirstOrDefault().Value;
                        newEntity.GorevBasTarihi = item.GorevBasTarihi;
                        newEntity.GorevBitisTarihi = item.GorevBitisTarihi;
                        newEntity.StenoSure = item.StenoSure;
                        //newEntity.GorevStatu = item.Stenograf.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                        //_stenoGorevRepo.Create(newEntity);
                        //_stenoGorevRepo.Save();
                        _gorevAtamaKomMRepo.AddAsync(newEntity);

                        var hedefStenoGorev = stenoGrevHedef.Where(x => DateTime.Parse(x.GorevBasTarihi) >= DateTime.Parse(item.GorevBasTarihi));
                        foreach (var hedef in hedefStenoGorev)
                        {
                            hedef.GorevBasTarihi = DateTime.Parse(hedef.GorevBasTarihi).AddMinutes(hedef.StenoSure).ToLongDateString();
                            hedef.GorevBitisTarihi = DateTime.Parse(hedef.GorevBasTarihi).AddMinutes(hedef.StenoSure).ToLongDateString();
                            //hedef.GorevStatu = hedef.Stenograf.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet &&  hedef.GorevBasTarihi.Value.AddMinutes(9 * hedef.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                            //_stenoGorevRepo.Update(hedef);
                            //_stenoGorevRepo.Save();
                            _gorevAtamaKomMRepo.UpdateAsync(hedef.Id, hedef);
                        }
                    }
                }
            }
            else
            {
                var stenoGrevHedef = _gorevAtamaKomMRepo.Get(x => x.BirlesimId == hedefBirlesimId).OrderBy(x => x.SatırNo);
                if (stenoGrevHedef != null && stenoGrevHedef.Count() > 0)
                {
                    var stenoList = new List<GorevAtamaKomM>();
                    var hedefSteno = stenoGrevHedef.Where(x => x.StenografId == hedefStenografList.FirstOrDefault().Key).OrderBy(x => x.SatırNo).ToArray();

                    var kaynakSteno = stenoGrevHedef.Where(x => x.StenografId == hedefStenografList.FirstOrDefault().Key).OrderBy(x => x.SatırNo).ToArray();

                    for (int i = 0; i < hedefSteno.Count(); i++)
                    {
                        var hedefGorevBasTarihi = hedefSteno[i].GorevBasTarihi;
                        var hedefGorevBitisTarihi = hedefSteno[i].GorevBitisTarihi;
                        var hedefSure = hedefSteno[i].StenoSure;

                        hedefSteno[i].GorevBasTarihi = kaynakSteno[i].GorevBasTarihi;
                        hedefSteno[i].GorevBitisTarihi = kaynakSteno[i].GorevBitisTarihi;
                        hedefSteno[i].StenoSure = kaynakSteno[i].StenoSure;
                        stenoList.Add(hedefSteno[i]);

                        kaynakSteno[i].GorevBasTarihi = hedefGorevBasTarihi;
                        kaynakSteno[i].GorevBitisTarihi = hedefGorevBitisTarihi;
                        kaynakSteno[i].StenoSure = hedefSure;
                        stenoList.Add(kaynakSteno[i]);
                    }
                    foreach (var item in stenoList)
                    {
                        _gorevAtamaKomMRepo.UpdateAsync(item.Id,item);
                    }
             
                    //_stenoGorevRepo.Update(stenoList);
                    //_stenoGorevRepo.Save();
                }
            }
        }


        public void DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, Guid stenografId,ToplanmaTuru toplanmaTuru)
        {
            var birlesim = _birlesimRepo.GetById(birlesimId);
            if (birlesim != null && (birlesim.ToplanmaDurumu == ToplanmaStatu.Oluşturuldu || birlesim.ToplanmaDurumu == ToplanmaStatu.Planlandı))
            {

                var gorevler = GetGorevAtama(toplanmaTuru, birlesimId);
                var gorevSteno = gorevler.Where(x => x.StenografId == stenografId);
                if (gorevSteno != null && gorevSteno.Count() > 0)
                {
                    DeleteGorevAtama(gorevSteno, toplanmaTuru);

                    gorevler = gorevler.Where(x => x.StenografId != stenografId).OrderBy(x => x.StenografId).OrderBy(x => x.GorevBasTarihi).ToList();
                    if (gorevler != null && gorevler.Count() > 0)
                    {
                        var minDate = gorevler.FirstOrDefault().BirlesimBasTarihi;
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
        private void UpdateGorevAtama(IEnumerable<GorevAtamaModel> model, ToplanmaTuru toplanmaTuru)
        {
            if (toplanmaTuru == ToplanmaTuru.GenelKurul)
            {
                var entity = Mapper.Map<GorevAtamaGenelKurul>(model);
                _gorevAtamaGKRepo.Update(entity);
                _gorevAtamaGKRepo.Save();
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                var entity = Mapper.Map<GorevAtamaKomisyon>(model);
                _gorevAtamaKomRepo.Update(entity);
                _gorevAtamaKomRepo.Save();
            }
            else if (toplanmaTuru == ToplanmaTuru.OzelToplanti)
            {
                var entity = Mapper.Map<GorevAtamaOzelToplanma>(model);
                _gorevAtamaOzelRepo.Update(entity);
                _gorevAtamaOzelRepo.Save();
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

        public List<GorevAtamaModel> GetGorevAtama(ToplanmaTuru toplanmaTuru,Guid birlesimId)
        {
            var model = new List<GorevAtamaModel>();
            if (toplanmaTuru == ToplanmaTuru.GenelKurul)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaGKRepo.Get(x => x.BirlesimId == birlesimId && x.Birlesim.BitisTarihi == null, includeProperties: "Birlesim").OrderBy(x => x.GorevBasTarihi).ToList());
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaKomRepo.Get(x => x.BirlesimId == birlesimId && x.Birlesim.BitisTarihi == null, includeProperties: "Birlesim").OrderBy(x => x.GorevBasTarihi).ToList());
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaOzelRepo.Get(x => x.BirlesimId == birlesimId && x.Birlesim.BitisTarihi == null, includeProperties: "Birlesim").OrderBy(x => x.GorevBasTarihi).ToList());
            }
            return model;
        }

        public void UpdateBirlesimStenoGorevDevamEtme(Guid birlesimId, DateTime basTarih, StenoGorevTuru stenoGorevTur, DateTime oturumKapanmaTarihi, Guid oturumId,ToplanmaTuru toplanmaTuru)
        {
            var updateList = new List<GorevAtamaModel>();
            var resultList = GetGorevAtama(toplanmaTuru, birlesimId).Where(x => x.stenoGorevTuru == stenoGorevTur && oturumKapanmaTarihi <= x.GorevBasTarihi);
                //_stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.Stenograf.StenoGorevTuru == stenoGorevTur && oturumKapanmaTarihi <= x.GorevBasTarihi, includeProperties: "Birlesim,Stenograf");
            if (resultList != null && resultList.Count() > 0)
            {
                var result = resultList.FirstOrDefault();
                var ilkGorevBitisTarihi = result.GorevBasTarihi.Value;
                var mindateDiff = basTarih.Subtract(result.GorevBasTarihi.Value).TotalMinutes;
                var modResult = stenoGorevTur == StenoGorevTuru.Stenograf ? result.StenoSure : result.UzmanStenoSure;
                if (modResult != null && modResult > 0)
                {
                    var sonuc = modResult - (mindateDiff % modResult);
                    result.GorevBasTarihi = basTarih;
                    result.GorevBitisTarihi = result.GorevBasTarihi.Value.AddMinutes(sonuc);
                    result.GorevStatu = GorevStatu.DevamEdiyor;
                    result.OturumId = oturumId;
                    updateList.Add(result);

                    var remain = result.GorevBitisTarihi.Value.Subtract(ilkGorevBitisTarihi).TotalMinutes;
                    var remainResult = resultList.Where(x => x.Id != result.Id);
                    foreach (var item in remainResult)
                    {
                        item.GorevBasTarihi = item.GorevBasTarihi.Value.AddMinutes(remain);
                        item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(modResult);
                        item.GorevStatu = GorevStatu.DevamEdiyor;
                        item.OturumId = oturumId;
                        updateList.Add(result);
                    }
                    UpdateGorevAtama(updateList, toplanmaTuru);
                }
            }
        }

        public void UpdateStenoGorevTamamla(Guid birlesimId, StenoGorevTuru stenoGorevTur,ToplanmaTuru toplanmaTuru)
        {
            var result = GetGorevAtama(toplanmaTuru, birlesimId).Where(x => x.stenoGorevTuru == stenoGorevTur);
               // _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.Stenograf.StenoGorevTuru == stenoGorevTur, includeProperties: "Birlesim,Stenograf").OrderBy(x => x.GorevBasTarihi);
            if (result != null && result.Count() > 0)
            {
                result.ToList().ForEach(x => x.GorevStatu = GorevStatu.Tamamlandı);
                UpdateGorevAtama(result,toplanmaTuru);
            }
        }

        public void UpdateBirlesimStenoGorevBaslama(Guid birlesimId, DateTime basTarih, StenoGorevTuru stenoGorevTur, ToplanmaTuru toplanmaTuru)
        {
            var updateList = new List<GorevAtamaModel>();
            var result = GetGorevAtama(toplanmaTuru, birlesimId).Where(x => x.stenoGorevTuru == stenoGorevTur);
            //var result = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.Stenograf.StenoGorevTuru == stenoGorevTur, includeProperties: "Birlesim,Stenograf").OrderBy(x => x.GorevBasTarihi);
            var resultFirst = result.FirstOrDefault();
            var mindate = resultFirst.GorevBasTarihi.Value;
            var gorevId = resultFirst.Id;
            var mindateDiff = basTarih.Subtract(result.Min(x => x.GorevBasTarihi).Value).TotalMinutes;

            UpdateBirlesimBaslamaSaat(birlesimId, mindateDiff);        

            var modResult = stenoGorevTur == StenoGorevTuru.Stenograf ? result.Select(x => x.StenoSure) : result.Select(x => x.UzmanStenoSure);
            if (modResult != null && modResult.Count() > 0)
            {
                var sonuc = modResult.FirstOrDefault() - (mindateDiff % modResult.FirstOrDefault());
                var minStenoResult = result.Where(x => x.GorevBasTarihi == mindate).FirstOrDefault();
                minStenoResult.GorevBasTarihi = mindate.AddMinutes(mindateDiff);
                minStenoResult.GorevBitisTarihi = minStenoResult.GorevBasTarihi.Value.AddMinutes(sonuc);
                minStenoResult.GorevStatu = GorevStatu.DevamEdiyor;
                var gorevBasPlan = minStenoResult.GorevBasTarihi.Value.AddMinutes(-(mindateDiff % modResult.FirstOrDefault()));
                var remain = gorevBasPlan.Subtract(mindate).TotalMinutes;
                updateList.Add(minStenoResult);

                var remainResult = result.Where(x => x.Id != gorevId);
                foreach (var item in remainResult)
                {
                    item.GorevBasTarihi = item.GorevBasTarihi.Value.AddMinutes(remain);
                    item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(modResult.FirstOrDefault());
                    minStenoResult.GorevStatu = GorevStatu.DevamEdiyor;
                    updateList.Add(item);
                }
                UpdateGorevAtama(updateList, toplanmaTuru);
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

        public void UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId,ToplanmaTuru toplanmaTuru)
        {
            var result = GetGorevAtama(toplanmaTuru, birlesimId).Where(x => x.StenografId == stenoId && x.GorevBasTarihi >= DateTime.Now);
            //var result = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.StenografId == stenoId && x.GorevBasTarihi >= DateTime.Now);
            if (result != null && result.Count() > 0)
            {
                var statuDevam = result.Where(x => x.GorevStatu == GorevStatu.Planlandı);
                if (statuDevam != null && statuDevam.Count() > 0)
                    result.ToList().ForEach(x => x.GorevStatu = GorevStatu.Iptal);
                else
                {
                    var statuIptal = result.Where(x => x.GorevStatu == GorevStatu.Iptal);
                    if (statuIptal != null && statuIptal.Count() > 0)
                        result.ToList().ForEach(x => x.GorevStatu = GorevStatu.Planlandı);
                }
                UpdateGorevAtama(result, toplanmaTuru);
            }
        }

        public void UpdateGorevDurumById(Guid id,ToplanmaTuru toplanmaTuru)
        {
            var result = new List<GorevAtamaModel>();
            if (toplanmaTuru == ToplanmaTuru.GenelKurul)
            {
                result = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaGKRepo.Get(x=>x.Id == id && x.GorevBasTarihi >= DateTime.Now));
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                result = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaKomRepo.Get(x => x.Id == id && x.GorevBasTarihi >= DateTime.Now));
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                result = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaOzelRepo.Get(x => x.Id == id && x.GorevBasTarihi >= DateTime.Now));
            }
            if (result != null && result.Count>0)
            {
                if (result.FirstOrDefault().GorevStatu == GorevStatu.Iptal)
                    result.FirstOrDefault().GorevStatu = GorevStatu.Planlandı;
                else if (result.FirstOrDefault().GorevStatu == GorevStatu.Planlandı)
                    result.FirstOrDefault().GorevStatu = GorevStatu.Iptal;
                UpdateGorevAtama(result, toplanmaTuru);
            }
        }
        #region kapatıldı, şimdilik,açılabilir
        //    //UpdateGidenGrup(atamaList);
        //}
        //else if (entity.StenografIds != null && entity.StenografIds.Count > 0)
        //{
        //    var birlesim = _birlesimRepo.GetById(entity.BirlesimId);
        //    if (birlesim != null)
        //    {
        //        var minDate = birlesim.BaslangicTarihi;
        //        var atamaList = new List<GorevAtamaKomM>();
        //        int firstRec = 1;
        //        foreach (var item in entity.StenografIds)
        //        {
        //            var newEntity = new GorevAtamaKomM();
        //            newEntity.BirlesimId = entity.BirlesimId;
        //            newEntity.OturumId = entity.OturumId;
        //            newEntity.StenografId = item;
        //            newEntity.GorevBasTarihi = minDate.Value;
        //            newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.Value.AddMinutes(firstRec * birlesim.StenoSure);
        //            newEntity.StenoSure = birlesim.StenoSure;
        //            newEntity.GorevStatu = GorevStatu.GidenGrup; //GetStenoGidenGrupDurum(item) == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
        //            atamaList.Add(newEntity);
        //            minDate = newEntity.GorevBitisTarihi;
        //            firstRec++;
        //        }
        //        _stenoGorevRepo.Create(atamaList, CurrentUser.Id);
        //        _stenoGorevRepo.Save();

        //        // UpdateGidenGrup(atamaList);
        //    }
        #endregion
    }
}


