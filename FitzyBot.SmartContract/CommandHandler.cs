using DragonchainSDK;
using FitzyBot.SmartContract.Entities;
using FitzyBot.SmartContract.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FitzyBot.SmartContract
{     
    public class CommandHandler
    {
        public async Task<string> Handle(string input)
        {
            TextWriter errorWriter = Console.Error;

            Command inputObj = JsonConvert.DeserializeObject<Command>(input);

            AdjustPointsRequestPayload requestObj = (AdjustPointsRequestPayload) inputObj.Payload;

            AdjustPointsResponsePayload responseObj = null;

            switch (inputObj.Payload.Operation)
            {
                case OperationType.AdjustPoints:
                    responseObj = await this.AdjustPoints(requestObj.CommandSource, requestObj.ExecutedByUsername, requestObj.TargetUsername, requestObj.PointAdjustment);
                    break;                
            }

            IDictionary<string, object> responsePayload = new ExpandoObject();

            responsePayload["response"] = responseObj;

            string userSource = requestObj.CommandSource == SourceServiceType.Twitch ? "twitchuser" : "discorduser";

            // TODO Switch to Person class to capture all detail in state of user
            responsePayload[$"{userSource}_{requestObj.TargetUsername}"] = new { balance = responseObj.Balance };

            return JsonConvert.SerializeObject(responsePayload);
        }

        public async Task<AdjustPointsResponsePayload> AdjustPoints(SourceServiceType userSource, string executedByUsername, string targetUsername, int pointAdjustment)
        {
            int balance = 0;
            try
            {
                balance = await GetUserBalance(userSource, targetUsername);
            }
            catch (Exception)
            {
                balance = 0;
            }

            // TODO Be sure (if points are negative) that user has sufficient balance // 

            var response = new AdjustPointsResponsePayload
            {         
                CommandSource = userSource,
                ExecutedByUsername = executedByUsername,
                TargetUsername = targetUsername,
                PointAdjustment = pointAdjustment,
                Balance = balance + pointAdjustment
            };

            return response;
        }

        public async Task<int> GetUserBalance(SourceServiceType userSource, string userName)
        {
            // Use Dragonchain client to get user object off of the heap and return the balance //
            
            await Task.Run(() => null);

            return 0;
        }
    }
}
