using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.Interfaces;
using System;

namespace InputReader.InputReaders.Extensions;

public static class InRangeAllowedValuesExtensions
{
    public static IInputReader<TInputType, TInputValueType> WithAllowedValues<TInputType, TInputValueType>(this IInputReader<TInputType, TInputValueType> inputReader, TInputType from, TInputType to)
        where TInputValueType : InputValue<TInputType>, IInRangeCompatible<TInputType>
    {
        if (inputReader is null)
            throw new ArgumentNullException(nameof(inputReader));

        var reader = inputReader as BaseInputReader<TInputType, TInputValueType>;
        reader.WithInRangeAllowedValues(from: from, to: to);

        return inputReader;
    }
}
