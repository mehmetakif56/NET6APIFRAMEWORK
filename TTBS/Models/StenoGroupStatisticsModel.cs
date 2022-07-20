namespace TTBS.Models
{
    public class StenoGroupStatisticsModel
    {
        public IEnumerable<KomisyonModel> komisyons { get; set; }
        public List<StenoSureFarkModel> stenos { get; set; }
    }
}
