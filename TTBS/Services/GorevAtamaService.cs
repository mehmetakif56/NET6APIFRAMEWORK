using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;
using TTBS.Core.Interfaces;
using TTBS.MongoDB;

namespace TTBS.Services
{
    public interface IGorevAtamaService
    {
        Birlesim CreateBirlesim(Birlesim birlesim);
        Guid CreateOturum(Oturum Oturum);
        void CreateStenoAtama(Birlesim birlesim, Guid oturumId, List<Guid> stenoListTmp);
        void CreateUzmanStenoAtama(Birlesim birlesim, Guid oturumId, List<Guid> stenoListTmp);
        Birlesim UpdateBirlesimGorevAtama(Guid birlesimId);
        void CreateBirlesimKomisyonRelation(Guid id, Guid komisyonId, Guid? altKomisyonId);
        void CreateBirlesimOzelToplanmaRelation(Guid id, Guid ozelToplanmaId);
        IEnumerable<GorevAtama> GetAssignedStenoByBirlesimId(Guid birlesimId);
    }
    public class GorevAtamaService : BaseService, IGorevAtamaService
    {
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<GorevAtama> _stenoGorevRepo;
        private IRepository<BirlesimKomisyon> _birlesimKomisyonRepo;
        private IRepository<BirlesimOzelToplanma> _birlesimOzeToplanmaRepo;
        private readonly IGorevAtamaMongoRepository _gorevAtamaMongoRepo;
        private IRepository<Stenograf> _stenografRepo;
        private IRepository<Oturum> _oturumRepo;

        public GorevAtamaService(IRepository<Birlesim> birlesimRepo, 
                             IRepository<GorevAtama> stenoGorevRepo,
                             IGorevAtamaMongoRepository gorevAtamaMongoRepo,
                             IRepository<BirlesimKomisyon> birlesimKomisyonRepo,
                             IRepository<BirlesimOzelToplanma> birlesimOzeToplanmaRepo,
                             IRepository<Stenograf> stenografRepo,
                             IRepository<Oturum> oturumRepo,
                             IServiceProvider provider) : base(provider)
        {
           _birlesimRepo=birlesimRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _gorevAtamaMongoRepo=gorevAtamaMongoRepo;
            _birlesimKomisyonRepo = birlesimKomisyonRepo;
            _birlesimOzeToplanmaRepo = birlesimOzeToplanmaRepo;
            _birlesimOzeToplanmaRepo = birlesimOzeToplanmaRepo;
            _stenografRepo = stenografRepo;
            _oturumRepo = oturumRepo;
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

        public void CreateUzmanStenoAtama(Birlesim birlesim, Guid oturumId, List<Guid> stenoListTmp)
        {
            var stenoList = stenoListTmp != null && stenoListTmp.Count() > 0 ? stenoListTmp :
                          _stenografRepo.Get(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf).OrderBy(x => x.SiraNo).Select(x => x.Id);
            int firstRec = 0;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                var atamaList = new List<GorevAtamaGKMongo>();
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtamaGKMongo();
                    newEntity.BirlesimId = birlesim.Id.ToString() ;
                    newEntity.OturumId = oturumId.ToString() ;
                    newEntity.StenografId = item;
                    newEntity.GorevBasTarihi = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value.AddMinutes(firstRec * birlesim.UzmanStenoSure) : null;
                    newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.HasValue ? newEntity.GorevBasTarihi.Value.AddMinutes(birlesim.UzmanStenoSure) : null;
                    newEntity.StenoSure = birlesim.UzmanStenoSure;
                    //newEntity.GorevStatu = item.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                    atamaList.Add(newEntity);
                    firstRec++;
                }
                var result = _gorevAtamaMongoRepo.AddRangeAsync(atamaList);
                //_stenoGorevRepo.Create(atamaList, CurrentUser.Id);
                //_stenoGorevRepo.Save();
            }
        }
        public void CreateStenoAtama(Birlesim birlesim, Guid oturumId, List<Guid> stenoListTmp)
        {
            var stenoList = stenoListTmp != null && stenoListTmp.Count()>0 ? stenoListTmp:
                          _stenografRepo.Get(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf).OrderBy(x => x.SiraNo).Select(x => x.Id);
            int firstRec = 0;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                var atamaList = new List<GorevAtamaGKMongo>();
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtamaGKMongo();
                    newEntity.BirlesimId = birlesim.Id.ToString();
                    newEntity.OturumId = oturumId.ToString();
                    newEntity.StenografId = item;
                    newEntity.GorevBasTarihi = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value.AddMinutes(firstRec * birlesim.StenoSure) : null;
                    newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.HasValue ? newEntity.GorevBasTarihi.Value.AddMinutes(birlesim.StenoSure) : null;
                    newEntity.StenoSure = birlesim.StenoSure;
                    //newEntity.GorevStatu = item.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                    atamaList.Add(newEntity);
                    firstRec++;
                }
                var result = _gorevAtamaMongoRepo.AddRangeAsync(atamaList);
                //_stenoGorevRepo.Create(atamaList, CurrentUser.Id);
                //_stenoGorevRepo.Save();
            }
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

        public Birlesim UpdateBirlesimGorevAtama(Guid birlesimId)
        {
            var birlesim = _birlesimRepo.GetById(birlesimId);
            birlesim.ToplanmaDurumu = ToplanmaStatu.Planlandı;
            birlesim.TurAdedi = birlesim.TurAdedi;
            _birlesimRepo.Update(birlesim);
            _birlesimRepo.Save();
            return birlesim;
        }

        public IEnumerable<GorevAtama> GetAssignedStenoByBirlesimId(Guid birlesimId)
        {
            return _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId, includeProperties: "Stenograf,Birlesim");
        }

    }
}
