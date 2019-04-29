using Xunit;

namespace DiceRoller.Tests
{
    public class DiceTests
    {
        Dice _dice;

        public DiceTests()
        {
            _dice = new Dice();
        }

        [Theory]
        [InlineData("1d4+2", 3, 6)]
        public void ValidEquation(string infixEquation, double min, double max)
        {
            var result = _dice.Roll(infixEquation);

            Assert.InRange<double>(result.Answer, min, max);
        }
    }
}
