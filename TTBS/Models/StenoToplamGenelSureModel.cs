using TTBS.Core.Entities;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoToplamGenelSureModel
    {
        public Guid Id { get; set; }
        public Guid BirlesimId { get; set; }
        public string BirlesimAd { get; set; }
        public ToplanmaTuru ToplantiTur { get; set; }
        public Guid YasamaId { get; set; }
        public Guid GroupId { get; set; }
        public Guid StenografId { get; set; }
        public string StenografAd { get; set; }
        public DateTime Tarih { get; set; }
        public double Sure { get; set; }
    }
}
