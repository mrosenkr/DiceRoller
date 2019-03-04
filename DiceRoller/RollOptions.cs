namespace DiceRoller
{
    enum Keep
    {
        Lowest,
        Highest
    }

    class RollOptions
    {
        public int dice;
        public int sides;

        public Keep keep;
        public int keepAmt;
    }
}
