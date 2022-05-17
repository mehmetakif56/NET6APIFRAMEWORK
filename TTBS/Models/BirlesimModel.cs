using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class BirlesimModel
    {
        public Guid Id { get; set; }
        public string BirlesimNo { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public Guid YasamaId { get; set; }
    }
}
