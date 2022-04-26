using System;
using System.Collections.Generic;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class StenoIzin : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? BaslangicTarihi { get; set; }  
        public DateTime? BitisTarihi { get; set; }
        public int BaslangicSaati { get; set; } 
        public int BitisSaati { get; set; }
        public IzınTuru IzınTuru { get; set; }
        public string AdSoyad { get; set; }
    }
}
