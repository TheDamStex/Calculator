using CalculatorApp.Models;

namespace CalculatorApp.Patterns.Adapter;

public class LegacyMathAdapter : ICalculator
{
    private readonly LegacyMathEngine _engine;

    public LegacyMathAdapter(LegacyMathEngine engine)
    {
        _engine = engine;
    }

    public double Add(double a, double b)
    {
        // Несумісність: у старому рушії інші назви методів.
        return _engine.Sum(a, b);
    }

    public double Subtract(double a, double b)
    {
        return _engine.Diff(a, b);
    }

    public double Multiply(double a, double b)
    {
        return _engine.Prod(a, b);
    }

    public double Divide(double a, double b)
    {
        return _engine.Quo(a, b);
    }
}
