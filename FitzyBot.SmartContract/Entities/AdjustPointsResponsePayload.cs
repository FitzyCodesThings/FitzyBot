using FitzyBot.SmartContract.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitzyBot.SmartContract.Entities
{
    public class AdjustPointsResponsePayload : Payload
    {
        public AdjustPointsResponsePayload()
        {
            Operation = OperationType.AdjustPoints;
        }

        [JsonProperty("targetUsername")]
        public string TargetUsername { get; set; }

        [JsonProperty("pointAdjustment")]
        public int PointAdjustment { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }
    }
}
