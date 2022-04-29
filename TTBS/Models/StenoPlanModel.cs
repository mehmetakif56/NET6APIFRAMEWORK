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
        public string GorevYeri { get; set; }
        public string GorevAd { get; set; }
        public SelectListItem BirlesimList { get; set; }

        private SelectListItem _komisyonList;
        public SelectListItem KomisyonList
        {
            get { return _komisyonList; }
            set { if (value == null) _komisyonList = new SelectListItem { }; }
        }
        private Guid? _komisyonId { get; set; }
        public Guid? KomisyonId
        {
            get { return _komisyonId; }
            set
            {
                if (KomisyonList.Value == null)
                    _komisyonId = null;
                else
                    _komisyonId = new Guid(KomisyonList.Value);
            }
        }

        public SelectListItem GorevList { get; set; }

    }
}
