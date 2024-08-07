using InputReader.Converters;
using InputReader.InputReaders.Interfaces;
using InputReader.PrintProcessor;
using InputReader.Validators;
using System;

namespace InputReader.InputReaders.With;
public interface IInternalSetterBuilder<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    IInternalSetterBuilder<TInputType, TInputValueType> WithConsoleReader(IInputReaderBase consoleReader);
    IInternalSetterBuilder<TInputType, TInputValueType> WithCustomConverter(Func<string, TInputType> action);
    IInternalSetterBuilder<TInputType, TInputValueType> WithCustomConverter(IValueConverter<TInputType> converter);
    IInternalSetterBuilder<TInputType, TInputValueType> WithPreValidator(Func<string, bool> validatorFunc);
    IInternalSetterBuilder<TInputType, TInputValueType> WithPreValidator(IPreValidator preValidator);
    IInternalSetterBuilder<TInputType, TInputValueType> WithPrintProcessor(IPrintProcessor printProcessor);
}