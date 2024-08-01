using InputReader.AllowedValues;
using InputReader.PrintProcessor;
using System.Text;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class ProcessPrintQueueItem<TInputType>(IPrintProcessor printProcessor,
                                          string message) : IQueueItem
{
    private IAllowedValueProcessor<string, TInputType> allowedValueProcessor;

    public int Order => QueueItemsOrder.ProcessPrintQueueItem;


    internal void SetAllowedValueProcessor(IAllowedValueProcessor<string, TInputType> allowedValueProcessor)
    {
        this.allowedValueProcessor = allowedValueProcessor;
    }

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        printProcessor.Print(message);

        if (allowedValueProcessor is not null)
            printProcessor.PrintAllowedValues(allowedValueProcessor?.AllowedValues, allowedValueProcessor?.InRangeValues, allowedValueProcessor?.IsCaseInSensitive);

        return previousItemResult;
    }
}
