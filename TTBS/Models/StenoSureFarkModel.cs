using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoSureFarkModel
    {
        public Guid? Id { get; set; }
        public string AdSoyad { get; set; }
        public double Sure { get; set; }

        public Guid BirlesimId { get; set; }

    }
}
