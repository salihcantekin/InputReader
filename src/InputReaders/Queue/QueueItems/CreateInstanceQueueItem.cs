using System;

namespace InputReader.InputReaders.Queue.QueueItems;

public class CreateInstanceQueueItem(Type inputValueType) : IQueueItem
{
    public int Order => 8;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
		try
		{
            var result = Activator.CreateInstance(inputValueType, previousItemResult.Result);
            previousItemResult.AddOutputParam(Constants.Queue.Params.InputValue, result);

            return QueueItemResult.FromResult(result, previousItemResult);
        }
		catch
		{
            return QueueItemResult.Failed();

        }
    }
}