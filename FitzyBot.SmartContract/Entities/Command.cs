using Newtonsoft.Json;

namespace FitzyBot.SmartContract.Entities
{
    [JsonConverter(typeof(CommandConverter))]
    public class Command
    {
        [JsonProperty("payload")]
        public Payload Payload { get; set; }
    }
}
