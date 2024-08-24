namespace InputReader.InputReaders.Queue;

public interface IQueueItem
{
    int Order { get; } // to order the queue items

    QueueItemResult Execute(QueueItemResult previousItemResult);
}

public interface IHasFailReason
{
    FailReason FailReason { get; }
}