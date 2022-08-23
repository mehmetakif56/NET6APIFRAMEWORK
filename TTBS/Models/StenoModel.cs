using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoModel
    {
        public Guid? Id { get; set; }
        public string AdSoyad { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public int SiraNo { get; set; }
        public DateTime? SonGorevTarihi { get; set; }
        public double HaftalikGorevSuresi { get; set; }
        public double YillikGorevSuresi { get; set; }
        public double GunlukGorevSuresi { get; set; }
        public bool StenoGorevDurum { get; set; }
        public int GorevStatu { get; set; }
        public Guid GrupId { get; set; }
        public IzınTuru StenoIzinTuru { get; set; }
        public bool ToplantiVar { get; set; }
    }
}
