using CalculatorApp.Models;

namespace CalculatorApp.Patterns.Decorator;

public abstract class CalculatorDecorator : ICalculator
{
    protected readonly ICalculator InnerCalculator;

    protected CalculatorDecorator(ICalculator innerCalculator)
    {
        InnerCalculator = innerCalculator;
    }

    public virtual double Add(double a, double b)
    {
        return InnerCalculator.Add(a, b);
    }

    public virtual double Subtract(double a, double b)
    {
        return InnerCalculator.Subtract(a, b);
    }

    public virtual double Multiply(double a, double b)
    {
        return InnerCalculator.Multiply(a, b);
    }

    public virtual double Divide(double a, double b)
    {
        return InnerCalculator.Divide(a, b);
    }
}
