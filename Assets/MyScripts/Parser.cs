//using System;
//using System.Globalization;
//using System.Text.RegularExpressions;
//using UnityEngine;

//public class Parser : MonoBehaviour
//{
//    private const string RegexBr = "\\(([1234567890\\.\\+\\-\\*\\/^%]*)\\)";    // Скобки
//    private const string RegexNum = "[-]?\\d+\\.?\\d*";                         // Числа
//    private const string RegexMulOp = "[\\*\\/^%]";                             // Первоприоритетные числа
//    private const string RegexAddOp = "[\\+\\-]";                               // Второприоритетные числа
//    public static double Parse(string str) {
//        Debug.Log(str + "    ---STR");

//        // Парсинг скобок
//        var matchSk = Regex.Match(str, RegexBr);
//        if (matchSk.Groups.Count > 1) {
//            string inner = matchSk.Groups[0].Value.Substring(1, matchSk.Groups[0].Value.Trim().Length - 2);
//            string left = str.Substring(0, matchSk.Index);
//            string right = str.Substring(matchSk.Index + matchSk.Length);

//            return Parse(left + Parse(inner).ToString(CultureInfo.InvariantCulture) + right);
//        }

//        // Парсинг действий
//        var matchMulOp = Regex.Match(str, string.Format("({0})\\s?({1})\\s?({2})\\s?", RegexNum, RegexMulOp, RegexNum));
//        var matchAddOp = Regex.Match(str, string.Format("({0})\\s?({1})\\s?({2})\\s?", RegexNum, RegexAddOp, RegexNum));
//        var match = matchMulOp.Groups.Count > 1 ? matchMulOp : matchAddOp.Groups.Count > 1 ? matchAddOp : null;
//        if (match != null) {
//            string left = str.Substring(0, match.Index);
//            string right = str.Substring(match.Index + match.Length);
//            return Parse(left + ParseAct(match).ToString(CultureInfo.InvariantCulture) + right);
//        }

//        // Парсинг числа
//        try {
//            return double.Parse(str, CultureInfo.InvariantCulture);
//        }
//        catch (FormatException) {
//            throw new FormatException(string.Format("Неверная входная строка '{0}'", str));
//        }
//    }
//    private static double ParseAct(Match match) {
//        double a = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
//        double b = double.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);

//        switch (match.Groups[2].Value) {
//            case "+":
//                return a + b;
//            case "-":
//                return a - b;
//            case "*":
//                return a * b;
//            case "/":
//                return a / b;
//            case "^":
//                return Math.Pow(a, b);
//            case "%":
//                return a % b;
//            default:
//                throw new FormatException($"Неверная входная строка '{match.Value}'");
//        }
//    }


//}

using System.Collections.Generic;
using static Microsoft.MixedReality.Toolkit.Experimental.UI.KeyboardKeyFunc;
using UnityEngine.XR.OpenXR;
using UnityEngine;
using System.Linq;
using System;


public class Parser : MonoBehaviour {
    public static class Calculator {
        private const string numberChars = "01234567890.";
        private const string operatorChars = "^*/+-";

        public static double Calculate(string expression) {
            if (string.IsNullOrEmpty(expression))
                throw new ArgumentException("Пустое выражение недопустимо", nameof(expression));

            CheckParenthesis(expression);

            return EvaluateParenthesis(expression);
        }

        private static double EvaluateParenthesis(string expression) {
            string planarExpression = expression;
            while (planarExpression.Contains('(')) {
                int clauseStart = planarExpression.IndexOf('(') + 1;
                int clauseEnd = IndexOfRightParenthesis(planarExpression, clauseStart);
                string clause = planarExpression.Substring(clauseStart, clauseEnd - clauseStart);
                planarExpression = planarExpression.Replace("(" + clause + ")", EvaluateParenthesis(clause).ToString());
            }
            return Evaluate(planarExpression);
        }

        private static int IndexOfRightParenthesis(string expression, int start) {
            int c = 1;
            for (int i = start; i < expression.Length; i++) {
                switch (expression[i]) {
                    case '(':
                        c++;
                        break;
                    case ')':
                        c--;
                        break;
                }
                if (c == 0)
                    return i;
            }
            return -1;
        }

        private static void CheckParenthesis(string expression) {
            int i = 0;
            foreach (char c in expression) {
                switch (c) {
                    case '(':
                        i++;
                        break;
                    case ')':
                        i--;
                        break;
                }
                if (i < 0)
                    throw new ArgumentException("Не хватает '('", nameof(expression));
            }
            if (i > 0)
                throw new ArgumentException("Не хватает ')'", nameof(expression));
        }

        private static double Evaluate(string expression) {
            string normalExpression = expression.Replace(" ", "");
            List<char> operators = normalExpression.Split(numberChars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => x[0]).ToList();
            List<double> numbers = normalExpression.Split(operatorChars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => double.Parse(x)).ToList();

            if (operators.Count + 1 != numbers.Count)
                throw new ArgumentException($"Неверный синтаксис в выражении '{expression}'", nameof(expression));

            foreach (char o in operatorChars) {
                for (int i = 0; i < operators.Count; i++) {
                    if (operators[i] == o) {
                        double result = Calc(numbers[i], numbers[i + 1], o);
                        numbers[i] = result;
                        numbers.RemoveAt(i + 1);
                        operators.RemoveAt(i);
                        i--;
                    }
                }
            }
            return numbers[0];
        }

        private static double Calc(double left, double right, char oper) {
            switch (oper) {
                case '+':
                    return left + right;
                case '-':
                    return left - right;
                case '*':
                    return left * right;
                case '/':
                    return left / right;
                case '^':
                    return Math.Pow(left, right);
                default:
                    throw new ArgumentException("Неподдерживаемый оператор", nameof(oper));
            }
        }
    }
}