using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class ReportPlanDetayModel
    {
        public PlanTuru GorevTuru { get; set; }
        public string  GorevAd { get; set; }
        public string GorevTarihi { get; set; }
        public string BasSaat { get; set; }
        public string Bitissaat { get; set; }
        public int ToplamSure { get; set; }
        public int NetSure { get; set; }
        public int Ara { get; set; }
    }
}
