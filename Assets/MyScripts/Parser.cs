using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

public class Parser : MonoBehaviour
{
    private const string RegexBr = "\\(([1234567890\\.\\+\\-\\*\\/^%]*)\\)";    // Скобки
    private const string RegexNum = "[-]?\\d+\\.?\\d*";                         // Числа
    private const string RegexMulOp = "[\\*\\/^%]";                             // Первоприоритетные числа
    private const string RegexAddOp = "[\\+\\-]";                               // Второприоритетные числа
    public static double Parse(string str) {
        Debug.Log(str + "    ---STR");

        // Парсинг скобок
        var matchSk = Regex.Match(str, RegexBr);
        if (matchSk.Groups.Count > 1) {
            string inner = matchSk.Groups[0].Value.Substring(1, matchSk.Groups[0].Value.Trim().Length - 2);
            string left = str.Substring(0, matchSk.Index);
            string right = str.Substring(matchSk.Index + matchSk.Length);

            return Parse(left + Parse(inner).ToString(CultureInfo.InvariantCulture) + right);
        }

        // Парсинг действий
        var matchMulOp = Regex.Match(str, string.Format("({0})\\s?({1})\\s?({2})\\s?", RegexNum, RegexMulOp, RegexNum));
        var matchAddOp = Regex.Match(str, string.Format("({0})\\s?({1})\\s?({2})\\s?", RegexNum, RegexAddOp, RegexNum));
        var match = matchMulOp.Groups.Count > 1 ? matchMulOp : matchAddOp.Groups.Count > 1 ? matchAddOp : null;
        if (match != null) {
            string left = str.Substring(0, match.Index);
            string right = str.Substring(match.Index + match.Length);
            return Parse(left + ParseAct(match).ToString(CultureInfo.InvariantCulture) + right);
        }

        // Парсинг числа
        try {
            return double.Parse(str, CultureInfo.InvariantCulture);
        }
        catch (FormatException) {
            throw new FormatException(string.Format("Неверная входная строка '{0}'", str));
        }
    }
    private static double ParseAct(Match match) {
        double a = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        double b = double.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);

        switch (match.Groups[2].Value) {
            case "+":
                return a + b;
            case "-":
                return a - b;
            case "*":
                return a * b;
            case "/":
                return a / b;
            case "^":
                return Math.Pow(a, b);
            case "%":
                return a % b;
            default:
                throw new FormatException($"Неверная входная строка '{match.Value}'");
        }
    }


}
