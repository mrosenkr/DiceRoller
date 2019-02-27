using DR = DiceRoller;
using Discord.Commands;
using System.Threading.Tasks;

namespace Discord.Bot.DiceRoller.Modules
{
    public class Roll : ModuleBase<SocketCommandContext>
    {
        [Command("roll")]
        public async Task RollAsync(string command)
        {
            var result = DR.DiceRoller.Roll(command);

            string reply;

            if (result.Error)
            {
                reply = result.ErrorMessage;
            }
            else
            {
                reply = result.Answer.ToString();
            }

            await ReplyAsync(reply);
        }
    }
}
