using System;
using System.Text.RegularExpressions;

namespace DiceRoller
{
    public class Dice : IDisposable
    {
        public static string ValidDiceRollPattern = @"(\d+)d(\d+)(?>k?(?'keep'[l|h]?)(?'keepamt'\d?))";
        private RandomCSP _roller;

        public Dice()
        {
            _roller = new RandomCSP();
        }

        public RollResult Roll(string infixEquation)
        {
            RollResult result = new RollResult();

            try
            {
                var matchCollection = Regex.Matches(infixEquation, ValidDiceRollPattern);
                foreach (Match match in matchCollection)
                {
                    var options = ParseOptions(match);
                    var rollResult = _roller.RollDice(options);

                    var subInValue = new Regex(Regex.Escape(match.Value));
                    infixEquation = subInValue.Replace(infixEquation, rollResult.Answer.ToString(), 1);
                }

                result = Calculator.Evaluate(infixEquation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.Error = true;
                result.ErrorMessage = "Unexpected error";
            }

            return result;
        }

        private RollOptions ParseOptions(Match match)
        {
            RollOptions options = new RollOptions()
            {
                dice = int.Parse(match.Groups[1].Value),
                sides = int.Parse(match.Groups[2].Value)
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
