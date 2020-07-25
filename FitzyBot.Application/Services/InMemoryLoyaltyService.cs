using FitzyBot.Core;
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
        
        public async Task AwardPoints(string recipientUsername, decimal points)
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

        public async Task<decimal> CheckBalance(string twitchUsername)
        {
            var person = people.FirstOrDefault(p => p.TwitchUsername == twitchUsername);

            if (person == null)            
                throw new Exception("Person does not exist.");

            return person.Balance;
        }
    }
}
