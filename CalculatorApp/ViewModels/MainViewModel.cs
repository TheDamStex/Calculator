using System.Collections.ObjectModel;
using System.Globalization;
using CalculatorApp.Infrastructure;
using CalculatorApp.Models;
using CalculatorApp.Patterns.Facade;
using CalculatorApp.Services;

namespace CalculatorApp.ViewModels;

public class MainViewModel : BaseViewModel
{
    // Тестові сценарії для перевірки:
    // 5 + 0, 0 + 5
    // 7 - 0, 0 - 7
    // 9 * 0, 0 * 9
    // 0 / 5
    // 5 / 0 (помилка)
    private readonly ICalculatorFacade _calculatorFacade;

    private string _inputA = string.Empty;
    private string _inputB = string.Empty;
    private string _resultText = string.Empty;
    private string _errorText = string.Empty;
    private string _selectedOperation;
    private string _selectedMode;
    private string _roundingDigits = "0";

    public MainViewModel()
    {
        History = new ObservableCollection<string>();
        _calculatorFacade = new CalculatorFacade(History);

        Operations = new ObservableCollection<string> { "+", "-", "*", "/" };
        Modes = new ObservableCollection<string> { "Light", "Full" };
        _selectedOperation = Operations[0];
        _selectedMode = Modes[0];

        CalculateCommand = new RelayCommand(Calculate);
    }

    public MainViewModel(ICalculatorFacade calculatorFacade, ObservableCollection<string> history)
    {
        _calculatorFacade = calculatorFacade ?? throw new ArgumentNullException(nameof(calculatorFacade));
        History = history ?? throw new ArgumentNullException(nameof(history));

        Operations = new ObservableCollection<string> { "+", "-", "*", "/" };
        Modes = new ObservableCollection<string> { "Light", "Full" };
        _selectedOperation = Operations[0];
        _selectedMode = Modes[0];

        CalculateCommand = new RelayCommand(Calculate);
    }

    public ObservableCollection<string> Operations { get; }

    public ObservableCollection<string> Modes { get; }

    public ObservableCollection<string> History { get; }

    public RelayCommand CalculateCommand { get; }

    public string InputA
    {
        get => _inputA;
        set
        {
            _inputA = value;
            OnPropertyChanged();
        }
    }

    public string InputB
    {
        get => _inputB;
        set
        {
            _inputB = value;
            OnPropertyChanged();
        }
    }

    public string SelectedOperation
    {
        get => _selectedOperation;
        set
        {
            _selectedOperation = value;
            OnPropertyChanged();
        }
    }

    public string SelectedMode
    {
        get => _selectedMode;
        set
        {
            _selectedMode = value;
            OnPropertyChanged();
        }
    }

    public string RoundingDigits
    {
        get => _roundingDigits;
        set
        {
            _roundingDigits = value;
            OnPropertyChanged();
        }
    }

    public string ResultText
    {
        get => _resultText;
        set
        {
            _resultText = value;
            OnPropertyChanged();
        }
    }

    public string ErrorText
    {
        get => _errorText;
        set
        {
            _errorText = value;
            OnPropertyChanged();
        }
    }

    private void Calculate()
    {
        ErrorText = string.Empty;
        ResultText = string.Empty;

        if (!TryParseDouble(InputA, out var a) || !TryParseDouble(InputB, out var b))
        {
            ErrorText = "Введіть коректні числа для A і B.";
            return;
        }

        if (!int.TryParse(RoundingDigits, out var digits))
        {
            digits = 0;
        }

        var operation = ParseOperation(SelectedOperation);
        var mode = ParseMode(SelectedMode);

        var result = _calculatorFacade.Calculate(a, b, operation, mode, digits);

        if (!string.IsNullOrWhiteSpace(result.Error))
        {
            ErrorText = result.Error ?? "Невідома помилка.";
            return;
        }

        ResultText = result.Result?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
    }

    private static bool TryParseDouble(string value, out double result)
    {
        return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result)
            || double.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out result);
    }

    private static OperationType ParseOperation(string operation)
    {
        return operation switch
        {
            "+" => OperationType.Add,
            "-" => OperationType.Subtract,
            "*" => OperationType.Multiply,
            "/" => OperationType.Divide,
            _ => OperationType.Add
        };
    }

    private static CalculatorMode ParseMode(string mode)
    {
        return mode == "Full" ? CalculatorMode.Full : CalculatorMode.Light;
    }
}
