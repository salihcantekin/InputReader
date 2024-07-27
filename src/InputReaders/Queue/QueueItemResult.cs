using System.Collections.Generic;

namespace InputReader.InputReaders.Queue;

public record QueueItemResult
{
    private QueueItemResult() { }

    public bool IsFailed { get; set; }

    public QueueItemResult PreviousItemResult { get; private set; }

    private Dictionary<string, object> OutputParams { get; set; } = new();

    public object Result { get; internal set; }

    public void AddOutputParam(string key, object value)
    {
        OutputParams ??= [];

        OutputParams[key] = value;
    }

    public object GetOutputParam(string key)
    {
        return OutputParams.TryGetValue(key, out var value) ? value : null;
    }


    public static QueueItemResult FromResult(object result, QueueItemResult previousItemResult)
    {
        return new QueueItemResult
        {
            Result = result,
            PreviousItemResult = previousItemResult,
            OutputParams = previousItemResult?.OutputParams
        };
    }

    public static QueueItemResult Failed() => new() { IsFailed = true };
}
