using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;
using System.Text;
using System.Threading.Tasks;

namespace Endy.Bot.Commands
{
    public class ServerCommands : BaseCommandModule
    {
        [Command("info")]
        [Aliases("i")]
        public async Task Info(CommandContext ctx, DiscordMember member)
        {
            var roles = new StringBuilder();
            foreach (var role in member.Roles)
                roles.Append($"{role.Name}, ");

            var embed = new DiscordEmbedBuilder
            {
                Title = "Information card. Summary",
                Color = DiscordColor.HotPink,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"Thanks for using Endy, {ctx.Member.Username}",
                    IconUrl = ctx.Member.AvatarUrl
                },
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = member.AvatarUrl }

            }
            .AddField("Name", member.Username, true)
            .AddField("Server name", member.DisplayName, true)
            .AddField("Discriminator", member.Discriminator, true)
            .AddField("Color", member.Color.ToString(), true)
            .AddField("Joined at", member.JoinedAt.DateTime.ToShortDateString(), true)
            .AddField("Premium", member.PremiumSince is null ? "no" : $"since {member.PremiumSince.Value.DateTime.ToShortDateString()}", true)
            .AddField("Is muted", member.IsMuted.ToString(), true)
            .AddField("Is owner", member.IsOwner.ToString(), true)
            .AddField("Is bot", member.IsBot.ToString(), true)
            .AddField("Current server", member.Guild.Name, true)
            .AddField("Status", member.Presence.Status.ToString(), true)
            //.AddField("Activity", member.Presence.Activity.Name ?? "no activity", true)
            .AddField("Roles", roles.ToString(), false)
            .AddField("Avatar url", member.AvatarUrl ?? "absent")
            .AddField("Guild avatar url", member.Guild.IconUrl ?? "absent");

            await ctx.Channel.SendMessageAsync(embed)
                .ConfigureAwait(false);
        }

        [Command("clear")]
        [Aliases("c")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task Clear(CommandContext ctx, int amount)
        {
            try
            {
                await ctx.Channel.DeleteMessagesAsync(
                    await ctx.Channel.GetMessagesAsync(amount + 1).ConfigureAwait(false)
                ).ConfigureAwait(false);

                var message = await ctx.Channel.SendMessageAsync(
                    new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Blue,
                        Description = "The deletion operation has been completed successfully.\nThis message will be automatically removed in 5 second"
                    }
                ).ConfigureAwait(false);

                await Task.Delay(5000);
                await ctx.Channel.DeleteMessageAsync(message);

            }
            catch (BadRequestException)
            {
                await ctx.Channel.SendMessageAsync("you cannot delete messages that have a lifetime of more than 14 days")
                    .ConfigureAwait(false);
            }
        }
    }
}

