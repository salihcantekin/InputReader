using InputReader.AllowedValues;
using InputReader.PrintProcessor;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class ProcessPrintQueueItem(IPrintProcessor printProcessor,
                             IAllowedValueProcessor<string> allowedValueProcessor,
                             string message) : IQueueItem
{
    public int Order => 1;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var processor = allowedValueProcessor;
        printProcessor.Print(message);

        if (allowedValueProcessor != null && allowedValueProcessor.IsEnabled)
            printProcessor.PrintAllowedValues(processor.Values, processor.IsCaseInSensitive);

        return previousItemResult;
    }
}
