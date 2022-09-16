using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoKomisyonGrupModel
    {
        public Guid Id { get; set; }
        public Guid? GrupId { get; set; }
    }
}
