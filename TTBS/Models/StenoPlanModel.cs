using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoPlanModel
    {
        public Guid? Id { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public int StenoSayisi { get; set; }
        public int StenoSure { get; set; }
        public int UzmanStenoSure { get; set; }
        public int UzmanStenoSayisi { get; set; }
        public string GorevYeri { get; set; }
        public string GorevAd { get; set; }
        public SelectListItem BirlesimList { get; set; }
        private SelectListItem _komisyonList { get; set; }
        public SelectListItem KomisyonList
        {
            get { return _komisyonList; }
            set { if (value != null) _komisyonList = value; _komisyonList = new SelectListItem { }; }
        }
        private SelectListItem _altKomisyonList { get; set; }
        public SelectListItem AltKomisyonList
        {
            get { return _altKomisyonList; }
            set { if (value != null) _altKomisyonList = value; _altKomisyonList = new SelectListItem { }; }
        }
        public PlanTuru GorevTuru { get; set; }
        public PlanStatu GorevStatu { get; set; }
    }
}
