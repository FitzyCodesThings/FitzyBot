using FitzyBot.Core;
using FitzyBot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitzyBot.Application.Services
{
    public class SimpleDragonLoyaltyService : ILoyaltyService
    {        

        public Task<int> AwardPoints(string twitchUsername, int points)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemovePoints(string twitchUsername, int points)
        {
            throw new NotImplementedException();
        }

        public Task<int> CheckBalance(string twitchUsername)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUserBalance(string twitchUsername)
        {
            throw new NotImplementedException();
        }

        public Task AddReward(Reward reward)
        {
            throw new NotImplementedException();
        }

        public Task DisableReward(Guid rewardId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reward>> GetRewards(bool includeDisabled = false)
        {
            throw new NotImplementedException();
        }

        public Task SetRewardSupply(Guid rewardId, int supply)
        {
            throw new NotImplementedException();
        }
    }
}
