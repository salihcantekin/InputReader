namespace InputReader.Converters;

public interface IValueConverter<TInputValueType>
{
    bool TryConvert(object consoleInput, out TInputValueType value);
}