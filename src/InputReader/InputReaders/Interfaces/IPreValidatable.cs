using InputReader.Validators;
using System;

namespace InputReader.InputReaders.Interfaces;

public interface IPreValidatable<TInputType, TCustomInputValueType>
    where TCustomInputValueType : InputValue<TInputType>
{
    IInputReader<TInputType, TCustomInputValueType> WithPreValidator<TPreValidator>(TPreValidator validator) where TPreValidator : IPreValidator;
    IInputReader<TInputType, TCustomInputValueType> WithPreValidator(Func<string, bool> validatorFunc);
}