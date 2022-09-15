using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class BirlesimStenoGorevModel
    {
        public DateTime BasTarihi { get; set; }
        public Guid BirlesimId { get; set; }
        public ToplanmaBaslatmaStatu ToplanmaBaslatmaStatu { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public int SatırNo { get; set; }
        public int AcanSira { get; set; }
        public int KapatanSira { get; set; }
        public int AcanSiraUzman { get; set; }
        public int KapatanSiraUzman { get; set; }
    }
}
