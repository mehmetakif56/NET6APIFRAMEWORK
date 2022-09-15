using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class GrupGuncelleModel
    {
        public Guid? GrupId { get; set; }
        public DurumStatu GidenGrupPasif { get; set; }
        public DurumStatu GidenGrupSaatUygula { get; set; }
        public DateTime? GidenGrupSaat
        {
            get { return GidenGrupSaat; }
            set
            {
                if (value == null)
                {
                    GidenGrupSaat = DateTime.Today;
                };
            }
        }
    }
}
