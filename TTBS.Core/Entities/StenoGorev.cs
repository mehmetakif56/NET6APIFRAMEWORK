using System;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class StenoGorev : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? GörevTarihi { get; set; }  
        public int GorevSaati { get; set; }
        public int GorevSuresi { get; set; }
        public string AdSoyad { get; set; }
        public StenoPlan StenoPlan { get; set; }
        public Guid? StenoPlanId { get; set; }

    }
}
