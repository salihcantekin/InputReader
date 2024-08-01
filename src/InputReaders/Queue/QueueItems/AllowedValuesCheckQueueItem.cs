using InputReader.AllowedValues;
using InputReader.PrintProcessor;

namespace InputReader.InputReaders.Queue.QueueItems;

internal class AllowedValuesCheckQueueItem<TInputType>(IAllowedValueProcessor<string, TInputType> allowedValueProcessor, IPrintProcessor printProcessor)
    : IQueueItem, IHasFailReason
{
    public FailReason FailReason => FailReason.AllowedValues;

    public int Order => QueueItemsOrder.AllowedValuesCheckQueueItem;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var checkRequired = allowedValueProcessor?.IsAllowedEnabled;

        if (checkRequired == false)
            return QueueItemResult.FromResult(null, previousItemResult);

        var rawValue = previousItemResult.GetOutputParam<string>(Constants.Queue.Params.Line);

        var isAllowed = allowedValueProcessor.IsAllowedValue(rawValue);

        // if not allowed, also check if InRangeAllowedValueManager is set. If so, it might be in range
        if (!isAllowed && allowedValueProcessor.IsInRangeEnabled)
            isAllowed = true;

        if (!string.IsNullOrEmpty(allowedValueProcessor.ErrorMessage))
            printProcessor.PrintError(allowedValueProcessor.ErrorMessage);

        return isAllowed
            ? QueueItemResult.FromResult(isAllowed, previousItemResult)
            : QueueItemResult.Failed();
    }
}
