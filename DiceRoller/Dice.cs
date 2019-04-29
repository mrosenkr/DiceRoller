using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DiceRoller
{
    public class Dice : IDisposable
    {
        public static string ValidDiceRollPattern = @"([0-9.]+)d([0-9.]+)(?>k?(?'keep'[l|h]?)(?'keepamt'\d?))";
        private RandomCSP _roller;

        public Dice()
        {
            _roller = new RandomCSP();
        }

        public RollResult Roll(string infixEquation)
        {
            string display = infixEquation;
            RollResult result = new RollResult();

            try
            {
                var matchCollection = Regex.Matches(infixEquation, ValidDiceRollPattern);
                foreach (Match match in matchCollection)
                {
                    var options = ParseOptions(match);
                    var roll = _roller.RollDice(options.dice, options.sides);
                    var total = roll.Sum();
                    var rollNumbers = $"[{String.Join('+', roll)}]";

                    // todo, handle keep H/L

                    var subInValue = new Regex(Regex.Escape(match.Value));
                    infixEquation = subInValue.Replace(infixEquation, total.ToString(), 1);
                    display = subInValue.Replace(display, rollNumbers, 1);
                }

                var answer = Calculator.Evaluate(infixEquation);
                result.Answer = answer;
                result.Equation = infixEquation;
                result.EquationDisplay = display;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.Error = true;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        private RollOptions ParseOptions(Match match)
        {
            if (!int.TryParse(match.Groups[1].Value, out int dice))
            {
                throw new ArgumentException("number of dice must be an integer");
            }
            if (!int.TryParse(match.Groups[2].Value, out int sides))
            {
                throw new ArgumentException("number of sides must be an integer");
            }

            RollOptions options = new RollOptions()
            {
                dice = dice,
                sides = sides
            };

            string keep = match.Groups["keep"].Value;
            if (!string.IsNullOrWhiteSpace(keep))
            {
                options.keep = keep == "l" ? Keep.Lowest : Keep.Highest;
                options.keepAmt = int.Parse(match.Groups["keepamt"].Value);
            }
            return options;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _roller.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
