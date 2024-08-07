using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue.QueueItems;
using InputReader.Validators;
using System;
using System.Collections.Generic;

namespace InputReader.InputReaders.BaseInputReaders;

public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private HashSet<IPreValidator> preValidators;

    internal IInputReader<TInputType, TInputValueType> SetPreValidator<TPreValidator>(TPreValidator validator)
        where TPreValidator : IPreValidator
    {
        preValidators ??= [];
        preValidators.Add(validator);

        var preValidatorQueueItem = new PreValidatorQueueItem(preValidators);
        AddItemToQueue(preValidatorQueueItem);

        return this;
    }

    internal IInputReader<TInputType, TInputValueType> SetPreValidator(Func<string, bool> validatorFunc)
    {
        return SetPreValidator(ValidatorBuilder.SetCustomPreValidator(validatorFunc));
    }
}
