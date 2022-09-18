using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

public class AdminCommands : BaseCommandModule
{
    [Command("adminscrem")]
    public async Task GreetCommand(CommandContext ctx)
    {
        await ctx.RespondAsync("This is me scremming but way louder and commanded by an admin");
    }
}
