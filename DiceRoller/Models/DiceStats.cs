using System.Collections.Generic;
using System.Linq;

namespace DiceRoller.Models
{
    public class DiceStats : IDiceStats
    {
        private Dictionary<int, int[]> _statistics = new Dictionary<int, int[]>();

        public void AddDieResult(int sides, int result)
        {
            // Add this die size to the stats if it isn't already present
            if (!_statistics.ContainsKey(sides))
            {
                // use +1 to simplify access in array to match rolled result.
                var emptyResults = new int[sides + 1];
                _statistics.Add(sides, emptyResults);
            }

            // increment the number of times this result has appeared for this size die
            var results = _statistics[sides];
            results[result] += 1;
        }

        // Returns an array of sizes of dice that have been tracked
        public int[] GetDiceSizes()
        {
            return _statistics.Keys.ToArray();
        }

        // Returns the number of times each side was rolled for a given die size
        public int[] GetDieRolls(int size)
        {
            var result = new int[size + 1];
            if (_statistics.ContainsKey(size))
            {
                result = _statistics[size];
            }

            return result;
        }
    }
}
