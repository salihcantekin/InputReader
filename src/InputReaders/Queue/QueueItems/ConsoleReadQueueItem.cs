using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders.Queue.QueueItems;

public class ConsoleReadQueueItem(IInputReaderBase inputReader) : IQueueItem
{
    private IInputReaderBase inputReader = inputReader;

    public int Order => QueueItemsOrder.ConsoleReadLineQueueItem;

    internal void SetInputReader(IInputReaderBase inputReader)
    {
        this.inputReader = inputReader;
    }

    public virtual QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var line = inputReader.Read();
        var queueItem = QueueItemResult.FromResult(line, previousItemResult);
        queueItem.SetOutputParam(Constants.Queue.Params.Line, line); // so it can be used in any queue item

        return queueItem;
    }
}
