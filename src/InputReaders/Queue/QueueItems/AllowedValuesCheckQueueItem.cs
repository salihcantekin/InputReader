using InputReader.AllowedValues;
using InputReader.PrintProcessor;

namespace InputReader.InputReaders.Queue.QueueItems;

internal class AllowedValuesCheckQueueItem(IAllowedValueProcessor<string> allowedValueProcessor, IPrintProcessor printProcessor)
    : IQueueItem, IHasFailReason
{
    //public const int Order = PreValidatorQueueItem.Order + 1;

    public FailReason FailReason => FailReason.AllowedValues;

    public int Order => QueueItemsOrder.AllowedValuesCheckQueueItem;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var checkRequired = allowedValueProcessor?.IsEnabled;

        if (checkRequired == false)
            return QueueItemResult.FromResult(null, previousItemResult);

        var isAllowed = allowedValueProcessor.IsAllowedValue(previousItemResult.Result.ToString());

        if (!string.IsNullOrEmpty(allowedValueProcessor.ErrorMessage))
            printProcessor.PrintError(allowedValueProcessor.ErrorMessage);

        return isAllowed
            ? QueueItemResult.FromResult(isAllowed, previousItemResult)
            : QueueItemResult.Failed();
    }
}
