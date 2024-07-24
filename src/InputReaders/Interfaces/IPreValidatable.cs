using InputReader.Validators;
using System;

namespace InputReader.InputReaders.Interfaces;

//public interface IPreValidatable<TInputType, TInputValueType>
//    where TInputValueType : InputValue<TInputType>
//{
//    IInputReader<TInputType, TInputValueType> WithPreValidator<TPreValidator>(TPreValidator validator) where TPreValidator : IPreValidator;
//    IInputReader<TInputType, TInputValueType> SetPreValidator(Func<string, bool> validatorFunc);
//}