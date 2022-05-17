using System;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class StenoGorev : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? GörevTarihi { get; set; }  
        public int GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
        public Guid StenografId { get; set; }
        public Stenograf Stenograf { get; set; }
        public StenoPlan StenoPlan { get; set; }
        public Guid? StenoPlanId { get; set; }
        public GorevStatu GorevStatu { get; set; }
    }
}
