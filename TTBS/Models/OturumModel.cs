using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class OturumModel
    {
        public Guid? Id { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public Guid BirlesimId { get; set; }
    }
}
