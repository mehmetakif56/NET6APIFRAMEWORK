using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGrupModel
    {
        public Guid StenoId { get; set; }
        public Guid GrupId { get; set; }
        public string StenoAdSoyad { get; set; }
        public string GrupAd { get; set; }
    }
}
