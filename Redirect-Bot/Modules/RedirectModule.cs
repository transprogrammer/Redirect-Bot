using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace InteractionFramework.Modules;

public class RedirectModule: InteractionModuleBase<SocketInteractionContext>
{
    // Dependencies can be accessed through Property injection, public properties with public setters will be set by the service provider
    public InteractionService Commands { get; set; }

    private InteractionHandler _handler;
    
    // Constructor injection is also a valid way to access the dependencies
    public RedirectModule(InteractionHandler handler)
    {
        _handler = handler;
    }

    [SlashCommand("redirect", "Redirects a user to a channel")]
    public async Task RedirectAsync(
        [Summary("channel", "The channel to redirect to")] ISocketMessageChannel channel,
        [Summary("message", "The message display to the users")] string message = "Redirecting you to another channel")
    {
        // The context is automatically injected into the method parameters
        var origChannelMessage = await Context.Channel.SendMessageAsync($"{message} <#{channel.Id}>");
        var newmsg=  await channel.SendMessageAsync($"Redirected from <#{Context.Channel.Id}> message: {origChannelMessage.GetJumpUrl()}"); 
        await origChannelMessage.ModifyAsync(x => x.Content = $"{message} <#{channel.Id}> message: {newmsg.GetJumpUrl()}");
        await  RespondAsync("Redirection Created");
    }
    
    
    
}