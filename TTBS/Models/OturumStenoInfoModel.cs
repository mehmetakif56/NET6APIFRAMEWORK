using TTBS.Core.Entities;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class OturumStenoInfoModel
    {
        public Guid BirlesimId { get; set; }
        public Guid OturumId { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public Guid? AcanStenografId { get; set; }
        public string? AcanStenograf { get; set; }
        public Guid? KapatanStenografId { get; set; }
        public string? KapatanStenograf { get; set; }       
        public string? AcanUzman { get; set; }
        public Guid? AcanUzmanId { get; set; }
        public string? KapatanUzman { get; set; }
        public Guid? KapatanUzmanId { get; set; }

        public int? KapatanSiraUzman { get; set; }
        public int? AcanSira { get; set; }
        public int? KapatanSira { get; set; }
        public int? AcanSiraUzman { get; set; }

    }
}
