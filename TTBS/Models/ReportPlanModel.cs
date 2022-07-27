using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class ReportPlanModel
    {

        //steno ad + toplantı ad
        public string StenografAd { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; }
        public int  UzmGorevlendirmeSay { get; set; }
        public int UzmGorevlendirmeSure { get; set; }
        public int GorevlendirmeSure { get; set; }
        public int GorevlendirmeSay { get; set; }
    }
}
