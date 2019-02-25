using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("DiceRoller.Tests")]
namespace DiceRoller
{
    public static class DiceRoller
    {
        public static string ValidDiceRollPattern = @"^(\d+)d(\d+)k?([l|h]?)(\d?)$";
        public static string ValidCharacterPattern = @"^[0-9dkhl+-\/\*\(\)]*$";

        public static double Roll(string command)
        {
            //char[] operators = { '+', '-', '*', '/' };


            //foreach (var op in operators)
            //{

            //}

            //var bob = string.Split(command, );


            return 0;
        }
    }
}
