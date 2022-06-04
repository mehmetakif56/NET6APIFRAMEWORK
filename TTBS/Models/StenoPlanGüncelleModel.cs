using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoPlanGüncelleModel
    {
        public Guid Id { get; set; }
        public DateTime? PlanlananBaslangicTarihi { get; set; }
        public DateTime? PlanlananBitisTarihi { get; set; }
        public DateTime? GerceklesenBaslangicTarihi { get; set; }
        public DateTime? GerceklesenBitisTarihi { get; set; }
        public int StenoSayisi { get; set; }
        public int StenoSure { get; set; }
        public int UzmanStenoSure { get; set; }
        public int UzmanStenoSayisi { get; set; }
        public string GorevYeri { get; set; }
        public string GorevAd { get; set; }
        public Guid? BirlesimId { get; set; }
        public Guid? KomisyonId { get; set; }
        public Guid? YasamaId { get; set; }     
        public PlanTuru GorevTuru { get; set; }
        public PlanStatu GorevStatu { get; set; }

    }
}
