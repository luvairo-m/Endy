using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.EventHandling;
using DSharpPlus.Interactivity.Extensions;
using System.Collections.Generic;
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

            var first_embed = new DiscordEmbedBuilder
            {
                Title = "Information card. Summary",
                Color = DiscordColor.HotPink
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
            .AddField("Roles", roles.ToString(), false);

            var second_embed = new DiscordEmbedBuilder
            {
                Title = "Information card. Urls",
                Color = DiscordColor.HotPink
            }
            .AddField("Avatar url", member.AvatarUrl ?? "absent")
            .AddField("Guild avatar url", member.Guild.IconUrl ?? "absent");

            await ctx.Channel.SendPaginatedMessageAsync(
                    member, new List<Page>() {
                        new Page { Embed = first_embed },
                        new Page { Embed = second_embed }
                    },
                    new PaginationButtons
                    {
                        Right = new DiscordButtonComponent(
                                ButtonStyle.Primary, "right", "Next page"
                            ),
                        Left = new DiscordButtonComponent(
                                ButtonStyle.Primary, "left", "Previous page"
                            ),
                        Stop = new DiscordButtonComponent(
                                ButtonStyle.Danger, "stop", "Stop reading"
                            ),
                        SkipLeft = null,
                        SkipRight = null,
                    }, deletion: ButtonPaginationBehavior.DeleteButtons);
        }
    }
}

