using InputReader.AllowedValues;
using InputReader.InputReaders.Queue;
using InputReader.PrintProcessor;

namespace InputReader.InputReaders.Queue.QueueItems;

internal class ProcessPrintQueueItem : IQueueItem
{
    private readonly IPrintProcessor printProcessor;
    private readonly IAllowedValueProcessor<string> allowedValueProcessor;
    private readonly string message;

    public ProcessPrintQueueItem(IPrintProcessor printProcessor,
                                 IAllowedValueProcessor<string> allowedValueProcessor,
                                 string message)
    {
        this.printProcessor = printProcessor;
        this.allowedValueProcessor = allowedValueProcessor;
        this.message = message;
    }

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
