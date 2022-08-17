using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class GrupGuncelleModel
    {
        public Guid? GrupId { get; set; }
        public DurumStatu GidenGrupPasif { get; set; }
        public string GidenGrupSaat { get; set; }
        public DurumStatu GidenGrupSaatUygula { get; set; }
    }
}
