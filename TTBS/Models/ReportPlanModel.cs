﻿using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class ReportPlanModel
    {
        public PlanTuru GorevTuru { get; set; }
        public int  UzmGorevlendirmeSay { get; set; }
        public int UzmGorevlendirmeSure { get; set; }
        public int GorevlendirmeSure { get; set; }
        public int GorevlendirmeSay { get; set; }
    }
}