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
        public bool ToplantiVar { get; set; }
        public bool GidenGrup { get; set; }
        public IzınTuru StenoIzinTuru { get; set; }
        public bool SureAsmaVar { get; set; }
        public List<Guid> StenografIds { get; set; }
        public DateTime? BirlesimBasTarihi { get; set; }
        public StenoGorevTuru stenoGorevTuru { get; set; }
        public double UzmanStenoSure { get; set; }
    }
}
