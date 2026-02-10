using CalculatorApp.Models;

namespace CalculatorApp.Services;

public interface ICalculatorFacade
{
    CalculationResult Calculate(double a, double b, OperationType operation, CalculatorMode mode, int roundingDigits);
}
