using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Logging;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;
using DSharpPlus.Entities;

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
                Intents = DiscordIntents.All,
                MinimumLogLevel = LogLevel.Debug,
                LogTimestampFormat = "MMM dd yyyy - hh:mm:ss tt"
            });

            // Create a new command handler with the prefix of "!"
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                    StringPrefixes = new[] { "!" }
            });
            commands.RegisterCommands(Assembly.GetExecutingAssembly());
            
            var endpoint = new ConnectionEndpoint
            {
                Hostname = "127.0.0.1", // From server configuration
                Port = 2333 // From server configuration
            };

            var lavalinkConfig = new LavalinkConfiguration
            {
                Password = "scremmingpassword", // From server configuration
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint
            };

            var lavalink = discord.UseLavalink();

            // Connect to the discord bot and wait to prevent the console window from closing prematurely
            await discord.ConnectAsync();
            await lavalink.ConnectAsync(lavalinkConfig);
            await Task.Delay(-1);
        }
    }
}