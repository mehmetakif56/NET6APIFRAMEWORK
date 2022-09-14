using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoCreateModel
    {
        public Guid? Id { get; set; }
        public string AdSoyad { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public int SiraNo { get; set; }
        public Guid GrupId { get; set; }
        public Guid UserId { get; set; } = Guid.Empty;
    }
}
