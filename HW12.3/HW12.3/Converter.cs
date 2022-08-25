using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12._3
{
    public class Converter
    {
        private static string _operators = "(+-*/^";
        public static double Evaluate(string expression)
        {
            string postfixExp = InfixToPostfix(expression);
            Stack<double> args = new Stack<double>();
            string[] operands = postfixExp.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            double result = 0;
            for (int i = 0; i < operands.Length; ++i)
            {
                string curr = operands[i];
                double digit;
                if (curr == "cos" || curr == "sin")
                {
                    ++i;
                    result = curr == "cos" ? Math.Cos(Double.Parse(operands[i]))
                        : Math.Sin(Double.Parse(operands[i]));
                    args.Push(result);
                }
                else if (curr == "0")
                {
                    //obviously, i suppose (:
                    ++i;
                    args.Push(Double.Parse(operands[i]) * -1);
                }
                else if (curr == "_")
                {
                    //use temp stack to change sign before digits inside bkts
                    Stack<double> tempArgs = new Stack<double>();
                    do
                    {
                        double temp = args.Pop();
                        if (temp != result)
                            temp *= -1;
                        tempArgs.Push(temp);
                    } while (args.Count > 0);

                    args = tempArgs;
                }
                else if (Double.TryParse(curr, out digit))
                    args.Push(digit);
                else
                {
                    double first = args.Pop();
                    double second = args.Pop();
                    switch (curr)
                    {
                        case "+":
                            result = second + first;
                            break;
                        case "-":
                            result = second - first;
                            break;
                        case "*":
                            result = second * first;
                            break;
                        case "/":
                            result = second / first;
                            break;
                        case "^":
                            result = Math.Pow(second, first);
                            break;
                    }
                    args.Push(result);
                }
            }
            return result;
        }

        public static string InfixToPostfix(string expression)
        {
            StringBuilder _output = new StringBuilder();
            char _temp;
            Stack<char> @operator = new Stack<char>();
            expression = expression.Replace(" ", string.Empty);
            bool isMinusBeforeBkt = false;
            for (int i = 0; i < expression.Length; ++i)
            {
                char symbol = expression[i];
                if (char.IsDigit(symbol))
                {
                    _output.Append(CheckNumberDigits(expression, ref i) + " ");
                }
                else if (symbol == '(')
                {
                    @operator.Push(symbol);
                }
                else if (symbol == ')')
                {
                    //if minus is before brackets, write _
                    if (isMinusBeforeBkt)
                    {
                        _output.Append("_ ");
                    }
                    _temp = @operator.Pop();
                    while (_temp != '(')
                    {
                        _output.Append(_temp + " ");
                        _temp = @operator.Pop();
                    }
                    //if there is two and more brackets inside => true
                    if (!@operator.Contains('('))
                        isMinusBeforeBkt = false;
                }
                else if (symbol == 'c' || symbol == 's')
                {
                    i += 2;
                    _output.Append(symbol == 'c' ? "cos " : "sin ");
                }
                else
                {
                    if (symbol == '-' && (i == 0 || (_operators.Contains(expression[i - 1]) && (Char.IsDigit(expression[i + 1]) || expression[i + 1] == '('))))
                    {   
                        if (expression[i + 1] == '(') //if negotation operator is before brackets 
                            isMinusBeforeBkt = true;
                        else
                            _output.Append("0 "); //if negotation operator is before operand
                    }
                    else if (@operator.Count != 0 && Predecessor(@operator.Peek(), symbol))
                    {
                        if (isMinusBeforeBkt)
                            _output.Append("_ ");

                        _temp = @operator.Pop();
                        while (Predecessor(_temp, symbol))
                        {
                            _output.Append(_temp + " ");
                            if (@operator.Count == 0)
                                break;

                            _temp = @operator.Pop();
                        }
                        @operator.Push(symbol);
                    }
                    else
                        @operator.Push(symbol);
                }
            }
            if (isMinusBeforeBkt)
                _output.Append("_ ");
            while (@operator.Count > 0)
            {
                _temp = @operator.Pop();
                _output.Append(_temp + " ");
            }
            return _output.ToString();
        }

        private static bool Predecessor(char firstOperator, char secondOperator)
        {
            int[] precedence = { 1, 2, 2, 3, 3, 3 };

            return precedence[_operators.IndexOf(firstOperator)] >= precedence[_operators.IndexOf(secondOperator)] ?
                true : false;
        }

        private static string CheckNumberDigits(string expression, ref int pos)
        {
            string temp = string.Empty;
            for (; pos < expression.Length; ++pos)
            {
                char c = expression[pos];
                if (c == '.' || char.IsDigit(c))
                {
                    temp += c;
                }
                else
                {
                    --pos;
                    break;
                }
            }

            return temp;
        }

        private static bool IsMinusForward(string expression, int pos)
        {
            if (expression.Length <= pos)
                throw new ArgumentException();

            if (expression[pos + 1] == '-')
            {
                if (Char.IsLetterOrDigit(expression[pos + 2]))
                {
                    ++pos;
                    return true;
                }
                throw new ArgumentException();
            }
            else if (Char.IsLetterOrDigit(expression[pos + 1]) || expression[pos+1] == '(')
                return false;
            else throw new ArgumentException();
        }
    }
}
