namespace InputReader.Converters;

public interface IValueConverter<TCustomInputValueType>
{
    bool TryConvertFromString(string consoleInput, out TCustomInputValueType value);
}