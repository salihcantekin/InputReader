using InputReader.InputReaders.Interfaces;
using System;

namespace InputReader.InputReaders.Extensions;

public static partial class InputReaderExtensions
{
    public static TInputValueType ReadUntilValid<TInputType, TInputValueType>(
        this IInputReader<TInputType, TInputValueType> reader)
        where TInputValueType : InputValue<TInputType>
    {
        TInputValueType result;

        do
        {
            result = reader.Read();

        } while (!result.IsValid);

        return result;
    }

    public static TInputValueType ReadUntil<TInputType, TInputValueType>(
        this IInputReader<TInputType, TInputValueType> reader, Func<TInputValueType, bool> valuePredicate)
        where TInputValueType : InputValue<TInputType>
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

    public static TInputValueType ReadUntilValidEmail<TInputType, TInputValueType>(
        this IInputReader<TInputType, TInputValueType> reader)
        where TInputValueType : InputValue<TInputType>
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