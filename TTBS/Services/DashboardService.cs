using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Interfaces;
using TTBS.Models;

namespace TTBS.Services
{
    public interface IDashboardService
    {
        IEnumerable<GorevAtamaModel> GetActiveGorevler(ToplanmaTuru toplanmaTuru);
    }
    public class DashboardService : BaseService, IDashboardService
    {
        private IRepository<GorevAtamaGenelKurul> _gorevAtamaGKRepo;
        private IRepository<GorevAtamaKomisyon> _gorevAtamaKomRepo;
        private IRepository<GorevAtamaOzelToplanma> _gorevAtamaOzelRepo;
        public readonly IMapper _mapper;
        public DashboardService(IServiceProvider provider, IRepository<GorevAtamaGenelKurul> gorevAtamaGKRepo,
                                 IRepository<GorevAtamaKomisyon> gorevAtamaKomRepo,
                                 IRepository<GorevAtamaOzelToplanma> gorevAtamaOzelRepo, IMapper mapper) : base(provider)
        {
            _gorevAtamaGKRepo = gorevAtamaGKRepo;
            _gorevAtamaKomRepo = gorevAtamaKomRepo;
            _gorevAtamaOzelRepo = gorevAtamaOzelRepo;
            _mapper = mapper;
        }

        public IEnumerable<GorevAtamaModel> GetActiveGorevler(ToplanmaTuru toplanmaTuru)
        {
            var model = new List<GorevAtamaModel>();
            if (toplanmaTuru == ToplanmaTuru.GenelKurul)
            {
                var result = _gorevAtamaGKRepo.Get(x => x.Stenograf.UserId == CurrentUser.Id && x.GorevBasTarihi >= DateTime.Now.Date && x.GorevBasTarihi < DateTime.Now.AddDays(1).Date);
                _mapper.Map(result, model);
            }
            else if (toplanmaTuru == ToplanmaTuru.Komisyon)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaKomRepo.Get(x => x.Stenograf.UserId == CurrentUser.Id && x.GorevBasTarihi >= DateTime.Now.Date && x.GorevBasTarihi < DateTime.Now.AddDays(1).Date));
            }
            else if (toplanmaTuru == ToplanmaTuru.OzelToplanti)
            {
                model = _mapper.Map<List<GorevAtamaModel>>(_gorevAtamaOzelRepo.Get(x => x.Stenograf.UserId == CurrentUser.Id && x.GorevBasTarihi >= DateTime.Now.Date && x.GorevBasTarihi < DateTime.Now.AddDays(1).Date));
            }

            return model;
        }
    }
}
