﻿using System;
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

    public bool TryConvert(object consoleInput, out TInputType value)
    {
        if (typeof(TInputType) == typeof(string))
        {
            value = (TInputType)consoleInput; // Cast to object first
            return true;
        }

        value = default;

        try
        {
            value = InternalFunc(consoleInput.ToString());
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}