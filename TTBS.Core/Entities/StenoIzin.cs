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
        public IzınTuru IzinTuru { get; set; }
        public Guid StenografId { get; set; }
        public Stenograf Stenograf { get; set; }
    }
}
