using DragonchainSDK;
using FitzyBot.Core;
using FitzyBot.Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FitzyBot.Application.Services
{
    public class DragonchainContractLoyaltyService : ILoyaltyService
    {
        private readonly IDragonchainClient dcClient;
        private readonly ILogger logger;

        public DragonchainContractLoyaltyService(IDragonchainClient dcClient)
        {
            this.dcClient = dcClient;
        }

        public async Task<int> AwardPoints(string executedByUsername, string twitchUsername, int points)
        {
            int balance;
            try
            {
                balance = await GetUserBalance(twitchUsername);
            }
            catch (Exception ex)
            {
                balance = 0;
            }

            var payload = new PointAdjustmentRequest
            {
                commandSource = "Twitch",
                operation = "adjustPoints",
                executedByUsername = executedByUsername,
                targetUsername = twitchUsername,                
                pointAdjustment = points
            };

            await this.dcClient.CreateTransaction("fitzybot_contract", payload, twitchUsername);

            return balance + points;
        }

        public async Task<int> RemovePoints(string executedByUsername, string twitchUsername, int points)
        {
            int balance;
            try
            {
                balance = await GetUserBalance(twitchUsername);
            }
            catch (Exception ex)
            {
                balance = 0;
            }

            if (balance < points)
                throw new Exception("User does not have that many points.");

            var payload = new PointAdjustmentRequest
            {
                commandSource = "Twitch",
                operation = "adjustPoints",
                executedByUsername = executedByUsername,
                targetUsername = twitchUsername,
                pointAdjustment = points * -1
            };

            try
            {
                await this.dcClient.CreateTransaction("fitzybot_contract", payload, twitchUsername);
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return balance - points;
        }

        public async Task<int> CheckBalance(string twitchUsername)
        {
            return await GetUserBalance(twitchUsername);
        }

        public async Task<int> GetUserBalance(string twitchUsername)
        {
            try
            {
                string commandSource = "Twitch";
                string targetUsername = twitchUsername;
                string key = $"{commandSource}-{targetUsername}";
                string contractId = "37a95460-9cfe-4b62-bb0a-b0928ecc7d00";

                string url = $"https://fct.dev/.netlify/functions/GetFitzyBotUserObject?commandSource={commandSource}&targetUsername={targetUsername}&contractId={contractId}";

                var client = new HttpClient();

                var responseString = await client.GetStringAsync(url);

                //{ "status":200,"response":"{\"commandSource\":\"Twitch\",\"userName\":\"jamie10298\",\"balance\":2000}","ok":true}

                dynamic responseObject = JsonConvert.DeserializeObject<ExpandoObject>(responseString, new ExpandoObjectConverter());

                if (responseObject.status != 200)
                    throw new Exception("User not found.");

                dynamic userObject = JsonConvert.DeserializeObject<ExpandoObject>(responseObject.response, new ExpandoObjectConverter());

                // TODO Move contract ID to config
                //var response = await this.dcClient.GetSmartContractObject(key, contractId);

                return (int) userObject.balance;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddReward(Reward reward)
        {
            throw new NotImplementedException();
        }

        public async Task DisableReward(Guid rewardId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reward>> GetRewards(bool includeDisabled = false)
        {
            throw new NotImplementedException();
        }

        public async Task SetRewardSupply(Guid rewardId, int supply)
        {
            throw new NotImplementedException();
        }

    }
}
