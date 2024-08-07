using InputReader.AllowedValues;
using InputReader.PrintProcessor;
using System.Linq;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class ProcessPrintQueueItem<TInputType>(IPrintProcessor printProcessor,
                                                      string message) : IQueueItem
{
    private IAllowedValueProcessor<TInputType> allowedValueProcessor;
    private IPrintProcessor printProcessor = printProcessor;

    internal void SetPrintProcessor(IPrintProcessor printProcessor)
    {
        this.printProcessor = printProcessor;
    }

    public int Order => QueueItemsOrder.ProcessPrintQueueItem;


    internal void SetAllowedValueProcessor(IAllowedValueProcessor<TInputType> allowedValueProcessor)
    {
        this.allowedValueProcessor = allowedValueProcessor;
    }

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        printProcessor.Print(message);

        if (allowedValueProcessor is not null)
            printProcessor.PrintAllowedValues(allowedValueProcessor?.AllowedValues.Select(i => i.ToString()),
                                              allowedValueProcessor?.InRangeValues,
                                              allowedValueProcessor?.IsCaseInSensitive);

        return previousItemResult;
    }
}
