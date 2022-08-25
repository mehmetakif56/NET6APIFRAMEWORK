using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class GorevAtamaModel
    {
        public Guid Id { get; set; }
        public int SatırNo { get; set; }
        public Guid BirlesimId { get; set; }
        public Guid StenografId { get; set; }
        public Guid OturumId { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public double StenoSure { get; set; }
        public GorevStatu GorevStatu { get; set; }
        public string KomisyonAd { get; set; }
        public int GidenGrupSaatUygula { get; set; }
        public string GidenGrupSaat { get; set; }
        public IzınTuru StenoIzinTuru { get; set; }
        public bool SureAsmaVar { get; set; }
        public List<Guid> StenografIds { get; set; }
        public DateTime? BirlesimBasTarihi { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public string StenoAdSoyad { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
