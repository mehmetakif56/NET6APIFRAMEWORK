using System;
using System.Collections.Generic;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class Yasama : BaseEntity
    {
        public Guid Id { get; set; }    
        public int YasamaYili { get; set; }         
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }   
        public Donem Donem { get; set; }
        public Guid DonemId { get; set; }
        public virtual ICollection<Birlesim> Birlesims { get; set; }
        public virtual ICollection<StenoPlan> StenoPlans { get; set; }
    }
}
