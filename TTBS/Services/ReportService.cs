using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IReportService
    {
        //IEnumerable<GorevAtama> GetReportStenoPlanBetweenDateGorevTur(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int gorevTuru);
        //IEnumerable<GorevAtama> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi);
    }
    public class ReportService : BaseService, IReportService
    {
        //private IRepository<StenoPlan> _stenoPlanRepo;
        private IRepository<GorevAtama> _stenoGorevRepo;

        public ReportService(/*IRepository<StenoPlan> stenoPlanRepo,*/
                             IRepository<GorevAtama> stenoGorevRepo,
                             IServiceProvider provider) : base(provider)
        {
            //_stenoPlanRepo = stenoPlanRepo;
            _stenoGorevRepo = stenoGorevRepo;
        }

        //public IEnumerable<GorevAtama> GetReportStenoPlanBetweenDateGorevTur(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int gorevTuru)
        //{
        //    return _stenoGorevRepo.Get(x => x.StenoPlan.PlanlananBaslangicTarihi >= gorevBasTarihi && x.StenoPlan.PlanlananBitisTarihi <= gorevBitTarihi && (int)x.StenoPlan.GorevTuru == gorevTuru, includeProperties: "StenoPlan");
        //}

        //public IEnumerable<GorevAtama> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        //{
        //    return _stenoGorevRepo.Get(x => x.StenografId == stenografId && x.StenoPlan.PlanlananBaslangicTarihi >= gorevBasTarihi && x.StenoPlan.PlanlananBitisTarihi <= gorevBitTarihi, includeProperties: "StenoPlan");
        //}
    }
}
