using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue;

namespace InputReader.InputReaders.Queue.QueueItems;

internal class ConsoleReadLineQueueItem(IInputReaderBase inputReader) : IQueueItem
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
