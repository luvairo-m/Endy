using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace Endy.Bot
{
    static class Handlers
    {
        internal static async Task OnCommandError(CommandsNextExtension _, CommandErrorEventArgs e)
        {
            var errors = ((ChecksFailedException)e.Exception).FailedChecks;
            foreach (var error in errors)
            {
                if (error is RequirePermissionsAttribute)
                {
                    await e.Context.Channel.SendMessageAsync("administrator rights are required")
                        .ConfigureAwait(false);
                }
            }
        }

        internal static Task OnCommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            Console.WriteLine(e.Exception);
            return Task.CompletedTask;
        }

        internal static Task OnReady(DiscordClient sender, ReadyEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"/// [Bot {sender.CurrentUser.Username} has been successfully launched]");
            Console.ResetColor();
            Console.Beep();

            return Task.CompletedTask;
        }
    }
}
