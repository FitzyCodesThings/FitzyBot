﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FitzyBot.Core.Entities
{
    public class PointAdjustmentRequest
    {
        public string commandSource { get; set; }
        public string operation { get; set; } // Award points, remove points, spend points, etc
        public string executedByUsername { get; set; }
        public string targetUsername { get; set; }
        public int pointAdjustment { get; set; }
    }
}
