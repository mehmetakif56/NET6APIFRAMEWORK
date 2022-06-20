using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

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
        public Guid KomisyonId { get; set; } = Guid.Empty;
        public Guid? AltKomisyonId { get; set; } = Guid.Empty;
        public string Yeri { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; }
        public int TurAdedi { get; set; }
        public GorevStatu ToplanmaDurumu { get; set; }
    }
}
