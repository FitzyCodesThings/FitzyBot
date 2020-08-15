using FitzyBot.SmartContract.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitzyBot.SmartContract.Entities
{
    public class AddRewardRequestPayload : Payload
    {
        public AddRewardRequestPayload()
        {
            Operation = OperationType.AddReward;
        }

        [JsonProperty("rewardName")]
        public string RewardName { get; set; }

        [JsonProperty("rewardDescription")]
        public string RewardDescription { get; set; }

        [JsonProperty("rewardCost")]
        public int RewardCost { get; set; }
    }
}
