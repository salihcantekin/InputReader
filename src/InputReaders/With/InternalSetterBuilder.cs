using InputReader.Converters;
using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.PrintProcessor;
using InputReader.Validators;
using System;

namespace InputReader.InputReaders.With;

public class InternalSetterBuilder<TInputType, TInputValueType>(BaseInputReader<TInputType, TInputValueType> reader)
    : IInternalSetterBuilder<TInputType, TInputValueType> where TInputValueType : InputValue<TInputType>
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

    public IInternalSetterBuilder<TInputType, TInputValueType> WithPrintProcessor(IPrintProcessor printProcessor)
    {
        reader.SetPrintProcessor(printProcessor);
        return this;
    }
}