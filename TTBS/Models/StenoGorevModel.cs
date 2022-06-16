using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevModel
    {
        public Guid? Id { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public DateTime? GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
        public Guid StenografId { get; set; }
        public string AdSoyad { get; set; }
        public Guid BirlesimId { get; set; }  
        public int SiraNo { get; set; }
        public DateTime? SonGorevTarihi { get; set; }
        public int SonGorevSuresi { get; set; }
        public GorevStatu GorevStatu { get; set; }
    }
}
