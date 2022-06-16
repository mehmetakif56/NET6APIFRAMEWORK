using System;
using System.Collections.Generic;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class Oturum : BaseEntity
    {
        public Guid Id { get; set; }
        public int OturumNo { get; set; } = 1;       
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string OturumBaskan { get; set; }
        public string KatipUye_1 { get; set; }
        public string KatipUye_2 { get; set; }
        public bool KapaliOturum { get; set; }
        public bool YoklamaliMi { get; set; }
        public Birlesim Birlesim { get; set; }
        public Guid BirlesimId { get; set; }
    }
}
