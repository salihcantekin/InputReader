using System.Collections.Generic;
using System.Linq;

namespace InputReader.InputReaders.Queue;

public record QueueItemResult
{
    private QueueItemResult() { }

    public bool IsFailed { get; set; }

    public QueueItemResult PreviousItemResult { get; private set; }

    private Dictionary<string, object> outputParams { get; set; } = new();

    public IEnumerable<KeyValuePair<string, object>> OutputParams => outputParams.AsEnumerable();

    public object Result { get; internal set; }

    public void SetOutputParam(string key, object value)
    {
        outputParams ??= [];

        outputParams[key] = value;
    }

    public object GetOutputParam(string key)
    {
        return outputParams.TryGetValue(key, out var value) ? value : null;
    }

    public T GetOutputParam<T>(string key)
    {
        var val = GetOutputParam(key);
        return val is null ? default : (T)val;
    }


    public static QueueItemResult FromResult(object result, QueueItemResult previousItemResult)
    {
        return new QueueItemResult
        {
            Result = result,
            PreviousItemResult = previousItemResult,
            outputParams = previousItemResult?.outputParams
        };
    }

    public static QueueItemResult Failed() => new() { IsFailed = true };
}
