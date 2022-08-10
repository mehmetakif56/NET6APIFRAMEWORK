using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TTBS.Core.Enums;

namespace TTBS.MongoDB
{
    public class GorevAtamaKomM : MongoDbEntity
    {
        public string GorevBasTarihi { get; set; }
        public string GorevBitisTarihi { get; set; }
        public string StenografId { get; set; }
        public GorevStatu GorevStatu { get; set; }
        public double StenoSure { get; set; }
        public string OturumId { get; set; }
        public string BirlesimId { get; set; }
        public double DifMin { get; set; }
        public bool ToplantiVar { get; set; }
        public IzınTuru StenoIzinTuru { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public int TurAdedi { get; set; }
        public bool StenoToplamSureAsım { get; set; }
        public int SatırNo { get; set; }
    }
}
