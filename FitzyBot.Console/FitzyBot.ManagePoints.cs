using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;

namespace FitzyBot.ConsoleApp
{
    public partial class FitzyBot 
    {
        public async Task HandleAwardPoints(TwitchClient client, object sender, OnMessageReceivedArgs e)
        {
            if (
                e.ChatMessage.UserType != UserType.Broadcaster &&
                e.ChatMessage.UserType != UserType.Moderator
            )
                return;
            
            string[] commandParts = e.ChatMessage.Message.Split(' ');

            string twitchUsername = commandParts[1];

            if (!currentViewers.Contains(twitchUsername))
            {
                client.SendMessage(e.ChatMessage.Channel, "User is not currently in the chat.");
                return;
            }

            int points;

            if (int.TryParse(commandParts[2], out points))
            {
                int newBalance = await loyaltyService.AwardPoints(twitchUsername, points);

                client.SendMessage(e.ChatMessage.Channel, $"Congratulations {twitchUsername}! You've received {points.ToString()} loyalty points! Your balance is currently {newBalance} points!");
            }
            else
            {
                // Invalid # of points //
                client.SendMessage(e.ChatMessage.Channel, $"Invalid # of points to award (must specify whole # of points only).");
            }
            
        }

        public async Task HandleAwardPointsAllUsers(TwitchClient client, object sender, OnMessageReceivedArgs e)
        {
            if (
                e.ChatMessage.UserType != UserType.Broadcaster &&
                e.ChatMessage.UserType != UserType.Moderator
            )
                return;

            string[] commandParts = e.ChatMessage.Message.Split(' ');

            int points;

            if (int.TryParse(commandParts[1], out points))
            {
                foreach (string twitchUsername in currentViewers)
                {
                    int newBalance = await loyaltyService.AwardPoints(twitchUsername, points);                    
                }

                client.SendMessage(e.ChatMessage.Channel, $"You get a point! You get a point! YOU get a point! ERR'BODY received {points.ToString()} loyalty points!");
            }
            else
            {
                // Invalid # of points //
                client.SendMessage(e.ChatMessage.Channel, $"Invalid # of points to award (must specify whole # of points only).");
            }

        }

        public async Task HandleRemovePoints(TwitchClient client, object sender, OnMessageReceivedArgs e)
        {
            if (
                e.ChatMessage.UserType != UserType.Broadcaster &&
                e.ChatMessage.UserType != UserType.Moderator
            )
                return;

            string[] commandParts = e.ChatMessage.Message.Split(' ');

            string twitchUsername = commandParts[1];

            // TODO Check twitchUsername is valid

            int points;

            if (int.TryParse(commandParts[2], out points))
            {
                try
                {
                    int newBalance = await loyaltyService.RemovePoints(twitchUsername, points);

                    client.SendMessage(e.ChatMessage.Channel, $"{points} points were removed from {twitchUsername}. Your new balance is {newBalance} points.");
                }
                catch (Exception ex)
                {
                    client.SendMessage(e.ChatMessage.Channel, $"Could not remove points from {twitchUsername} because: {ex.Message}");
                }
            }
            else
            {
                // Invalid # of points //
                client.SendMessage(e.ChatMessage.Channel, $"Invalid # of points to award (must specify whole # of points only).");
            }

        }

        public async Task HandleCheckBalance(TwitchClient client, object sender, OnMessageReceivedArgs e)
        {
            try
            {
                var balance = await loyaltyService.CheckBalance(e.ChatMessage.Username);

                client.SendMessage(e.ChatMessage.Channel, $"{e.ChatMessage.Username} currently has {balance} loyalty points!");
            }
            catch (Exception)
            {
                client.SendMessage(e.ChatMessage.Channel, $"You've never been given points.");
            }
        }
    }
}
