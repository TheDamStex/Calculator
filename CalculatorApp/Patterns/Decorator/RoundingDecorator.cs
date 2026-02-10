using CalculatorApp.Models;

namespace CalculatorApp.Patterns.Decorator;

public class RoundingDecorator : CalculatorDecorator
{
    private readonly int _digits;

    public RoundingDecorator(ICalculator innerCalculator, int digits)
        : base(innerCalculator)
    {
        _digits = digits < 0 ? 0 : digits;
    }

    public override double Add(double a, double b)
    {
        return Round(base.Add(a, b));
    }

    public override double Subtract(double a, double b)
    {
        return Round(base.Subtract(a, b));
    }

    public override double Multiply(double a, double b)
    {
        return Round(base.Multiply(a, b));
    }

    public override double Divide(double a, double b)
    {
        return Round(base.Divide(a, b));
    }

    private double Round(double value)
    {
        return Math.Round(value, _digits);
    }
}
