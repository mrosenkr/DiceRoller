using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiceRoller
{
    internal static class Calculator
    {
        public static RollResult Evaluate(string equation)
        {
            var rpn = EquationConversion.ReversePolishNotation(equation);
            Stack<double> S = new Stack<double>();

            while (rpn.Count > 0)
            {
                var item = rpn.Dequeue();

                // number found, push it
                if (Double.TryParse(item, out double num))
                {
                    S.Push(num);
                }
                // operator found, evaluate against prior two numbers and push result
                else
                {
                    if (S.Count >= 2)
                    {
                        double right = S.Pop();
                        double left = S.Pop();
                        double value = 0;

                        switch (item)
                        {
                            case "*":
                                value = left * right;
                                break;
                            case "/":
                                value = left / right;
                                break;
                            case "+":
                                value = left + right;
                                break;
                            case "-":
                                value = left - right;
                                break;
                            case "^":
                                value = Math.Pow(left, right);
                                break;
                            case "%":
                                value = left % right;
                                break;
                            default:
                                throw new ArgumentException($"Operator not found: {item}");
                        }
                        S.Push(value);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid equation.  Operator does not have two operands.");
                    }
                }
            }

            if (S.Count != 1)
            {
                throw new ArgumentException("Incomplete Equation.  Mismatched operators and operands.");
                
            }
            else
            {
                var result = new RollResult(S.Pop());
                return result;
            }
        }
    }
}
