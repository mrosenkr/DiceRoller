using System;
using System.Security.Cryptography;

namespace DiceRoller
{
    internal class RandomCSP : IDisposable
    {
        private RNGCryptoServiceProvider _rng;

        public RandomCSP()
        {
            _rng = new RNGCryptoServiceProvider();
        }

        public int RollDice(int dice, int sides)
        {
            if (dice <= 0)
            {
                throw new ArgumentOutOfRangeException("dice");
            }
            if (sides <= 0)
            {
                throw new ArgumentOutOfRangeException("sides");
            }

            int result = 0;
            byte[] randomNumber = new byte[1];
            for (int i = 0; i < dice; i++)
            {
                do
                {
                    _rng.GetBytes(randomNumber);

                } while (!IsFairRoll(randomNumber[0], sides));

                result += (randomNumber[0] % sides) + 1;
            }

            return result;
        }

        public RollResult RollDice(RollOptions options)
        {
            var result = RollDice(options.dice, options.sides);
            RollResult rollResult = new RollResult(result);
            return rollResult;
        }

        private bool IsFairRoll(byte roll, int numberSides)
        {
            int fairSetOfValues = byte.MaxValue / numberSides;

            return roll < numberSides * fairSetOfValues;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _rng.Dispose();
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
