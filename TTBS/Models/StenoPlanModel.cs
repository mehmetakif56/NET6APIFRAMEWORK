using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoPlanModel
    {
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public int StenoSayisi { get; set; }
        public int StenoSure { get; set; }
        public int UzmanStenoSure { get; set; }
        public int UzmanStenoSayisi { get; set; }
        public string Yeri { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; }
        public string Ad { get; set; }
        public List<SelectListItem> BirlesimList { get; set; }
        public List<SelectListItem> KomisyonList {get;set;}

    }
}
