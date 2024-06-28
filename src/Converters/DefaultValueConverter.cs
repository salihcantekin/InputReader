using System;
using System.ComponentModel;

namespace InputReader.Converters;

public class DefaultValueConverter<TInputType> : IValueConverter<TInputType>
{
    internal Func<string, TInputType> InternalFunc { get; set; }

    public DefaultValueConverter(Func<string, TInputType> func = null)
    {
        if (func is not null)
        {
            InternalFunc = func;
            return;
        }

        InternalFunc = (string message) =>
        {
            var converter = TypeDescriptor.GetConverter(typeof(TInputType));

            if (!converter.CanConvertFrom(typeof(string)))
                return default;

            var value = (TInputType)converter.ConvertFromString(message);

            return value;
        };
    }

    public bool TryConvertFromString(string consoleInput, out TInputType value)
    {
        value = default;

        try
        {
            value = InternalFunc(consoleInput);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}