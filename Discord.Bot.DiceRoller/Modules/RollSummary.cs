using DiceRoller.Models;
using Discord.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Bot.DiceRoller.Modules
{
    public class RollSummary : ModuleBase<SocketCommandContext>
    {
        private DiceStats _stats;

        public RollSummary(DiceStats stats)
        {
            _stats = stats;
        }

        [Command("RollSummary")]
        public async Task DiceSummaryAynsc(params string[] args)
        {
            int reportSize = 0;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out reportSize);
            }

            string tab = "\t";
            StringBuilder output = new StringBuilder();

            output.AppendLine("Roll History:");

            foreach (var sides in _stats.GetDiceSizes().Where(x => x.Equals(reportSize) || reportSize == 0))
            {
                var rolls = _stats.GetDieRolls(sides);
                var totalRolls = rolls.Sum();

                output.AppendLine("");
                output.AppendLine($"Results for {sides} sided die: ");

                for (int side = 1; side <= sides; side++)
                {
                    decimal perc = rolls[side] * 100M / totalRolls;
                    output.AppendLine($"{tab}{side}: {rolls[side]} ({perc:f1}%)");
                }
            }

            await ReplyAsync(output.ToString());
        }
    }
}
