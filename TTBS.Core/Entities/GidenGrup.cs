using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class GidenGrup:BaseEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Kaydetmede bugünün tarihini alır
        /// </summary>
        public DateTime? GidenGrupTarihi { get; set; }
        /// <summary>
        /// Belirtilen grup için gidengrup özelliğinin kullanılması
        /// </summary>
        public DurumStatu GidenGrupMu { get; set; } = DurumStatu.Hayır;
        /// <summary>
        /// <summary>
        /// saat 18:00 e kadar giden grup çalışma durumu
        /// </summary>
        public DurumStatu GidenGrupTamamlama { get; set; } = DurumStatu.Hayır;

        public Guid GrupId  { get; set; }
        public Grup Grup { get; set; }
    }
}
