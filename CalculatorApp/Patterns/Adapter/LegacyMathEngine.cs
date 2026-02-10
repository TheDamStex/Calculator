namespace CalculatorApp.Patterns.Adapter;

public class LegacyMathEngine
{
    public double Sum(double x, double y)
    {
        return x + y;
    }

    public double Diff(double x, double y)
    {
        return x - y;
    }

    public double Prod(double x, double y)
    {
        return x * y;
    }

    public double Quo(double x, double y)
    {
        return x / y;
    }
}
