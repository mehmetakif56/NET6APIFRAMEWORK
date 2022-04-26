using System;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class StenoPlan:BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? BaslangicTarihi { get; set; }  
        public DateTime? BitisTarihi { get; set; }
        public int StenoSayisi { get; set; } 
        public int StenoSure{ get; set; }
        public int UzmanStenoSure { get; set; }
        public int UzmanStenoSayisi { get; set; }
        public string Yeri { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; }
        public string Ad { get; set; }
        public Birlesim Birlesim { get; set; }
        public Guid? BirlesimId { get; set; }
        public Komisyon Komisyon { get; set; }
        public Guid? KomisyonId { get; set; }

    }
}
