using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;


public class Program
{
    private DiscordSocketClient _client;
    static void Main(string[] args)
    {
        Console.WriteLine("Token is " + Environment.GetEnvironmentVariable("DiscordToken"), EnvironmentVariableTarget.User);
        new Program().MainAsync().GetAwaiter().GetResult();
    }


    public async Task MainAsync()
    {

        // When working with events that have Cacheable<IMessage, ulong> parameters,
        // you must enable the message cache in your config settings if you plan to
        // use the cached message entity. 
        var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
        _client = new DiscordSocketClient(_config);


        //Console.WriteLine(Discord.permissi);
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken", EnvironmentVariableTarget.User));
        await _client.StartAsync();
        
        _client.Ready += async () =>
        {
            Console.WriteLine("ArmadaBot is connected!");
            _client.MessageReceived += Client_MessageReceived;
            var guild = _client.GetGuild(652617292768870450);
            var guildCommand = new SlashCommandBuilder();
            guildCommand.WithName("first-command");
            guildCommand.WithName("First slash command");

            var globalCommand = new SlashCommandBuilder();
            globalCommand.WithName("first-global-command");
            globalCommand.WithDescription("Global command woo");

            try
            {
                await guild.CreateApplicationCommandAsync(guildCommand.Build());
                await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());

                _client.SlashCommandExecuted += _client_SlashCommandExecuted;
            } catch (ApplicationCommandException e)
            {
                var json = JsonConvert.SerializeObject(e.Errors, Formatting.Indented);
                Console.Write(json);
            }
        };


        await Task.Delay(-1);
    }

    private async Task Client_MessageReceived(SocketMessage arg)
    {
        if (arg.Content == "!ping")
        {
            // Sends a message to the channel the message was received from
            await arg.Channel.SendMessageAsync("Pong!");
        }
    }

    private Task _client_SlashCommandExecuted(SocketSlashCommand command)
    {
        return command.RespondAsync($"You ran {command.Data.Name}");
    }

    private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
    {
        // If the message was not in the cache, downloading it will result in getting a copy of `after`.
        var message = await before.GetOrDownloadAsync();
        Console.WriteLine($"{message} -> {after}");
    }
}