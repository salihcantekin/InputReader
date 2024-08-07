using System;

namespace InputReader.InputReaders.Queue.QueueItems;

public class CreateInstanceQueueItem(Type inputValueType) : IQueueItem
{
    public int Order => QueueItemsOrder.CreateInstanceQueueItem; // has to be the last item in the queue for the time being

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        try
        {
            var convertedValue = previousItemResult.GetOutputParam(Constants.Queue.Params.ConvertedValue);
            var result = Activator.CreateInstance(inputValueType, convertedValue);
            previousItemResult.SetOutputParam(Constants.Queue.Params.InputValue, result);

            return QueueItemResult.FromResult(result, previousItemResult);
        }
        catch
        {
            return QueueItemResult.Failed();
        }
    }
}