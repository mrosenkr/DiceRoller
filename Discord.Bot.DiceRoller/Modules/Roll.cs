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
                reply = RollOutput(result, command);
            }

            await ReplyAsync(reply);
        }

        private string RollOutput(RollResult result, string command)
        {
            string username = this.Context.User.Username;
            string equation = result.EquationDisplay;
            string answer = result.Answer.ToString();

            int length = username.Length + command.Length + equation.Length + answer.Length;

            // output is capped at 2000 characters for discord
            string reply = string.Format("{0} rolling: `{1}` \n {2} = {3}",
                username,
                command,
                length < 1900 ? "`" + equation + "` \n" : string.Empty,
                answer);

            // last ditch effort to give a result
            if (reply.Length > 2000)
            {
                reply = string.Format("{0} rolled: {1}", username, answer);
            }

            // give up
            if (reply.Length > 2000)
            {
                reply = "Equation output too long.";
            }

            return reply;
        }
    }
}
