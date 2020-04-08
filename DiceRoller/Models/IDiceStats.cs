namespace DiceRoller.Models
{
    public interface IDiceStats
    {
        void AddDieResult(int sides, int result);
        int[] GetDiceSizes();
        int[] GetDieRolls(int size);
    }
}
