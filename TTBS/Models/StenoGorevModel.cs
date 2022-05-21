﻿using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevModel
    {
        public Guid Id { get; set; }
        public DateTime? GörevTarihi { get; set; }
        public DateTime? GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
        public Guid StenografId { get; set; }
        public string AdSoyad { get; set; }
        public Guid StenoPlanId { get; set; }  
        public int SiraNo { get; set; }
        public DateTime? SonGorevTarihi { get; set; }
        public int SonGorevSuresi { get; set; }
    }
}
