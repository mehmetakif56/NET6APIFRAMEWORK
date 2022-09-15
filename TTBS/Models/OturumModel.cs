using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class OturumModel
    {
        public Guid? Id { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public Guid BirlesimId { get; set; }
        public string Acan { get; set; }
        public string Kapatan { get; set; }
        public string AcanUzman { get; set; }
        public string KapatanUzman { get; set; }
    }
}
