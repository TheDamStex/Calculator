namespace CalculatorApp.Models;

public class CalculationResult
{
    public double? Result { get; init; }
    public string? Error { get; init; }

    public static CalculationResult Success(double result)
    {
        return new CalculationResult { Result = result };
    }

    public static CalculationResult Fail(string error)
    {
        return new CalculationResult { Error = error };
    }
}
