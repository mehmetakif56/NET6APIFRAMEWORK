using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class Stenograf : BaseEntity
    {
        public Guid Id { get; set; }   
        public string AdSoyad { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public int SiraNo { get; set; }
        public string GidenGrupSaat { get; set; }
        public Guid? GrupId { get; set; }
        public Grup Grup { get; set; }
        public virtual ICollection<GorevAtama> GorevAtamas { get; set; }
        public virtual ICollection<StenoIzin> StenoIzins { get; set; }
        [NotMapped]
        public bool StenoGorevDurum { get; set;}
        [NotMapped]
        public DateTime? SonGorevTarihi { get; set; }
        [NotMapped]
        public int SonGorevSuresi { get; set; }
        [NotMapped]
        public int GorevStatu { get; set; } 
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        [NotMapped]
        public IzınTuru StenoIzinTuru { get; set; }
        [NotMapped]
        public bool ToplantiVar { get; set; }
        public int BirlesimSıraNo { get; set; }
    }
}
