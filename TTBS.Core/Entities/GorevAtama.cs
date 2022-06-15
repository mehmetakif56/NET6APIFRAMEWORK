using Newtonsoft.Json;
using System;
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
        public KomisyonToplanma KomisyonToplanma { get; set; }
        public Guid KomisyonToplanmaId { get; set; } = Guid.Empty;
        public Birlesim Birlesim { get; set; }
        public Guid BirlesimId { get; set; } = Guid.Empty;
        public OzelToplanma OzelToplanma { get; set; }
        public Guid OzelToplanmaId { get; set; } = Guid.Empty;
        public ToplanmaTuru ToplanmaTuru { get; set; }
    }
}
