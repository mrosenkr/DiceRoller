using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DiceRoller.Tests")]
namespace DiceRoller
{
    public static class DiceRoller
    {
        public static string ValidDiceRollPattern = @"^(\d+)d(\d+)k?([l|h]?)(\d?)$";
        public static string ValidCharacterPattern = @"^[0-9dkhl+-\/\*\(\)]*$";

        public static RollResult Roll(string infixEquation)
        {
            RollResult result = new RollResult();

            try
            {
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
    }
}
