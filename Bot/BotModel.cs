using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.EventHandling;
using DSharpPlus.Interactivity.Extensions;
using Endy.Bot.Commands;
using Microsoft.Extensions.Configuration;

namespace Endy.Bot
{
    public class BotModel
    {
        private readonly DiscordClient _client;

        private readonly CommandsNextExtension _commands;

        private readonly IConfiguration _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

        internal BotModel(IServiceProvider services)
        {
            _client = new DiscordClient(new DiscordConfiguration
            {
                AutoReconnect = true,
                Token = _config["Bot:Auth:Token"],
                Intents = DiscordIntents.All,
            });

            _commands = _client.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { _config["Bot:Auth:Prefix"] },
                CaseSensitive = false,
                DmHelp = false,
                Services = services
            });

            _commands.RegisterCommands<ServerCommands>();

            _client.Ready += Handlers.OnReady;
            _commands.CommandErrored += Handlers.OnCommandErrored;

            _client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromSeconds(50)
            });

            _client.ConnectAsync(new DiscordActivity { ActivityType = ActivityType.Watching, Name = "on butterflies"});
            Task.Delay(-1);
        }
    }
}
