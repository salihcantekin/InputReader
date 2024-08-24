using InputReader.AllowedValues;
using InputReader.PrintProcessor;

namespace InputReader.InputReaders.Queue.QueueItems;

internal class AllowedValuesCheckQueueItem<TInputType>(IAllowedValueProcessor<TInputType> allowedValueProcessor, IPrintProcessor printProcessor)
    : IQueueItem, IHasFailReason
{
    public FailReason FailReason => FailReason.AllowedValues;

    public int Order => QueueItemsOrder.InRangeAllowedValueQueueItem;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var convertedValue = previousItemResult.GetOutputParam<TInputType>(Constants.Queue.Params.ConvertedValue);

        var isAllowed = allowedValueProcessor.IsAllowedValue(convertedValue) == true
                     || allowedValueProcessor.IsInRange(convertedValue) == true;

        if (!string.IsNullOrEmpty(allowedValueProcessor.ErrorMessage))
            printProcessor.PrintError(allowedValueProcessor.ErrorMessage);

        return isAllowed
            ? QueueItemResult.FromResult(isAllowed, previousItemResult)
            : QueueItemResult.Failed();
    }
}
