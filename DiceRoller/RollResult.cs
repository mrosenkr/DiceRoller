namespace DiceRoller
{
    public class RollResult
    {
        public double Answer { get; set; }
        public string Detail { get; set; }
        public bool Error { get; set; } = false;
        public string ErrorMessage { get; set; }

        public RollResult(double answer)
        {
            Answer = answer;
        }

        public RollResult()
        {
        }
    }
}
