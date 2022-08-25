using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class GorevAtamalar:BaseEntity 
    {
        public Guid BirlesimId { get; set; }
        public Guid StenografId { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public int GidenGrupSaatUygula { get; set; }
        public string GidenGrupSaat { get; set; }
        public int IzinTuru { get; set; }
        public GorevStatu GorevStatu { get; set; }
        public string StenoAdSoyad { get; set; }
        public string KomisyonAd { get; set; }
        public Guid OturumId { get; set; }
    }
}
