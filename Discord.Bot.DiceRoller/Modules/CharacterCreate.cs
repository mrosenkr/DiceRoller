using DiceRoller;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Bot.DiceRoller.Modules
{
    public class CharacterCreate : ModuleBase<SocketCommandContext>
    {
        private Dice _dice;
        string[] _Stats = { "STR", "DEX", "CON", "INT", "WIL", "CHA", "PER" };

        public CharacterCreate(Dice dice)
        {
            _dice = dice;
        }

        [Command("rfc")]
        public async Task RollForCharacterAsync()
        {
            StringBuilder sb = new StringBuilder();
            string rollStat = "4d6kh3";
            string username = this.Context.User.Username;

            sb.AppendFormat("{0} rolling character\n", username);

            foreach (var stat in _Stats)
            {
                sb.AppendFormat("{0}:{1}, ", stat, (int)_dice.Roll(rollStat).Answer);
            }

            await ReplyAsync(sb.ToString().TrimEnd(new char[] { ',', ' ' }));
        }
    }
}
