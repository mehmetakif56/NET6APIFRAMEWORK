﻿using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class GrupModel
    {
        public Guid? Id { get; set; }
        public string Ad { get; set; }
        public StenoGorevTuru StenoGrupTuru { get; set; }
        public bool gidenGrup { get; set; }
    }
}
