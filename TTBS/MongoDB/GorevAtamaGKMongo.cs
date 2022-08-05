using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.Enums;
using TTBS.MongoDB;

namespace TTBS.MongoDB
{
    public class GorevAtamaGKMongo : MongoDbEntity
    {
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public int GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
        public Guid StenografId { get; set; }
        public GorevStatu GorevStatu { get; set; }
        public double StenoSure { get; set; }
        public string OturumId { get; set; }
        public string BirlesimId { get; set; }
        public List<string> StenografIds { get; set; }
        public double DifMin { get; set; }
        public bool ToplantiVar { get; set; }
        public IzınTuru StenoIzinTuru { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public int TurAdedi { get; set; }
        public bool StenoToplamSureAsım { get; set; }
    }
}
