namespace DiceRoller
{
    enum Keep
    {
        All,
        Lowest,
        Highest
    }

    class RollOptions
    {
        public int dice;
        public int sides;

        public Keep keep = Keep.All;
        public int keepAmt;
    }
}
