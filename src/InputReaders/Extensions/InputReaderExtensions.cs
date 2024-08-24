using InputReader.InputReaders;
using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.With;
using System;

namespace InputReader;

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

    public static TInputValueType ReadUntilValid<TInputType, TInputValueType>(
        this IInputReader<TInputType, TInputValueType> reader, int maxTry)
        where TInputValueType : InputValue<TInputType>
    {
        TInputValueType result;

        if (maxTry <= 0)
        {
            throw new ArgumentException("maxTry must be greater than 0");
        }

        do
        {
            result = reader.Read();

        } while (!result.IsValid && maxTry-- > 0);

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

    public static TInputValueType ReadUntil<TInputType, TInputValueType>(
        this IInputReader<TInputType, TInputValueType> reader, TInputValueType value)
        where TInputValueType : InputValue<TInputType>
    {
        do
        {
            var input = reader.Read();

            if (input.Equals(value))
            {
                return input;
            }

        } while (true);
    }

    public static StringInputValue ReadUntilValidEmail(this StringInputReader reader)
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


    public static IInputReader<TInputType, TInputValueType> With<TInputType, TInputValueType>(
        this IInputReader<TInputType, TInputValueType> reader, Action<IInternalSetterBuilder<TInputType, TInputValueType>> builderAction)
        where TInputValueType : InputValue<TInputType>
    {
        var builder = new InternalSetterBuilder<TInputType, TInputValueType>(reader as BaseInputReader<TInputType, TInputValueType>);
        builderAction(builder);

        return reader;
    }
}
