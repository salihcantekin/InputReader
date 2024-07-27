using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class ConsoleReadLineQueueItem(IInputReaderBase inputReader) : IQueueItem
{
    private IInputReaderBase inputReader = inputReader;

    public int Order => 2;

    internal void SetInputReader(IInputReaderBase inputReader)
    {
        this.inputReader = inputReader;
    }

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var line = inputReader.ReadLine();
        var queueItem = QueueItemResult.FromResult(line, previousItemResult);
        queueItem.AddOutputParam(Constants.Queue.Params.Line, line); // so it can be used in any

        return queueItem;
    }
}
