﻿using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class BirlesimViewModel
    {
        public DateTime? BaslangicTarihi { get; set; }
        public string Yeri { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; }
        public string ToplanmaDurumu { get; set; }

    }
}