using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class GidenGrupOlusturModel
    {
        public Guid? Id { get; set; }
        public Guid GrupId { get; set; }
        /// <summary>
        /// Belirtilen grup için gidengrup özelliğinin kullanılması
        /// </summary>
        public DurumStatu GidenGrupMu { get; set; }
        /// <summary>
        /// <summary>
        /// saat 18:00 e kadar giden grup çalışma durumu
        /// </summary>
        public DurumStatu GidenGrupTamamlama { get; set; } = DurumStatu.Hayır;
    }
}
