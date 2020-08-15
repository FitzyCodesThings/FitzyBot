﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FitzyBot.Core.Entities
{
    public class PointAdjustmentResponse
    {
        public string executedByUsername { get; set; }

        public string operation { get; set; } // Award points, remove points, spend points, etc

        public string targetTwitchUsername { get; set; }

        public int pointAdjustment { get; set; }

        public int newBalance { get; set; }
    }
}
