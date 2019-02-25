using System;
using Xunit;

namespace DiceRoller.Tests
{
    public class EquationConversionTests
    {
        [Theory]
        [InlineData("14", "14")]
        [InlineData("1+4*3", "1 4 3 * +")]
        [InlineData("1*4+3", "1 4 * 3 +")]
        [InlineData("((15/(7-(1+1)))*3)-(2+(1+1))", "15 7 1 1 + - / 3 * 2 1 1 + + -")]
        public void RPN_ValidStack(string infix, string expected)
        {
            var result = EquationConversion.ReversePolishNotation(infix);

            string actual = String.Join(" ", result.ToArray());

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("12p")]
        [InlineData("12+p")]
        [InlineData("1+4*3)")]
        [InlineData("1+(4*3")]
        public void RPN_ThrowsException(string infix)
        {
            Assert.Throws<ArgumentException>(() => EquationConversion.ReversePolishNotation(infix));
        }


        [Theory]
        [InlineData("1+4*3", 5)]
        [InlineData("1*4+3", 5)]
        [InlineData("1*(4+3)", 7)]
        public void EquationToList(string equation, int expected)
        {
            var result = EquationConversion.CommandToList(equation);
            Assert.Equal<int>(expected, result.Count);
        }
    }
}
