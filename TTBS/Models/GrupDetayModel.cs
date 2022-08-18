using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class GrupDetayModel
    {
        public DurumStatu GidenGrupPasif { get; set; }
        public DurumStatu GidenGrupSaatUygula { get; set; }
        public Guid GrupId { get; set; }
        public string GidenGrupSaat { get; set; }
    }
}
