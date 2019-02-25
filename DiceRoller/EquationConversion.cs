using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceRoller
{
    internal static class EquationConversion
    {
        public static Dictionary<string, int> Precedence = new Dictionary<string, int>()
        {
            { "d", 7 },
            { "^", 6 },
            { "%", 5 },
            { "*", 5 },
            { "/", 5 },
            { "+", 4 },
            { "-", 4 },
            { "(", 0 },
            { ")", 0 },
        };

        public static Queue<string> ReversePolishNotation(string infixEquation)
        {
            return ReversePolishNotation(EquationToList(infixEquation));
        }

        /// <summary>
        /// Use the shunting-yard algorithm to convert the infix equation into Reverse Polish Notation (RPN)
        /// </summary>
        /// <param name="infixEquation">Equation in Infix notation</param>
        /// <returns>Equation in Reverse Polish notation</returns>
        public static Queue<string> ReversePolishNotation(List<string> infixEquation)
        {
            var Q = new Queue<string>();
            var S = new Stack<string>();

            foreach (var item in infixEquation)
            {
                if (Double.TryParse(item, out double result))
                {
                    Q.Enqueue(item);
                }
                else if (item == "(")
                {
                    S.Push(item);
                }
                else if (item == ")")
                {
                    string operation = string.Empty;
                    do
                    {
                        if (S.Count == 0)
                        {
                            throw new ArgumentException("Closing parenthesis has no corresponding opening parenthesis.");
                        }
                        operation = S.Pop();
                        if (operation != "(")
                        {
                            Q.Enqueue(operation);
                        }
                    } while (operation != "(");
                }
                else if (Precedence.ContainsKey(item))
                {
                    while (S.Count > 0)
                    {
                        if (Precedence[S.Peek()] >= Precedence[item])
                        {
                            Q.Enqueue(S.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    S.Push(item);
                }
                else
                {
                    throw new ArgumentException("Invalid character found in equation");
                }
            }

            while (S.Count > 0)
            {
                var item = S.Pop();
                if (item == "(")
                {
                    throw new ArgumentException("Opening parenthesis has no corresponding closing parenthesis.");
                }
                Q.Enqueue(item);
            }

            return Q;
        }

        public static List<string> EquationToList(string infixEquation)
        {
            infixEquation.Replace(" ", "");

            foreach (string key in Precedence.Keys)
            {
                infixEquation = infixEquation.Replace(key, $" {key} ");
            }

            return infixEquation.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }
    }
}
