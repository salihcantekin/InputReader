using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class ConsoleReadLineQueueItem(IInputReaderBase inputReader) : IQueueItem
{
    public int Order => 2;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var line = inputReader.ReadLine();
        var queueItem = QueueItemResult.FromResult(line, previousItemResult);
        queueItem.AddOutputParam("line", line); // so it can be used in any

        return queueItem;
    }
}
