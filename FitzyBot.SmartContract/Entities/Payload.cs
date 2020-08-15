using FitzyBot.SmartContract.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitzyBot.SmartContract.Entities
{    
    public abstract class Payload
    {
        [JsonProperty("commandSource")]
        public SourceServiceType CommandSource { get; set; }

        [JsonProperty("operation")]
        public OperationType Operation { get; set; }

        [JsonProperty("executedByUsername")]
        public string ExecutedByUsername { get; set; }
    }
}
