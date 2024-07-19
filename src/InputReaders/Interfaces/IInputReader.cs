using InputReader.Converters;
using InputReader.PrintProcessor;
using System;
using System.Collections.Generic;

namespace InputReader.InputReaders.Interfaces;

public interface IInputReader<TInputType, TInputValueType> : IInputReaderBase<TInputType, TInputValueType>,
                                                                   IInputReadUntil<TInputType, TInputValueType>,
                                                                   IPreValidatable<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    IInputReader<TInputType, TInputValueType> WithMessage(string message);
    IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<string> allowedValues);

    IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<string> allowedValues,
        bool caseInsensitive = true);


    IInputReader<TInputType, TInputValueType> WithValueConverter(Func<string, TInputType> convertFunc);

    IInputReader<TInputType, TInputValueType> WithValueConverter(
        IValueConverter<TInputType> converter);

    IInputReader<TInputType, TInputValueType> WithIteration(
        Action<TInputValueType, IPrintProcessor> action);
}