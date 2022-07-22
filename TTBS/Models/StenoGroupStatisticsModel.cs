namespace TTBS.Models
{
    public class StenoGroupStatisticsModel
    {
        public IEnumerable<HaftalikSureIStatistikModel> komisyons { get; set; }
        public List<StenoSureFarkModel> stenos { get; set; }
    }
}
