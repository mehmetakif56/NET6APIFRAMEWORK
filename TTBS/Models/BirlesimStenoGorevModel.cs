using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class BirlesimStenoGorevModel
    {
        public DateTime BasTarihi { get; set; }
        public Guid OturumId { get; set; }
        public Guid BirlesimId { get; set; }
        public ToplanmaBaslatmaStatu ToplanmaBaslatmaStatu { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; }
    }
}
