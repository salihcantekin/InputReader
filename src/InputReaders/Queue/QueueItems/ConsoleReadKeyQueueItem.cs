namespace InputReader.InputReaders.Queue.QueueItems;

//public sealed class ConsoleReadKeyQueueItem(IInputReaderBase inputReader) : ConsoleReadQueueItem(inputReader)
//{
//    public override QueueItemResult Execute(QueueItemResult previousItemResult)
//    {
//        var key = inputReader.ReadKey();
//        var queueItem = QueueItemResult.FromResult(key, previousItemResult);

//        var serialized = $"{key.KeyChar}-{key.Key}-{key.Modifiers}";
//        queueItem.SetOutputParam(Constants.Queue.Params.Line, serialized);

//        return queueItem;
//    }
//}
