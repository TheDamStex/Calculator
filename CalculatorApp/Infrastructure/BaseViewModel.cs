using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculatorApp.Infrastructure;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        // Повідомляємо інтерфейс про зміну властивості.
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
