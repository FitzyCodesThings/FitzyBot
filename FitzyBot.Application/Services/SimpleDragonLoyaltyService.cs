using DragonchainSDK;
using FitzyBot.Core;
using FitzyBot.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitzyBot.Application.Services
{
    public class SimpleDragonLoyaltyService : ILoyaltyService
    {
        private readonly IDragonchainClient dcClient;

        public SimpleDragonLoyaltyService(IDragonchainClient dcClient)
        {
            this.dcClient = dcClient;
        }

        public async Task<int> AwardPoints(string executedByUsername, string twitchUsername, int points)
        {
            int balance;
            try
            {
                balance = await CalculateUserBalance(twitchUsername);
            }
            catch (Exception)
            {
                balance = 0;
            }

            var payload = new 
            {
                twitchUsername = twitchUsername,                
                pointAdjustment = points
            };

            await this.dcClient.CreateTransaction("dragonloyaltysimple", payload, twitchUsername);

            return balance + points;
        }

        public async Task<int> RemovePoints(string executedByUsername, string twitchUsername, int points)
        {
            int balance;

            try
            {
                balance = await CalculateUserBalance(twitchUsername);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (balance < points)
                throw new Exception("User does not have that many points.");

            var payload = new
            {
                twitchUsername = twitchUsername,
                pointAdjustment = points
            };

            try
            {
                await this.dcClient.CreateTransaction("dragonloyaltysimple", payload, twitchUsername);
            }
            catch (Exception)
            {
                throw;
            }
            
            return balance - points;
        }

        public async Task<int> CheckBalance(string twitchUsername)
        {
            return await CalculateUserBalance(twitchUsername);
        }

        public async Task<int> GetUserBalance(string twitchUsername)
        {
            throw new NotImplementedException();
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

        // Helpers //
        private async Task<int> CalculateUserBalance(string twitchUsername)
        {
            var queryResponse = await this.dcClient.QueryTransactions("dragonloyaltysimple", $"@tag:{twitchUsername}");

            if (queryResponse.Response.Results.Count() == 0)
                throw new Exception("User has not received points.");

            int currentBalance = 0;

            // TODO Verify success of call 
            foreach (var txn in queryResponse.Response.Results)
            {
                string json = JsonConvert.SerializeObject(txn.Payload);

                var transaction = JsonConvert.DeserializeObject<SimpleDragonchainTransaction>(json);

                currentBalance += transaction.PointAdjustment;
            }

            return currentBalance;
        }

    }
}
