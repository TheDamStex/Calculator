namespace CalculatorApp.Models;

public class FullCalculator : ICalculator
{
    private readonly ICalculator _legacyAdapter;

    public FullCalculator(ICalculator legacyAdapter)
    {
        _legacyAdapter = legacyAdapter;
    }

    public double Add(double a, double b)
    {
        return _legacyAdapter.Add(a, b);
    }

    public double Subtract(double a, double b)
    {
        return _legacyAdapter.Subtract(a, b);
    }

    public double Multiply(double a, double b)
    {
        return _legacyAdapter.Multiply(a, b);
    }

    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Ділення на нуль неможливе.");
        }

        return a / b;
    }
}
