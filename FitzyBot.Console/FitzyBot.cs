using FitzyBot.Core;
using FitzyBot.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace FitzyBot.ConsoleApp
{
    public partial class FitzyBot
    {
        TwitchClient client;        
        
        private readonly IOptions<TwitchConfigurationOptions> options;
        private readonly ILoyaltyService loyaltyService;

        List<string> currentViewers = new List<string>();

        public FitzyBot(IOptions<TwitchConfigurationOptions> options, ILoyaltyService loyaltyService)
        {               
            this.options = options;
            this.loyaltyService = loyaltyService;

            ConnectionCredentials credentials = new ConnectionCredentials(options.Value.TwitchUsername, options.Value.TwitchOAuth);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, options.Value.TwitchChannel);

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;

            client.OnUserJoined += Client_OnUserJoined;
            client.OnUserLeft += Client_OnUserLeft;
        }

        public void Run() => client.Connect();

        private void Client_OnLog(object sender, OnLogArgs e)
        {   
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        private void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
        {            
            if (!currentViewers.Contains(e.Username))
                currentViewers.Add(e.Username);
        }

        private void Client_OnUserLeft(object sender, OnUserLeftArgs e)
        {
            currentViewers.Remove(e.Username);
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            var message = e.ChatMessage.Message.ToLower();

            if (message.StartsWith("!fbtawardall"))
            {
                _ = this.HandleAwardPointsAllUsers(client, sender, e);
            }
            else if (message.StartsWith("!fbtaward"))
            {
                _ = this.HandleAwardPoints(client, sender, e);
            }
            else if (message.StartsWith("!fbtremovepoints"))
            {
                _ = this.HandleRemovePoints(client, sender, e);
            }
            else if (message.StartsWith("!fbtbalance"))
            {
                _ = this.HandleCheckBalance(client, sender, e);
            }
            else if (message.StartsWith("!fbt"))
            {
                client.SendMessage(e.ChatMessage.Channel, $"Don't know what you're trying to do there, boss... (invalid command)");
            }
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            if (e.WhisperMessage.Username == "my_friend")
                client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            else
                client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }
    }
}

