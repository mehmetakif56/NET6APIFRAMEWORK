using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class GorevAtama : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public int GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
        public Guid StenografId { get; set; }
        public Stenograf Stenograf { get; set; }
        public GorevStatu GorevStatu { get; set; }
        public Guid OturumId { get; set; }
        public Birlesim Birlesim { get; set; }
        public Oturum Oturum { get; set; }
        public Guid BirlesimId { get; set; }
        [NotMapped]
        public List<Guid> StenografIds { get; set; }

        [NotMapped]
        public double DifMin { get; set; }
        [NotMapped]
        public bool ToplantiVar { get; set; }
        [NotMapped]
        public IzınTuru StenoIzinTuru { get; set; }
        [NotMapped]
        public StenoGorevTuru StenoGorevTuru { get; set; }

    }
}
