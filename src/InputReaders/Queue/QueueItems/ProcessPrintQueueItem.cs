using InputReader.AllowedValues;
using InputReader.PrintProcessor;
using System.Text;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class ProcessPrintQueueItem<TInputType>(IPrintProcessor printProcessor,
                                          string message) : IQueueItem
{
    private IAllowedValueProcessor<string> allowedValueProcessor;
    private IInRangeAllowedValueProcessor<TInputType> inRangeAllowedValueManager;

    public int Order => QueueItemsOrder.ProcessPrintQueueItem;


    public void SetAllowedValueProcessor(IAllowedValueProcessor<string> allowedValueProcessor)
    {
        this.allowedValueProcessor = allowedValueProcessor;
    }

    internal void SetInRangeAllowedValueProcessor(IInRangeAllowedValueProcessor<TInputType> inRangeAllowedValueManager)
    {
        this.inRangeAllowedValueManager = inRangeAllowedValueManager;
    }

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        printProcessor.Print(message);

        if (allowedValueProcessor is not null || inRangeAllowedValueManager is not null)
            printProcessor.PrintAllowedValues(allowedValueProcessor?.Values, inRangeAllowedValueManager?.Values, allowedValueProcessor?.IsCaseInSensitive);

        return previousItemResult;
    }
}
