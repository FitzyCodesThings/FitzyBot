# FitzyBot

My experiments in creating a Twitch bot using the TwitchLib library.

Goals are (currently) to:

1. Support a custom loyalty points program (award, check balances, redeem)
2. Support a giveaway mechanism
3. Support a variety of storage models (EntityFramework, Azure Cosmos DB, Dragonchain blockchain solution)

Stretch goal: make it work across multiple channels (Team-based loyalty programs)

## How to Test It
After cloning, edit `appsettings.json` (or better yet, copy the contents of appsettings.json into your "user secrets" file so credentials don't get stored in your repo).

The `TwitchChannel` setting should be the actual Twitch channel you want the bot to work in.

The `TwitchUsername` setting should be your username (or the username of a bot account you control that you want to use instead).

The `TwitchOAuth` setting should be the OAuth identifier you get when you visit (https://twitchapps.com/tmi/) (as found in the [official twitch docs](https://dev.twitch.tv/docs/irc)).

Then run the app and you should see your bot connect to your chat!