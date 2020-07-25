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

            string recipientUsername = commandParts[1];

            // TODO Check recipientUsername is valid

            decimal points;

            if (decimal.TryParse(commandParts[2], out points))
            {
                await loyaltyService.AwardPoints(recipientUsername, points);

                client.SendMessage(e.ChatMessage.Channel, $"Congratulations {recipientUsername}! You've received {points.ToString()} loyalty points!");
            }
            else
            {
                // Invalid # of points //
                client.SendMessage(e.ChatMessage.Channel, $"Invalid # of points to award.");
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
