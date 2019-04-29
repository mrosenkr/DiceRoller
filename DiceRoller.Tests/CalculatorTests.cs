using System;
using Xunit;

namespace DiceRoller.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData("14", 14)]
        [InlineData("1+4", 5)]
        [InlineData("1+4*3", 13)]
        [InlineData("1*4+3", 7)]
        [InlineData("4*3+1", 13)]
        [InlineData("4*3-2", 10)]
        [InlineData("(1+4)*3", 15)]
        [InlineData("2*(4+3)", 14)]
        [InlineData("((15/(7-(1+1)))*3)-(2+(1+1))", 5)]
        [InlineData("10%4", 2)]
        [InlineData("10%4*3", 6)]
        [InlineData("2^3", 8)]
        [InlineData("2^3*4", 32)]
        public void EvaluateInfixEquation(string infixEquation, double expected)
        {
            var result = Calculator.Evaluate(infixEquation);

            Assert.Equal<double>(expected, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("12p")]
        [InlineData("12+p")]
        [InlineData("1+4*3)")]
        [InlineData("1+(4*3")]
        [InlineData("1(4*3)")]
        public void EvaluateInfixEquation_ThrowsException(string infixEquation)
        {
            Assert.Throws<ArgumentException>(() => Calculator.Evaluate(infixEquation));
        }
    }
}
