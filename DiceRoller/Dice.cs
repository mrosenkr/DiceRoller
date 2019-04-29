using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var result = new RollResult();

            try
            {
                var matchCollection = Regex.Matches(infixEquation, ValidDiceRollPattern);
                foreach (Match match in matchCollection)
                {
                    var options = ParseOptions(match);
                    var roll = _roller.RollDice(options.dice, options.sides);

                    if (options.keep != Keep.All && options.keepAmt > options.dice)
                    {
                        throw new ArgumentException("Keep amount exceeds number of dice");
                    }

                    int total = 0;
                    string rollNumbers = string.Empty;

                    if (options.keep == Keep.All)
                    {
                        total = roll.Sum();
                        rollNumbers = $"[{String.Join('+', roll)}]";
                    }
                    else
                    {
                        List<int> keep;
                        if (options.keep == Keep.Lowest)
                        {
                            keep = roll.OrderBy(i => i).Take(options.keepAmt).ToList();
                        }
                        else
                        {
                            keep = roll.OrderByDescending(i => i).Take(options.keepAmt).ToList();
                        }
                        total = keep.Sum();

                        var numberDisplay = new List<string>(options.dice);
                        foreach(var die in roll)
                        {
                            if (keep.Contains(die))
                            {
                                numberDisplay.Add($"{die.ToString()}");
                                keep.Remove(die);
                            }
                            else
                            {
                                numberDisplay.Add($"||{die.ToString()}||");
                            }
                        }

                        rollNumbers = $"[{String.Join('+', numberDisplay)}]";
                    }

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
