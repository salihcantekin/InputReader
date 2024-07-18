using InputReader.AllowedValues;

namespace InputReader.InputReaders.Queue.QueueItems;

internal class AllowedValuesCheckQueueItem(IAllowedValueProcessor<string> allowedValueProcessor) : IQueueItem
{
    public int Order => 4;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var checkRequired = allowedValueProcessor != null
            && allowedValueProcessor.IsEnabled;

        if (!checkRequired)
            return QueueItemResult.FromResult(null, previousItemResult);

        var isAllowed = allowedValueProcessor.IsAllowedValue(previousItemResult.Result.ToString());

        return isAllowed
            ? QueueItemResult.FromResult(isAllowed, previousItemResult)
            : QueueItemResult.Failed();
    }
}
