namespace CalculatorApp.Models;

public class LightCalculator : ICalculator
{
    public double Add(double a, double b)
    {
        // Правила додавання для LightCalculator.
        if (a == 0)
        {
            return b;
        }

        if (b == 0)
        {
            return a;
        }

        return a + b;
    }

    public double Subtract(double a, double b)
    {
        // Правила віднімання для LightCalculator.
        if (b == 0)
        {
            return a;
        }

        if (a == 0)
        {
            return -b;
        }

        return a - b;
    }

    public double Multiply(double a, double b)
    {
        // Правила множення для LightCalculator.
        if (a == 0 || b == 0)
        {
            return 0;
        }

        return a * b;
    }

    public double Divide(double a, double b)
    {
        // Правила ділення для LightCalculator.
        if (b == 0)
        {
            throw new DivideByZeroException("Ділення на нуль неможливе.");
        }

        if (a == 0)
        {
            return 0;
        }

        return a / b;
    }
}
