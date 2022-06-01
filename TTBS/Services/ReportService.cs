using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IReportService
    {
        IEnumerable<StenoGorev> GetReportStenoPlanBetweenDateGorevTur(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int gorevTuru);
        IEnumerable<StenoGorev> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi);
    }
    public class ReportService : BaseService, IReportService
    {
        private IRepository<StenoPlan> _stenoPlanRepo;
        private IRepository<StenoGorev> _stenoGorevRepo;

        public ReportService(IRepository<StenoPlan> stenoPlanRepo,
                             IRepository<StenoGorev> stenoGorevRepo,
                             IServiceProvider provider) : base(provider)
        {
            _stenoPlanRepo = stenoPlanRepo;
            _stenoGorevRepo = stenoGorevRepo;
        }

        public IEnumerable<StenoGorev> GetReportStenoPlanBetweenDateGorevTur(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int gorevTuru)
        {
            return _stenoGorevRepo.Get(x => x.StenoPlan.BaslangicTarihi <= gorevBasTarihi && x.StenoPlan.BitisTarihi >= gorevBitTarihi && (int)x.StenoPlan.GorevTuru == gorevTuru, includeProperties: "StenoPlan");
        }

        public IEnumerable<StenoGorev> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            return _stenoGorevRepo.Get(x => x.StenografId == stenografId && x.StenoPlan.BaslangicTarihi <= gorevBasTarihi && x.StenoPlan.BitisTarihi >= gorevBitTarihi, includeProperties: "StenoPlan");
        }
    }
}
