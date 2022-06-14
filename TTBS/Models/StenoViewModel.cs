using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoViewModel
    {
        public Guid Id { get; set; }
        public string AdSoyad { get; set; }
        public int SonGorevSuresi { get; set; }

    }
}
