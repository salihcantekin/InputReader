using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue.QueueItems;
using InputReader.Validators;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.InputReaders.BaseInputReaders;

public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private HashSet<IPreValidator> preValidators;
    //private readonly HashSet<IPostValidator<TInputType>> postValidators;

    public FrozenSet<IPreValidator> PreValidators => preValidators.ToFrozenSet();
    //public FrozenSet<IPostValidator<TInputType>> PostValidators => postValidators.ToFrozenSet();

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
