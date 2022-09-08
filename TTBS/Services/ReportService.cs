using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Interfaces;
using TTBS.Models;

namespace TTBS.Services
{
    public interface IReportService
    {
        IEnumerable<Birlesim> GetReportStenoPlanBetweenDateGorevTur(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int? gorevTuru);
        IEnumerable<ReportPlanModel> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi);
    }
    public class ReportService : BaseService, IReportService
    {
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<GorevAtama> _stenoGorevRepo;
        private IRepository<GorevAtamaGenelKurul> _genelKurulAtamaRepo;
        private IRepository<GorevAtamaKomisyon> _komisyonAtamaRepo;
        private IRepository<GorevAtamaOzelToplanma> _ozelToplanmaAtamaRepo;
        public readonly IMapper _mapper;


        public ReportService(IRepository<Birlesim> birlesimRepo, 
                             IRepository<GorevAtama> stenoGorevRepo,
                             IRepository<GorevAtamaGenelKurul> genelKurulAtamaRepo,
                             IRepository<GorevAtamaKomisyon> komisyonAtamaRepo,
                             IRepository<GorevAtamaOzelToplanma> ozelToplanmaAtamaRepo,
                             IMapper mapper,
                             IServiceProvider provider) : base(provider)
        {
            _birlesimRepo=birlesimRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _genelKurulAtamaRepo = genelKurulAtamaRepo;
            _ozelToplanmaAtamaRepo = ozelToplanmaAtamaRepo;
            _komisyonAtamaRepo = komisyonAtamaRepo;
            _mapper = mapper;
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

        public IEnumerable<ReportPlanModel> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            if (stenografId != null)
            {
                var birlesim = _genelKurulAtamaRepo.Get(x => x.StenografId == stenografId && x.Birlesim.BaslangicTarihi >= gorevBasTarihi && x.Birlesim.BitisTarihi <= gorevBitTarihi, includeProperties: "Birlesim,Stenograf").GroupBy(x => new { x.BirlesimId, x.StenografId });
                var komisyon = _komisyonAtamaRepo.Get(x => x.StenografId == stenografId && x.Birlesim.BaslangicTarihi >= gorevBasTarihi && x.Birlesim.BitisTarihi <= gorevBitTarihi, includeProperties: "Birlesim,Stenograf").GroupBy(x => new { x.BirlesimId, x.StenografId });
                var ozel = _ozelToplanmaAtamaRepo.Get(x => x.StenografId == stenografId && x.Birlesim.BaslangicTarihi >= gorevBasTarihi && x.Birlesim.BitisTarihi <= gorevBitTarihi, includeProperties: "Birlesim,Stenograf").GroupBy(x => new { x.BirlesimId, x.StenografId });

                var model = _mapper.Map<IEnumerable<ReportPlanModel>>(birlesim)
                    .Concat(_mapper.Map<IEnumerable<ReportPlanModel>>(komisyon))
                    .Concat(_mapper.Map<IEnumerable<ReportPlanModel>>(ozel));

                return model;
            }
            else
            {
                var birlesim = _genelKurulAtamaRepo.Get(x => x.Birlesim.BaslangicTarihi >= gorevBasTarihi && x.Birlesim.BitisTarihi <= gorevBitTarihi, includeProperties: "Birlesim,Stenograf").GroupBy(x => new { x.BirlesimId, x.StenografId });
                var komisyon = _komisyonAtamaRepo.Get(x => x.Birlesim.BaslangicTarihi >= gorevBasTarihi && x.Birlesim.BitisTarihi <= gorevBitTarihi, includeProperties: "Birlesim,Stenograf").GroupBy(x => new { x.BirlesimId, x.StenografId });
                var ozel = _ozelToplanmaAtamaRepo.Get(x => x.Birlesim.BaslangicTarihi >= gorevBasTarihi && x.Birlesim.BitisTarihi <= gorevBitTarihi, includeProperties: "Birlesim,Stenograf").GroupBy(x => new { x.BirlesimId, x.StenografId });

                var model = _mapper.Map<IEnumerable<ReportPlanModel>>(birlesim)
                    .Concat(_mapper.Map<IEnumerable<ReportPlanModel>>(komisyon))
                    .Concat(_mapper.Map<IEnumerable<ReportPlanModel>>(ozel));

                return model;
            }
        }
    }
}
