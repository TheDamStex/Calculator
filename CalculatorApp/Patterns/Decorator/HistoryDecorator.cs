using System.Collections.Generic;
using CalculatorApp.Models;

namespace CalculatorApp.Patterns.Decorator;

public class HistoryDecorator : CalculatorDecorator
{
    private readonly IList<string> _history;

    public HistoryDecorator(ICalculator innerCalculator, IList<string> history)
        : base(innerCalculator)
    {
        _history = history;
    }

    public override double Add(double a, double b)
    {
        var result = base.Add(a, b);
        AddRecord(a, b, "+", result);
        return result;
    }

    public override double Subtract(double a, double b)
    {
        var result = base.Subtract(a, b);
        AddRecord(a, b, "-", result);
        return result;
    }

    public override double Multiply(double a, double b)
    {
        var result = base.Multiply(a, b);
        AddRecord(a, b, "*", result);
        return result;
    }

    public override double Divide(double a, double b)
    {
        var result = base.Divide(a, b);
        AddRecord(a, b, "/", result);
        return result;
    }

    private void AddRecord(double a, double b, string op, double result)
    {
        // Додаємо запис у простому форматі.
        _history.Add($"{a} {op} {b} = {result}");
    }
}
