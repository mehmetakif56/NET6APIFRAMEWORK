﻿using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class BirlesimModel
    {
        public Guid? Id { get; set; }
        public string BirlesimNo { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public Guid YasamaId { get; set; }
        public decimal StenoSure { get; set; }
        public decimal UzmanStenoSure { get; set; }
        public Guid KomisyonId { get; set; } = Guid.Empty;
        public Guid? AltKomisyonId { get; set; } = Guid.Empty;
        public string Yeri { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; } 

    }
}
