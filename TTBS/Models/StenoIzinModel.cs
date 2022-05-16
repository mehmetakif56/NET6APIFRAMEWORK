using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoIzinModel
    {
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        //public DateTime? BaslangicSaati { get; set; }
        //public DateTime? BitisSaati { get; set; }
        public IzınTuru IzınTuru { get; set; }
        public string AdSoyad { get; set; }

    }
}
