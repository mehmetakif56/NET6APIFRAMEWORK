using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class HaftalikSureIStatistikModel
    {
        public Guid? Id { get; set; }
        public string Ad { get; set; }
        public string Kodu { get; set; }
        public ToplanmaTuru toplanmaTuru { get; set; }
    }
}
