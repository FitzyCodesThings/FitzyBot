using FitzyBot.Core;
using FitzyBot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;

namespace FitzyBot.Application
{

    public class InMemoryLoyaltyService : ILoyaltyService
    {
        List<Person> people = new List<Person>();


        public async Task AwardPoints(string recipientUsername, int points)
        {       
            // Do something to award the points.... //
            var person = people.FirstOrDefault(p => p.TwitchUsername == recipientUsername);

            if (person == null)
            {
                people.Add(new Person()
                {
                    TwitchUsername = recipientUsername,
                    Balance = 0
                });
            }

            person = people.FirstOrDefault(p => p.TwitchUsername == recipientUsername);

            person.Balance += points;
        }

        public Task RemovePoints(string recipientUsername, int points)
        {
            throw new NotImplementedException();
        }

        public Task AddReward(Reward reward)
        {
            throw new NotImplementedException();
        }
        public Task SetRewardSupply(Guid rewardId, int supply)
        {
            throw new NotImplementedException();
        }

        public Task DisableReward(Guid rewardId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUserBalance(string username)
        {
            throw new NotImplementedException();
        }

        // General Purpose Functions //
        public Task<List<Reward>> GetRewards(bool includeDisabled = false)
        {
            throw new NotImplementedException();
        }

        // User Functions //
        public async Task<int> CheckBalance(string twitchUsername)
        {
            var person = people.FirstOrDefault(p => p.TwitchUsername == twitchUsername);

            if (person == null)            
                throw new Exception("Person does not exist.");

            return person.Balance;
        }
    }
}
