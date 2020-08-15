using FitzyBot.SmartContract.Entities;
using FitzyBot.SmartContract.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitzyBot.SmartContract
{
    public class CommandConverter : JsonConverter
    {   
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken commandObject = JToken.ReadFrom(reader);
            string operation = commandObject["payload"]["operation"].ToString();
            string source = commandObject["payload"]["commandSource"].ToString();

            Command command = new Command();

            switch (operation)
            {
                case "adjustPoints":
                    command.Payload = new AdjustPointsRequestPayload();
                    break;
                case "addReward":
                    command.Payload = new AddRewardRequestPayload();
                    break;
                default:
                    throw new Exception("Unsupported operation requested");
            }

            switch (source)
            {
                case "twitch":
                    command.Payload.CommandSource = SourceServiceType.Twitch;
                    break;
                case "discord":
                    command.Payload.CommandSource = SourceServiceType.Discord;
                    break;
                default:
                    throw new Exception("Unsupported command source");
            }

            serializer.Populate(commandObject.CreateReader(), command);

            return command;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
