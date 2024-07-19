using System;

namespace InputReader.InputReaders.Queue.QueueItems;

public class CreateInstanceQueueItem(Type inputValueType) : IQueueItem
{
    public int Order => 8;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var result = Activator.CreateInstance(inputValueType, previousItemResult.Result);
        previousItemResult.AddOutputParam("InputValue", result);

        return QueueItemResult.FromResult(result, previousItemResult);
    }
}