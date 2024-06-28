using InputReader.Converters.CustomConverters;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using System;

namespace InputReader.InputReaders.Extensions;

public static partial class InputReaderExtensions
{
    public static TCustomInputValueType ReadUntilValid<TInputType, TCustomInputValueType>(
        this IInputReader<TInputType, TCustomInputValueType> reader)
        where TCustomInputValueType : InputValue<TInputType>
    {
        TCustomInputValueType result;

        do
        {
            result = reader.Read();

        } while (!result.IsValid);

        return result;
    }

    public static TCustomInputValueType ReadUntil<TInputType, TCustomInputValueType>(
        this IInputReader<TInputType, TCustomInputValueType> reader, Func<TCustomInputValueType, bool> valuePredicate)
        where TCustomInputValueType : InputValue<TInputType>
    {
        do
        {
            var input = reader.Read();

            if (valuePredicate(input))
            {
                return input;
            }

        } while (true);
    }



    public static TCustomInputValueType ReadValidEmail<TInputType, TCustomInputValueType>(
        this IInputReader<TInputType, TCustomInputValueType> reader)
        where TCustomInputValueType : InputValue<TInputType>
    {
        return reader.ReadUntil(input =>
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(input.Value.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        });
    }
}