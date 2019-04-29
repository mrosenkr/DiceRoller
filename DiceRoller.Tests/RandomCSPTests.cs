using System.Linq;
using System;
using Xunit;

namespace DiceRoller.Tests
{
    public class RandomCSPTests : IDisposable
    {
        private RandomCSP _roller;

        public RandomCSPTests()
        {
            _roller = new RandomCSP();
        }

        public void Dispose()
        {
            _roller.Dispose();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(10)]
        [InlineData(20)]
        public void RollDie(int sides)
        {
            var result = _roller.RollDice(1, sides);

            Assert.InRange<int>(result.Sum(), 1, sides);
        }

        [Theory]
        [InlineData(20000, 4)]
        [InlineData(200000, 4)]
        [InlineData(2000000, 4)]
        public void RollManyDice(int dice, int sides)
        {
            var result = _roller.RollDice(dice, sides);

            Assert.InRange<int>(result.Sum(), dice, dice * sides);
        }
    }
}
