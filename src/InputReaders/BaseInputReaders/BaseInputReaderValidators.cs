using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue.QueueItems;
using InputReader.Validators;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.InputReaders.BaseInputReaders;

public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>, IPreValidatable<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private HashSet<IPreValidator> preValidators;
    private HashSet<IPostValidator<TInputType>> postValidators;
    private readonly bool isPreBuildProcessed;

    public FrozenSet<IPreValidator> PreValidators => preValidators.ToFrozenSet();
    public FrozenSet<IPostValidator<TInputType>> PostValidators => postValidators.ToFrozenSet();

    #region Pre-Post Validators

    public IInputReader<TInputType, TInputValueType> WithPreValidator<TPreValidator>(TPreValidator validator)
        where TPreValidator : IPreValidator
    {
        preValidators ??= [];
        preValidators.Add(validator);

        var preValidatorQueueItem = new PreValidatorQueueItem(preValidators);
        AddItemToQueue(preValidatorQueueItem);

        return this;
    }

    public void AddPostValidator<TPostValidator>(TPostValidator validator)
        where TPostValidator : IPostValidator<TInputType>
    {
        postValidators ??= [];
        postValidators.Add(validator);
    }

    public IInputReader<TInputType, TInputValueType> WithPreValidator(Func<string, bool> validatorFunc)
    {
        return WithPreValidator(ValidatorBuilder.SetCustomPreValidator(validatorFunc));
    }

    #endregion

    private bool AnyPreValidatorFailed(string value)
    {
        return preValidators?.Any(v => !v.IsValid(value)) ?? false;
    }
}
