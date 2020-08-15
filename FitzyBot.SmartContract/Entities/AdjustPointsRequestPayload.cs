using FitzyBot.SmartContract.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitzyBot.SmartContract.Entities
{
    public class AdjustPointsRequestPayload : Payload
    {
        public AdjustPointsRequestPayload()
        {
            Operation = OperationType.AdjustPoints;
        }

        [JsonProperty("targetUsername")]
        public string TargetUsername { get; set; }

        [JsonProperty("pointAdjustment")]
        public int PointAdjustment { get; set; }
    }
}
