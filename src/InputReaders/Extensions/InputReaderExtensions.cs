using InputReader.Converters;
using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.Validators;
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







    public static IInputReader<TInputType, TInputValueType> With<TInputType, TInputValueType>(
        this IInputReader<TInputType, TInputValueType> reader, Action<InternalSetterBuilder<TInputType, TInputValueType>> builderAction)
        where TInputValueType : InputValue<TInputType>
    {
        var builder = new InternalSetterBuilder<TInputType, TInputValueType>(reader as BaseInputReader<TInputType, TInputValueType>);
        builderAction(builder);

        return reader;
    }
}

public interface IInternalSetterBuilder<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    IInternalSetterBuilder<TInputType, TInputValueType> WithConsoleReader(IInputReaderBase consoleReader);
    IInternalSetterBuilder<TInputType, TInputValueType> WithCustomConverter(IValueConverter<TInputType> converter);
}

public class InternalSetterBuilder<TInputType, TInputValueType>(BaseInputReader<TInputType, TInputValueType> reader) 
    : IInternalSetterBuilder<TInputType, TInputValueType>
        where TInputValueType : InputValue<TInputType>
{
    public IInternalSetterBuilder<TInputType, TInputValueType> WithConsoleReader(IInputReaderBase consoleReader)
    {
        reader.SetConsoleReader(consoleReader);
        return this;
    }


    public IInternalSetterBuilder<TInputType, TInputValueType> WithCustomConverter(IValueConverter<TInputType> converter)
    {
        reader.SetValueConverter(converter);
        return this;
    }

    public IInternalSetterBuilder<TInputType, TInputValueType> WithCustomConverter(Func<string, TInputType> action)
    {
        var internalConverter = new DefaultValueConverter<TInputType>(action);

        reader.SetValueConverter(internalConverter);
        return this;
    }

    public IInternalSetterBuilder<TInputType, TInputValueType> WithPreValidator(IPreValidator preValidator)
    {
        reader.SetPreValidator(preValidator);
        return this;
    }

    public IInternalSetterBuilder<TInputType, TInputValueType> WithPreValidator(Func<string, bool> validatorFunc)
    {
        reader.SetPreValidator(validatorFunc);
        return this;
    }
}