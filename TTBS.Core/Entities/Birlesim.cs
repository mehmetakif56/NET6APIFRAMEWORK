using System;
using System.Collections.Generic;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class Birlesim : BaseEntity
    {
        public Guid Id { get; set; }    
        public string BirlesimNo { get; set; }         
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public virtual ICollection<StenoPlan> StenoPlans { get; set; }
    }
}
