namespace DiceRoller
{
    public class RollResult
    {
        public double Answer { get; set; }
        public string Equation { get; set; }
        public string EquationDisplay { get; set; }

        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}
