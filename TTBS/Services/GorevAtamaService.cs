using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;
using TTBS.Core.Interfaces;
using TTBS.MongoDB;

namespace TTBS.Services
{
    public interface IGorevAtamaService
    {
        Result CreateBirlesim(Birlesim birlesim);
        Guid CreateOturum(Oturum Oturum);
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
        public Result CreateBirlesim(Birlesim birlesim)
        {
            var result = new Result();
            try
            {
                _birlesimRepo.Create(birlesim, CurrentUser.Id);
                _birlesimRepo.Save();

                var oturumId = CreateOturum(new Oturum
                {
                    BirlesimId = birlesim.Id,
                    BaslangicTarihi = birlesim.BaslangicTarihi
                });
                if (birlesim.ToplanmaTuru == ToplanmaTuru.GenelKurul)
                    CreateStenoGorevAtamaGenelKurul(birlesim, oturumId);
                else if (birlesim.ToplanmaTuru == ToplanmaTuru.Komisyon)
                    CreateBirlesimKomisyonRelation(birlesim.Id, birlesim.KomisyonId, birlesim.AltKomisyonId);
                else if (birlesim.ToplanmaTuru == ToplanmaTuru.OzelToplanti)
                    CreateBirlesimOzelToplanmaRelation(birlesim.Id, birlesim.OzelToplanmaId);

                result.HasError = false;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
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
        public void CreateStenoGorevAtamaGenelKurul(Birlesim birlesim, Guid oturumId)
        {
            CreateSteno(birlesim, oturumId);
            CreateUzmanSteno(birlesim, oturumId);
        }
        private void CreateUzmanSteno(Birlesim birlesim, Guid oturumId)
        {
            var stenoList = _stenografRepo.Get(x => x.StenoGorevTuru == StenoGorevTuru.Uzman, includeProperties: "StenoGrups").OrderBy(x => x.SiraNo);
            int firstRec = 0;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                var atamaList = new List<GorevAtamaGKMongo>();
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtamaGKMongo();
                    newEntity.BirlesimId = birlesim.Id.ToString(); ;
                    newEntity.OturumId = oturumId.ToString(); ;
                    newEntity.StenografId = item.Id;
                    newEntity.GorevBasTarihi = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value.AddMinutes(firstRec * birlesim.UzmanStenoSure) : null;
                    newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.HasValue ? newEntity.GorevBasTarihi.Value.AddMinutes(birlesim.UzmanStenoSure) : null;
                    newEntity.StenoSure = birlesim.UzmanStenoSure;
                    newEntity.GorevStatu = item.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                    atamaList.Add(newEntity);
                    firstRec++;
                }
                var result = _gorevAtamaMongoRepo.AddRangeAsync(atamaList);
                //_stenoGorevRepo.Create(atamaList, CurrentUser.Id);
                //_stenoGorevRepo.Save();
            }
        }
        private void CreateSteno(Birlesim birlesim, Guid oturumId)
        {
            var stenoList = _stenografRepo.Get(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf, includeProperties: "StenoGrups").OrderBy(x => x.SiraNo);
            int firstRec = 0;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                var atamaList = new List<GorevAtamaGKMongo>();
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtamaGKMongo();
                    newEntity.BirlesimId = birlesim.Id.ToString();
                    newEntity.OturumId = oturumId.ToString();
                    newEntity.StenografId = item.Id;
                    newEntity.GorevBasTarihi = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value.AddMinutes(firstRec * birlesim.StenoSure) : null;
                    newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.HasValue ? newEntity.GorevBasTarihi.Value.AddMinutes(birlesim.StenoSure) : null;
                    newEntity.StenoSure = birlesim.StenoSure;
                    newEntity.GorevStatu = item.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                    atamaList.Add(newEntity);
                    firstRec++;
                }
                var result = _gorevAtamaMongoRepo.AddRangeAsync(atamaList);
                //_stenoGorevRepo.Create(atamaList, CurrentUser.Id);
                //_stenoGorevRepo.Save();
            }
        }
        private void CreateBirlesimKomisyonRelation(Guid id, Guid komisyonId, Guid? altKomisyonId)
        {
            _birlesimKomisyonRepo.Create(new BirlesimKomisyon { BirlesimId = id, KomisyonId = komisyonId, AltKomisyonId = altKomisyonId });
            _birlesimKomisyonRepo.Save();
        }
        private void CreateBirlesimOzelToplanmaRelation(Guid id, Guid ozelToplanmaId)
        {
            _birlesimOzeToplanmaRepo.Create(new BirlesimOzelToplanma { BirlesimId = id, OzelToplanmaId = ozelToplanmaId });
            _birlesimOzeToplanmaRepo.Save();
        }
    }
}
