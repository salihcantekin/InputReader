using InputReader.AllowedValues;
using InputReader.PrintProcessor;

namespace InputReader.InputReaders.Queue.QueueItems;

internal class AllowedValuesCheckQueueItem(IAllowedValueProcessor<string> allowedValueProcessor) 
    : IQueueItem, IHasFailReason
{
    public int Order => 4;

    public FailReason FailReason => FailReason.AllowedValues;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var checkRequired = allowedValueProcessor?.IsEnabled;

        if (checkRequired == false)
            return QueueItemResult.FromResult(null, previousItemResult);

        var isAllowed = allowedValueProcessor.IsAllowedValue(previousItemResult.Result.ToString());

        return isAllowed
            ? QueueItemResult.FromResult(isAllowed, previousItemResult)
            : QueueItemResult.Failed();
    }
}
