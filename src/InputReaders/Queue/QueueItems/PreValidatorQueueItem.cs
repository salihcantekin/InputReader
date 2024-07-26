using InputReader.Validators;
using System.Collections.Generic;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class PreValidatorQueueItem(HashSet<IPreValidator> validators) : IQueueItem, IHasFailReason
{
    public int Order => 3;

    public FailReason FailReason => FailReason.PreValidation;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var validationRequired = validators?.Count > 0;

        if (validationRequired == false)
            return QueueItemResult.FromResult(null, previousItemResult);

        var message = previousItemResult.GetOutputParam("line").ToString();

        foreach (var validator in validators)
        {
            var isValid = validator.IsValid(message);

            if (!isValid)
                return QueueItemResult.Failed();
        }

        return previousItemResult;
    }

}
