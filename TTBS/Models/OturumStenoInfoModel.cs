using TTBS.Core.Entities;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class OturumStenoInfoModel
    {
        public Guid BirlesimId { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }        
        public Stenograf? AcanStenograf { get; set; }
        public Stenograf? KapatanStenograf { get; set; }
        public Stenograf? AcanSiraUzman { get; set; }
        public Stenograf? KapatanSiraUzman { get; set; }

    }
}
