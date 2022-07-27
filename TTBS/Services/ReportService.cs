using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IReportService
    {
        IEnumerable<Birlesim> GetReportStenoPlanBetweenDateGorevTur(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int? gorevTuru);
        IEnumerable<GorevAtama> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi);
    }
    public class ReportService : BaseService, IReportService
    {
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<GorevAtama> _stenoGorevRepo;

        public ReportService(IRepository<Birlesim> birlesimRepo, 
                             IRepository<GorevAtama> stenoGorevRepo,
                             IServiceProvider provider) : base(provider)
        {
           _birlesimRepo=birlesimRepo;
            _stenoGorevRepo = stenoGorevRepo;
        }

        public IEnumerable<Birlesim> GetReportStenoPlanBetweenDateGorevTur(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int? gorevTuru)
        {
            switch (gorevTuru)
            {
                //Genel Kurul
                case 0:
                    return _birlesimRepo.Get(x => x.BaslangicTarihi >= gorevBasTarihi && x.BitisTarihi <= gorevBitTarihi && (int)x.ToplanmaTuru == gorevTuru);

                // Komisyon
                case 1:
                    return _birlesimRepo.Get(x => x.BaslangicTarihi >= gorevBasTarihi && x.BitisTarihi <= gorevBitTarihi && (int)x.ToplanmaTuru == gorevTuru, includeProperties: "Komisyon");

                //Özel Toplantı
                case 2:
                    return _birlesimRepo.Get(x => x.BaslangicTarihi >= gorevBasTarihi && x.BitisTarihi <= gorevBitTarihi && (int)x.ToplanmaTuru == gorevTuru, includeProperties: "OzelToplanma");

                //Hepsi
                default:
                    var birlesim = _birlesimRepo.Get(x => x.BaslangicTarihi >= gorevBasTarihi && x.BitisTarihi <= gorevBitTarihi && x.ToplanmaTuru == ToplanmaTuru.GenelKurul);
                    var komisyon = _birlesimRepo.Get(x => x.BaslangicTarihi >= gorevBasTarihi && x.BitisTarihi <= gorevBitTarihi && x.ToplanmaTuru == ToplanmaTuru.Komisyon, includeProperties: "Komisyon");
                    var ozel = _birlesimRepo.Get(x => x.BaslangicTarihi >= gorevBasTarihi && x.BitisTarihi <= gorevBitTarihi && x.ToplanmaTuru == ToplanmaTuru.OzelToplanti, includeProperties: "OzelToplanma");
                    return birlesim.Concat(komisyon).Concat(ozel).OrderBy(x => x.BaslangicTarihi);
            }
        }

        public IEnumerable<GorevAtama> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            if(stenografId!=null)
                 return _stenoGorevRepo.Get(x => x.StenografId == stenografId && x.Birlesim.BaslangicTarihi >= gorevBasTarihi && x.Birlesim.BitisTarihi <= gorevBitTarihi, includeProperties: "Birlesim,Stenograf");
            return  _stenoGorevRepo.Get(x => x.Birlesim.BaslangicTarihi >= gorevBasTarihi && x.Birlesim.BitisTarihi <= gorevBitTarihi, includeProperties: "Birlesim,Stenograf");
        }
    }
}
