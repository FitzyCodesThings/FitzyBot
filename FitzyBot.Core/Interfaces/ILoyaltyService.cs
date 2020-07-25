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
        public Task AwardPoints(string recipientUsername, decimal points);

        public Task<decimal> CheckBalance(string twitchUsername);
    }
}
