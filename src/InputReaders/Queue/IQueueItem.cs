namespace InputReader.InputReaders.Queue;

internal interface IQueueItem
{
    int Order { get; } // to order the queue items

    QueueItemResult Execute(QueueItemResult previousItemResult);
}
