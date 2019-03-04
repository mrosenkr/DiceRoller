using DiceRoller;
using Discord.Commands;
using System.Threading.Tasks;

namespace Discord.Bot.DiceRoller.Modules
{
    public class Roll : ModuleBase<SocketCommandContext>
    {
        private Dice _dice;

        public Roll(Dice dice)
        {
            _dice = dice;
        }

        [Command("roll")]
        public async Task RollAsync(string command)
        {
            var result = _dice.Roll(command);

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
