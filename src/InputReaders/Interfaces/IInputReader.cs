using InputReader.Converters;
using System;
using System.Collections.Generic;

namespace InputReader.InputReaders.Interfaces;

public interface IInputReader<TInputType, TCustomInputValueType> : IInputReaderBase<TInputType, TCustomInputValueType>,
                                                                   IInputReadUntil<TInputType, TCustomInputValueType>,
                                                                   IPreValidatable<TInputType, TCustomInputValueType>
    where TCustomInputValueType : InputValue<TInputType>
{
    IInputReader<TInputType, TCustomInputValueType> WithMessage(string message);
    IInputReader<TInputType, TCustomInputValueType> WithAllowedValues(IEnumerable<string> allowedValues);

    IInputReader<TInputType, TCustomInputValueType> WithAllowedValues(IEnumerable<string> allowedValues,
        bool caseInsensitive = true);


    IInputReader<TInputType, TCustomInputValueType> WithValueConverter(Func<string, TInputType> convertFunc);

    IInputReader<TInputType, TCustomInputValueType> WithValueConverter(
        IValueConverter<TInputType> converter);
}