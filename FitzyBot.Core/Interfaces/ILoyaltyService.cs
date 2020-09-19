using FitzyBot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;

namespace FitzyBot.Core
{
    public interface ILoyaltyService
    {
        // Administrative Functions //
        public Task<int> AwardPoints(string executedByUsername, string twitchUsername, int points);

        public Task<int> RemovePoints(string executedByUsername, string twitchUsername, int points);

        public Task AddReward(Reward reward);

        public Task SetRewardSupply(Guid rewardId, int supply);

        public Task DisableReward(Guid rewardId);

        public Task<int> GetUserBalance(string twitchUsername);

        // General Functions //
        public Task<List<Reward>> GetRewards(bool includeDisabled = false);

        // User Functions //
        public Task<int> CheckBalance(string twitchUsername);
    }
}
