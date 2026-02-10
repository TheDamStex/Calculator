using System.Collections.Generic;
using CalculatorApp.Models;
using CalculatorApp.Patterns.Adapter;
using CalculatorApp.Patterns.Decorator;
using CalculatorApp.Services;

namespace CalculatorApp.Patterns.Facade;

public class CalculatorFacade : ICalculatorFacade
{
    private readonly IList<string> _history;

    public CalculatorFacade(IList<string> history)
    {
        _history = history ?? throw new ArgumentNullException(nameof(history));
    }

    public CalculationResult Calculate(
        double a,
        double b,
        OperationType operation,
        CalculatorMode mode,
        int roundingDigits)
    {
        try
        {
            // Facade приховує від ViewModel побудову потрібної комбінації обчислювачів.
            var calculator = CreateCalculator(mode, roundingDigits);
            var result = ExecuteOperation(calculator, a, b, operation);
            return CalculationResult.Success(result);
        }
        catch (Exception ex)
        {
            return CalculationResult.Fail(ex.Message);
        }
    }

    private ICalculator CreateCalculator(CalculatorMode mode, int roundingDigits)
    {
        ICalculator calculator;

        if (mode == CalculatorMode.Light)
        {
            calculator = new LightCalculator();
        }
        else
        {
            // Adapter: узгоджує старий LegacyMathEngine з інтерфейсом ICalculator.
            var legacy = new LegacyMathEngine();
            var adapter = new LegacyMathAdapter(legacy);
            calculator = new FullCalculator(adapter);
            // Decorator: додає округлення без змін базового калькулятора (OCP).
            calculator = new RoundingDecorator(calculator, roundingDigits);
        }

        // Decorator: додає історію окремою відповідальністю (SRP).
        calculator = new HistoryDecorator(calculator, _history);
        return calculator;
    }

    private static double ExecuteOperation(ICalculator calculator, double a, double b, OperationType operation)
    {
        return operation switch
        {
            OperationType.Add => calculator.Add(a, b),
            OperationType.Subtract => calculator.Subtract(a, b),
            OperationType.Multiply => calculator.Multiply(a, b),
            OperationType.Divide => calculator.Divide(a, b),
            _ => throw new InvalidOperationException("Невідома операція.")
        };
    }
}
