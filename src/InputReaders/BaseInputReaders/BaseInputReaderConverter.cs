using InputReader.Converters;
using InputReader.InputReaders.Interfaces;
using System;

namespace InputReader.InputReaders.BaseInputReaders;
public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>, IPreValidatable<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private IValueConverter<TInputType> valueConverter;

    #region Converter Methods

    public IInputReader<TInputType, TInputValueType> WithValueConverter(
        IValueConverter<TInputType> converter)
    {
        valueConverter = converter;

        return this;
    }

    public IInputReader<TInputType, TInputValueType> WithValueConverter(
        Func<string, TInputType> convertFunc)
    {
        (valueConverter as DefaultValueConverter<TInputType>).InternalFunc = convertFunc;

        return this;
    }

    #endregion
}
