using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class ReportPlanModel
    {
        public string AdSoyad { get; set; }
        public DateTime? GorevTarihi { get; set; }
        public DateTime? GorevDakika { get; set; }
        public string GorevYeri { get; set; }
        public string GorevAd { get; set; }
        public PlanTuru GorevTuru { get; set; }
    }
}
