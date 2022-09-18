using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DSharpPlus;
using Microsoft.Extensions.Logging;

namespace ArmadaBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            // create a variable getToken to store the environment variable "DiscordToken"
            string getToken = Environment.GetEnvironmentVariable(variable: "DiscordToken");


            // Enter in both the token for the application, the type of token, and the intents of what the application will do
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = getToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });

            new DiscordConfiguration()
            {
                MinimumLogLevel = LogLevel.Debug,
                LogTimestampFormat = "MMM dd yyyy - hh:mm:ss tt"
            };

            discord.MessageCreated += async (s, e) =>
            {

                // Take an incoming message and make it lowercase, then check to see if any of the listed conditions are met
                switch (e.Message.Content.ToLower())
                {
                    case "!ping":
                        await e.Message.RespondAsync("Pong!");
                        break;
                    case "!pong":
                        await e.Message.RespondAsync("Ping!");
                        break;
                    case "!screm":
                        await e.Message.RespondAsync("SCREM SCREM SCREM SCREM");
                        break;

                }
                if (s.CurrentUser.Id != e.Author.Id)
                {
                    if (e.Message.Content.ToLower().Contains("scromse"))
                    {
                        await e.Message.RespondAsync("THAT'S ILLEGAL, GO TO SCREM JAIL");
                    } 
                    else if (e.Message.Content.ToLower().Contains("coe"))
                    {
                        await e.Message.RespondAsync("Leefnie gets ear hurmt at the smallest screm, very much a coe");
                    }
                    else if (e.Message.Content.ToLower().Contains("foe"))
                    {
                        await e.Message.RespondAsync("Armada is definitely not a foe, he doesnt even scroe, smhhh");
                    }
                }


                // Old ineffecient way of processing specific outputs for a given message input.

                /*                if (e.Message.Content.ToLower().StartsWith("!ping"))
                                    await e.Message.RespondAsync("pong!");
                                else if (e.Message.Content.ToLower().StartsWith("!screm"))
                                    await e.Message.RespondAsync("SCREM SCREM SCREM SCREM");
                                else if (e.Message.Content.ToLower().StartsWith("!cum"))
                                    await e.Message.RespondAsync("*moans*");
                                else if (e.Message.Content.ToLower().Contains("scromse"))
                                    await e.Message.RespondAsync("THAT'S ILLEGAL, GO TO SCREM JAIL");
                                else if (e.Message.Content.ToLower().Contains("coe"))
                                    await e.Message.RespondAsync("Leefnie gets ear hurmt at the smallest screm, very much a coe");*/

            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}