namespace InputReader.Converters;

public interface IValueConverter<TInputValueType>
{
    bool TryConvertFromString(string consoleInput, out TInputValueType value);
}