using InputReader.AllowedValues;

namespace InputReader.InputReaders.Queue.QueueItems;

internal class InRangeAllowedValuesQeueItem<TInputType>() : IQueueItem, IHasFailReason
{
    private IInRangeAllowedValueProcessor<TInputType> manager;

    public int Order => QueueItemsOrder.InRangeAllowedValueQueueItem;
    public FailReason FailReason => FailReason.AllowedValues;

    internal void SetManager(IInRangeAllowedValueProcessor<TInputType> manager)
    {
        this.manager = manager;
    }

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var convertedValue = previousItemResult.GetOutputParam<TInputType>(Constants.Queue.Params.ConvertedValue);
        var inRange = manager.IsInRange(convertedValue);

        return inRange
            ? QueueItemResult.FromResult(inRange, previousItemResult)
            : QueueItemResult.Failed();
    }
}
