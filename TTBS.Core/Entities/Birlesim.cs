using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class Birlesim : BaseEntity
    {
        public Guid Id { get; set; }    
        public string BirlesimNo { get; set; }         
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public Yasama Yasama { get; set; }
        public Guid YasamaId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal StenoSure { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UzmanStenoSure { get; set; }

    }
}
