namespace InputReader.Converters;

internal class InternalValueConverter<T> : IValueConverter<T>
{
    public bool TryConvert(object consoleInput, out T value)
    {
        value = (T)consoleInput;
        return true;
    }
}